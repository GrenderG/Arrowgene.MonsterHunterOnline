using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Nested inside TlvSkillWeaponItem).
    /// C++ Writer: Inlined within crygame.dll+sub_101EF630
    /// C++ Printer: Inlined within crygame.dll+sub_101F0B50
    /// </summary>
    public class TlvSkillLearnIdItem : Structure, ITlvStructure
    {
        public int Id { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
        }
    }
}