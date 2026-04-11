using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for ID with state byte.
    /// C++ Reader: crygame.dll+sub_10124930 (UnkTlv0028)
    /// C++ Printer: crygame.dll+sub_10124A80
    /// </summary>
    public class TlvIdState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// ID value (short).
        /// Field ID: 1
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        /// State byte value.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadShort(); break;
                    case 2: State = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, Id);
            WriteTlvByte(writer, 2, State);
        }
    }
}
