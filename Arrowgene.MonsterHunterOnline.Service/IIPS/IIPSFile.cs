using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Arrowgene.Buffers;
using Arrowgene.Logging;

namespace Arrowgene.MonsterHunterOnline.Service.IIPS;

public class IIPSFile
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(IIPSFile));

    public const uint IIPSHeaderLength = 0xAC;
    public const uint IIPSMagic = 0x7366696E; // "nifs"
    public const uint HetSignature = 0x1A544548; // "HET\x1A"
    public const uint BetSignature = 0x1A544542; // "BET\x1A"
    public uint Magic { get; set; }
    public uint HeaderLength { get; set; }
    public ushort FormatVersion { get; set; }
    public ushort SectorSizeShift { get; set; }
    public ulong ArchiveSize { get; set; }
    public ulong BetOffset { get; set; }
    public ulong HetOffset { get; set; }
    public ulong Md5TableOffset { get; set; }
    public ulong BitmapOffset { get; set; }
    public ulong HetLength { get; set; }
    public ulong BetLength { get; set; }
    public ulong Md5TableLength { get; set; }
    public ulong BitmapLength { get; set; }
    public uint Md5PieceSize { get; set; }
    public uint RawChunkSize { get; set; }
    public byte[] Md5PatchBaseTag { get; set; }
    public byte[] Md5PatchedTag { get; set; }
    public string BetMd5 { get; set; }
    public string HetMd5 { get; set; }
    public string HeaderMd5 { get; set; }

    public List<IIPSFileEntry> Entries { get; set; }
    public List<string> FileNames { get; set; }

    private FileStream _archiveStream;
    private BinaryReader _archive;
    private long _archiveSize;

    public IIPSFile()
    {
        Entries = new List<IIPSFileEntry>();
        FileNames = new List<string>();
    }

    public void Open(string path)
    {
        _archiveStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        _archive = new BinaryReader(_archiveStream);
        _archiveSize = _archiveStream.Length;

        _archiveStream.Position = 0;
        Magic = _archive.ReadUInt32();
        if (IIPSMagic != Magic)
        {
            Logger.Info($"MAGIC miss match");
        }

        HeaderLength = _archive.ReadUInt32();
        if (IIPSHeaderLength != HeaderLength)
        {
            Logger.Info($"header length miss match");
        }

        FormatVersion = _archive.ReadUInt16();
        SectorSizeShift = _archive.ReadUInt16();
        ArchiveSize = _archive.ReadUInt64();
        BetOffset = _archive.ReadUInt64();
        HetOffset = _archive.ReadUInt64();
        Md5TableOffset = _archive.ReadUInt64();
        BitmapOffset = _archive.ReadUInt64();
        HetLength = _archive.ReadUInt64();
        BetLength = _archive.ReadUInt64();
        Md5TableLength = _archive.ReadUInt64();
        BitmapLength = _archive.ReadUInt64();
        Md5PieceSize = _archive.ReadUInt32();
        RawChunkSize = _archive.ReadUInt32();
        Md5PatchBaseTag = _archive.ReadBytes(16);
        Md5PatchedTag = _archive.ReadBytes(16);
        byte[] md5Bet = _archive.ReadBytes(16);
        byte[] md5Het = _archive.ReadBytes(16);
        byte[] md5Header = _archive.ReadBytes(16);
        BetMd5 = BitConverter.ToString(md5Bet).Replace("-", "").ToLowerInvariant();
        HetMd5 = BitConverter.ToString(md5Het).Replace("-", "").ToLowerInvariant();
        HeaderMd5 = BitConverter.ToString(md5Header).Replace("-", "").ToLowerInvariant();

        byte[] md5headerTest = ReadBytesAt(0, (int)HeaderLength - 16);
        string headerMd5Test = IIPSCrypto.Md5(md5headerTest);
        if (HeaderMd5 != headerMd5Test)
        {
            Logger.Info($"Header MD5 miss match");
        }

        if (BetOffset + BetLength > (ulong)_archiveSize)
        {
            Logger.Info($"BET overflow");
        }

        byte[] md5BetTest = ReadBytesAt((long)BetOffset, (int)BetLength);
        string BetMd5Test = IIPSCrypto.Md5(md5BetTest);

        if (BetMd5 != BetMd5Test)
        {
            Logger.Info($"BET MD5 miss match");
        }

        if (HetOffset + HetLength > (ulong)_archiveSize)
        {
            Logger.Info($"HET overflow");
        }

        byte[] md5hetTest = ReadBytesAt((long)HetOffset, (int)HetLength);
        string hetMd5Test = IIPSCrypto.Md5(md5hetTest);
        if (HetMd5 != hetMd5Test)
        {
            Logger.Info($"HET MD5 miss match");
        }

        // Decrypt HET
        _archiveStream.Position = (long)HetOffset;
        uint hetMagic = _archive.ReadUInt32();
        uint hetVersion = _archive.ReadUInt32();
        uint hetDataLength = _archive.ReadUInt32();
        byte[] hetData = _archive.ReadBytes((int)hetDataLength);
        IIPSCrypto.IfsSectionCrypt(hetData);
        ParseHetTable(hetData);

        // Decrypt BET
        _archiveStream.Position = (long)BetOffset;
        uint betMagic = _archive.ReadUInt32();
        uint betVersion = _archive.ReadUInt32();
        uint betDataLength = _archive.ReadUInt32();
        byte[] betData = _archive.ReadBytes((int)betDataLength);
        IIPSCrypto.IfsSectionCrypt(betData);

        // Parse BET into file entries and reconstruct name hashes
        ParseBetEntries(betData);
        Logger.Info($"Parsed {Entries.Count} file entries");

        // Look up (listfile) by hash and parse it for real filenames
        LoadListFile();
        Logger.Info($"Loaded {FileNames.Count} filenames from (listfile)");
    }

    /// <summary>Read bytes at an absolute position without disturbing sequential reads.</summary>
    private byte[] ReadBytesAt(long offset, int count)
    {
        _archiveStream.Position = offset;
        return _archive.ReadBytes(count);
    }

    // HET parsed state
    private uint _hetEntryCount;
    private uint _hetTotalCount;
    private uint _hetIndexBits;
    private uint _hetIndexTotalBits;
    private uint _hetHashBits;
    private byte[] _hetNameHashes;

    private byte[] _hetFileIndexData;

    // RE: FUN_10012b70 computes AND/OR masks from hashBitSize
    private ulong _hetAndMask;
    private ulong _hetOrMask;

    // BET hash array state (for Phase 1 name hash reconstruction)
    private byte[] _betHashData;
    private uint _betHashSizeTotal;
    private uint _betHashSize;

    /// <summary>
    /// Parse the decrypted HET table.
    /// HET header: 8 uint32 fields, then name hash array, then bit-packed file index array.
    /// RE: FUN_10012c80 parses header, FUN_10012b70 allocates and computes masks.
    /// </summary>
    private void ParseHetTable(byte[] hetData)
    {
        if (hetData.Length < 32) return;

        StreamBuffer het = new StreamBuffer(hetData);
        het.SetPositionStart();

        uint tableSize = het.ReadUInt32(); // [0] total data size
        uint entryCount = het.ReadUInt32(); // [1] used entries (file count)
        uint totalCount = het.ReadUInt32(); // [2] total slots (hash table capacity)
        uint hashBitSize = het.ReadUInt32(); // [3] name hash bit size (typically 64)
        uint indexSizeTotal = het.ReadUInt32(); // [4] total bits per file index entry
        uint indexSizeExtra = het.ReadUInt32(); // [5] extra bits
        uint indexSize = het.ReadUInt32(); // [6] effective index bits
        uint indexTableSize = het.ReadUInt32(); // [7] file index array size in bytes

        _hetEntryCount = entryCount;
        _hetTotalCount = totalCount;
        _hetHashBits = hashBitSize;
        _hetIndexBits = indexSize;
        _hetIndexTotalBits = indexSizeTotal;

        // Compute AND/OR masks (RE: FUN_10012b70)
        // AND mask = (1 << hashBitSize) - 1
        if (hashBitSize >= 64)
        {
            _hetAndMask = 0xFFFFFFFFFFFFFFFF;
        }
        else
        {
            _hetAndMask = (1UL << (int)hashBitSize) - 1;
        }

        // OR mask = 1 << (hashBitSize - 1) — ensures top bit set so check byte is never 0x00
        _hetOrMask = 1UL << ((int)hashBitSize - 1);

        // Name hash array: 1 byte per slot
        _hetNameHashes = het.ReadBytes((int)totalCount);

        // Bit-packed file index array
        _hetFileIndexData = het.ReadBytes((int)indexTableSize);

        Logger.Info($"HET: entries={entryCount}, slots={totalCount}, hashBits={hashBitSize}, indexBits={indexSize}");
    }

    /// <summary>
    /// Compute 64-bit NIFS name hash for a filename.
    /// RE: FUN_100065f0 normalizes (lowercase, / -> \) then calls Jenkins hashlittle2 with seeds pc=2, pb=1.
    /// Result: hashLo=pc (bits 0-31), hashHi=pb (bits 32-63).
    /// </summary>
    private ulong ComputeNameHash(string fileName)
    {
        string normalized = fileName.Replace('/', '\\').ToLowerInvariant();
        byte[] nameBytes = Encoding.ASCII.GetBytes(normalized);
        IIPSCrypto.JenkinsHashlittle2(nameBytes, 2, 1, out uint pc, out uint pb);
        return ((ulong)pb << 32) | pc;
    }

    /// <summary>
    /// Apply HET AND/OR masks to a raw hash.
    /// RE: FUN_10012ee0 — maskedHash = (hash & andMask) | orMask
    /// </summary>
    private ulong MaskHash(ulong rawHash)
    {
        return (rawHash & _hetAndMask) | _hetOrMask;
    }

    /// <summary>
    /// Look up a file index by name using the HET hash table.
    /// Returns the BET file index, or -1 if not found.
    /// RE: FUN_10012df0 (by name) calls FUN_100065f0 then FUN_10012ee0 (by hash).
    /// </summary>
    public int FindFileByName(string fileName)
    {
        if (_hetNameHashes == null || _hetFileIndexData == null) return -1;

        ulong rawHash = ComputeNameHash(fileName);
        ulong maskedHash = MaskHash(rawHash);

        uint maskedLo = (uint)(maskedHash & 0xFFFFFFFF);
        uint maskedHi = (uint)(maskedHash >> 32);

        // Check byte = top 8 bits of masked hash
        byte expectedByte = (byte)(maskedHash >> ((int)_hetHashBits - 8));

        // Starting slot = maskedHash % hetTableSize
        uint startSlot = (uint)(maskedHash % _hetTotalCount);

        for (uint attempt = 0; attempt < _hetTotalCount; attempt++)
        {
            uint slot = (startSlot + attempt) % _hetTotalCount;
            byte nameHashByte = _hetNameHashes[slot];

            // 0x00 = empty slot, end of chain
            if (nameHashByte == 0x00) return -1;

            if (nameHashByte == expectedByte)
            {
                // Read the file index from the bit-packed array
                long bitPos = (long)slot * _hetIndexTotalBits;
                int fileIndex = (int)ReadBits(_hetFileIndexData, bitPos, (int)_hetIndexBits);

                if (fileIndex < Entries.Count)
                {
                    // Verify full 64-bit hash match (RE: FUN_10012ee0 checks entry+0x10/0x14)
                    var entry = Entries[fileIndex];
                    uint entryHashLo = (uint)(entry.NameHash & 0xFFFFFFFF);
                    uint entryHashHi = (uint)(entry.NameHash >> 32);
                    if (entryHashLo == maskedLo && entryHashHi == maskedHi)
                    {
                        return fileIndex;
                    }
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// Parse decrypted BET data into file entries, then reconstruct name hashes from HET+BET.
    /// RE: FUN_100132e0 parses 21-field header; FUN_10013ee0 does Phase 1 (hash) + Phase 2 (entries).
    ///
    /// BET header (21 uint32 fields = 84 bytes):
    ///   [0]  tableSize           [7]  biFlagIndex        [14] bcUnknown2
    ///   [1]  entryCount          [8]  biUnknown          [15] betHashSizeTotal
    ///   [2]  tableEntrySize      [9]  bcFilePos          [16] betHashSizeExtra
    ///   [3]  biUnknown2          [10] bcFileSize         [17] betHashSize
    ///   [4]  biFilePos           [11] bcCmpSize          [18] unknown
    ///   [5]  biFileSize          [12] bcFlags            [19] unknown
    ///   [6]  biCmpSize           [13] bcMd5              [20] bcExtra
    ///
    /// BET entry bit layout (from bit indices [3]-[7]):
    ///   FilePos → FileSize → CmpSize → Flags → MD5 → Extra
    /// </summary>
    private void ParseBetEntries(byte[] betData)
    {
        Entries.Clear();
        if (betData.Length < 84) return;

        StreamBuffer bet = new StreamBuffer(betData);
        bet.SetPositionStart();

        // 21-field header — dump all fields for analysis
        uint[] hdr = new uint[21];
        for (int i = 0; i < 21; i++) hdr[i] = bet.ReadUInt32();

        uint tableSize = hdr[0];
        uint entryCount = hdr[1];
        uint tableEntrySize = hdr[2];
        // Bit indices — where each field starts within an entry
        uint biFilePos = hdr[3];
        uint biFileSize = hdr[4];
        uint biCmpSize = hdr[5];
        uint biFlags = hdr[6];
        uint biMd5 = hdr[7];
        // Bit counts — how many bits each field uses
        uint bcFilePos = hdr[9];
        uint bcFileSize = hdr[10];
        uint bcCmpSize = hdr[11];
        uint bcFlags = hdr[12];
        uint bcMd5 = hdr[13];
        uint betHashSizeTotal = hdr[15];
        uint betHashSize = hdr[17];
        uint bcExtra = hdr[20];
        // Extra starts after MD5
        uint biExtra = biMd5 + bcMd5;

        _betHashSizeTotal = betHashSizeTotal;
        _betHashSize = betHashSize;

        // Compute totalBitsPerEntry from actual field bit counts — the DLL reads fields
        // sequentially (FUN_10013ee0 Phase 2), so the total is their sum.
        // Field[2] may also hold this value, but we compute it to be safe.
        uint totalBitsPerEntry = bcFilePos + bcFileSize + bcCmpSize + bcMd5 + bcExtra + bcFlags;
        if (tableEntrySize > 0 && tableEntrySize != totalBitsPerEntry)
        {
            Logger.Info($"BET: field[2]={tableEntrySize} != computed={totalBitsPerEntry}, using field[2]");
            totalBitsPerEntry = tableEntrySize;
        }

        Logger.Info($"BET header: entries={entryCount}, bitsPerEntry={totalBitsPerEntry}, " +
                    $"bcFilePos={bcFilePos}, bcFileSize={bcFileSize}, bcCmpSize={bcCmpSize}, " +
                    $"bcMd5={bcMd5}, bcExtra={bcExtra}, bcFlags={bcFlags}, " +
                    $"betHashSize={betHashSize}/{betHashSizeTotal}");

        // All bit data starts at byte 84 (21 * 4)
        byte[] allBitData = new byte[betData.Length - 84];
        Array.Copy(betData, 84, allBitData, 0, allBitData.Length);

        // Entry bit data occupies the first portion
        int entryDataBits = (int)((long)entryCount * totalBitsPerEntry);
        int entryDataBytes = (entryDataBits + 7) / 8;

        // Hash bit data follows entry data (RE: FUN_100132e0 copies two arrays sequentially)
        if (entryDataBytes < allBitData.Length)
        {
            int hashDataLen = allBitData.Length - entryDataBytes;
            _betHashData = new byte[hashDataLen];
            Array.Copy(allBitData, entryDataBytes, _betHashData, 0, hashDataLen);
        }

        // Phase 2: Read file entries using bit INDICES from header (not sequential).
        // Layout: FilePos → FileSize → CmpSize → Flags → MD5 → Extra
        for (uint i = 0; i < entryCount; i++)
        {
            long basePos = (long)i * totalBitsPerEntry;

            ulong filePos = ReadBits(allBitData, basePos + biFilePos, (int)bcFilePos);
            ulong fileSize = ReadBits(allBitData, basePos + biFileSize, (int)bcFileSize);
            ulong cmpSize = ReadBits(allBitData, basePos + biCmpSize, (int)bcCmpSize);
            uint flags = (uint)ReadBits(allBitData, basePos + biFlags, (int)bcFlags);

            byte[] md5 = null;
            if (bcMd5 > 0)
                md5 = ReadBitsAsBytes(allBitData, basePos + biMd5, (int)bcMd5);

            ulong extra = 0;
            if (bcExtra > 0)
                extra = ReadBits(allBitData, basePos + biExtra, (int)bcExtra);

            Entries.Add(new IIPSFileEntry
            {
                Index = (int)i,
                FileOffset = filePos,
                FileSize = fileSize,
                CompressedSize = cmpSize,
                Flags = flags,
                Md5 = md5,
                Extra = extra
            });
        }

        // Phase 1: Reconstruct full 64-bit name hashes from HET byte + BET hash bits
        // RE: FUN_10013ee0 Phase 1 — iterates HET slots, reads hash bits, combines with HET byte
        ReconstructNameHashes();
    }

    /// <summary>
    /// Phase 1: Reconstruct full name hashes for each file entry.
    /// For each occupied HET slot, read the BET index and hash bits from the BET hash array,
    /// then combine with the HET hash byte to form the full masked 64-bit name hash.
    /// RE: FUN_10013ee0 Phase 1: fullHash = betHashBits + (hetByte << (hashBitSize - 8))
    /// </summary>
    private void ReconstructNameHashes()
    {
        if (_hetNameHashes == null || _hetFileIndexData == null || _betHashData == null)
            return;

        int shiftAmount = (int)_hetHashBits - 8;

        for (uint slot = 0; slot < _hetTotalCount; slot++)
        {
            byte hetByte = _hetNameHashes[slot];
            if (hetByte == 0x00) continue;

            // Read BET index from HET file index array
            long indexBitPos = (long)slot * _hetIndexTotalBits;
            int betIndex = (int)ReadBits(_hetFileIndexData, indexBitPos, (int)_hetIndexBits);
            if (betIndex >= Entries.Count) continue;

            // Read hash bits from BET hash array (indexed by BET entry index)
            long hashBitPos = (long)betIndex * _betHashSizeTotal;
            ulong hashBits = ReadBits(_betHashData, hashBitPos, (int)_betHashSize);

            // Reconstruct: hash = betHashBits + (hetByte << (hashBitSize - 8))
            ulong fullHash = hashBits + ((ulong)hetByte << shiftAmount);

            Entries[betIndex].NameHash = fullHash;
            Entries[betIndex].HetIndex = (int)slot;
        }
    }

    /// <summary>
    /// Try to load filenames from the internal (listfile).
    /// RE: FUN_10016d00 (SFileAddListFile) opens "(listfile)" from inside the archive,
    /// reads line-by-line via FUN_10016890, strips ~P suffixes, then associates names.
    /// </summary>
    private void LoadListFile()
    {
        FileNames.Clear();

        int idx = FindFileByName("(listfile)");
        if (idx >= 0 && Entries[idx].FileSize > 0)
        {
            try
            {
                var entry = Entries[idx];
                entry.FileName = "(listfile)"; // Set name before extract so encryption key works
                byte[] fileData = ExtractFile(entry);
                Logger.Info($"Found (listfile) at entry {idx} ({fileData.Length} bytes)");
                ParseFileNameList(Encoding.UTF8.GetString(fileData));
                return;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to parse (listfile): {ex.Message}");
            }
        }

        Logger.Info("No embedded file list found. Use LoadFileNames() to supply filenames externally.");
    }

    /// <summary>
    /// Load filenames from an external source (e.g. decoded iipsfiles.txt or data.txt).
    /// </summary>
    public void LoadFileNames(string fileNameList)
    {
        ParseFileNameList(fileNameList);
        Logger.Info($"Loaded {FileNames.Count} filenames from external source");
    }

    /// <summary>
    /// Load filenames from a file on disk.
    /// </summary>
    public void LoadFileNamesFromFile(string path)
    {
        LoadFileNames(File.ReadAllText(path));
    }

    /// <summary>
    /// Parse file name list text content.
    /// RE: FUN_10016890 — reads lines, strips ~P patch suffixes.
    /// Names are associated with entries by hashing each name and looking up in HET.
    /// </summary>
    private void ParseFileNameList(string content)
    {
        FileNames.Clear();
        string[] lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in lines)
        {
            string name = line.Trim();
            if (string.IsNullOrEmpty(name)) continue;

            // Strip ~P patch marker (RE: FUN_10016890)
            int tildeIdx = name.IndexOf('~');
            if (tildeIdx >= 0)
            {
                name = name.Substring(0, tildeIdx);
                if (string.IsNullOrEmpty(name)) continue;
            }

            FileNames.Add(name);

            // Associate with entry by hash lookup
            int idx = FindFileByName(name);
            if (idx >= 0 && idx < Entries.Count)
            {
                Entries[idx].FileName = name;
            }
        }
    }

    /// <summary>
    /// Extract file data for a given entry.
    /// RE: Sector-based files (no SINGLE_UNIT flag 0x1000000):
    ///   - Sector offset table at start (encrypted with key-1)
    ///   - Each sector encrypted with key+sectorIdx, then compressed
    /// Single-unit files (flag 0x1000000): one blob, encrypt then compress.
    /// </summary>
    public byte[] ExtractFile(IIPSFileEntry entry)
    {
        if (_archiveStream == null || entry.FileSize == 0)
            return Array.Empty<byte>();

        if ((long)entry.FileOffset >= _archiveSize)
        {
            Logger.Error($"File offset 0x{entry.FileOffset:X} out of range for entry {entry.Index}");
            return Array.Empty<byte>();
        }

        bool encrypted = (entry.Flags & 0x10000) != 0;
        bool compressed = (entry.Flags & 0x200) != 0;
        bool singleUnit = (entry.Flags & 0x1000000) != 0;
        uint fileKey = encrypted ? ComputeFileKey(entry) : 0;
        uint sectorSize = 0x200u << (SectorSizeShift & 0x1F);

        // Single-unit: entire file in one block
        if (singleUnit || entry.CompressedSize == 0 || entry.CompressedSize == entry.FileSize)
        {
            ulong readSize = (entry.CompressedSize > 0 && entry.CompressedSize < entry.FileSize)
                ? entry.CompressedSize
                : entry.FileSize;
            byte[] data = ReadBytesAt((long)entry.FileOffset, (int)readSize);
            if (encrypted)
                IIPSCrypto.MpqDecryptBlock(data, fileKey);
            if (compressed && entry.CompressedSize > 0 && entry.CompressedSize < entry.FileSize)
                return Decompress(data, (int)entry.FileSize);
            return data;
        }

        // Sector-based reading
        int numSectors = ((int)entry.FileSize + (int)sectorSize - 1) / (int)sectorSize;
        int offsetTableEntries = numSectors + 1;
        // Check for CRC table (flag 0x04000000)
        if ((entry.Flags & 0x04000000) != 0)
            offsetTableEntries += numSectors;

        // Read sector offset table
        byte[] offsetTableBytes = ReadBytesAt((long)entry.FileOffset, offsetTableEntries * 4);
        if (encrypted)
            IIPSCrypto.MpqDecryptBlock(offsetTableBytes, fileKey - 1);

        uint[] sectorOffsets = new uint[offsetTableEntries];
        for (int i = 0; i < offsetTableEntries; i++)
            sectorOffsets[i] = BitConverter.ToUInt32(offsetTableBytes, i * 4);

        // Validate: first offset should be the offset table size
        uint expectedFirst = (uint)(offsetTableEntries * 4);
        if (sectorOffsets[0] != expectedFirst)
            Logger.Info($"  Sector[0] = 0x{sectorOffsets[0]:X} (expected 0x{expectedFirst:X})");

        // Read and process each sector
        using var result = new MemoryStream();
        for (int s = 0; s < numSectors; s++)
        {
            uint sectorStart = sectorOffsets[s];
            uint sectorEnd = sectorOffsets[s + 1];
            int sectorCompSize = (int)(sectorEnd - sectorStart);
            int sectorRawSize = (int)Math.Min(sectorSize, entry.FileSize - (ulong)s * sectorSize);

            if (sectorCompSize <= 0 || sectorCompSize > _archiveSize)
            {
                Logger.Error($"Bad sector {s}: offset={sectorStart}-{sectorEnd}");
                break;
            }

            byte[] sectorData = ReadBytesAt((long)entry.FileOffset + sectorStart, sectorCompSize);

            if (encrypted)
                IIPSCrypto.MpqDecryptBlock(sectorData, fileKey + (uint)s);

            if (compressed && sectorCompSize < sectorRawSize)
            {
                byte[] decompressed = Decompress(sectorData, sectorRawSize);
                result.Write(decompressed, 0, decompressed.Length);
            }
            else
            {
                result.Write(sectorData, 0, sectorData.Length);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Compute the MPQ file encryption key for an entry.
    /// RE: FUN_100069f0 → FUN_10006440 (MPQ HashString with 0x300).
    /// If flag 0x20000 (FIX_KEY): key = (hash + fileOffset) ^ fileSize.
    /// </summary>
    private uint ComputeFileKey(IIPSFileEntry entry)
    {
        string name = entry.FileName;
        if (string.IsNullOrEmpty(name))
        {
            Logger.Error($"Cannot compute encryption key for unnamed entry {entry.Index}");
            return 0;
        }

        uint key = IIPSCrypto.ComputeFileKey(name);
        if ((entry.Flags & 0x20000) != 0)
        {
            key = (key + (uint)entry.FileOffset) ^ (uint)entry.FileSize;
        }

        return key;
    }


    /// <summary>
    /// Extract a file by name. Uses HET hash lookup, falls back to listfile name scan.
    /// </summary>
    public byte[] ExtractFile(string fileName)
    {
        // Try direct hash lookup first
        int idx = FindFileByName(fileName);
        if (idx >= 0) return ExtractFile(Entries[idx]);

        // Fall back to listfile name match
        for (int i = 0; i < Entries.Count; i++)
        {
            if (string.Equals(Entries[i].FileName, fileName, StringComparison.OrdinalIgnoreCase))
            {
                return ExtractFile(Entries[i]);
            }
        }

        throw new FileNotFoundException($"File not found in archive: {fileName}");
    }

    /// <summary>
    /// Extract all files to a directory using real filenames where available.
    /// </summary>
    public void ExtractAll(string outputDir)
    {
        Directory.CreateDirectory(outputDir);
        int extracted = 0;
        int skipped = 0;
        for (int i = 0; i < Entries.Count; i++)
        {
            var entry = Entries[i];
            if (entry.FileSize == 0 || !entry.Exists)
            {
                skipped++;
                continue;
            }

            try
            {
                byte[] data = ExtractFile(entry);
                string outPath;
                if (!string.IsNullOrEmpty(entry.FileName))
                {
                    outPath = Path.Combine(outputDir, entry.FileName.Replace('\\', Path.DirectorySeparatorChar));
                    string dir = Path.GetDirectoryName(outPath);
                    if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
                }
                else
                {
                    outPath = Path.Combine(outputDir, "_unnamed", $"{i:D6}_{entry.NameHash:X16}.bin");
                    Directory.CreateDirectory(Path.Combine(outputDir, "_unnamed"));
                }

                File.WriteAllBytes(outPath, data);
                extracted++;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to extract {i} ({entry.FileName ?? "unnamed"}): {ex.Message}");
                skipped++;
            }
        }

        Logger.Info($"Extracted {extracted}, skipped {skipped}");
    }

    /// <summary>
    /// Decompress file data.
    /// RE: FUN_1000e670 — first byte is compression type (bit field):
    ///   0x02 = zlib (FUN_1000e180)
    ///   0x10 = bzip2 (FUN_1000e2c0)
    ///   0x20 = custom (FUN_1000e020)
    ///   0x00 = raw (no compression bits set, skip type byte)
    /// If compressedSize == fileSize, data is raw (no type byte).
    /// </summary>
    private static byte[] Decompress(byte[] compressed, int expectedSize)
    {
        if (compressed.Length == 0) return Array.Empty<byte>();

        byte method = compressed[0];
        byte[] payload = new byte[compressed.Length - 1];
        Array.Copy(compressed, 1, payload, 0, payload.Length);

        // Type byte is a bit field — check individual compression flags
        if ((method & 0x02) != 0)
        {
            // 0x02 = zlib/deflate (most common in NIFS)
            return ZlibDecompress(payload, expectedSize);
        }

        if ((method & 0x10) != 0)
        {
            // 0x10 = bzip2 — try zlib as fallback
            Logger.Info($"Bzip2 compression (0x{method:X2}) not fully supported, trying zlib");
            return ZlibDecompress(payload, expectedSize);
        }

        if (method == 0x00)
        {
            // No compression flags — raw data (skip type byte)
            return payload;
        }

        // Unknown method — try zlib as fallback
        Logger.Info($"Unknown compression type 0x{method:X2}, trying zlib");
        return ZlibDecompress(payload, expectedSize);
    }

    private static byte[] ZlibDecompress(byte[] data, int expectedSize)
    {
        try
        {
            using var ms = new MemoryStream(data);
            // Skip 2-byte zlib header if present (CMF byte has CM=8 in low nibble)
            if (data.Length >= 2 && (data[0] & 0x0F) == 0x08)
            {
                ms.Position = 2;
            }

            using var deflate = new DeflateStream(ms, CompressionMode.Decompress);
            byte[] result = new byte[expectedSize];
            int total = 0;
            while (total < expectedSize)
            {
                int read = deflate.Read(result, total, expectedSize - total);
                if (read == 0) break;
                total += read;
            }

            return result;
        }
        catch (Exception ex)
        {
            Logger.Error($"Zlib decompress failed: {ex.Message}");
            return data;
        }
    }

    /// <summary>
    /// Read N bits from a byte array at a given bit offset (little-endian).
    /// RE: FUN_10012750 — reads from param_1 + 4 + byteOffset (we skip the 4-byte header,
    /// since the C# arrays don't have it).
    /// </summary>
    private static ulong ReadBits(byte[] data, long bitOffset, int bitCount)
    {
        if (bitCount == 0 || bitCount > 64) return 0;
        ulong result = 0;
        for (int i = 0; i < bitCount; i++)
        {
            long cur = bitOffset + i;
            int byteIdx = (int)(cur / 8);
            int bitIdx = (int)(cur % 8);
            if (byteIdx >= data.Length) break;
            if ((data[byteIdx] & (1 << bitIdx)) != 0)
            {
                result |= (1UL << i);
            }
        }

        return result;
    }

    /// <summary>
    /// Read N bits as a raw byte array (for MD5 hashes etc).
    /// </summary>
    private static byte[] ReadBitsAsBytes(byte[] data, long bitOffset, int bitCount)
    {
        int byteCount = (bitCount + 7) / 8;
        byte[] result = new byte[byteCount];
        for (int i = 0; i < bitCount; i++)
        {
            long cur = bitOffset + i;
            int srcByteIdx = (int)(cur / 8);
            int srcBitIdx = (int)(cur % 8);
            if (srcByteIdx >= data.Length) break;
            if ((data[srcByteIdx] & (1 << srcBitIdx)) != 0)
            {
                result[i / 8] |= (byte)(1 << (i % 8));
            }
        }

        return result;
    }
}

public class IIPSFileEntry
{
    public int Index { get; set; }
    public ulong FileOffset { get; set; }
    public ulong FileSize { get; set; }
    public ulong CompressedSize { get; set; }
    public uint Flags { get; set; }
    public ulong NameHash { get; set; }
    public int HetIndex { get; set; } = -1;
    public string FileName { get; set; }
    public byte[] Md5 { get; set; }
    public ulong Extra { get; set; }

    /// <summary>RE: Flag bit 31 (0x80000000) = file exists.</summary>
    public bool Exists => (Flags & 0x80000000) != 0;

    /// <summary>RE: Flag bit 9 (0x200) = compressed.</summary>
    public bool IsCompressed => (Flags & 0x200) != 0 && CompressedSize != 0 && CompressedSize != FileSize;

    /// <summary>RE: Flag bit 27 (0x08000000) = directory entry.</summary>
    public bool IsDirectory => (Flags & 0x08000000) != 0;

    /// <summary>RE: Flag bit 24 (0x01000000) = single unit (not sector-based).</summary>
    public bool IsSingleUnit => (Flags & 0x01000000) != 0;
}