using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_1010D720
    /// C++ Printer: crygame.dll+sub_1010DA70
    /// </summary>
    public class TlvAttributeItem : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public byte AttrId { get; set; }
        public int AttrValue { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1:
                        AttrId = reader.ReadByte();
                        break;

                    case 2:
                        AttrValue = reader.ReadInt();
                        break;

                    default:
                        SkipTlvField(reader, wireType);
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, AttrId);
            WriteTlvInt(writer, 2, AttrValue);
        }
    }
}