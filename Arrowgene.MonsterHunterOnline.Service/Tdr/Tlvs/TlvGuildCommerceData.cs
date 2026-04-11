using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild commerce data.
    /// C++ Reader: crygame.dll+sub_1012FC60 (UnkTlv0043)
    /// C++ Printer: crygame.dll+sub_10130510
    /// </summary>
    public class TlvGuildCommerceData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxCommerce = 10;
        public const int MaxBuffs = 5;

        /// <summary>Commerce count (derived). Field ID: 1</summary>
        public int CommerceCount => CommerceInfo?.Count ?? 0;

        /// <summary>Commerce info entries. Field ID: 2</summary>
        public List<TlvGoodsItem> CommerceInfo { get; set; }

        /// <summary>Selected commerce ID. Field ID: 3</summary>
        public int SelectCommerceId { get; set; }

        /// <summary>Guild war history info. Field ID: 4</summary>
        public int GuildWarHistoryInfo { get; set; }

        /// <summary>Buff count (derived). Field ID: 5</summary>
        public int BuffCount => CommerceBuffInfo?.Count ?? 0;

        /// <summary>Commerce buff info entries. Field ID: 6</summary>
        public List<TlvCommerceTimeout> CommerceBuffInfo { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: CommerceInfo = ReadTlvList<TlvGoodsItem>(reader); break;
                    case 3: SelectCommerceId = reader.ReadInt(); break;
                    case 4: GuildWarHistoryInfo = reader.ReadInt(); break;
                    case 5: reader.ReadInt(); break;
                    case 6: CommerceBuffInfo = ReadTlvList<TlvCommerceTimeout>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((CommerceInfo?.Count ?? 0) > MaxCommerce)
                throw new InvalidDataException($"[TlvGuildCommerceData] CommerceInfo exceeds {MaxCommerce}.");
            if ((CommerceBuffInfo?.Count ?? 0) > MaxBuffs)
                throw new InvalidDataException($"[TlvGuildCommerceData] CommerceBuffInfo exceeds {MaxBuffs}.");

            WriteTlvInt(writer, 1, CommerceCount);
            WriteTlvList(writer, 2, CommerceInfo);
            WriteTlvInt(writer, 3, SelectCommerceId);
            WriteTlvInt(writer, 4, GuildWarHistoryInfo);
            WriteTlvInt(writer, 5, BuffCount);
            WriteTlvList(writer, 6, CommerceBuffInfo);
        }
    }
}
