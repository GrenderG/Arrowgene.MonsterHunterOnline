using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101ECF70
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101ED330
    /// </summary>
    public class TlvSkillItem : TlvStructure
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public byte Level { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                switch (tag >> 4)
                {
                    case 1: Id = reader.ReadVarInt(); break;
                    case 2: Value = reader.ReadVarInt(); break;
                    case 3: Level = reader.ReadByte(); break;
                    default: SkipTlvField(reader, tag & 0xF); break;
                }
            }
        }
        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvVarInt(writer, 1, Id);
            WriteTlvVarInt(writer, 2, Value);
            WriteTlvByte(writer, 3, Level);
        }
    }
}
