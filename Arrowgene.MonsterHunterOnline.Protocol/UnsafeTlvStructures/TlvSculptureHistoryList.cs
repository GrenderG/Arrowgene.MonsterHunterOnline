using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture history list (count + entries).
    /// C++ Reader: crygame.dll+sub_10139980 (UnkTlv0057 internal)
    /// </summary>
    public class TlvSculptureHistoryList : Structure, ITlvStructure
    {
        public const int MaxEntries = 3;

        /// <summary>Count (derived). Field ID: 1</summary>
        public int Count => Entries?.Count ?? 0;

        /// <summary>History entries. Field ID: 2</summary>
        public List<TlvSculptureScoreEntry> Entries { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Entries?.Count ?? 0) > MaxEntries)
                throw new InvalidDataException($"[TlvSculptureHistoryList] Entries exceeds {MaxEntries}.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Entries.Count, Entries);
        }
    }
}
