#nullable enable
using System;
using System.IO;
using System.Security.Cryptography;

namespace Arrowgene.MonsterHunterOnline.ClientTools.IIPS;

public static class IIPSArchiveCrypto
{
    private static readonly uint[] IfsSectionTable =
    [
        0x193AA698, 0x5496F7D5, 0x4208931B, 0x7A4106EC, 0x83E86840, 0xF49B6F8C, 0xBA3D9A51, 0x55F54DDD,
        0x2DE51372, 0x9AFB571B, 0x3AB35406, 0xAD64FF1F, 0xC77764FE, 0x7F864466, 0x416D9CD4, 0xA2489278,
        0xE30B86E4, 0x0B5231B6, 0xBA67AED6, 0xE5AB2467, 0x60028B90, 0x1D9E20C6, 0x2A7C692A, 0x6B691CDB,
        0x9E51F817, 0x9B763DEC, 0x3D29323F, 0xCFE12B68, 0x754B459B, 0xA2238047, 0xD9C55514, 0x6BDCFFC1,
        0x693E6340, 0x82383FE7, 0x1916EA5F, 0xEC7BCD59, 0x72DE165A, 0xE79A1617, 0x8EC86234, 0xA8F0D284,
        0x20C90226, 0x7BF98884, 0x28A58331, 0x3EC3FA6E, 0x4CE0895B, 0xC353B4D0, 0x33EF064F, 0x21E5E210,
        0xC8BB589D, 0xE85DCAB2, 0xAC65829F, 0xA7BF92D0, 0x05A6174D, 0x25A50C2E, 0xE5C78777, 0x3D75021F,
        0x4BAA9C98, 0x23BDC884, 0x9653BBD7, 0xBADCE7F5, 0xC283A484, 0xC040DF2E, 0x9370A841, 0x2F316022,
        0x36EED231, 0xAC2CBC0C, 0x13C0A49B, 0xCDD12997, 0x07FE91B2, 0xCD7EABCD, 0x2C01271D, 0x18432DF8,
        0x599C6BC7, 0x75E93D5A, 0xB67A6EE2, 0x8E738E16, 0xFF9073FD, 0xAF77026A, 0xF86EA2FC, 0x91509EA3,
        0x33A78DC6, 0x4F79234A, 0x3A7535BC, 0x3539FCB1, 0x3103EE52, 0x4F6F1E69, 0x6BB3EBBC, 0x4CB77555,
        0x8DD1E999, 0x2ADE439D, 0x11521FAE, 0xB94D2545, 0x8DDE9ABD, 0x1909393F, 0xB792A23D, 0x749C455B,
        0xB5B60F2C, 0x380459CE, 0x0DAD5820, 0xB130845B, 0x291CBD52, 0xDE9A5BB7, 0x51DEF961, 0x515B6408,
        0xCA6E823E, 0x382E6E74, 0xEEBE3D71, 0x4C8F0C6A, 0xE676DCEA, 0x14E1DC7C, 0x6F7FC634, 0xCF85A943,
        0xD39EA96E, 0x136E7C93, 0x7164B304, 0xF32F1333, 0x35C34034, 0xDE39D721, 0x91A87439, 0xC410111F,
        0x29F17AAC, 0x1316A6FF, 0x12F194EE, 0x420B9499, 0xF72DB0DC, 0x690B9F93, 0x17D14BB2, 0x8F931AB8,
        0x217500BC, 0x875413F8, 0x98B2E43D, 0xC51F9571, 0x54CEBDCA, 0x0719CC79, 0xF3C7080D, 0xE4286771,
        0xA3EAB3CD, 0x4A6B00E0, 0x11CF0759, 0x7E897379, 0x5B32876C, 0x5E8CD4F6, 0x0CEDFA64, 0x919AC2C7,
        0xB214F3B3, 0x0E89C38C, 0xF0C43A39, 0xEAE10522, 0x835BCE06, 0x9EEC43C2, 0xEA26A9D6, 0x69531821,
        0x6725B24A, 0xDA81B0E2, 0xD5B4AE33, 0x080F99FB, 0x15A83DAF, 0x29DFC720, 0x91E1900F, 0x28163D58,
        0x83D107A2, 0x4EAC149A, 0x9F71DA18, 0x61D5C4FA, 0xE3AB2A5F, 0xC7B0D63F, 0xB3CC752A, 0x61EBCFB6,
        0x26FFB52A, 0xED789E3F, 0xAA3BC958, 0x455A8788, 0xC9C082A9, 0x0A1BEF0E, 0xC29A5A7E, 0x150D4735,
        0x943809E0, 0x69215510, 0xEF0B0DA9, 0x3B4E9FB3, 0xD8B5D04C, 0xC7A023A8, 0xB0D50288, 0x64821375,
        0xC260E8CF, 0x8496BD2C, 0xFF4F5435, 0x0FB5560C, 0x7CD74A52, 0x93589C80, 0x88975C47, 0x83BDA89D,
        0x8BCC4296, 0x01B82C21, 0xFD821DBF, 0x26520B47, 0x04983E19, 0xD3E1CA27, 0x782C580F, 0x326FF573,
        0xC157BCC7, 0x4F5E6B84, 0x44EBFBFB, 0xDA26D9D8, 0x6CD9D08E, 0x1719F1D8, 0x715C0487, 0x2C2D3C92,
        0x53FAABA9, 0xBC836146, 0x510C92D6, 0xE089F82A, 0x4680171F, 0x369F00DE, 0x70EC2331, 0x0E253D55,
        0xDAFB9717, 0xE5DD922D, 0x95915D21, 0xA0202F96, 0xA161CC47, 0xEACFA6F1, 0xED5E9189, 0xDAB87684,
        0xA4B76D4A, 0xFA704897, 0x631F10BA, 0xD39DA8F9, 0x5DB4C0E4, 0x16FDE42A, 0x2DFF7580, 0xB56FEC7E,
        0xC3FFB370, 0x8E6F36BC, 0x6097D459, 0x514D5D36, 0xA5A737E2, 0x3977B9B3, 0xFD31A0CA, 0x903368DB,
        0xE8370D61, 0x98109520, 0xADE23CAC, 0x99F82E04, 0x41DE7EA3, 0x84A1C295, 0x09191BE0, 0x30930D02,
        0x1C9FA44A, 0xC406B6D7, 0xEEDCA152, 0x6149809C, 0xB0099EF4, 0xC5F653A5, 0x4C10790D, 0x7303286C,
    ];


