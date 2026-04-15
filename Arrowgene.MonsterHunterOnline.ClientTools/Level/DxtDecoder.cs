using System;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

/// <summary>
/// Decodes DXT1 and DXT5 compressed texture data to BGRA8888 pixels.
/// </summary>
public static class DxtDecoder
{
    public static byte[] DecodeDxt5(byte[] dxtData, int width, int height)
    {
        byte[] pixels = new byte[width * height * 4];
        int blocksX = width / 4;
        int blocksY = height / 4;
        int blockIdx = 0;

        for (int by = 0; by < blocksY; by++)
        {
            for (int bx = 0; bx < blocksX; bx++)
            {
                int offset = blockIdx * 16;
                if (offset + 16 > dxtData.Length) break;

                // Alpha block (8 bytes)
                byte alpha0 = dxtData[offset];
                byte alpha1 = dxtData[offset + 1];
                ulong alphaBits = 0;
                for (int i = 2; i < 8; i++)
                    alphaBits |= (ulong)dxtData[offset + i] << ((i - 2) * 8);

                byte[] alphaTable = new byte[8];
                alphaTable[0] = alpha0;
                alphaTable[1] = alpha1;
                if (alpha0 > alpha1)
                {
                    alphaTable[2] = (byte)((6 * alpha0 + 1 * alpha1) / 7);
                    alphaTable[3] = (byte)((5 * alpha0 + 2 * alpha1) / 7);
                    alphaTable[4] = (byte)((4 * alpha0 + 3 * alpha1) / 7);
                    alphaTable[5] = (byte)((3 * alpha0 + 4 * alpha1) / 7);
                    alphaTable[6] = (byte)((2 * alpha0 + 5 * alpha1) / 7);
                    alphaTable[7] = (byte)((1 * alpha0 + 6 * alpha1) / 7);
                }
                else
                {
                    alphaTable[2] = (byte)((4 * alpha0 + 1 * alpha1) / 5);
                    alphaTable[3] = (byte)((3 * alpha0 + 2 * alpha1) / 5);
                    alphaTable[4] = (byte)((2 * alpha0 + 3 * alpha1) / 5);
                    alphaTable[5] = (byte)((1 * alpha0 + 4 * alpha1) / 5);
                    alphaTable[6] = 0;
                    alphaTable[7] = 255;
                }

                // Color block (8 bytes at offset+8)
                ushort c0 = BitConverter.ToUInt16(dxtData, offset + 8);
                ushort c1 = BitConverter.ToUInt16(dxtData, offset + 10);
                uint colorBits = BitConverter.ToUInt32(dxtData, offset + 12);

                Rgb565ToRgb(c0, out byte r0, out byte g0, out byte b0);
                Rgb565ToRgb(c1, out byte r1, out byte g1, out byte b1);

                byte[][] colorTable = new byte[4][];
                colorTable[0] = [b0, g0, r0];
                colorTable[1] = [b1, g1, r1];
                colorTable[2] = [(byte)((2 * b0 + b1) / 3), (byte)((2 * g0 + g1) / 3), (byte)((2 * r0 + r1) / 3)];
                colorTable[3] = [(byte)((b0 + 2 * b1) / 3), (byte)((g0 + 2 * g1) / 3), (byte)((r0 + 2 * r1) / 3)];

                for (int py = 0; py < 4; py++)
                {
                    for (int px = 0; px < 4; px++)
                    {
                        int imgX = bx * 4 + px;
                        int imgY = by * 4 + py;
                        int pixelIdx = py * 4 + px;

                        int alphaIdx = (int)((alphaBits >> (pixelIdx * 3)) & 7);
                        int colorIdx = (int)((colorBits >> (pixelIdx * 2)) & 3);

                        int pi = (imgY * width + imgX) * 4;
                        pixels[pi + 0] = colorTable[colorIdx][0]; // B
                        pixels[pi + 1] = colorTable[colorIdx][1]; // G
                        pixels[pi + 2] = colorTable[colorIdx][2]; // R
                        pixels[pi + 3] = alphaTable[alphaIdx];    // A
                    }
                }

                blockIdx++;
            }
        }

        return pixels;
    }

    private static void Rgb565ToRgb(ushort c, out byte r, out byte g, out byte b)
    {
        r = (byte)(((c >> 11) & 0x1F) * 255 / 31);
        g = (byte)(((c >> 5) & 0x3F) * 255 / 63);
        b = (byte)((c & 0x1F) * 255 / 31);
    }
}
