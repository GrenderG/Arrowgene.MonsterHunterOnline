using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for auction record container.
    /// C++ Reader: crygame.dll+sub_10239120 (UnkTlv0282)
    /// C++ Printer: crygame.dll+sub_10239680
    /// </summary>
    public class TlvAuctionRecordContainer : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxSales = 100;
        public const int MaxRecords = 400;

        /// <summary>Sale count (derived). Field ID: 1</summary>
        public short SaleCount => (short)(Sales?.Count ?? 0);

        /// <summary>Active sale records. Field ID: 2</summary>
        public List<TlvAuctionRecord> Sales { get; set; }

        /// <summary>Record sale count (derived). Field ID: 3</summary>
        public short RecordSaleCount => (short)(RecordSale?.Count ?? 0);

        /// <summary>Historical sale records. Field ID: 4</summary>
        public List<TlvAuctionRecord> RecordSale { get; set; }

        /// <summary>Record buy count (derived). Field ID: 5</summary>
        public short RecordBuyCount => (short)(RecordBuy?.Count ?? 0);

        /// <summary>Historical buy records. Field ID: 6</summary>
        public List<TlvAuctionRecord> RecordBuy { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Sales?.Count ?? 0) > MaxSales) throw new InvalidDataException($"[TlvAuctionRecordContainer] Sales exceeds {MaxSales}.");
            if ((RecordSale?.Count ?? 0) > MaxRecords) throw new InvalidDataException($"[TlvAuctionRecordContainer] RecordSale exceeds {MaxRecords}.");
            if ((RecordBuy?.Count ?? 0) > MaxRecords) throw new InvalidDataException($"[TlvAuctionRecordContainer] RecordBuy exceeds {MaxRecords}.");

            WriteTlvInt16(buffer, 1, SaleCount);
            WriteTlvSubStructureList(buffer, 2, Sales.Count, Sales);
            WriteTlvInt16(buffer, 3, RecordSaleCount);
            WriteTlvSubStructureList(buffer, 4, RecordSale.Count, RecordSale);
            WriteTlvInt16(buffer, 5, RecordBuyCount);
            WriteTlvSubStructureList(buffer, 6, RecordBuy.Count, RecordBuy);
        }
    }
}
