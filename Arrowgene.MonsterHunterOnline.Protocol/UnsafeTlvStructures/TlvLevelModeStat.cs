using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for level mode data with byte and int arrays.
    /// C++ Reader: crygame.dll+sub_10147B50 (UnkTlv0071)
    /// C++ Printer: crygame.dll+sub_10148060
    /// </summary>
    public class TlvLevelModeStat : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxDataCount = 10;

        /// <summary>
        /// Level mode.
        /// Field ID: 1
        /// </summary>
        public int LevelMode { get; set; }

        /// <summary>
        /// Mode stat count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public byte ModeStatCnt => (byte)(ModeStatType?.Length ?? 0);

        /// <summary>
        /// Stat type bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] ModeStatType { get; set; }

        /// <summary>
        /// Stat values (int array).
        /// Field ID: 4
        /// </summary>
        public int[] ModeStatVal { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ModeStatType?.Length ?? 0) > MaxDataCount)
                throw new InvalidDataException($"[TlvLevelModeStat] ModeStatType exceeds the maximum of {MaxDataCount} elements.");
            if ((ModeStatVal?.Length ?? 0) > MaxDataCount)
                throw new InvalidDataException($"[TlvLevelModeStat] ModeStatVal exceeds the maximum of {MaxDataCount} elements.");

            WriteTlvInt32(buffer, 1, LevelMode);
            WriteTlvByte(buffer, 2, ModeStatCnt);
            WriteTlvByteArr(buffer, 3, ModeStatType);
            WriteTlvInt32Arr(buffer, 4, ModeStatVal);
        }
    }
}
