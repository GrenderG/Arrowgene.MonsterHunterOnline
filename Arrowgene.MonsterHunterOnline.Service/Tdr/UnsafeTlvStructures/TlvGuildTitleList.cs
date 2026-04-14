using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for guild title list.
    /// C++ Reader: crygame.dll+sub_1011F020 (UnkTlv0022)
    /// C++ Printer: crygame.dll+sub_1011F430
    /// </summary>
    public class TlvGuildTitleList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxTitles = 16;

        /// <summary>
        /// Title count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Titles?.Count ?? 0;

        /// <summary>
        /// Title entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvGuildTitleData> Titles { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Titles?.Count ?? 0) > MaxTitles)
                throw new InvalidDataException($"[TlvGuildTitleList] Titles exceeds the maximum of {MaxTitles} elements.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Titles.Count, Titles);
        }
    }
}