    private static readonly uint[] StormBuffer = GenerateStormBuffer();

    public static void IfsSectionCrypt(byte[] data)
    {
        IfsSectionDecrypt(data);
    }

    public static void IfsSectionDecrypt(byte[] data)
    {
        uint edx = 0x863;
        uint eax = 0xEEEEEEEE;
        int count = data.Length & ~3;
        for (int i = 0; i < count; i += 4)
        {
            uint input =
                (uint)data[i] |
                (uint)data[i + 1] << 8 |
                (uint)data[i + 2] << 16 |
                (uint)data[i + 3] << 24;
            eax += IfsSectionTable[(byte)edx];
            uint plain = (eax + edx) ^ input;
            edx = (edx >> 0x0B) | ((~edx << 0x15) + 0x11111111);
            data[i] = (byte)plain;
            data[i + 1] = (byte)(plain >> 8);
            data[i + 2] = (byte)(plain >> 16);
            data[i + 3] = (byte)(plain >> 24);
            eax = eax + (eax << 0x5) + plain + 0x03;
        }
    }

    public static void IfsSectionEncrypt(byte[] data)
    {
        uint edx = 0x863;
        uint eax = 0xEEEEEEEE;
        int count = data.Length & ~3;
        for (int i = 0; i < count; i += 4)
        {
            uint plain =
                (uint)data[i] |
                (uint)data[i + 1] << 8 |
                (uint)data[i + 2] << 16 |
                (uint)data[i + 3] << 24;
            eax += IfsSectionTable[(byte)edx];
            uint cipher = (eax + edx) ^ plain;
            edx = (edx >> 0x0B) | ((~edx << 0x15) + 0x11111111);
            data[i] = (byte)cipher;
            data[i + 1] = (byte)(cipher >> 8);
            data[i + 2] = (byte)(cipher >> 16);
            data[i + 3] = (byte)(cipher >> 24);
            eax = eax + (eax << 0x5) + plain + 0x03;
        }
    }


    public static uint MpqHashString(string s, uint hashType)
    {
        uint seed1 = 0x7FED7FED;
        uint seed2 = 0xEEEEEEEE;
        foreach (char c in s)
        {
            uint ch = (uint)char.ToUpperInvariant(c);
            seed1 = StormBuffer[hashType + ch] ^ (seed1 + seed2);
            seed2 = ch + seed1 + seed2 + (seed2 << 5) + 3;
        }

        return seed1;
    }

    public static uint ComputeFileKey(string fileName)
    {
        int lastSep = fileName.LastIndexOfAny(['\\', '/']);
        string plainName = lastSep >= 0 ? fileName[(lastSep + 1)..] : fileName;
        return MpqHashString(plainName, 0x300);
    }

    public static ulong ComputeNameHash(string fileName)
    {
        string normalized = NormalizeArchivePath(fileName);
        byte[] nameBytes = global::System.Text.Encoding.ASCII.GetBytes(normalized);
        JenkinsHashlittle2(nameBytes, 2, 1, out uint pc, out uint pb);
        return ((ulong)pb << 32) | pc;
    }

    public static string NormalizeArchivePath(string fileName)
    {
        return fileName.Replace('/', '\\').ToLowerInvariant();
    }

