using System;
using System.IO;
using Arrowgene.Logging;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

/// <summary>
/// Extracts the composite terrain texture from CryEngine cover.ctc files.
/// Parses the DXT5-compressed sector tiles and composes them into a BGRA image.
/// </summary>
public sealed class TerrainTextureLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(TerrainTextureLoader));

    private const int DXT5_FORMAT = 24;

    /// <summary>
    /// Reads terrain dimensions from the terrain.dat header.
    /// Returns (nHeightMapSize_InUnits, nUnitSize_InMeters).
    /// </summary>
    public static (int HeightmapSize, int UnitSize) ReadTerrainInfo(string terrainDatPath)
    {
        if (!File.Exists(terrainDatPath)) return (0, 1);
        byte[] hdr = new byte[32];
        using var fs = File.OpenRead(terrainDatPath);
        if (fs.Read(hdr, 0, 32) < 32) return (0, 1);
        int heightmapSize = BitConverter.ToInt32(hdr, 8);  // nHeightMapSize_InUnits
        int unitSize = BitConverter.ToInt32(hdr, 12);       // nUnitSize_InMeters
        if (unitSize < 1) unitSize = 1;
        return (heightmapSize, unitSize);
    }

    /// <summary>
    /// Loads cover.ctc and returns a composite BGRA terrain texture.
    /// The output image has Y=0 at the top (screen coords), matching flipped world Y.
    /// </summary>
    public byte[]? LoadTexture(string coverCtcPath, int terrainWorldSize, out int width, out int height)
    {
        width = 0;
        height = 0;

        if (!File.Exists(coverCtcPath))
            return null;

        try
        {
            byte[] data = File.ReadAllBytes(coverCtcPath);
            return ParseCoverCtc(data, terrainWorldSize, out width, out height);
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load cover.ctc: {ex.Message}");
            return null;
        }
    }

    private byte[]? ParseCoverCtc(byte[] data, int terrainWorldSize, out int outWidth, out int outHeight)
    {
        outWidth = 0;
        outHeight = 0;

        if (data.Length < 40) return null;

        // SCommonFileHeader (8 bytes)
        if (data[0] != (byte)'C' || data[1] != (byte)'R' || data[2] != (byte)'Y')
        {
            Logger.Error("Invalid cover.ctc signature");
            return null;
        }

        // STerrainTextureFileHeader (8 bytes at offset 8)
        int layerCount = BitConverter.ToUInt16(data, 8);
        float brMult = BitConverter.ToSingle(data, 12);

        if (layerCount < 1 || layerCount > 4) return null;

        // Layer headers (12 bytes each, starting at offset 16)
        int offset = 16;
        int sectorPixels = BitConverter.ToUInt16(data, offset);
        int texFormat = BitConverter.ToInt32(data, offset + 4);
        int sectorBytes = BitConverter.ToInt32(data, offset + 8);

        if (texFormat != DXT5_FORMAT)
        {
            Logger.Error($"Unsupported texture format: {texFormat} (expected DXT5={DXT5_FORMAT})");
            return null;
        }

        // Skip all layer headers
        offset = 16 + layerCount * 12;

        // Index table
        int indexCount = BitConverter.ToUInt16(data, offset);
        offset += 2;

        short[] indices = new short[indexCount];
        for (int i = 0; i < indexCount; i++)
        {
            indices[i] = BitConverter.ToInt16(data, offset + i * 2);
        }

        int indexTableBytes = 2 + indexCount * 2;
        int dataStart = 16 + layerCount * 12 + indexTableBytes;
        int totalSectorBytes = sectorBytes * layerCount;

        // Composite image in CryEngine native coords (Y=0 at bottom)
        int imgSize = terrainWorldSize;
        if (imgSize <= 0) imgSize = 2048;
        outWidth = imgSize;
        outHeight = imgSize;
        byte[] composite = new byte[imgSize * imgSize * 4];

        int indexPos = 0;
        RenderNode(data, indices, ref indexPos, dataStart, totalSectorBytes,
            sectorPixels, sectorBytes, brMult,
            composite, imgSize, 0, 0, terrainWorldSize);

        // Flip vertically so Y=0 is at top (screen coords)
        FlipVertically(composite, imgSize, imgSize);

        return composite;
    }

    private void RenderNode(byte[] data, short[] indices, ref int indexPos,
        int dataStart, int totalSectorBytes,
        int sectorPixels, int sectorBytes, float brMult,
        byte[] composite, int imgSize,
        int worldX, int worldY, int nodeSize)
    {
        if (indexPos >= indices.Length) return;

        short texIndex = indices[indexPos];
        indexPos++;

        int childSize = nodeSize / 2;

        // Count how many children have textures by peeking ahead, properly
        // skipping each child's subtree to find the next sibling's root index.
        int childrenWithTex = 0;
        if (childSize >= 1)
        {
            int peekPos = indexPos;
            for (int i = 0; i < 4 && peekPos < indices.Length; i++)
            {
                if (indices[peekPos] >= 0)
                    childrenWithTex++;
                peekPos += SubtreeSize(indices, peekPos, childSize);
            }
        }

        // Only render this node's tile if NOT all 4 children will fully overwrite it.
        // This eliminates LOD-boundary seams where parent/child brightness differs.
        if (texIndex >= 0 && childrenWithTex < 4)
        {
            DecodeTileAndBlit(data, texIndex, dataStart, totalSectorBytes,
                sectorPixels, sectorBytes, brMult,
                composite, imgSize, worldX, worldY, nodeSize);
        }

        // Process 4 children in CTC file order (X-outer, Y-inner / column-major):
        // 0=(x,y) 1=(x,y+s) 2=(x+s,y) 3=(x+s,y+s)
        if (childSize < 1) return;

        int[][] childOff = [[0, 0], [0, childSize], [childSize, 0], [childSize, childSize]];

        for (int i = 0; i < 4; i++)
        {
            if (indexPos >= indices.Length) return;

            if (indices[indexPos] >= 0)
            {
                RenderNode(data, indices, ref indexPos, dataStart, totalSectorBytes,
                    sectorPixels, sectorBytes, brMult, composite, imgSize,
                    worldX + childOff[i][0], worldY + childOff[i][1], childSize);
            }
            else
            {
                indexPos++; // consume the -1
            }
        }
    }

    /// <summary>
    /// Returns how many index entries the subtree rooted at indices[pos] consumes.
    /// A -1 leaf is 1 entry. A node with texture at a non-leaf depth is
    /// 1 (itself) plus the sum of its 4 children's subtree sizes.
    /// </summary>
    private static int SubtreeSize(short[] indices, int pos, int nodeSize)
    {
        if (pos >= indices.Length || indices[pos] < 0)
            return 1;

        int childSize = nodeSize / 2;
        if (childSize < 1)
            return 1;

        int size = 1;
        for (int i = 0; i < 4; i++)
            size += SubtreeSize(indices, pos + size, childSize);

        return size;
    }

    private void DecodeTileAndBlit(byte[] data, short texIndex,
        int dataStart, int totalSectorBytes,
        int sectorPixels, int sectorBytes, float brMult,
        byte[] composite, int imgSize,
        int worldX, int worldY, int nodeSize)
    {
        long tileOffset = dataStart + (long)texIndex * totalSectorBytes;
        if (tileOffset + sectorBytes > data.Length) return;

        byte[] dxtData = new byte[sectorBytes];
        Array.Copy(data, tileOffset, dxtData, 0, sectorBytes);

        byte[] tilePixels = DxtDecoder.DecodeDxt5(dxtData, sectorPixels, sectorPixels);

        if (brMult > 0 && brMult < 1.0f)
        {
            float mult = 1.0f / brMult;
            for (int i = 0; i < tilePixels.Length; i += 4)
            {
                tilePixels[i + 0] = ToneMap(tilePixels[i + 0], mult);
                tilePixels[i + 1] = ToneMap(tilePixels[i + 1], mult);
                tilePixels[i + 2] = ToneMap(tilePixels[i + 2], mult);
            }
        }

        BlitTile(tilePixels, sectorPixels, sectorPixels,
            composite, imgSize, worldX, worldY, nodeSize);
    }

    private static void BlitTile(byte[] tile, int tileW, int tileH,
        byte[] target, int targetSize,
        int worldX, int worldY, int worldSize)
    {
        // No Y-flip here - compose in native CryEngine coords.
        // The final image is flipped after full composition.
        for (int ly = 0; ly < worldSize; ly++)
        {
            int dy = worldY + ly;
            if (dy < 0 || dy >= targetSize) continue;

            int sy = Math.Clamp(ly * tileH / worldSize, 0, tileH - 1);

            for (int lx = 0; lx < worldSize; lx++)
            {
                int dx = worldX + lx;
                if (dx < 0 || dx >= targetSize) continue;

                int sx = Math.Clamp(lx * tileW / worldSize, 0, tileW - 1);

                int srcIdx = (sy * tileW + sx) * 4;
                int dstIdx = (dy * targetSize + dx) * 4;

                target[dstIdx + 0] = tile[srcIdx + 0];
                target[dstIdx + 1] = tile[srcIdx + 1];
                target[dstIdx + 2] = tile[srcIdx + 2];
                target[dstIdx + 3] = 255;
            }
        }
    }

    private static byte ToneMap(byte value, float mult)
    {
        // Reinhard-style tone mapping: boosted = v*mult / (1 + v*mult/255)
        float v = value * mult;
        float mapped = v / (1.0f + v / 255.0f);
        return (byte)Math.Clamp(mapped, 0, 255);
    }

    private static void FlipVertically(byte[] pixels, int width, int height)
    {
        int stride = width * 4;
        byte[] rowBuf = new byte[stride];
        for (int y = 0; y < height / 2; y++)
        {
            int topOff = y * stride;
            int botOff = (height - 1 - y) * stride;
            Array.Copy(pixels, topOff, rowBuf, 0, stride);
            Array.Copy(pixels, botOff, pixels, topOff, stride);
            Array.Copy(rowBuf, 0, pixels, botOff, stride);
        }
    }
}
