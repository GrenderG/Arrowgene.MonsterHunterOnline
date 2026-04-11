using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task state with arguments.
    /// C++ Reader: crygame.dll+sub_10125280 (UnkTlv0029)
    /// C++ Printer: crygame.dll+sub_101253C0
    /// </summary>
    public class TlvTaskState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Task identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// ID value.
        /// Field ID: 2
        /// </summary>
        public byte Id { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 3
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// Argument 1.
        /// Field ID: 4
        /// </summary>
        public byte Arg1 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Task = reader.ReadShort(); break;
                    case 2: Id = reader.ReadByte(); break;
                    case 3: State = reader.ReadByte(); break;
                    case 4: Arg1 = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, Task);
            WriteTlvByte(writer, 2, Id);
            WriteTlvByte(writer, 3, State);
            WriteTlvByte(writer, 4, Arg1);
        }
    }
}
