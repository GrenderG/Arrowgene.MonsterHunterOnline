using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for multiple lottery boxes with daily refresh.
    /// C++ Reader: crygame.dll+sub_10192890 (UnkTlv0168)
    /// C++ Printer: crygame.dll+sub_10192D70
    /// </summary>
    public class TlvLotteryBoxContainer : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxBoxes = 3;

        /// <summary>
        /// Lottery boxes.
        /// Field ID: 1
        /// </summary>
        public List<TlvLotteryBoxItemPool> LotteryBox { get; set; }

        /// <summary>
        /// Last daily refresh time.
        /// Field ID: 2
        /// </summary>
        public int LastDailyRefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((LotteryBox?.Count ?? 0) > MaxBoxes)
                throw new InvalidDataException($"[TlvLotteryBoxContainer] LotteryBox exceeds the maximum of {MaxBoxes} elements.");

            WriteTlvSubStructureList(buffer, 1, LotteryBox.Count, LotteryBox);
            WriteTlvInt32(buffer, 2, LastDailyRefreshTime);
        }
    }
}
