using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvAuctionRecord.
    /// C++ Reader: crygame.dll+sub_1023A7F0 (UnkTlv0283)
    /// </summary>
    public class TlvAuctionRecords : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: Records = ReadTlvList<TlvAuctionRecord>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Records);
        }
    }
}
