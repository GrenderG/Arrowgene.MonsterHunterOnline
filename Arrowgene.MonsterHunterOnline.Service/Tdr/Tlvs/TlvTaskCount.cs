using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task count.
    /// C++ Reader: crygame.dll+sub_10125700 (UnkTlv0030)
    /// C++ Printer: crygame.dll+sub_101257E0
    /// </summary>
    public class TlvTaskCount : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Task identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// Count value.
        /// Field ID: 2
        /// </summary>
        public byte Count { get; set; }

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
                    case 2: Count = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, Task);
            WriteTlvByte(writer, 2, Count);
        }
    }
}
