using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvAuctionRecord.
    /// C++ Reader: crygame.dll+sub_1023A7F0 (UnkTlv0283)
    /// </summary>
    public class TlvAuctionRecords : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Records).
        /// Field ID: 1
        /// </summary>
        public int Count => Records?.Count ?? 0;

        /// <summary>
        /// List of TlvAuctionRecord.
        /// Field ID: 2
        /// </summary>
        public List<TlvAuctionRecord> Records { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Records.Count, Records);
        }
    }
}
