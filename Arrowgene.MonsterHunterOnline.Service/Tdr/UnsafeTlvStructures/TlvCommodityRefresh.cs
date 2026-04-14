using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for commodity refresh data.
    /// C++ Reader: crygame.dll+sub_10212D10 (UnkTlv0230)
    /// C++ Printer: crygame.dll+sub_10213570
    /// </summary>
    public class TlvCommodityRefresh : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxCommodities = 64;

        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Library ID.
        /// Field ID: 2
        /// </summary>
        public int Lib { get; set; }

        /// <summary>
        /// Commodity count (derived from list).
        /// Field ID: 3
        /// </summary>
        public short CommodityCount => (short)(Commodity?.Count ?? 0);

        /// <summary>
        /// Commodity sales data.
        /// Field ID: 4
        /// </summary>
        public List<TlvCommoditySalesShort> Commodity { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Commodity?.Count ?? 0) > MaxCommodities)
                throw new InvalidDataException($"[TlvCommodityRefresh] Commodity exceeds the maximum of {MaxCommodities} elements.");

            WriteTlvInt32(buffer, 1, (int)RefreshTime);
            WriteTlvInt32(buffer, 2, Lib);
            WriteTlvInt16(buffer, 3, CommodityCount);
            WriteTlvSubStructureList(buffer, 4, Commodity.Count, Commodity);
        }
    }
}
