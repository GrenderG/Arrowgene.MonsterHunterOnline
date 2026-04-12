using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvGiftIdState.
    /// C++ Reader: crygame.dll+sub_1024B730 (UnkTlv0297)
    /// </summary>
    public class TlvGiftList : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from GiftList).
        /// Field ID: 1
        /// </summary>
        public int Count => GiftList?.Count ?? 0;

        /// <summary>
        /// List of TlvGiftIdState.
        /// Field ID: 2
        /// </summary>
        public List<TlvGiftIdState> GiftList { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, GiftList.Count, GiftList);
        }
    }
}
