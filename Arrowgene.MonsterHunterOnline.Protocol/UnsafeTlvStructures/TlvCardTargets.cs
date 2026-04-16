using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvTargetIdxData.
    /// C++ Reader: crygame.dll+sub_102188C0 (UnkTlv0238)
    /// </summary>
    public class TlvCardTargets : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from TargetList).
        /// Field ID: 1
        /// </summary>
        public int Count => TargetList?.Count ?? 0;

        /// <summary>
        /// List of TlvTargetIdxData.
        /// Field ID: 2
        /// </summary>
        public List<TlvTargetIdxData> TargetList { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, TargetList.Count, TargetList);
        }
    }
}
