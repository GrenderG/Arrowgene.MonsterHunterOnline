using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvRoundScore.
    /// C++ Reader: crygame.dll+sub_10139740 (UnkTlv0050)
    /// </summary>
    public class TlvSculptureHistory : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Sculptures).
        /// Field ID: 1
        /// </summary>
        public int Count => Sculptures?.Count ?? 0;

        /// <summary>
        /// List of TlvRoundScore.
        /// Field ID: 2
        /// </summary>
        public List<TlvRoundScore> Sculptures { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Sculptures.Count, Sculptures);
        }
    }
}
