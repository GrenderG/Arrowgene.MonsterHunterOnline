using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101EE0C0
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101EE510
    /// </summary>
    public class TlvTalentLearnItem : TlvStructure
    {
        public int Id { get; set; }
        public byte Level { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                switch (tag >> 4)
                {
                    case 1:
                        Id = reader.ReadVarInt();
                        break;
                    case 2:
                        Level = reader.ReadByte();
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvVarInt(writer, 1, Id);
            WriteTlvByte(writer, 2, Level);
        }
    }
}