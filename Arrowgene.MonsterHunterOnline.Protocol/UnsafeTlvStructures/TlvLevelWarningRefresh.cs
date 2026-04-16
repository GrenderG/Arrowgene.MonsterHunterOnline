using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for level warning refresh data.
    /// C++ Reader: crygame.dll+sub_1014A730 (UnkTlv0075)
    /// C++ Printer: crygame.dll+sub_1014B090
    /// </summary>
    public class TlvLevelWarningRefresh : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxWarnings = 10;

        /// <summary>
        /// Last refresh time.
        /// Field ID: 1
        /// </summary>
        public uint LastRefreshTm { get; set; }

        /// <summary>
        /// Reward count.
        /// Field ID: 2
        /// </summary>
        public byte RewardCnt { get; set; }

        /// <summary>
        /// Level count (derived from WarningData).
        /// Field ID: 3
        /// </summary>
        public byte LevelCnt => (byte)(WarningData?.Count ?? 0);

        /// <summary>
        /// Warning data list.
        /// Field ID: 4
        /// </summary>
        public List<TlvLevelWarning> WarningData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((WarningData?.Count ?? 0) > MaxWarnings)
                throw new InvalidDataException($"[TlvLevelWarningRefresh] WarningData exceeds the maximum of {MaxWarnings} elements.");

            WriteTlvInt32(buffer, 1, (int)LastRefreshTm);
            WriteTlvByte(buffer, 2, RewardCnt);
            WriteTlvByte(buffer, 3, LevelCnt);
            WriteTlvSubStructureList(buffer, 4, WarningData.Count, WarningData);
        }
    }
}
