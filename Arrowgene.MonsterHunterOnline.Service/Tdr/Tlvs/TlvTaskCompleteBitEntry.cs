using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task complete bit entry (id + state + 2 byte args).
    /// C++ Reader: crygame.dll+sub_1021F440 (UnkTlv0257 internal)
    /// </summary>
    public class TlvTaskCompleteBitEntry : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 1</summary>
        public short Id { get; set; }

        /// <summary>Field ID: 2</summary>
        public byte State { get; set; }

        /// <summary>Field ID: 3</summary>
        public byte Arg { get; set; }

        /// <summary>Field ID: 4</summary>
        public byte Arg2 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadVarShort(); break;
                    case 2: State = reader.ReadByte(); break;
                    case 3: Arg = reader.ReadByte(); break;
                    case 4: Arg2 = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvVarShort(writer, 1, Id);
            WriteTlvByte(writer, 2, State);
            WriteTlvByte(writer, 3, Arg);
            WriteTlvByte(writer, 4, Arg2);
        }
    }
}
