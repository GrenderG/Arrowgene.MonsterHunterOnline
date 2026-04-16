using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for full pet attribute data (54 typed base-or-bonus fields).
    /// C++ Reader: crygame.dll+sub_10240590 (UnkTlv0286)
    /// C++ Printer: crygame.dll+sub_10241DF0
    /// </summary>
    public class TlvPetFullAttrData : Structure, ITlvStructure
    {
        /// <summary>Field ID: 4</summary>
        public TlvTypedBaseOrBonus OPetName { get; set; } = new();
        /// <summary>Field ID: 5</summary>
        public TlvTypedBaseOrBonus OPetSex { get; set; } = new();
        /// <summary>Field ID: 7</summary>
        public TlvTypedBaseOrBonus OOwner { get; set; } = new();
        /// <summary>Field ID: 8</summary>
        public TlvTypedBaseOrBonus OPetSignature { get; set; } = new();
        /// <summary>Field ID: 9</summary>
        public TlvTypedBaseOrBonus OPetExp { get; set; } = new();
        /// <summary>Field ID: 10</summary>
        public TlvTypedBaseOrBonus OPetLevel { get; set; } = new();
        /// <summary>Field ID: 18</summary>
        public TlvTypedBaseOrBonus OSpirit { get; set; } = new();
        /// <summary>Field ID: 19</summary>
        public TlvTypedBaseOrBonus OVigour { get; set; } = new();
        /// <summary>Field ID: 20</summary>
        public TlvTypedBaseOrBonus OPetHP { get; set; } = new();
        /// <summary>Field ID: 21</summary>
        public TlvTypedBaseOrBonus OPetMaxHP { get; set; } = new();
        /// <summary>Field ID: 23</summary>
        public TlvTypedBaseOrBonus OPetMelee { get; set; } = new();
        /// <summary>Field ID: 24</summary>
        public TlvTypedBaseOrBonus OPetDefence { get; set; } = new();
        /// <summary>Field ID: 25</summary>
        public TlvTypedBaseOrBonus OCritLevel { get; set; } = new();
        /// <summary>Field ID: 30</summary>
        public TlvTypedBaseOrBonus OPetExecution { get; set; } = new();
        /// <summary>Field ID: 31</summary>
        public TlvTypedBaseOrBonus OPetObservation { get; set; } = new();
        /// <summary>Field ID: 32</summary>
        public TlvTypedBaseOrBonus OPetLoadBearing { get; set; } = new();
        /// <summary>Field ID: 34</summary>
        public TlvTypedBaseOrBonus OPetMaxSp { get; set; } = new();
        /// <summary>Field ID: 35</summary>
        public TlvTypedBaseOrBonus OPetSp { get; set; } = new();
        /// <summary>Field ID: 36</summary>
        public TlvTypedBaseOrBonus OPetPotential { get; set; } = new();
        /// <summary>Field ID: 42</summary>
        public TlvTypedBaseOrBonus OWaterAtk { get; set; } = new();
        /// <summary>Field ID: 43</summary>
        public TlvTypedBaseOrBonus OFireAtk { get; set; } = new();
        /// <summary>Field ID: 44</summary>
        public TlvTypedBaseOrBonus OLightningAtk { get; set; } = new();
        /// <summary>Field ID: 45</summary>
        public TlvTypedBaseOrBonus ODragonAtk { get; set; } = new();
        /// <summary>Field ID: 46</summary>
        public TlvTypedBaseOrBonus OIceAtk { get; set; } = new();
        /// <summary>Field ID: 47</summary>
        public TlvTypedBaseOrBonus OWaterRes { get; set; } = new();
        /// <summary>Field ID: 48</summary>
        public TlvTypedBaseOrBonus OFireRes { get; set; } = new();
        /// <summary>Field ID: 49</summary>
        public TlvTypedBaseOrBonus OLightningRes { get; set; } = new();
        /// <summary>Field ID: 50</summary>
        public TlvTypedBaseOrBonus ODragonRes { get; set; } = new();
        /// <summary>Field ID: 51</summary>
        public TlvTypedBaseOrBonus OIceRes { get; set; } = new();
        /// <summary>Field ID: 52</summary>
        public TlvTypedBaseOrBonus OWaterThrsh { get; set; } = new();
        /// <summary>Field ID: 53</summary>
        public TlvTypedBaseOrBonus OFireThrsh { get; set; } = new();
        /// <summary>Field ID: 54</summary>
        public TlvTypedBaseOrBonus OLightningThrsh { get; set; } = new();
        /// <summary>Field ID: 55</summary>
        public TlvTypedBaseOrBonus ODragonThrsh { get; set; } = new();
        /// <summary>Field ID: 56</summary>
        public TlvTypedBaseOrBonus OIceThrsh { get; set; } = new();
        /// <summary>Field ID: 67</summary>
        public TlvTypedBaseOrBonus OComaThrsh { get; set; } = new();
        /// <summary>Field ID: 68</summary>
        public TlvTypedBaseOrBonus OPoisonThrsh { get; set; } = new();
        /// <summary>Field ID: 69</summary>
        public TlvTypedBaseOrBonus OSleepingThrsh { get; set; } = new();
        /// <summary>Field ID: 70</summary>
        public TlvTypedBaseOrBonus OParaThrsh { get; set; } = new();
        /// <summary>Field ID: 76</summary>
        public TlvTypedBaseOrBonus OWindPressureDef { get; set; } = new();
        /// <summary>Field ID: 77</summary>
        public TlvTypedBaseOrBonus OQuakeDef { get; set; } = new();
        /// <summary>Field ID: 78</summary>
        public TlvTypedBaseOrBonus ORoarDef { get; set; } = new();
        /// <summary>Field ID: 79</summary>
        public TlvTypedBaseOrBonus OPalsyDef { get; set; } = new();
        /// <summary>Field ID: 80</summary>
        public TlvTypedBaseOrBonus OSnowManDef { get; set; } = new();
        /// <summary>Field ID: 81</summary>
        public TlvTypedBaseOrBonus OTiredDef { get; set; } = new();
        /// <summary>Field ID: 82</summary>
        public TlvTypedBaseOrBonus OAttackLevelDef { get; set; } = new();
        /// <summary>Field ID: 83</summary>
        public TlvTypedBaseOrBonus OPetSkillID { get; set; } = new();
        /// <summary>Field ID: 84</summary>
        public TlvTypedBaseOrBonus OEquipedSkillID { get; set; } = new();
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
        /// <summary>Field ID: 91</summary>
        public TlvTypedBaseOrBonus ORegion { get; set; } = new();

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
            WriteTlvSubStructure(buffer, 9, OPetExp);
            WriteTlvSubStructure(buffer, 10, OPetLevel);
            WriteTlvSubStructure(buffer, 18, OSpirit);
            WriteTlvSubStructure(buffer, 19, OVigour);
            WriteTlvSubStructure(buffer, 20, OPetHP);
            WriteTlvSubStructure(buffer, 21, OPetMaxHP);
            WriteTlvSubStructure(buffer, 23, OPetMelee);
            WriteTlvSubStructure(buffer, 24, OPetDefence);
            WriteTlvSubStructure(buffer, 25, OCritLevel);
            WriteTlvSubStructure(buffer, 30, OPetExecution);
            WriteTlvSubStructure(buffer, 31, OPetObservation);
            WriteTlvSubStructure(buffer, 32, OPetLoadBearing);
            WriteTlvSubStructure(buffer, 34, OPetMaxSp);
            WriteTlvSubStructure(buffer, 35, OPetSp);
            WriteTlvSubStructure(buffer, 36, OPetPotential);
            WriteTlvSubStructure(buffer, 42, OWaterAtk);
            WriteTlvSubStructure(buffer, 43, OFireAtk);
            WriteTlvSubStructure(buffer, 44, OLightningAtk);
            WriteTlvSubStructure(buffer, 45, ODragonAtk);
            WriteTlvSubStructure(buffer, 46, OIceAtk);
            WriteTlvSubStructure(buffer, 47, OWaterRes);
            WriteTlvSubStructure(buffer, 48, OFireRes);
            WriteTlvSubStructure(buffer, 49, OLightningRes);
            WriteTlvSubStructure(buffer, 50, ODragonRes);
            WriteTlvSubStructure(buffer, 51, OIceRes);
            WriteTlvSubStructure(buffer, 52, OWaterThrsh);
            WriteTlvSubStructure(buffer, 53, OFireThrsh);
            WriteTlvSubStructure(buffer, 54, OLightningThrsh);
            WriteTlvSubStructure(buffer, 55, ODragonThrsh);
            WriteTlvSubStructure(buffer, 56, OIceThrsh);
            WriteTlvSubStructure(buffer, 67, OComaThrsh);
            WriteTlvSubStructure(buffer, 68, OPoisonThrsh);
            WriteTlvSubStructure(buffer, 69, OSleepingThrsh);
            WriteTlvSubStructure(buffer, 70, OParaThrsh);
            WriteTlvSubStructure(buffer, 76, OWindPressureDef);
            WriteTlvSubStructure(buffer, 77, OQuakeDef);
            WriteTlvSubStructure(buffer, 78, ORoarDef);
            WriteTlvSubStructure(buffer, 79, OPalsyDef);
            WriteTlvSubStructure(buffer, 80, OSnowManDef);
            WriteTlvSubStructure(buffer, 81, OTiredDef);
            WriteTlvSubStructure(buffer, 82, OAttackLevelDef);
            WriteTlvSubStructure(buffer, 83, OPetSkillID);
            WriteTlvSubStructure(buffer, 84, OEquipedSkillID);
            WriteTlvSubStructure(buffer, 85, OPetWeaponID);
            WriteTlvSubStructure(buffer, 86, OPetHatID);
            WriteTlvSubStructure(buffer, 87, OPetBodyID);
            WriteTlvSubStructure(buffer, 88, OFashionWeaponID);
            WriteTlvSubStructure(buffer, 89, OFashionHatID);
            WriteTlvSubStructure(buffer, 90, OFashionBodyID);
            WriteTlvSubStructure(buffer, 91, ORegion);
        }
    }
}
