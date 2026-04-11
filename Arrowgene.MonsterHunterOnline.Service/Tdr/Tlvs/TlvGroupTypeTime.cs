using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for group type with time.
    /// C++ Reader: crygame.dll+sub_1016E820 (UnkTlv0117)
    /// C++ Printer: crygame.dll+sub_1016E980
    /// </summary>
    public class TlvGroupTypeTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Group ID.
        /// Field ID: 1
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Type value.
        /// Field ID: 2
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 3
        /// </summary>
        public uint Time { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: GroupId = reader.ReadInt(); break;
                    case 2: Type = reader.ReadByte(); break;
                    case 3: Time = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, GroupId);
            WriteTlvByte(writer, 2, Type);
            WriteTlvInt(writer, 3, (int)Time);
        }
    }
}
