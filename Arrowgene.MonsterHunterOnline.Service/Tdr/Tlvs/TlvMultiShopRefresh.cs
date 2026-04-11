using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for multi-shop refresh data with day/week/month times.
    /// C++ Reader: crygame.dll+sub_10213930 (UnkTlv0231)
    /// C++ Printer: crygame.dll+sub_10214140
    /// </summary>
    public class TlvMultiShopRefresh : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // count, derived
                    case 2: Shops = ReadTlvList<TlvCommodityRefresh>(reader); break;
                    case 3: RefreshTimeD = (uint)reader.ReadInt(); break;
                    case 4: RefreshTimeW = (uint)reader.ReadInt(); break;
                    case 5: RefreshTimeM = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Shops?.Count ?? 0) > MaxShops)
                throw new InvalidDataException($"[TlvMultiShopRefresh] Shops exceeds the maximum of {MaxShops} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Shops);
            WriteTlvInt(writer, 3, (int)RefreshTimeD);
            WriteTlvInt(writer, 4, (int)RefreshTimeW);
            WriteTlvInt(writer, 5, (int)RefreshTimeM);
        }
    }
}
