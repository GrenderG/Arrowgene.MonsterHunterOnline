using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101EE780
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101EEBD0
    /// </summary>
    public class TlvTalentEquipItem : TlvStructure
    {
        public int Id { get; set; }
        public byte Idx { get; set; }

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
                        Idx = reader.ReadByte();
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvVarInt(writer, 1, Id);
            WriteTlvByte(writer, 2, Idx);
        }
    }
}