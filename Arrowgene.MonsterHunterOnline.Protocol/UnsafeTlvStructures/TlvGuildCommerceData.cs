using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for guild commerce data.
    /// C++ Reader: crygame.dll+sub_1012FC60 (UnkTlv0043)
    /// C++ Printer: crygame.dll+sub_10130510
    /// </summary>
    public class TlvGuildCommerceData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((CommerceInfo?.Count ?? 0) > MaxCommerce)
                throw new InvalidDataException($"[TlvGuildCommerceData] CommerceInfo exceeds {MaxCommerce}.");
            if ((CommerceBuffInfo?.Count ?? 0) > MaxBuffs)
                throw new InvalidDataException($"[TlvGuildCommerceData] CommerceBuffInfo exceeds {MaxBuffs}.");

            WriteTlvInt32(buffer, 1, CommerceCount);
            WriteTlvSubStructureList(buffer, 2, CommerceInfo.Count, CommerceInfo);
            WriteTlvInt32(buffer, 3, SelectCommerceId);
            WriteTlvInt32(buffer, 4, GuildWarHistoryInfo);
            WriteTlvInt32(buffer, 5, BuffCount);
            WriteTlvSubStructureList(buffer, 6, CommerceBuffInfo.Count, CommerceBuffInfo);
        }
    }
}
