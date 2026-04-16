using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for lottery box item pool data.
    /// C++ Reader: crygame.dll+sub_10191850 (UnkTlv0167)
    /// C++ Printer: crygame.dll+sub_10192230
    /// </summary>
    public class TlvLotteryBoxItemPool : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxItems = 8;

        /// <summary>
        /// Re-search count.
        /// Field ID: 1
        /// </summary>
        public int ReSearchCount { get; set; }

        /// <summary>
        /// Refresh count.
        /// Field ID: 2
        /// </summary>
        public int RefreshCount { get; set; }

        /// <summary>
        /// VIP refresh count.
        /// Field ID: 3
        /// </summary>
        public int VipRefreshCount { get; set; }

        /// <summary>
        /// Item pool list.
        /// Field ID: 4
        /// </summary>
        public List<TlvPositionItemQuality> ItemPoolList { get; set; }

        /// <summary>
        /// Box ID.
        /// Field ID: 5
        /// </summary>
        public int BoxId { get; set; }

        /// <summary>
        /// Last clock refresh time.
        /// Field ID: 6
        /// </summary>
        public int LastClockRefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemPoolList?.Count ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvLotteryBoxItemPool] ItemPoolList exceeds the maximum of {MaxItems} elements.");

            WriteTlvInt32(buffer, 1, ReSearchCount);
            WriteTlvInt32(buffer, 2, RefreshCount);
            WriteTlvInt32(buffer, 3, VipRefreshCount);
            WriteTlvSubStructureList(buffer, 4, ItemPoolList.Count, ItemPoolList);
            WriteTlvInt32(buffer, 5, BoxId);
            WriteTlvInt32(buffer, 6, LastClockRefreshTime);
        }
    }
}
