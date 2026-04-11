using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task + count (variant B).
    /// C++ Reader: crygame.dll+sub_1021FBB0 (UnkTlv0248)
    /// C++ Printer: crygame.dll+sub_1021FFA0
    /// </summary>
    public class TlvTaskCountB : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Task ID.
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// Count.
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
