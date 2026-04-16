using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvValHit.
    /// C++ Reader: crygame.dll+sub_1022C7C0 (UnkTlv0267)
    /// </summary>
    public class TlvPieces : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Pieces).
        /// Field ID: 1
        /// </summary>
        public int Count => Pieces?.Count ?? 0;

        /// <summary>
        /// List of TlvValHit.
        /// Field ID: 2
        /// </summary>
        public List<TlvValHit> Pieces { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Pieces.Count, Pieces);
        }
    }
}
