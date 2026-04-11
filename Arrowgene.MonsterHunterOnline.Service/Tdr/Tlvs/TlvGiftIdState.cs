using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for gift ID with state.
    /// C++ Reader: crygame.dll+sub_1024AED0 (UnkTlv0296)
    /// C++ Printer: crygame.dll+sub_1024B1A0
    /// </summary>
    public class TlvGiftIdState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Gift ID.
        /// Field ID: 1
        /// </summary>
        public int GiftId { get; set; }

        /// <summary>
        /// Gift state.
        /// Field ID: 2
        /// </summary>
        public byte GiftState { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: GiftId = reader.ReadInt(); break;
                    case 2: GiftState = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, GiftId);
            WriteTlvByte(writer, 2, GiftState);
        }
    }
}
