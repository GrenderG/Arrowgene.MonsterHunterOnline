using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for group entrust stat data.
    /// C++ Reader: crygame.dll+sub_10145950 (UnkTlv0069)
    /// C++ Printer: crygame.dll+sub_10146230
    /// </summary>
    public class TlvGroupEntrustStatData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxGroupStats = 10;
        public const int MaxLevels = 64;

        /// <summary>
        /// Group ID.
        /// Field ID: 1
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// Group stat count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public short GroupStatCnt => (short)(GroupStatType?.Length ?? 0);

        /// <summary>
        /// Group stat types (byte array).
        /// Field ID: 3
        /// </summary>
        public byte[] GroupStatType { get; set; }

        /// <summary>
        /// Group stat values (int array).
        /// Field ID: 4
        /// </summary>
        public int[] GroupStatValue { get; set; }

        /// <summary>
        /// Group level count (derived from list).
        /// Field ID: 5
        /// </summary>
        public short GroupLevelCnt => (short)(EntrustLevelStat?.Count ?? 0);

        /// <summary>
        /// Entrust level stats.
        /// Field ID: 6
        /// </summary>
        public List<TlvLevelStatData> EntrustLevelStat { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((GroupStatType?.Length ?? 0) > MaxGroupStats)
                throw new InvalidDataException($"[TlvGroupEntrustStatData] GroupStatType exceeds the maximum of {MaxGroupStats} elements.");
            if ((EntrustLevelStat?.Count ?? 0) > MaxLevels)
                throw new InvalidDataException($"[TlvGroupEntrustStatData] EntrustLevelStat exceeds the maximum of {MaxLevels} elements.");

            WriteTlvInt32(buffer, 1, GroupID);
            WriteTlvInt16(buffer, 2, GroupStatCnt);
            WriteTlvByteArr(buffer, 3, GroupStatType);
            WriteTlvInt32Arr(buffer, 4, GroupStatValue);
            WriteTlvInt16(buffer, 5, GroupLevelCnt);
            WriteTlvSubStructureList(buffer, 6, EntrustLevelStat.Count, EntrustLevelStat);
        }
    }
}
