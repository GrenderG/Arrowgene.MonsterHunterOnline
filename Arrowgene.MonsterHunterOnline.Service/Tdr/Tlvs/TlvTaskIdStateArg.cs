using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task ID with state and argument.
    /// C++ Reader: crygame.dll+sub_1021F370 (UnkTlv0247)
    /// C++ Printer: crygame.dll+sub_1021F7C0
    /// </summary>
    public class TlvTaskIdStateArg : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Task identifier.
        /// Field ID: 1
        /// </summary>
        public int Task { get; set; }

        /// <summary>
        /// Task sub-ID.
        /// Field ID: 2
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// State.
        /// Field ID: 3
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Argument 1.
        /// Field ID: 4
        /// </summary>
        public int Arg1 { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Task = reader.ReadInt(); break;
                    case 2: Id = reader.ReadInt(); break;
                    case 3: State = reader.ReadInt(); break;
                    case 4: Arg1 = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Task);
            WriteTlvInt(writer, 2, Id);
            WriteTlvInt(writer, 3, State);
            WriteTlvInt(writer, 4, Arg1);
        }
    }
}
