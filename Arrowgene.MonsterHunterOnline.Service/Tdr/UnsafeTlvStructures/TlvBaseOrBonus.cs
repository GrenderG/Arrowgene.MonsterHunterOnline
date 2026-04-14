using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for a discriminated variant (base or bonus).
    /// Field 1 → single TlvTypedVariant (base).
    /// Field 2 → TlvTypedVariantList (bonus).
    /// C++ Reader: crygame.dll+sub_101AFE20 (UnkTlv0190)
    /// C++ Printer: crygame.dll+sub_101AFFD0
    /// </summary>
    public class TlvBaseOrBonus : Structure, ITlvStructure
    {
        /// <summary>
        /// Discriminator (1=base, 2=bonus).
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// Base variant (when Kind == 1).
        /// Field ID: 1
        /// </summary>
        public TlvTypedVariant Base { get; set; } = new();

        /// <summary>
        /// Bonus variant list (when Kind == 2).
        /// Field ID: 2
        /// </summary>
        public TlvTypedVariantList Bonus { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            switch (Kind)
            {
                case 1: WriteTlvSubStructure(buffer, 1, Base); break;
                case 2: WriteTlvSubStructure(buffer, 2, Bonus); break;
            }
        }
    }
}
