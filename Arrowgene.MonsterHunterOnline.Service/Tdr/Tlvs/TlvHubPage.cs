using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for hub page reference.
    /// C++ Reader: crygame.dll+sub_101436B0 (UnkTlv0064)
    /// C++ Printer: crygame.dll+sub_10143770
    /// </summary>
    public class TlvHubPage : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Hub identifier.
        /// Field ID: 1
        /// </summary>
        public int HubId { get; set; }

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
                    case 1: HubId = reader.ReadInt(); break;
                    case 2: PageIndex = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, HubId);
            WriteTlvInt(writer, 2, PageIndex);
        }
    }
}
