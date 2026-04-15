using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

internal static class LooseZipArchiveReader
{
    private const uint LocalFileHeaderSignature = 0x04034B50;

    public static byte[]? ReadEntry(string archivePath, string entryName)
    {
        Dictionary<string, byte[]> entries = ReadEntries(archivePath, [entryName]);
        return entries.TryGetValue(NormalizeEntryName(entryName), out byte[] data) ? data : null;
    }

    public static Dictionary<string, byte[]> ReadEntries(string archivePath, IEnumerable<string> entryNames)
    {
        HashSet<string> wanted = new(entryNames
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Select(NormalizeEntryName), StringComparer.OrdinalIgnoreCase);
        Dictionary<string, byte[]> result = new(StringComparer.OrdinalIgnoreCase);
        if (wanted.Count == 0)
        {
            return result;
        }

        using FileStream stream = File.OpenRead(archivePath);
        using BinaryReader reader = new(stream, Encoding.UTF8, leaveOpen: false);

        while (stream.Position <= stream.Length - 30)
        {
            long headerOffset = stream.Position;
            uint signature = reader.ReadUInt32();
            if (signature != LocalFileHeaderSignature)
            {
                stream.Position = headerOffset;
                break;
            }

            reader.ReadUInt16(); // versionNeededToExtract
            ushort generalPurposeBitFlag = reader.ReadUInt16();
            ushort compressionMethod = reader.ReadUInt16();
            reader.ReadUInt16(); // lastModTime
            reader.ReadUInt16(); // lastModDate
            reader.ReadUInt32(); // crc32
            uint compressedSize = reader.ReadUInt32();
            uint uncompressedSize = reader.ReadUInt32();
            ushort fileNameLength = reader.ReadUInt16();
            ushort extraFieldLength = reader.ReadUInt16();

            string entryName = NormalizeEntryName(ReadEntryName(reader.ReadBytes(fileNameLength), generalPurposeBitFlag));
            if (extraFieldLength > 0)
            {
                stream.Position += extraFieldLength;
            }

            if ((generalPurposeBitFlag & 0x0008) != 0)
            {
                throw new InvalidDataException($"ZIP entry '{entryName}' in '{archivePath}' uses a data descriptor, which is not supported.");
            }

            byte[] compressedData = reader.ReadBytes(checked((int)compressedSize));
            if (compressedData.Length != compressedSize)
            {
                throw new EndOfStreamException($"Unexpected end of ZIP entry '{entryName}' in '{archivePath}'.");
            }

            if (!wanted.Contains(entryName))
            {
                continue;
            }

            result[entryName] = DecompressEntry(compressionMethod, compressedData, checked((int)uncompressedSize));
            if (result.Count == wanted.Count)
            {
                break;
            }
        }

        return result;
    }

    private static byte[] DecompressEntry(ushort compressionMethod, byte[] compressedData, int uncompressedSize)
    {
        return compressionMethod switch
        {
            0 => compressedData,
            8 => Inflate(compressedData, uncompressedSize),
            _ => throw new InvalidDataException($"Unsupported ZIP compression method: {compressionMethod}."),
        };
    }

    private static byte[] Inflate(byte[] compressedData, int uncompressedSize)
    {
        using MemoryStream input = new(compressedData, writable: false);
        using DeflateStream deflate = new(input, CompressionMode.Decompress);
        using MemoryStream output = uncompressedSize > 0 ? new MemoryStream(uncompressedSize) : new MemoryStream();
        deflate.CopyTo(output);
        return output.ToArray();
    }

    private static string ReadEntryName(byte[] entryNameData, ushort generalPurposeBitFlag)
    {
        Encoding encoding = (generalPurposeBitFlag & 0x0800) != 0 ? Encoding.UTF8 : Encoding.GetEncoding(437);
        return encoding.GetString(entryNameData);
    }

    private static string NormalizeEntryName(string entryName)
    {
        return entryName.Replace('\\', '/');
    }
}
