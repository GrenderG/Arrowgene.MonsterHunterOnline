using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task with time.
    /// C++ Reader: crygame.dll+sub_10220BC0 (UnkTlv0250)
    /// C++ Printer: crygame.dll+sub_10220CA0
    /// </summary>
    public class TlvTaskTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Task identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 2
        /// </summary>
        public uint Time { get; set; }

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
                    case 2: Time = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, Task);
            WriteTlvInt(writer, 2, (int)Time);
        }
    }
}
