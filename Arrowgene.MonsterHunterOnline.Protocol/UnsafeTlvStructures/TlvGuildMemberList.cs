using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for guild member list.
    /// C++ Reader: crygame.dll+sub_10122920 (UnkTlv0025)
    /// C++ Printer: crygame.dll+sub_10122B60
    /// </summary>
    public class TlvGuildMemberList : Structure, ITlvStructure
    {
        public const int MaxGuilders = 256;

        /// <summary>Count (derived). Field ID: 1</summary>
        public int Count => Guilders?.Count ?? 0;

        /// <summary>Guild member entries. Field ID: 2</summary>
        public List<TlvGuildMemberData> Guilders { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Guilders?.Count ?? 0) > MaxGuilders)
                throw new InvalidDataException($"[TlvGuildMemberList] Guilders exceeds {MaxGuilders}.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Guilders.Count, Guilders);
        }
    }
}
