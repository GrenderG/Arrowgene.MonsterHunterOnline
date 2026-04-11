using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for quality with finish time (VarUInt variant).
    /// C++ Reader: crygame.dll+sub_10216BA0 (inner of UnkTlv0236)
    /// C++ Printer: crygame.dll+sub_10216C90 (via DebugFormat)
    /// </summary>
    public class TlvQualityFinishTimeVar : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Quality value.
        /// Field ID: 1
        /// </summary>
        public byte Quality { get; set; }

        /// <summary>
        /// Finish time (stored as VarUInt).
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
                    case 2: FinishTime = reader.ReadVarUInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, Quality);
            writer.WriteVarUInt((2u << 4) | 0);
            writer.WriteVarUInt(FinishTime);
        }
    }
}
