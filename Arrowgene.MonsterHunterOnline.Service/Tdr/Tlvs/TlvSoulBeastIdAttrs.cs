using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for soul beast with ID and attributes.
    /// C++ Reader: crygame.dll+sub_1018A540 (UnkTlv0156)
    /// C++ Printer: crygame.dll+sub_1018A9D0
    /// </summary>
    public class TlvSoulBeastIdAttrs : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Soul beast identifier.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Soul beast attributes.
        /// Field ID: 2
        /// </summary>
        public TlvSoulBeastStats Attrs { get; set; } = new();

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
                    case 2: Attrs = ReadTlvObject<TlvSoulBeastStats>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvObject(writer, 2, Attrs);
        }
    }
}
