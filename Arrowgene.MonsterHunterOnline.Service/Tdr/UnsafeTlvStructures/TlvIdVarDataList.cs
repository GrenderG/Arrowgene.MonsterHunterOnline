using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvIdVarData.
    /// C++ Reader: crygame.dll+sub_10246090 (UnkTlv0291)
    /// </summary>
    public class TlvIdVarDataList : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Data).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// List of TlvIdVarData.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdVarData> Data { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Data.Count, Data);
        }
    }
}
