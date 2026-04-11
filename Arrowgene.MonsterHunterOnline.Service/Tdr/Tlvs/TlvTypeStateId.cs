using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for type state with ID.
    /// C++ Reader: crygame.dll+sub_10177010 (UnkTlv0130)
    /// C++ Printer: crygame.dll+sub_10177100
    /// </summary>
    public class TlvTypeStateId : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Type value.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// ID value.
        /// Field ID: 3
        /// </summary>
        public int Id { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Type = reader.ReadByte(); break;
                    case 2: State = reader.ReadByte(); break;
                    case 3: Id = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, Type);
            WriteTlvByte(writer, 2, State);
            WriteTlvInt(writer, 3, Id);
        }
    }
}
