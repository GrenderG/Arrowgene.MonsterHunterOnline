using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for multi-shop refresh data with day/week/month times.
    /// C++ Reader: crygame.dll+sub_10213930 (UnkTlv0231)
    /// C++ Printer: crygame.dll+sub_10214140
    /// </summary>
    public class TlvMultiShopRefresh : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxShops = 20;

        /// <summary>
        /// Shop count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Shops?.Count ?? 0;

        /// <summary>
        /// Shops.
        /// Field ID: 2
        /// </summary>
        public List<TlvCommodityRefresh> Shops { get; set; }

        /// <summary>
        /// Daily refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTimeD { get; set; }

        /// <summary>
        /// Weekly refresh time.
        /// Field ID: 4
        /// </summary>
        public uint RefreshTimeW { get; set; }

        /// <summary>
        /// Monthly refresh time.
        /// Field ID: 5
        /// </summary>
        public uint RefreshTimeM { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Shops?.Count ?? 0) > MaxShops)
                throw new InvalidDataException($"[TlvMultiShopRefresh] Shops exceeds the maximum of {MaxShops} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Shops.Count, Shops);
            WriteTlvInt32(buffer, 3, (int)RefreshTimeD);
            WriteTlvInt32(buffer, 4, (int)RefreshTimeW);
            WriteTlvInt32(buffer, 5, (int)RefreshTimeM);
        }
    }
}
