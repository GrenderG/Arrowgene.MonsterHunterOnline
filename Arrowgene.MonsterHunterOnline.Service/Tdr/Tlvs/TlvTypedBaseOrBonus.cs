using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for typed base/bonus variant value.
    /// C++ Reader: crygame.dll+sub_101B0290 (UnkTlv0191)
    /// C++ Printer: crygame.dll+sub_101B0710
    /// </summary>
    public class TlvTypedBaseOrBonus : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Type.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Value (base or bonus variant).
        /// Field ID: 2
        /// </summary>
        public TlvBaseOrBonus Value { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Type = reader.ReadByte(); break;
                    case 2: Value = ReadTlvObject<TlvBaseOrBonus>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, Type);
            WriteTlvObject(writer, 2, Value);
        }
    }
}