    public static void MpqDecryptBlock(byte[] data, uint key)
    {
        DecryptBlockWithTable(data, key, StormBuffer, 0x400);
    }

    public static void MpqEncryptBlock(byte[] data, uint key)
    {
        EncryptBlockWithTable(data, key, StormBuffer, 0x400);
    }

    public static void IfsDecryptBlock(byte[] data, uint key)
    {
        DecryptBlockWithTable(data, key, IfsSectionTable, 0);
    }

    public static void IfsEncryptBlock(byte[] data, uint key)
    {
        EncryptBlockWithTable(data, key, IfsSectionTable, 0);
    }

    public static string Md5(byte[] inputData)
    {
        byte[] hash = MD5.HashData(inputData);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    public static void JenkinsHashlittle2(byte[] key, uint pcInit, uint pbInit, out uint pc, out uint pb)
    {
        uint a = 0xDEADBEEF + (uint)key.Length + pcInit;
        uint b = a;
        uint c = a + pbInit;

        int offset = 0;
        int length = key.Length;

        while (length > 12)
        {
            a += BitConverter.ToUInt32(key, offset);
            b += BitConverter.ToUInt32(key, offset + 4);
            c += BitConverter.ToUInt32(key, offset + 8);

            a -= c; a ^= (c << 4) | (c >> 28); c += b;
            b -= a; b ^= (a << 6) | (a >> 26); a += c;
            c -= b; c ^= (b << 8) | (b >> 24); b += a;
            a -= c; a ^= (c << 16) | (c >> 16); c += b;
            b -= a; b ^= (a << 19) | (a >> 13); a += c;
            c -= b; c ^= (b << 4) | (b >> 28); b += a;

            offset += 12;
            length -= 12;
        }

        if (length > 0)
        {
            byte[] tail = new byte[12];
            Array.Copy(key, offset, tail, 0, length);

            a += BitConverter.ToUInt32(tail, 0);
            b += BitConverter.ToUInt32(tail, 4);
            c += BitConverter.ToUInt32(tail, 8);

            c ^= b; c -= (b << 14) | (b >> 18);
            a ^= c; a -= (c << 11) | (c >> 21);
            b ^= a; b -= (a << 25) | (a >> 7);
            c ^= b; c -= (b << 16) | (b >> 16);
            a ^= c; a -= (c << 4) | (c >> 28);
            b ^= a; b -= (a << 14) | (a >> 18);
            c ^= b; c -= (b << 24) | (b >> 8);
        }

        pc = c;
        pb = b;
    }

    private static void DecryptBlockWithTable(byte[] data, uint key, uint[] table, int tableOffset)
    {
        uint key2 = 0xEEEEEEEE;
        int count = data.Length & ~3;
        for (int i = 0; i < count; i += 4)
        {
            key2 += table[tableOffset + (key & 0xFF)];
            uint input =
                (uint)data[i] |
                (uint)data[i + 1] << 8 |
                (uint)data[i + 2] << 16 |
                (uint)data[i + 3] << 24;
            uint plain = input ^ (key + key2);
            key = ((~key << 0x15) + 0x11111111) | (key >> 0x0B);
            key2 = plain + key2 + (key2 << 5) + 3;
            data[i] = (byte)plain;
            data[i + 1] = (byte)(plain >> 8);
            data[i + 2] = (byte)(plain >> 16);
            data[i + 3] = (byte)(plain >> 24);
        }
    }

    private static void EncryptBlockWithTable(byte[] data, uint key, uint[] table, int tableOffset)
    {
        uint key2 = 0xEEEEEEEE;
        int count = data.Length & ~3;
        for (int i = 0; i < count; i += 4)
        {
            key2 += table[tableOffset + (key & 0xFF)];
            uint plain =
                (uint)data[i] |
                (uint)data[i + 1] << 8 |
                (uint)data[i + 2] << 16 |
                (uint)data[i + 3] << 24;
            uint cipher = plain ^ (key + key2);
            key = ((~key << 0x15) + 0x11111111) | (key >> 0x0B);
            key2 = plain + key2 + (key2 << 5) + 3;
            data[i] = (byte)cipher;
            data[i + 1] = (byte)(cipher >> 8);
            data[i + 2] = (byte)(cipher >> 16);
            data[i + 3] = (byte)(cipher >> 24);
        }
    }

    private static uint[] GenerateStormBuffer()
    {
        uint[] buffer = new uint[0x500];
        uint seed = 0x00100001;
        for (uint index1 = 0; index1 < 0x100; index1++)
        {
            uint index2 = index1;
            for (int i = 0; i < 5; i++, index2 += 0x100)
            {
                seed = (seed * 125 + 3) % 0x2AAAAB;
                uint temp1 = (seed & 0xFFFF) << 16;
                seed = (seed * 125 + 3) % 0x2AAAAB;
                uint temp2 = seed & 0xFFFF;
                buffer[index2] = temp1 | temp2;
            }
        }

        return buffer;
    }
}
