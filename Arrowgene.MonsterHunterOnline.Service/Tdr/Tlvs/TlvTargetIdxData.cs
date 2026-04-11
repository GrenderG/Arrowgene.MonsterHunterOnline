using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for target index with data.
    /// C++ Reader: crygame.dll+sub_10218090 (UnkTlv0237)
    /// C++ Printer: crygame.dll+sub_10218360
    /// </summary>
    public class TlvTargetIdxData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Target index.
        /// Field ID: 1
        /// </summary>
        public byte TargetIdx { get; set; }

        /// <summary>
        /// Target data.
        /// Field ID: 2
        /// </summary>
        public uint TargetData { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: TargetIdx = reader.ReadByte(); break;
                    case 2: TargetData = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, TargetIdx);
            WriteTlvInt(writer, 2, (int)TargetData);
        }
    }
}
