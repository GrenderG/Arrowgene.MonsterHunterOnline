using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for ID with value pair.
    /// C++ Reader: crygame.dll+sub_1019E830 (UnkTlv0175)
    /// C++ Printer: crygame.dll+sub_1019EAC0
    /// </summary>
    public class TlvIdValuePair : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Value.
        /// Field ID: 2
        /// </summary>
        public int Value { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadInt(); break;
                    case 2: Value = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, Value);
        }
    }
}
