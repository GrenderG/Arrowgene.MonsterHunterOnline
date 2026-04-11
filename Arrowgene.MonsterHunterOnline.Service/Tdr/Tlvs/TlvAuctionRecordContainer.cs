using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for auction record container.
    /// C++ Reader: crygame.dll+sub_10239120 (UnkTlv0282)
    /// C++ Printer: crygame.dll+sub_10239680
    /// </summary>
    public class TlvAuctionRecordContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadShort(); break;
                    case 2: Sales = ReadTlvList<TlvAuctionRecord>(reader); break;
                    case 3: reader.ReadShort(); break;
                    case 4: RecordSale = ReadTlvList<TlvAuctionRecord>(reader); break;
                    case 5: reader.ReadShort(); break;
                    case 6: RecordBuy = ReadTlvList<TlvAuctionRecord>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Sales?.Count ?? 0) > MaxSales) throw new InvalidDataException($"[TlvAuctionRecordContainer] Sales exceeds {MaxSales}.");
            if ((RecordSale?.Count ?? 0) > MaxRecords) throw new InvalidDataException($"[TlvAuctionRecordContainer] RecordSale exceeds {MaxRecords}.");
            if ((RecordBuy?.Count ?? 0) > MaxRecords) throw new InvalidDataException($"[TlvAuctionRecordContainer] RecordBuy exceeds {MaxRecords}.");

            WriteTlvShort(writer, 1, SaleCount);
            WriteTlvList(writer, 2, Sales);
            WriteTlvShort(writer, 3, RecordSaleCount);
            WriteTlvList(writer, 4, RecordSale);
            WriteTlvShort(writer, 5, RecordBuyCount);
            WriteTlvList(writer, 6, RecordBuy);
        }
    }
}
