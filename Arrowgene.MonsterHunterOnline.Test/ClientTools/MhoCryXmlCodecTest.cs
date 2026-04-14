#nullable enable
using System.IO;
using System.Text;
using System.Xml.Linq;
using Arrowgene.MonsterHunterOnline.ClientTools;
using Xunit;

namespace Arrowgene.MonsterHunterOnline.Test.ClientTools;

public class MhoCryXmlCodecTest
{
    private static readonly byte[] EncryptedHeader = [0xFF, 0xFF, 0x6D, 0x68];

    private static readonly byte[] EncryptTable0 =
    [
        0x09, 0x40, 0x48, 0x19, 0xC1, 0x8F, 0x83, 0xF5, 0x60, 0x09, 0x6F, 0x14, 0x0F, 0xBE, 0x51, 0xEA,
        0x7A, 0x81, 0x08, 0xB4, 0x76, 0xB6, 0x1A, 0x91, 0x5A, 0x74, 0x70, 0xC9, 0xDD, 0x83, 0xE5, 0x04,
        0x9F, 0x48, 0xC8, 0x48, 0xA0, 0x9E, 0x9B, 0x8F, 0x8B, 0x0F, 0x9C, 0x01, 0x94, 0x34, 0x62, 0x29,
        0x99, 0xB7, 0xDC, 0x77, 0xFC, 0x87, 0xB2, 0x39, 0xFB, 0x8F, 0x6D, 0xD6, 0x51, 0x97, 0x6C, 0xD8,
        0x91,
    ];

    private static readonly byte[] EncryptTable1 =
    [
        0xC7, 0xAB, 0x19, 0x5A, 0x77, 0x88, 0xFA, 0x21, 0xAB, 0x5D, 0x7D, 0x33, 0xAA, 0x3A, 0x75, 0x0A,
        0xF9, 0x7C, 0x76, 0xB6, 0x6A, 0xE3, 0x05, 0xD5, 0x77, 0xCF, 0xF2, 0xFB, 0x2D, 0xB2, 0x1B, 0x29,
        0x17, 0x50, 0x04, 0xDA, 0x4A, 0xC7, 0x8C, 0x31, 0x4A, 0x51, 0xA8, 0x3B, 0x9E, 0xE5, 0xDE, 0x4B,
        0x75, 0x7C, 0x47, 0x54, 0xFB, 0x03, 0x24, 0xA6, 0x13, 0x4A, 0xCB, 0xE9, 0x5E, 0x34, 0xE1, 0xA1,
        0x80,
    ];

    [Fact]
    public void CanLoadEncryptedCryXmlDocument()
    {
        byte[] cryXmlBinary = BuildCryXmlBinary();
        byte[] encrypted = EncryptPayload(cryXmlBinary);

        Assert.True(MhoCryXmlCodec.IsEncrypted(encrypted));
        Assert.True(MhoCryXmlCodec.IsCryXmlBinary(cryXmlBinary));
        Assert.True(MhoCryXmlCodec.IsCryXmlCodec(encrypted));
        Assert.True(MhoCryXmlCodec.IsCryXmlCodex(encrypted));
        Assert.True(MhoCryXmlCodec.NeedsDecryption(encrypted));
        Assert.Equal(MhoCryXmlFormat.EncryptedCryXmlBinary, MhoCryXmlCodec.DetectFormat(encrypted));
        Assert.Equal(cryXmlBinary, MhoCryXmlCodec.Decrypt(encrypted));

        XDocument document = MhoCryXmlCodec.LoadDocument(encrypted);

        Assert.Equal("Root", document.Root?.Name.LocalName);
        Assert.Equal("1", document.Root?.Attribute("id")?.Value);
        Assert.Equal("Hello", document.Root?.Element("Child")?.Value);

        string xml = MhoCryXmlCodec.ReadXml(encrypted);
        Assert.Contains("<Root id=\"1\">", xml);
        Assert.Contains("<Child>Hello</Child>", xml);
    }

    [Fact]
    public void CanLoadPlainXmlDocument()
    {
        byte[] xml = Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?><Root><Child>Hello</Child></Root>");

        Assert.True(MhoCryXmlCodec.IsCryXmlCodec(xml));
        Assert.True(MhoCryXmlCodec.IsCryXmlCodex(xml));
        Assert.False(MhoCryXmlCodec.NeedsDecryption(xml));
        Assert.Equal(MhoCryXmlFormat.PlainXml, MhoCryXmlCodec.DetectFormat(xml));

        XDocument document = MhoCryXmlCodec.LoadDocument(xml);

        Assert.Equal("Root", document.Root?.Name.LocalName);
        Assert.Equal("Hello", document.Root?.Element("Child")?.Value);
    }

