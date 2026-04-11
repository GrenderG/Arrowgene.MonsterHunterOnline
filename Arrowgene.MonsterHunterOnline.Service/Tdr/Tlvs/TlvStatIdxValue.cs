using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for stat index with value (short fields).
    /// C++ Reader: crygame.dll+sub_1021AF30 (UnkTlv0242)
    /// C++ Printer: crygame.dll+sub_1021B1C0
    /// </summary>
    public class TlvStatIdxValue : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Stat index.
        /// Field ID: 1
        /// </summary>
        public short StatIdx { get; set; }

        /// <summary>
        /// Stat value.
        /// Field ID: 2
        /// </summary>
        public uint StatValue { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: StatIdx = reader.ReadShort(); break;
                    case 2: StatValue = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, StatIdx);
            WriteTlvInt(writer, 2, (int)StatValue);
        }
    }
}
