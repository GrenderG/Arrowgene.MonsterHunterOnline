using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for quality with finish time.
    /// C++ Reader: crygame.dll+sub_10209880 (UnkTlv0235)
    /// C++ Printer: crygame.dll+sub_10209960
    /// </summary>
    public class TlvQualityFinishTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Quality value.
        /// Field ID: 1
        /// </summary>
        public byte Quality { get; set; }

        /// <summary>
        /// Finish time.
        /// Field ID: 2
        /// </summary>
        public uint FinishTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Quality = reader.ReadByte(); break;
                    case 2: FinishTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, Quality);
            WriteTlvInt(writer, 2, (int)FinishTime);
        }
    }
}
