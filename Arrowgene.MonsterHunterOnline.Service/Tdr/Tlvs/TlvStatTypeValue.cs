using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for stat type with value.
    /// C++ Reader: crygame.dll+sub_10214CF0 (UnkTlv0232)
    /// C++ Printer: crygame.dll+sub_10214F80
    /// </summary>
    public class TlvStatTypeValue : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Stat type.
        /// Field ID: 1
        /// </summary>
        public int StatType { get; set; }

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
                    case 1: StatType = reader.ReadInt(); break;
                    case 2: StatValue = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, StatType);
            WriteTlvInt(writer, 2, (int)StatValue);
        }
    }
}
