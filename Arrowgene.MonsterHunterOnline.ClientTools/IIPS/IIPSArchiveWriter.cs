#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.ClientTools.IIPS;

internal static class IIPSArchiveWriter
{
    public static void Save(IIPSArchive archive, string path, IIPSArchiveSaveOptions options)
    {
        string targetPath = Path.GetFullPath(path);
        string? targetDirectory = Path.GetDirectoryName(targetPath);
        if (string.IsNullOrEmpty(targetDirectory))
        {
            throw new InvalidOperationException($"Could not determine output directory for path '{path}'.");
        }

        Directory.CreateDirectory(targetDirectory);
        string tempPath = Path.Combine(targetDirectory, Path.GetRandomFileName());
        List<IIPSArchiveEntryRecord> records = PrepareRecords(archive, options);

        try
        {
            using (FileStream output = new FileStream(tempPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                output.Position = IIPSArchiveFormat.HeaderLength;
                foreach (IIPSArchiveEntryRecord record in records)
                {
                    ulong sourceFileOffset = record.FileOffset;
                    record.FileOffset = (ulong)output.Position;
                    byte[] storedData = BuildStoredData(archive, record, options, sourceFileOffset);
                    output.Write(storedData, 0, storedData.Length);
                    record.CompressedSize = (ulong)storedData.Length;
                }

                byte[] hetSection = IIPSArchiveSerialization.BuildSection(IIPSArchiveFormat.HetSignature, BuildHetData(records));
                ulong hetOffset = (ulong)output.Position;
                output.Write(hetSection, 0, hetSection.Length);

                byte[] betSection = IIPSArchiveSerialization.BuildSection(IIPSArchiveFormat.BetSignature, BuildBetData(records));
                ulong betOffset = (ulong)output.Position;
                output.Write(betSection, 0, betSection.Length);

                IIPSArchiveHeaderData header = new IIPSArchiveHeaderData
                {
                    Magic = IIPSArchiveFormat.Magic,
                    HeaderLength = IIPSArchiveFormat.HeaderLength,
                    FormatVersion = archive.Metadata.FormatVersion,
                    SectorSizeShift = archive.Metadata.SectorSizeShift,
                    ArchiveSize = (ulong)output.Position,
                    BetOffset = betOffset,
                    HetOffset = hetOffset,
                    Md5TableOffset = 0,
                    BitmapOffset = 0,
                    HetLength = (ulong)hetSection.Length,
                    BetLength = (ulong)betSection.Length,
                    Md5TableLength = 0,
                    BitmapLength = 0,
                    Md5PieceSize = 0,
                    RawChunkSize = 0,
                    Md5PatchBaseTag = new byte[16],
                    Md5PatchedTag = new byte[16],
                    BetMd5 = IIPSArchiveCrypto.Md5(betSection),
                    HetMd5 = IIPSArchiveCrypto.Md5(hetSection),
                };

                byte[] headerBytes = IIPSArchiveSerialization.BuildHeader(header);
                output.Position = 0;
                output.Write(headerBytes, 0, headerBytes.Length);
            }

            bool overwriteCurrentSource = archive.CurrentSourcePath != null &&
                                          string.Equals(Path.GetFullPath(archive.CurrentSourcePath), targetPath, StringComparison.OrdinalIgnoreCase);
            if (overwriteCurrentSource)
            {
                archive.ReleaseSourceHandles();
            }

            File.Move(tempPath, targetPath, overwrite: true);
            IIPSArchiveReader.Load(archive, targetPath, new IIPSArchiveOpenOptions());
        }
        finally
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }

    private static List<IIPSArchiveEntryRecord> PrepareRecords(IIPSArchive archive, IIPSArchiveSaveOptions options)
    {
        List<IIPSArchiveEntryRecord> records = archive.Records.Select(CloneRecord).ToList();
        if (!options.IncludeListFile)
        {
            Reindex(records);
            return records;
        }

        List<string> fileNames = records
            .Where(record => !string.IsNullOrEmpty(record.FileName) && !string.Equals(record.FileName, "(listfile)", StringComparison.OrdinalIgnoreCase))
            .Select(record => record.FileName!)
            .ToList();

        List<string> listFileEntries = new List<string>(fileNames.Count + 1) { "(listfile)" };
        listFileEntries.AddRange(fileNames);
        byte[] listFileContent = Encoding.UTF8.GetBytes(string.Join('\n', listFileEntries) + '\n');

        IIPSArchiveEntryRecord? listFileRecord = records.FirstOrDefault(record => string.Equals(record.FileName, "(listfile)", StringComparison.OrdinalIgnoreCase));
        if (listFileRecord == null)
        {
            listFileRecord = new IIPSArchiveEntryRecord
            {
                FileName = "(listfile)",
                SourceKind = IIPSArchiveEntrySourceKind.Memory,
                WriteOptions = new IIPSArchiveEntryOptions(),
            };
            records.Add(listFileRecord);
        }

        listFileRecord.FileName = "(listfile)";
        listFileRecord.NameHash = IIPSArchiveFormat.MaskNameHash(IIPSArchiveCrypto.ComputeNameHash("(listfile)"));
        listFileRecord.SourceKind = IIPSArchiveEntrySourceKind.Memory;
        listFileRecord.Content = listFileContent;
        listFileRecord.FileSize = (ulong)listFileContent.Length;
        listFileRecord.CompressedSize = (ulong)listFileContent.Length;
        listFileRecord.Md5 = MD5.HashData(listFileContent);
        listFileRecord.Extra = 0;
        listFileRecord.WriteOptions = new IIPSArchiveEntryOptions();
        listFileRecord.Flags = (uint)IIPSArchiveEntryFlags.Exists | (uint)IIPSArchiveEntryFlags.SingleUnit;

        Reindex(records);
        return records;
    }

    private static byte[] BuildStoredData(IIPSArchive archive, IIPSArchiveEntryRecord record, IIPSArchiveSaveOptions options, ulong sourceFileOffset)
    {
        if (CanPreserveStoredBytes(record, options))
        {
            record.NameHash = EnsureNameHash(record);
            ulong targetFileOffset = record.FileOffset;
            record.FileOffset = sourceFileOffset;
            try
            {
                record.Md5 ??= record.FileSize == 0
                    ? MD5.HashData(Array.Empty<byte>())
                    : MD5.HashData(archive.ExtractRecord(record));
                return archive.ReadStoredBytes(record);
            }
            finally
            {
                record.FileOffset = targetFileOffset;
            }
        }

        byte[] content;
        if (record.SourceKind == IIPSArchiveEntrySourceKind.Memory)
        {
            content = record.Content == null ? Array.Empty<byte>() : (byte[])record.Content.Clone();
        }
        else
        {
            ulong targetFileOffset = record.FileOffset;
            record.FileOffset = sourceFileOffset;
            try
            {
                content = archive.ExtractRecord(record);
            }
            finally
            {
                record.FileOffset = targetFileOffset;
            }
        }

        record.NameHash = EnsureNameHash(record);
        record.FileSize = (ulong)content.Length;
        record.Md5 = MD5.HashData(content);
        record.Extra = 0;

        if (content.Length == 0)
        {
            record.Flags = (uint)IIPSArchiveEntryFlags.Exists | (uint)IIPSArchiveEntryFlags.SingleUnit;
            record.CompressedSize = 0;
            return Array.Empty<byte>();
        }

        return record.WriteOptions.StorageMode == IIPSArchiveStorageMode.SectorBased
            ? EncodeSectorBased(record, content, archive.Metadata.SectorSize)
            : EncodeSingleUnit(record, content);
    }

    private static byte[] EncodeSingleUnit(IIPSArchiveEntryRecord record, byte[] content)
    {
        IIPSArchiveEntryOptions options = record.WriteOptions;
        byte[] stored = content;
        bool compressed = false;
        if (options.Compress)
        {
            byte[] compressedCandidate = IIPSArchiveFormat.Compress(content);
            if (compressedCandidate.Length < content.Length)
            {
                stored = compressedCandidate;
                compressed = true;
            }
        }

        if (options.Encrypt)
        {
            stored = (byte[])stored.Clone();
            IIPSArchiveCrypto.MpqEncryptBlock(stored, ComputeFileKey(record.FileName!, options.UseFixedKey, record.FileOffset, (ulong)content.Length));
        }

        uint flags = (uint)IIPSArchiveEntryFlags.Exists | (uint)IIPSArchiveEntryFlags.SingleUnit;
        if (compressed)
        {
            flags |= (uint)IIPSArchiveEntryFlags.Compressed;
        }

        if (options.Encrypt)
        {
            flags |= (uint)IIPSArchiveEntryFlags.Encrypted;
        }

        if (options.UseFixedKey)
        {
            flags |= (uint)IIPSArchiveEntryFlags.FixKey;
        }

        record.Flags = flags;
        record.CompressedSize = (ulong)stored.Length;
        return stored;
    }

    private static byte[] EncodeSectorBased(IIPSArchiveEntryRecord record, byte[] content, uint sectorSize)
    {
        IIPSArchiveEntryOptions options = record.WriteOptions;
        int sectorCount = (content.Length + (int)sectorSize - 1) / (int)sectorSize;
        if (sectorCount == 0)
        {
            return EncodeSingleUnit(record, content);
        }

        uint fileKey = options.Encrypt
            ? ComputeFileKey(record.FileName!, options.UseFixedKey, record.FileOffset, (ulong)content.Length)
            : 0;

        List<byte[]> sectors = new List<byte[]>(sectorCount);
        uint[] offsets = new uint[sectorCount + 1];
        uint currentOffset = (uint)(offsets.Length * sizeof(uint));
        bool anyCompressed = false;

        for (int sectorIndex = 0; sectorIndex < sectorCount; sectorIndex++)
        {
            int start = sectorIndex * (int)sectorSize;
            int rawLength = Math.Min((int)sectorSize, content.Length - start);
            byte[] rawSector = new byte[rawLength];
            Array.Copy(content, start, rawSector, 0, rawLength);

            byte[] storedSector = rawSector;
            if (options.Compress)
            {
                byte[] compressedSector = IIPSArchiveFormat.Compress(rawSector);
                if (compressedSector.Length < rawSector.Length)
                {
                    storedSector = compressedSector;
                    anyCompressed = true;
                }
            }

            if (options.Encrypt && storedSector.Length > 0)
            {
                storedSector = (byte[])storedSector.Clone();
                IIPSArchiveCrypto.MpqEncryptBlock(storedSector, fileKey + (uint)sectorIndex);
            }

            offsets[sectorIndex] = currentOffset;
            currentOffset += (uint)storedSector.Length;
            sectors.Add(storedSector);
        }

        offsets[sectorCount] = currentOffset;

        byte[] offsetTable = new byte[offsets.Length * sizeof(uint)];
        for (int i = 0; i < offsets.Length; i++)
        {
            BitConverter.GetBytes(offsets[i]).CopyTo(offsetTable, i * sizeof(uint));
        }

        if (options.Encrypt && offsetTable.Length > 0)
        {
            IIPSArchiveCrypto.MpqEncryptBlock(offsetTable, fileKey - 1);
        }

        using MemoryStream ms = new MemoryStream((int)currentOffset);
        ms.Write(offsetTable, 0, offsetTable.Length);
        foreach (byte[] sector in sectors)
        {
            ms.Write(sector, 0, sector.Length);
        }

        uint flags = (uint)IIPSArchiveEntryFlags.Exists;
        if (anyCompressed)
        {
            flags |= (uint)IIPSArchiveEntryFlags.Compressed;
        }

        if (options.Encrypt)
        {
            flags |= (uint)IIPSArchiveEntryFlags.Encrypted;
        }

        if (options.UseFixedKey)
        {
            flags |= (uint)IIPSArchiveEntryFlags.FixKey;
        }

        record.Flags = flags;
        record.CompressedSize = (ulong)ms.Length;
        return ms.ToArray();
    }

    private static byte[] BuildHetData(List<IIPSArchiveEntryRecord> records)
    {
        uint entryCount = (uint)records.Count;
        uint slotCount = NextSlotCount(entryCount);
        uint indexBits = (uint)IIPSArchiveFormat.BitsRequired(entryCount == 0 ? 0 : entryCount - 1);
        uint indexStrideBits = indexBits;
        int indexTableBytes = (int)((slotCount * indexStrideBits + 7) / 8);

        byte[] nameHashBytes = new byte[slotCount];
        byte[] fileIndexData = new byte[indexTableBytes];

        foreach (IIPSArchiveEntryRecord record in records)
        {
            ulong nameHash = EnsureNameHash(record);
            byte hashByte = (byte)(nameHash >> 56);
            uint slot = slotCount == 0 ? 0 : (uint)(nameHash % slotCount);
            while (nameHashBytes[slot] != 0)
            {
                slot = (slot + 1) % slotCount;
            }

            nameHashBytes[slot] = hashByte;
            record.HetIndex = (int)slot;
            IIPSArchiveFormat.WriteBits(fileIndexData, (long)slot * indexStrideBits, (int)indexBits, (ulong)record.Index);
        }

        using MemoryStream ms = new MemoryStream();
        using BinaryWriter writer = new BinaryWriter(ms, Encoding.UTF8, leaveOpen: true);
        writer.Write((uint)(32 + nameHashBytes.Length + fileIndexData.Length));
        writer.Write(entryCount);
        writer.Write(slotCount);
        writer.Write(IIPSArchiveFormat.HashBits);
        writer.Write(indexStrideBits);
        writer.Write(0u);
        writer.Write(indexBits);
        writer.Write((uint)fileIndexData.Length);
        writer.Write(nameHashBytes);
        writer.Write(fileIndexData);
        return ms.ToArray();
    }

    private static byte[] BuildBetData(List<IIPSArchiveEntryRecord> records)
    {
        ulong maxFileOffset = records.Count == 0 ? 0 : records.Max(record => record.FileOffset);
        ulong maxFileSize = records.Count == 0 ? 0 : records.Max(record => record.FileSize);
        ulong maxStoredSize = records.Count == 0 ? 0 : records.Max(record => record.CompressedSize);

        uint filePosBits = (uint)IIPSArchiveFormat.BitsRequired(maxFileOffset);
        uint fileSizeBits = (uint)IIPSArchiveFormat.BitsRequired(maxFileSize);
        uint compressedSizeBits = (uint)IIPSArchiveFormat.BitsRequired(maxStoredSize);
        const uint flagsBits = 32;
        const uint md5Bits = 128;
        const uint extraBits = 0;
        const uint betHashBits = IIPSArchiveFormat.BetHashBits;

        uint bitIndexFilePos = 0;
        uint bitIndexFileSize = bitIndexFilePos + filePosBits;
        uint bitIndexCompressedSize = bitIndexFileSize + fileSizeBits;
        uint bitIndexFlags = bitIndexCompressedSize + compressedSizeBits;
        uint bitIndexMd5 = bitIndexFlags + flagsBits;
        uint totalEntryBits = bitIndexMd5 + md5Bits + extraBits;

        byte[] entryData = new byte[(int)((records.Count * (long)totalEntryBits + 7) / 8)];
        byte[] hashData = new byte[(int)((records.Count * (long)betHashBits + 7) / 8)];

        for (int i = 0; i < records.Count; i++)
        {
            IIPSArchiveEntryRecord record = records[i];
            long entryBitOffset = (long)i * totalEntryBits;
            IIPSArchiveFormat.WriteBits(entryData, entryBitOffset + bitIndexFilePos, (int)filePosBits, record.FileOffset);
            IIPSArchiveFormat.WriteBits(entryData, entryBitOffset + bitIndexFileSize, (int)fileSizeBits, record.FileSize);
            IIPSArchiveFormat.WriteBits(entryData, entryBitOffset + bitIndexCompressedSize, (int)compressedSizeBits, record.CompressedSize);
            IIPSArchiveFormat.WriteBits(entryData, entryBitOffset + bitIndexFlags, (int)flagsBits, record.Flags);
            IIPSArchiveFormat.WriteBits(entryData, entryBitOffset + bitIndexMd5, record.Md5 ?? new byte[16]);
            IIPSArchiveFormat.WriteBits(hashData, (long)i * betHashBits, (int)betHashBits, record.NameHash & 0x00FFFFFFFFFFFFFFUL);
        }

        using MemoryStream ms = new MemoryStream();
        using BinaryWriter writer = new BinaryWriter(ms, Encoding.UTF8, leaveOpen: true);
        writer.Write((uint)(84 + entryData.Length + hashData.Length));
        writer.Write((uint)records.Count);
        writer.Write(totalEntryBits);
        writer.Write(bitIndexFilePos);
        writer.Write(bitIndexFileSize);
        writer.Write(bitIndexCompressedSize);
        writer.Write(bitIndexFlags);
        writer.Write(bitIndexMd5);
        writer.Write(0u);
        writer.Write(filePosBits);
        writer.Write(fileSizeBits);
        writer.Write(compressedSizeBits);
        writer.Write(flagsBits);
        writer.Write(md5Bits);
        writer.Write(0u);
        writer.Write(betHashBits);
        writer.Write(0u);
        writer.Write(betHashBits);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(extraBits);
        writer.Write(entryData);
        writer.Write(hashData);
        return ms.ToArray();
    }

    private static bool CanPreserveStoredBytes(IIPSArchiveEntryRecord record, IIPSArchiveSaveOptions options)
    {
        return options.PreserveUnchangedEntries &&
               record.SourceKind == IIPSArchiveEntrySourceKind.ExistingArchive &&
               !record.UsesFixedKey;
    }

    private static ulong EnsureNameHash(IIPSArchiveEntryRecord record)
    {
        if (record.NameHash != 0)
        {
            return record.NameHash;
        }

        if (string.IsNullOrEmpty(record.FileName))
        {
            throw new InvalidOperationException($"Entry {record.Index} has no name and no reconstructed hash.");
        }

        record.NameHash = IIPSArchiveFormat.MaskNameHash(IIPSArchiveCrypto.ComputeNameHash(record.FileName));
        return record.NameHash;
    }

    private static uint ComputeFileKey(string fileName, bool useFixedKey, ulong fileOffset, ulong fileSize)
    {
        uint key = IIPSArchiveCrypto.ComputeFileKey(fileName);
        if (useFixedKey)
        {
            key = (key + (uint)fileOffset) ^ (uint)fileSize;
        }

        return key;
    }

    private static IIPSArchiveEntryRecord CloneRecord(IIPSArchiveEntryRecord record)
    {
        return new IIPSArchiveEntryRecord
        {
            Index = record.Index,
            FileOffset = record.FileOffset,
            FileSize = record.FileSize,
            CompressedSize = record.CompressedSize,
            Flags = record.Flags,
            NameHash = record.NameHash,
            HetIndex = record.HetIndex,
            FileName = record.FileName,
            Md5 = record.Md5 == null ? null : (byte[])record.Md5.Clone(),
            Extra = record.Extra,
            SourceKind = record.SourceKind,
            Content = record.Content == null ? null : (byte[])record.Content.Clone(),
            WriteOptions = new IIPSArchiveEntryOptions
            {
                StorageMode = record.WriteOptions.StorageMode,
                Compress = record.WriteOptions.Compress,
                Encrypt = record.WriteOptions.Encrypt,
                UseFixedKey = record.WriteOptions.UseFixedKey,
            },
        };
    }

    private static void Reindex(List<IIPSArchiveEntryRecord> records)
    {
        for (int i = 0; i < records.Count; i++)
        {
            records[i].Index = i;
        }
    }

    private static uint NextSlotCount(uint entryCount)
    {
        uint minimumSlots = Math.Max(1u, (uint)Math.Ceiling(entryCount / 0.75d));
        uint slotCount = 1;
        while (slotCount < minimumSlots)
        {
            slotCount <<= 1;
        }

        return slotCount;
    }
}
