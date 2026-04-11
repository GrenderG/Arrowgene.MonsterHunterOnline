using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for full shop data container.
    /// C++ Reader: crygame.dll+sub_10211300 (UnkTlv0228)
    /// C++ Printer: crygame.dll+sub_102121E0
    /// </summary>
    public class TlvShopDataContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // shopCount, derived
                    case 2: Shops = ReadTlvList<TlvCommodityRefreshReset>(reader); break;
                    case 3: DayBuyItemLimitData = ReadTlvObject<TlvShopBuyLimitData>(reader); break;
                    case 4: WeekBuyItemLimitData = ReadTlvObject<TlvShopBuyLimitData>(reader); break;
                    case 5: MonthBuyItemLimitData = ReadTlvObject<TlvShopBuyLimitData>(reader); break;
                    case 6: ForeverBuyLimitData = ReadTlvObject<TlvShopBuyLimitData>(reader); break;
                    case 7: reader.ReadInt(); break; // groupCount, derived
                    case 8: Groups = ReadTlvList<TlvGroupSaleRefresh>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Shops?.Count ?? 0) > MaxShops)
                throw new InvalidDataException($"[TlvShopDataContainer] Shops exceeds the maximum of {MaxShops} elements.");
            if ((Groups?.Count ?? 0) > MaxGroups)
                throw new InvalidDataException($"[TlvShopDataContainer] Groups exceeds the maximum of {MaxGroups} elements.");

            WriteTlvInt(writer, 1, ShopCount);
            WriteTlvList(writer, 2, Shops);
            WriteTlvObject(writer, 3, DayBuyItemLimitData);
            WriteTlvObject(writer, 4, WeekBuyItemLimitData);
            WriteTlvObject(writer, 5, MonthBuyItemLimitData);
            WriteTlvObject(writer, 6, ForeverBuyLimitData);
            WriteTlvInt(writer, 7, GroupCount);
            WriteTlvList(writer, 8, Groups);
        }
    }
}
