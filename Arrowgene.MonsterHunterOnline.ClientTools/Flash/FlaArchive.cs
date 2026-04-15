using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Flash;

public sealed class FlaArchive
{
    private const uint LocalFileHeaderSignature = 0x04034B50;
    private static readonly StringComparer EntryComparer = StringComparer.OrdinalIgnoreCase;

    private readonly Dictionary<string, FlaArchiveEntry> _entriesByName;
    private readonly List<FlaArchiveEntry> _entries;

    private FlaArchive(string sourcePath)
    {
        SourcePath = sourcePath;
        _entries = [];
        _entriesByName = new Dictionary<string, FlaArchiveEntry>(EntryComparer);
    }

    public string SourcePath { get; }
    public IReadOnlyList<FlaArchiveEntry> Entries => _entries;

    public static bool IsFla(string path)
    {
        if (!File.Exists(path))
        {
            return false;
        }

        Span<byte> header = stackalloc byte[4];
        using FileStream stream = File.OpenRead(path);
        int read = stream.Read(header);
        return read == 4 && BinaryPrimitives.ReadUInt32LittleEndian(header) == LocalFileHeaderSignature;
    }

    public static FlaArchive Open(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"FLA archive not found: {path}");
        }

        FlaArchive archive = new(path);
        archive.Load();
        return archive;
    }

    public bool TryGetEntry(string entryName, out FlaArchiveEntry? entry)
    {
        return _entriesByName.TryGetValue(NormalizeEntryName(entryName), out entry);
    }

    public FlaArchiveEntry GetEntry(string entryName)
    {
        if (!TryGetEntry(entryName, out FlaArchiveEntry? entry) || entry == null)
        {
            throw new FileNotFoundException($"Entry not found in FLA archive: {entryName}");
        }

        return entry;
    }

    public byte[] Extract(string entryName)
    {
        return Extract(GetEntry(entryName));
    }

    public byte[] Extract(FlaArchiveEntry entry)
    {
        using FileStream stream = File.OpenRead(SourcePath);
        stream.Position = entry.DataOffset;

        byte[] compressedData = new byte[checked((int)entry.CompressedSize)];
        ReadExactly(stream, compressedData);
        return DecompressEntry(entry.CompressionMethod, compressedData, checked((int)entry.UncompressedSize));
    }

    public string ReadText(string entryName, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetString(Extract(entryName));
    }

    public void ExtractAll(string outputDirectory, bool writeManifest = true)
    {
        Directory.CreateDirectory(outputDirectory);

        foreach (FlaArchiveEntry entry in _entries)
        {
            string outputPath = Path.Combine(outputDirectory, entry.FullName.Replace('/', Path.DirectorySeparatorChar));
            if (entry.IsDirectory)
            {
                Directory.CreateDirectory(outputPath);
                continue;
            }

            string? directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(outputPath, Extract(entry));
        }

        if (writeManifest)
        {
            WriteManifest(outputDirectory);
        }
    }

    private void Load()
    {
        using FileStream stream = File.OpenRead(SourcePath);
        using BinaryReader reader = new(stream, Encoding.UTF8, leaveOpen: true);

        while (stream.Position <= stream.Length - 4)
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
            ushort lastModTime = reader.ReadUInt16();
            ushort lastModDate = reader.ReadUInt16();
            uint crc32 = reader.ReadUInt32();
            uint compressedSize = reader.ReadUInt32();
            uint uncompressedSize = reader.ReadUInt32();
            ushort fileNameLength = reader.ReadUInt16();
            ushort extraFieldLength = reader.ReadUInt16();

            string fullName = NormalizeEntryName(ReadEntryName(reader.ReadBytes(fileNameLength), generalPurposeBitFlag));
            if ((generalPurposeBitFlag & 0x0008) != 0)
            {
                throw new InvalidDataException(
                    $"FLA archive '{SourcePath}' uses ZIP data descriptors for '{fullName}', which are not supported.");
            }

            if (extraFieldLength > 0)
            {
                stream.Position += extraFieldLength;
            }

            long dataOffset = stream.Position;
            stream.Position += compressedSize;

            bool isDirectory = fullName.EndsWith("/", StringComparison.Ordinal);
            DateTime? lastWriteTime = DecodeDosTimestamp(lastModTime, lastModDate);
            FlaArchiveEntry entry = new(fullName, compressionMethod, compressedSize, uncompressedSize, crc32, dataOffset,
                isDirectory, lastWriteTime);

            if (_entriesByName.TryGetValue(fullName, out FlaArchiveEntry? existing))
            {
                int existingIndex = _entries.IndexOf(existing);
                if (existingIndex >= 0)
                {
                    _entries[existingIndex] = entry;
                }

                _entriesByName[fullName] = entry;
                continue;
            }

            _entries.Add(entry);
            _entriesByName.Add(fullName, entry);
        }
    }

    private void WriteManifest(string outputDirectory)
    {
        var manifest = new
        {
            Type = "FLA",
            SourceFile = Path.GetFileName(SourcePath),
            EntryCount = _entries.Count,
            FileCount = _entries.Count(entry => !entry.IsDirectory),
            DirectoryCount = _entries.Count(entry => entry.IsDirectory),
            Entries = _entries.Select(entry => new
            {
                entry.FullName,
                entry.CompressionMethod,
                CompressionName = GetCompressionMethodName(entry.CompressionMethod),
                entry.CompressedSize,
                entry.UncompressedSize,
                entry.Crc32,
                entry.IsDirectory,
                entry.LastWriteTime,
            }),
        };

        string manifestPath = Path.Combine(outputDirectory, "manifest.json");
        string json = JsonSerializer.Serialize(manifest, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(manifestPath, json);
    }

    private static string NormalizeEntryName(string entryName)
    {
        return entryName.Replace('\\', '/');
    }

    private static string ReadEntryName(byte[] entryNameData, ushort generalPurposeBitFlag)
    {
        Encoding encoding = (generalPurposeBitFlag & 0x0800) != 0 ? Encoding.UTF8 : Encoding.GetEncoding(437);
        return encoding.GetString(entryNameData);
    }

    private static byte[] DecompressEntry(ushort compressionMethod, byte[] compressedData, int uncompressedSize)
    {
        return compressionMethod switch
        {
            0 => compressedData,
            8 => InflateDeflate(compressedData, uncompressedSize),
            _ => throw new InvalidDataException($"Unsupported ZIP compression method in FLA archive: {compressionMethod}."),
        };
    }

    private static byte[] InflateDeflate(byte[] compressedData, int uncompressedSize)
    {
        using MemoryStream input = new(compressedData, writable: false);
        using DeflateStream deflate = new(input, CompressionMode.Decompress);
        using MemoryStream output = uncompressedSize > 0 ? new MemoryStream(uncompressedSize) : new MemoryStream();
        deflate.CopyTo(output);
        return output.ToArray();
    }

    private static string GetCompressionMethodName(ushort compressionMethod)
    {
        return compressionMethod switch
        {
            0 => "Stored",
            8 => "Deflate",
            _ => $"Method {compressionMethod}",
        };
    }

    private static DateTime? DecodeDosTimestamp(ushort time, ushort date)
    {
        if (time == 0 && date == 0)
        {
            return null;
        }

        try
        {
            int year = 1980 + ((date >> 9) & 0x7F);
            int month = (date >> 5) & 0x0F;
            int day = date & 0x1F;
            int hour = (time >> 11) & 0x1F;
            int minute = (time >> 5) & 0x3F;
            int second = (time & 0x1F) * 2;
            return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    private static void ReadExactly(Stream stream, byte[] buffer)
    {
        int totalRead = 0;
        while (totalRead < buffer.Length)
        {
            int read = stream.Read(buffer, totalRead, buffer.Length - totalRead);
            if (read <= 0)
            {
                throw new EndOfStreamException("Unexpected end of FLA archive.");
            }

            totalRead += read;
        }
    }
}

public sealed class FlaArchiveEntry
{
    internal FlaArchiveEntry(string fullName, ushort compressionMethod, uint compressedSize, uint uncompressedSize,
        uint crc32, long dataOffset, bool isDirectory, DateTime? lastWriteTime)
    {
        FullName = fullName;
        CompressionMethod = compressionMethod;
        CompressedSize = compressedSize;
        UncompressedSize = uncompressedSize;
        Crc32 = crc32;
        DataOffset = dataOffset;
        IsDirectory = isDirectory;
        LastWriteTime = lastWriteTime;
    }

    public string FullName { get; }
    public string Name => Path.GetFileName(FullName.TrimEnd('/'));
    public ushort CompressionMethod { get; }
    public uint CompressedSize { get; }
    public uint UncompressedSize { get; }
    public uint Crc32 { get; }
    public bool IsDirectory { get; }
    public DateTime? LastWriteTime { get; }
    internal long DataOffset { get; }
}
