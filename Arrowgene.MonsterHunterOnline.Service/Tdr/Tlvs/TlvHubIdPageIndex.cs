using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for hub ID with page index.
    /// C++ Reader: crygame.dll+sub_10143CB0 (UnkTlv0065)
    /// C++ Printer: crygame.dll+sub_10143D90
    /// </summary>
    public class TlvHubIdPageIndex : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Hub ID.
        /// Field ID: 1
        /// </summary>
        public byte HubId { get; set; }

        /// <summary>
        /// Page index.
        /// Field ID: 2
        /// </summary>
        public int PageIndex { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: HubId = reader.ReadByte(); break;
                    case 2: PageIndex = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, HubId);
            WriteTlvInt(writer, 2, PageIndex);
        }
    }
}
