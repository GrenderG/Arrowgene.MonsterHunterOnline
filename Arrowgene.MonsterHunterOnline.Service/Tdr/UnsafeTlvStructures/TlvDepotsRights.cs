using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvDepotRights.
    /// C++ Reader: crygame.dll+sub_1011DE00 (UnkTlv0020)
    /// </summary>
    public class TlvDepotsRights : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from DepotsRights).
        /// Field ID: 1
        /// </summary>
        public int Count => DepotsRights?.Count ?? 0;

        /// <summary>
        /// List of TlvDepotRights.
        /// Field ID: 2
        /// </summary>
        public List<TlvDepotRights> DepotsRights { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, DepotsRights.Count, DepotsRights);
        }
    }
}
