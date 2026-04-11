using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for typed variant value (wType + stValue).
    /// C++ Reader: crygame.dll+sub_101AECC0 (UnkTlv0188)
    /// C++ Printer: crygame.dll+sub_101AEEB0
    /// </summary>
    public class TlvTypedVariant : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Variant wire type (1=int, 2=float, 4=bool/short, 6=ulong).
        /// Field ID: 1
        /// </summary>
        public byte WType { get; set; }

        /// <summary>
        /// Variant value.
        /// Field ID: 2
        /// </summary>
        public TlvVariantArgs StValue { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: WType = reader.ReadByte(); break;
                    case 2: StValue = ReadTlvObject<TlvVariantArgs>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, WType);
            WriteTlvObject(writer, 2, StValue);
        }
    }
}
