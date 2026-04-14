using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for full shop data container.
    /// C++ Reader: crygame.dll+sub_10211300 (UnkTlv0228)
    /// C++ Printer: crygame.dll+sub_102121E0
    /// </summary>
    public class TlvShopDataContainer : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxShops = 20;
        public const int MaxGroups = 128;

        /// <summary>
        /// Shop count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int ShopCount => Shops?.Count ?? 0;

        /// <summary>
        /// Shops.
        /// Field ID: 2
        /// </summary>
        public List<TlvCommodityRefreshReset> Shops { get; set; }

        /// <summary>
        /// Day buy item limit data.
        /// Field ID: 3
        /// </summary>
        public TlvShopBuyLimitData DayBuyItemLimitData { get; set; } = new();

        /// <summary>
        /// Week buy item limit data.
        /// Field ID: 4
        /// </summary>
        public TlvShopBuyLimitData WeekBuyItemLimitData { get; set; } = new();

        /// <summary>
        /// Month buy item limit data.
        /// Field ID: 5
        /// </summary>
        public TlvShopBuyLimitData MonthBuyItemLimitData { get; set; } = new();

        /// <summary>
        /// Forever buy limit data.
        /// Field ID: 6
        /// </summary>
        public TlvShopBuyLimitData ForeverBuyLimitData { get; set; } = new();

        /// <summary>
        /// Group count (derived from list).
        /// Field ID: 7
        /// </summary>
        public int GroupCount => Groups?.Count ?? 0;

        /// <summary>
        /// Groups.
        /// Field ID: 8
        /// </summary>
        public List<TlvGroupSaleRefresh> Groups { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Shops?.Count ?? 0) > MaxShops)
                throw new InvalidDataException($"[TlvShopDataContainer] Shops exceeds the maximum of {MaxShops} elements.");
            if ((Groups?.Count ?? 0) > MaxGroups)
                throw new InvalidDataException($"[TlvShopDataContainer] Groups exceeds the maximum of {MaxGroups} elements.");

            WriteTlvInt32(buffer, 1, ShopCount);
            WriteTlvSubStructureList(buffer, 2, Shops.Count, Shops);
            WriteTlvSubStructure(buffer, 3, DayBuyItemLimitData);
            WriteTlvSubStructure(buffer, 4, WeekBuyItemLimitData);
            WriteTlvSubStructure(buffer, 5, MonthBuyItemLimitData);
            WriteTlvSubStructure(buffer, 6, ForeverBuyLimitData);
            WriteTlvInt32(buffer, 7, GroupCount);
            WriteTlvSubStructureList(buffer, 8, Groups.Count, Groups);
        }
    }
}
