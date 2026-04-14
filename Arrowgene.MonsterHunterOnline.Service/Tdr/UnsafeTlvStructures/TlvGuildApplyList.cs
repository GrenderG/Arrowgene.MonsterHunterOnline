using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for guild apply list.
    /// C++ Reader: crygame.dll+sub_10124190 (UnkTlv0027)
    /// C++ Printer: crygame.dll+sub_10124650
    /// </summary>
    public class TlvGuildApplyList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxApplys = 128;

        /// <summary>
        /// Apply count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Applys?.Count ?? 0;

        /// <summary>
        /// Apply entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvGuildMemberInfo> Applys { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Applys?.Count ?? 0) > MaxApplys)
                throw new InvalidDataException($"[TlvGuildApplyList] Applys exceeds the maximum of {MaxApplys} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Applys.Count, Applys);
        }
    }
}