    [Fact]
    public void CanDetectEncryptedTextXml()
    {
        byte[] xml = Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?><Root id=\"plain\" />");
        byte[] encrypted = EncryptPayload(xml);

        Assert.Equal(MhoCryXmlFormat.EncryptedXml, MhoCryXmlCodec.DetectFormat(encrypted));
        Assert.True(MhoCryXmlCodec.IsCryXmlCodec(encrypted));
        Assert.True(MhoCryXmlCodec.IsCryXmlCodex(encrypted));
        Assert.True(MhoCryXmlCodec.NeedsDecryption(encrypted));
        Assert.Equal("plain", MhoCryXmlCodec.LoadDocument(encrypted).Root?.Attribute("id")?.Value);
    }

    private static byte[] BuildCryXmlBinary()
    {
        byte[] stringTable = Encoding.UTF8.GetBytes("Root\0id\01\0Child\0Hello\0\0");

        const int headerSize = 44;
        const int nodeSize = 28;
        const int nodeCount = 2;
        const int attributeCount = 1;
        const int childCount = 1;

        int nodeTableOffset = headerSize;
        int attrTableOffset = nodeTableOffset + (nodeSize * nodeCount);
        int childTableOffset = attrTableOffset + (attributeCount * 8);
        int dataTableOffset = childTableOffset + (childCount * 4);
        int fileSize = dataTableOffset + stringTable.Length;

        using MemoryStream stream = new();
        using BinaryWriter writer = new(stream, Encoding.UTF8, leaveOpen: true);

        writer.Write(Encoding.ASCII.GetBytes("CryXmlB"));
        writer.Write((byte)0);
        writer.Write(fileSize);
        writer.Write(nodeTableOffset);
        writer.Write(nodeCount);
        writer.Write(attrTableOffset);
        writer.Write(attributeCount);
        writer.Write(childTableOffset);
        writer.Write(childCount);
        writer.Write(dataTableOffset);
        writer.Write(stringTable.Length);

        writer.Write(0);
        writer.Write(22);
        writer.Write((short)1);
        writer.Write((short)1);
        writer.Write(-1);
        writer.Write(0);
        writer.Write(0);
        writer.Write(0);

        writer.Write(10);
        writer.Write(16);
        writer.Write((short)0);
        writer.Write((short)0);
        writer.Write(0);
        writer.Write(1);
        writer.Write(0);
        writer.Write(0);

        writer.Write(5);
        writer.Write(8);

        writer.Write(1);
        writer.Write(stringTable);

        return stream.ToArray();
    }

    private static byte[] EncryptPayload(byte[] plaintext)
    {
        byte[] encryptedPayload = new byte[plaintext.Length];
        int offset = 0;
        int remaining = plaintext.Length;

        while (remaining >= 129)
        {
            for (int i = 0; i < 64; i++)
            {
                encryptedPayload[offset + i] = (byte)(plaintext[offset + i + 65] ^ EncryptTable0[i]);
            }

            for (int i = 0; i < 65; i++)
            {
                encryptedPayload[offset + i + 64] = (byte)(plaintext[offset + i] ^ EncryptTable1[i]);
            }

            offset += 129;
            remaining -= 129;
        }

        if (remaining > 0)
        {
            if (remaining <= 65)
            {
                for (int i = 0; i < remaining; i++)
                {
                    encryptedPayload[offset + i] = (byte)(plaintext[offset + i] ^ EncryptTable1[i]);
                }
            }
            else
            {
                int prefixLength = remaining - 65;
                for (int i = 0; i < prefixLength; i++)
                {
                    encryptedPayload[offset + i] = (byte)(plaintext[offset + i + 65] ^ EncryptTable0[i]);
                }

                for (int i = 0; i < 65; i++)
                {
                    encryptedPayload[offset + prefixLength + i] = (byte)(plaintext[offset + i] ^ EncryptTable1[i]);
                }
            }
        }

        byte[] result = new byte[EncryptedHeader.Length + encryptedPayload.Length];
        EncryptedHeader.CopyTo(result, 0);
        encryptedPayload.CopyTo(result, EncryptedHeader.Length);
        return result;
    }
}
