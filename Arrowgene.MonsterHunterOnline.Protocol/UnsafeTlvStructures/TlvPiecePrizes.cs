using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvIdStateByte.
    /// C++ Reader: crygame.dll+sub_1022D930 (UnkTlv0269)
    /// </summary>
    public class TlvPiecePrizes : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from PiecePrizes).
        /// Field ID: 1
        /// </summary>
        public int Count => PiecePrizes?.Count ?? 0;

        /// <summary>
        /// List of TlvIdStateByte.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdStateByte> PiecePrizes { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, PiecePrizes.Count, PiecePrizes);
        }
    }
}
