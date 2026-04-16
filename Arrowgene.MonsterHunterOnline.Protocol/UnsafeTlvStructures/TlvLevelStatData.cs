using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for level stat data with byte and int arrays.
    /// C++ Reader: crygame.dll+sub_10145360 (UnkTlv0068)
    /// C++ Printer: crygame.dll+sub_10145870
    /// </summary>
    public class TlvLevelStatData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxStatCount = 10;

        /// <summary>
        /// Stat count (derived from arrays, stored as short).
        /// Field ID: 1
        /// </summary>
        public short LevelStatCnt => (short)(LevelStatType?.Length ?? 0);

        /// <summary>
        /// Stat type bytes.
        /// Field ID: 2
        /// </summary>
        public byte[] LevelStatType { get; set; }

        /// <summary>
        /// Stat values (int array).
        /// Field ID: 3
        /// </summary>
        public int[] LevelStatValue { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((LevelStatType?.Length ?? 0) > MaxStatCount)
                throw new InvalidDataException($"[TlvLevelStatData] LevelStatType exceeds the maximum of {MaxStatCount} elements.");
            if ((LevelStatValue?.Length ?? 0) > MaxStatCount)
                throw new InvalidDataException($"[TlvLevelStatData] LevelStatValue exceeds the maximum of {MaxStatCount} elements.");

            WriteTlvInt16(buffer, 1, LevelStatCnt);
            WriteTlvByteArr(buffer, 2, LevelStatType);
            WriteTlvInt32Arr(buffer, 3, LevelStatValue);
        }
    }
}
