using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for variant arguments (tagged union).
    /// Only one field is set at a time based on the tag type.
    /// C++ Reader: crygame.dll+sub_101AE410 (UnkTlv0187)
    /// C++ Printer: crygame.dll+sub_101AE8A0
    /// </summary>
    public class TlvVariantArgs : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Active variant tag (1=int, 2=float, 4=short, 6=long).
        /// </summary>
        public int TypeTag { get; set; }

        /// <summary>
        /// Integer value.
        /// Field ID: 1
        /// </summary>
        public int IntValue { get; set; }

        /// <summary>
        /// Float value (stored as int bits).
        /// Field ID: 2
        /// </summary>
        public float FloatValue { get; set; }

        /// <summary>
        /// Boolean/short value.
        /// Field ID: 4
        /// </summary>
        public short BoolValue { get; set; }

        /// <summary>
        /// UInt64 value.
        /// Field ID: 6
        /// </summary>
        public ulong UInt64Value { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: TypeTag = 1; IntValue = reader.ReadInt(); break;
                    case 2: TypeTag = 2; FloatValue = reader.ReadFloat(); break;
                    case 4: TypeTag = 4; BoolValue = reader.ReadShort(); break;
                    case 6: TypeTag = 6; UInt64Value = (ulong)reader.ReadLong(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            switch (TypeTag)
            {
                case 1: WriteTlvInt(writer, 1, IntValue); break;
                case 2: WriteTlvFloat(writer, 2, FloatValue); break;
                case 4: WriteTlvShort(writer, 4, BoolValue); break;
                case 6: WriteTlvULong(writer, 6, UInt64Value); break;
            }
        }
    }
}
