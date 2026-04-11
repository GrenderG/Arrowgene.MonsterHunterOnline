using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for NPC organization.
    /// C++ Reader: crygame.dll+sub_102296A0 (UnkTlv0263)
    /// C++ Printer: crygame.dll+sub_102298F0
    /// </summary>
    public class TlvNpcOrganization : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// NPC organization ID.
        /// Field ID: 1
        /// </summary>
        public int NpcOrgId { get; set; }

        /// <summary>
        /// NPC organization value.
        /// Field ID: 2
        /// </summary>
        public int NpcOrgValue { get; set; }

        /// <summary>
        /// NPC organization stage.
        /// Field ID: 3
        /// </summary>
        public int NpcOrgStage { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: NpcOrgId = reader.ReadInt(); break;
                    case 2: NpcOrgValue = reader.ReadInt(); break;
                    case 3: NpcOrgStage = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, NpcOrgId);
            WriteTlvInt(writer, 2, NpcOrgValue);
            WriteTlvInt(writer, 3, NpcOrgStage);
        }
    }
}
