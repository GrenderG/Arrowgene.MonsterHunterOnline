using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvGroupTypeTime.
    /// C++ Reader: crygame.dll+sub_1016EE00 (UnkTlv0118)
    /// </summary>
    public class TlvGroupTypeTimeList : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Data).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// List of TlvGroupTypeTime.
        /// Field ID: 2
        /// </summary>
        public List<TlvGroupTypeTime> Data { get; set; }

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
