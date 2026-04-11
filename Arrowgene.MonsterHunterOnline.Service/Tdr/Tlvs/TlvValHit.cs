using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for val/hit byte pair.
    /// C++ Reader: crygame.dll+sub_1022BF00 (UnkTlv0266)
    /// C++ Printer: crygame.dll+sub_1022C230
    /// </summary>
    public class TlvValHit : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Value byte.
        /// Field ID: 1
        /// </summary>
        public byte Val { get; set; }

        /// <summary>
        /// Hit byte.
        /// Field ID: 2
        /// </summary>
        public byte Hit { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Val = reader.ReadByte(); break;
                    case 2: Hit = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, Val);
            WriteTlvByte(writer, 2, Hit);
        }
    }
}
