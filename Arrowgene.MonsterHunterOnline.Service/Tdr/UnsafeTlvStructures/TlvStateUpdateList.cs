using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvIdStateUpdate.
    /// C++ Reader: crygame.dll+sub_10244C20 (UnkTlv0289)
    /// </summary>
    public class TlvStateUpdateList : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from State).
        /// Field ID: 1
        /// </summary>
        public int Count => State?.Count ?? 0;

        /// <summary>
        /// List of TlvIdStateUpdate.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdStateUpdate> State { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, State.Count, State);
        }
    }
}
