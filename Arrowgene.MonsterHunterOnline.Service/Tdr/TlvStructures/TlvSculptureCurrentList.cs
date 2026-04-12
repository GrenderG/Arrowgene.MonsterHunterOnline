using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture current list (count + entries).
    /// C++ Reader: crygame.dll+sub_1013AD20 (UnkTlv0057 internal)
    /// </summary>
    public class TlvSculptureCurrentList : Structure, ITlvStructure
    {
        public const int MaxEntries = 5;

        /// <summary>Count (derived). Field ID: 1</summary>
        public int Count => Entries?.Count ?? 0;

        /// <summary>Current entries. Field ID: 2</summary>
        public List<TlvSculptureCurrentEntry> Entries { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Entries?.Count ?? 0) > MaxEntries)
                throw new InvalidDataException($"[TlvSculptureCurrentList] Entries exceeds {MaxEntries}.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Entries.Count, Entries);
        }
    }
}
