using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for pet option data (variant without region).
    /// C++ Reader: crygame.dll+sub_10242870 (UnkTlv0287)
    /// C++ Printer: crygame.dll+sub_10243900
    /// </summary>
    public class TlvPetOptionDataB : Structure, ITlvStructure
    {
        /// <summary>Field ID: 4</summary>
        public TlvTypedBaseOrBonus OPetName { get; set; } = new();

        /// <summary>Field ID: 5</summary>
        public TlvTypedBaseOrBonus OPetSex { get; set; } = new();

        /// <summary>Field ID: 7</summary>
        public TlvTypedBaseOrBonus OOwner { get; set; } = new();

        /// <summary>Field ID: 8</summary>
        public TlvTypedBaseOrBonus OPetSignature { get; set; } = new();

        /// <summary>Field ID: 10</summary>
        public TlvTypedBaseOrBonus OPetLevel { get; set; } = new();

        /// <summary>Field ID: 85</summary>
        public TlvTypedBaseOrBonus OPetWeaponID { get; set; } = new();

        /// <summary>Field ID: 86</summary>
        public TlvTypedBaseOrBonus OPetHatID { get; set; } = new();

        /// <summary>Field ID: 87</summary>
        public TlvTypedBaseOrBonus OPetBodyID { get; set; } = new();

        /// <summary>Field ID: 88</summary>
        public TlvTypedBaseOrBonus OFashionWeaponID { get; set; } = new();

        /// <summary>Field ID: 89</summary>
        public TlvTypedBaseOrBonus OFashionHatID { get; set; } = new();

        /// <summary>Field ID: 90</summary>
        public TlvTypedBaseOrBonus OFashionBodyID { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 4, OPetName);
            WriteTlvSubStructure(buffer, 5, OPetSex);
            WriteTlvSubStructure(buffer, 7, OOwner);
            WriteTlvSubStructure(buffer, 8, OPetSignature);
            WriteTlvSubStructure(buffer, 10, OPetLevel);
            WriteTlvSubStructure(buffer, 85, OPetWeaponID);
            WriteTlvSubStructure(buffer, 86, OPetHatID);
            WriteTlvSubStructure(buffer, 87, OPetBodyID);
            WriteTlvSubStructure(buffer, 88, OFashionWeaponID);
            WriteTlvSubStructure(buffer, 89, OFashionHatID);
            WriteTlvSubStructure(buffer, 90, OFashionBodyID);
        }
    }
}
