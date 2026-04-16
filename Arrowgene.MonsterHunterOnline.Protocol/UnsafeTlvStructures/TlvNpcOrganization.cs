using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for NPC organization.
    /// C++ Reader: crygame.dll+sub_102296A0 (UnkTlv0263)
    /// C++ Printer: crygame.dll+sub_102298F0
    /// </summary>
    public class TlvNpcOrganization : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, NpcOrgId);
            WriteTlvInt32(buffer, 2, NpcOrgValue);
            WriteTlvInt32(buffer, 3, NpcOrgStage);
        }
    }
}
