using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for commodity refresh data with reset times.
    /// C++ Reader: crygame.dll+sub_1020FBE0 (UnkTlv0226)
    /// C++ Printer: crygame.dll+sub_10210430
    /// </summary>
    public class TlvCommodityRefreshReset : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        /// <summary>
        /// Reset times.
        /// Field ID: 5
        /// </summary>
        public int ResetTimes { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RefreshTime = (uint)reader.ReadInt(); break;
                    case 2: Lib = reader.ReadInt(); break;
                    case 3: reader.ReadShort(); break; // commodityCount, derived
                    case 4: Commodity = ReadTlvList<TlvCommoditySalesShort>(reader); break;
                    case 5: ResetTimes = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Commodity?.Count ?? 0) > MaxCommodities)
                throw new InvalidDataException($"[TlvCommodityRefreshReset] Commodity exceeds the maximum of {MaxCommodities} elements.");

            WriteTlvInt(writer, 1, (int)RefreshTime);
            WriteTlvInt(writer, 2, Lib);
            WriteTlvShort(writer, 3, CommodityCount);
            WriteTlvList(writer, 4, Commodity);
            WriteTlvInt(writer, 5, ResetTimes);
        }
    }
}
