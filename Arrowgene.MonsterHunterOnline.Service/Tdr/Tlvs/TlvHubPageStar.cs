using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for hub page with star level.
    /// C++ Reader: crygame.dll+sub_101445D0 (UnkTlv0066)
    /// C++ Printer: crygame.dll+sub_101446E0
    /// </summary>
    public class TlvHubPageStar : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Hub identifier.
        /// Field ID: 1
        /// </summary>
        public byte HubId { get; set; }

        /// <summary>
        /// Page index.
        /// Field ID: 2
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Star level.
        /// Field ID: 3
        /// </summary>
        public int StarLv { get; set; }

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
                    case 3: StarLv = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, HubId);
            WriteTlvInt(writer, 2, PageIndex);
            WriteTlvInt(writer, 3, StarLv);
        }
    }
}
