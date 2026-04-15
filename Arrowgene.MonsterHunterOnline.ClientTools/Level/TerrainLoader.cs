using System;
using System.Collections.Generic;
using System.IO;
using Arrowgene.Logging;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

/// <summary>
/// Extracts a grayscale heightmap from CryEngine terrain.dat files.
/// </summary>
public sealed class TerrainLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(TerrainLoader));

    private const int SectorHmSize = 33; // 33x33 per sector (32+1 overlap)

    /// <summary>
    /// Loads terrain.dat and produces a grayscale heightmap image as a byte array.
    /// Returns null if the file cannot be parsed.
    /// </summary>
    public byte[]? LoadHeightmap(string terrainDatPath, out int width, out int height)
    {
        width = 0;
        height = 0;

        if (!File.Exists(terrainDatPath))
            return null;

        try
        {
            byte[] data = File.ReadAllBytes(terrainDatPath);
            return ExtractHeightmap(data, out width, out height);
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load terrain: {ex.Message}");
            return null;
        }
    }

    private byte[]? ExtractHeightmap(byte[] data, out int width, out int height)
    {
        width = 0;
        height = 0;

        if (data.Length < 32) return null;

        int terrainSize = BitConverter.ToInt32(data, 8);
        int sectorSize = BitConverter.ToInt32(data, 16);
        float heightRatio = BitConverter.ToSingle(data, 24);

        if (terrainSize <= 0 || terrainSize > 16384 || sectorSize <= 0)
            return null;

        // Find all heightmap blocks (33x33 uint16 blocks with values in terrain range)
        List<long> blocks = FindHeightmapBlocks(data);
        if (blocks.Count == 0)
            return null;

        // Match each block to its sector position by finding the AABB in the preceding bytes
        ushort[] heightmap = new ushort[terrainSize * terrainSize];
        bool[] written = new bool[terrainSize * terrainSize];

        foreach (long blk in blocks)
        {
            if (!FindSectorPosition(data, blk, sectorSize, terrainSize, out int baseX, out int baseY))
                continue;

            for (int py = 0; py < SectorHmSize; py++)
            {
                for (int px = 0; px < SectorHmSize; px++)
                {
                    int imgX = baseX + px;
                    int imgY = baseY + py;
                    if (imgX >= terrainSize || imgY >= terrainSize) continue;

                    ushort rawH = BitConverter.ToUInt16(data, (int)blk + (py * SectorHmSize + px) * 2);
                    int idx = imgY * terrainSize + imgX;
                    heightmap[idx] = rawH;
                    written[idx] = true;
                }
            }
        }

        // Find height range
        ushort hMin = ushort.MaxValue, hMax = 0;
        for (int i = 0; i < heightmap.Length; i++)
        {
            if (!written[i]) continue;
            if (heightmap[i] < hMin) hMin = heightmap[i];
            if (heightmap[i] > hMax) hMax = heightmap[i];
        }

        if (hMin >= hMax) return null;

        // Convert to grayscale byte array (BGRA format for Avalonia bitmap)
        width = terrainSize;
        height = terrainSize;
        byte[] pixels = new byte[terrainSize * terrainSize * 4];

        for (int i = 0; i < terrainSize * terrainSize; i++)
        {
            int pi = i * 4;
            if (!written[i])
            {
                // Transparent for unmapped areas
                pixels[pi] = 0;     // B
                pixels[pi + 1] = 0; // G
                pixels[pi + 2] = 0; // R
                pixels[pi + 3] = 0; // A
            }
            else
            {
                byte gray = (byte)Math.Clamp((heightmap[i] - hMin) * 200 / Math.Max(1, hMax - hMin) + 40, 40, 240);
                // Green-tinted terrain
                pixels[pi] = (byte)(gray * 0.4f);     // B
                pixels[pi + 1] = (byte)(gray * 0.8f); // G
                pixels[pi + 2] = (byte)(gray * 0.5f); // R
                pixels[pi + 3] = 255;                  // A
            }
        }

        return pixels;
    }

    private static List<long> FindHeightmapBlocks(byte[] data)
    {
        List<long> blocks = [];
        int blockBytes = SectorHmSize * SectorHmSize * 2;

        for (long off = 200; off < data.Length - blockBytes; off += 2)
        {
            // Quick check first 6 values
            bool quick = true;
            for (int i = 0; i < 6 && quick; i++)
            {
                ushort v = BitConverter.ToUInt16(data, (int)off + i * 2);
                if (v < 40000 || v > 64000) quick = false;
            }
            if (!quick) continue;

            // Full validation
            bool valid = true;
            for (int i = 6; i < SectorHmSize * SectorHmSize && valid; i++)
            {
                ushort v = BitConverter.ToUInt16(data, (int)off + i * 2);
                if (v < 40000 || v > 64000) valid = false;
            }
            if (!valid) continue;

            blocks.Add(off);
            off += blockBytes - 2;
        }

        return blocks;
    }

    private static bool FindSectorPosition(byte[] data, long blockOffset, int sectorSize, int terrainSize,
        out int baseX, out int baseY)
    {
        baseX = 0;
        baseY = 0;

        // Scan backwards from the heightmap block to find the AABB
        // AABB has: minX, minY (both multiples of sectorSize), maxX=minX+sectorSize, maxY=minY+sectorSize
        for (long probe = blockOffset - 40; probe > blockOffset - 80 && probe >= 4; probe -= 2)
        {
            float f0 = BitConverter.ToSingle(data, (int)probe);
            float f1 = BitConverter.ToSingle(data, (int)probe + 4);

            if (f0 < 0 || f0 > terrainSize || f1 < 0 || f1 > terrainSize) continue;
            if (f0 % sectorSize != 0 || f1 % sectorSize != 0) continue;

            float f3 = BitConverter.ToSingle(data, (int)probe + 12);
            float f4 = BitConverter.ToSingle(data, (int)probe + 16);

            if (Math.Abs(f3 - f0 - sectorSize) > 0.01 || Math.Abs(f4 - f1 - sectorSize) > 0.01) continue;

            baseX = (int)f0;
            baseY = (int)f1;
            return true;
        }

        return false;
    }
}
