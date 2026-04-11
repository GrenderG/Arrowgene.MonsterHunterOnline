using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Nested inside TlvSkillWeaponItem).
    /// C++ Writer: Inlined within crygame.dll+sub_101EF630
    /// C++ Printer: Inlined within crygame.dll+sub_101F0B50
    /// </summary>
    public class TlvSkillLearnIdItem : TlvStructure
    {
        public int Id { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                if ((tag >> 4) == 1) Id = reader.ReadVarInt();
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvVarInt(writer, 1, Id);
        }
    }
}