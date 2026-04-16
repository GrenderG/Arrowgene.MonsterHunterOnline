using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvAuctionSaleRecord.
    /// C++ Reader: crygame.dll+sub_10236960 (UnkTlv0280)
    /// </summary>
    public class TlvAuctionSaleRecords : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Records).
        /// Field ID: 1
        /// </summary>
        public int Count => Records?.Count ?? 0;

        /// <summary>
        /// List of TlvAuctionSaleRecord.
        /// Field ID: 2
        /// </summary>
        public List<TlvAuctionSaleRecord> Records { get; set; }

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
