using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvGiftIdState.
    /// C++ Reader: crygame.dll+sub_1024B730 (UnkTlv0297)
    /// </summary>
    public class TlvGiftList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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
                    case 2: GiftList = ReadTlvList<TlvGiftIdState>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, GiftList);
        }
    }
}
