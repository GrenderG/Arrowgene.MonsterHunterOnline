using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for extended pet attribute data (58 typed base-or-bonus fields).
    /// C++ Reader: crygame.dll+sub_101F6310 (UnkTlv0204)
    /// C++ Printer: crygame.dll+sub_101F8270
    /// </summary>
    public class TlvPetExtAttrData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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
        /// <summary>Field ID: 142</summary>
        public TlvTypedBaseOrBonus OPetMana { get; set; } = new();
        /// <summary>Field ID: 143</summary>
        public TlvTypedBaseOrBonus OPetMaxMana { get; set; } = new();
        /// <summary>Field ID: 145</summary>
        public TlvTypedBaseOrBonus OAnimusHide { get; set; } = new();
        /// <summary>Field ID: 146</summary>
        public TlvTypedBaseOrBonus ONoInterupt { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 4: OPetName = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 5: OPetSex = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 7: OOwner = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 8: OPetSignature = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 9: OPetExp = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 10: OPetLevel = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 18: OSpirit = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 19: OVigour = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 20: OPetHP = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 21: OPetMaxHP = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 23: OPetMelee = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 24: OPetDefence = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 25: OCritLevel = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 30: OPetExecution = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 31: OPetObservation = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 32: OPetLoadBearing = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 34: OPetMaxSp = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 35: OPetSp = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 36: OPetPotential = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 42: OWaterAtk = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 43: OFireAtk = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 44: OLightningAtk = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 45: ODragonAtk = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 46: OIceAtk = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 47: OWaterRes = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 48: OFireRes = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 49: OLightningRes = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 50: ODragonRes = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 51: OIceRes = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 52: OWaterThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 53: OFireThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 54: OLightningThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 55: ODragonThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 56: OIceThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 67: OComaThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 68: OPoisonThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 69: OSleepingThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 70: OParaThrsh = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 76: OWindPressureDef = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 77: OQuakeDef = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 78: ORoarDef = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 79: OPalsyDef = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 80: OSnowManDef = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 81: OTiredDef = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 82: OAttackLevelDef = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 83: OPetSkillID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 84: OEquipedSkillID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 85: OPetWeaponID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 86: OPetHatID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 87: OPetBodyID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 88: OFashionWeaponID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 89: OFashionHatID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 90: OFashionBodyID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 91: ORegion = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 142: OPetMana = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 143: OPetMaxMana = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 145: OAnimusHide = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 146: ONoInterupt = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 4, OPetName);
            WriteTlvObject(writer, 5, OPetSex);
            WriteTlvObject(writer, 7, OOwner);
            WriteTlvObject(writer, 8, OPetSignature);
            WriteTlvObject(writer, 9, OPetExp);
            WriteTlvObject(writer, 10, OPetLevel);
            WriteTlvObject(writer, 18, OSpirit);
            WriteTlvObject(writer, 19, OVigour);
            WriteTlvObject(writer, 20, OPetHP);
            WriteTlvObject(writer, 21, OPetMaxHP);
            WriteTlvObject(writer, 23, OPetMelee);
            WriteTlvObject(writer, 24, OPetDefence);
            WriteTlvObject(writer, 25, OCritLevel);
            WriteTlvObject(writer, 30, OPetExecution);
            WriteTlvObject(writer, 31, OPetObservation);
            WriteTlvObject(writer, 32, OPetLoadBearing);
            WriteTlvObject(writer, 34, OPetMaxSp);
            WriteTlvObject(writer, 35, OPetSp);
            WriteTlvObject(writer, 36, OPetPotential);
            WriteTlvObject(writer, 42, OWaterAtk);
            WriteTlvObject(writer, 43, OFireAtk);
            WriteTlvObject(writer, 44, OLightningAtk);
            WriteTlvObject(writer, 45, ODragonAtk);
            WriteTlvObject(writer, 46, OIceAtk);
            WriteTlvObject(writer, 47, OWaterRes);
            WriteTlvObject(writer, 48, OFireRes);
            WriteTlvObject(writer, 49, OLightningRes);
            WriteTlvObject(writer, 50, ODragonRes);
            WriteTlvObject(writer, 51, OIceRes);
            WriteTlvObject(writer, 52, OWaterThrsh);
            WriteTlvObject(writer, 53, OFireThrsh);
            WriteTlvObject(writer, 54, OLightningThrsh);
            WriteTlvObject(writer, 55, ODragonThrsh);
            WriteTlvObject(writer, 56, OIceThrsh);
            WriteTlvObject(writer, 67, OComaThrsh);
            WriteTlvObject(writer, 68, OPoisonThrsh);
            WriteTlvObject(writer, 69, OSleepingThrsh);
            WriteTlvObject(writer, 70, OParaThrsh);
            WriteTlvObject(writer, 76, OWindPressureDef);
            WriteTlvObject(writer, 77, OQuakeDef);
            WriteTlvObject(writer, 78, ORoarDef);
            WriteTlvObject(writer, 79, OPalsyDef);
            WriteTlvObject(writer, 80, OSnowManDef);
            WriteTlvObject(writer, 81, OTiredDef);
            WriteTlvObject(writer, 82, OAttackLevelDef);
            WriteTlvObject(writer, 83, OPetSkillID);
            WriteTlvObject(writer, 84, OEquipedSkillID);
            WriteTlvObject(writer, 85, OPetWeaponID);
            WriteTlvObject(writer, 86, OPetHatID);
            WriteTlvObject(writer, 87, OPetBodyID);
            WriteTlvObject(writer, 88, OFashionWeaponID);
            WriteTlvObject(writer, 89, OFashionHatID);
            WriteTlvObject(writer, 90, OFashionBodyID);
            WriteTlvObject(writer, 91, ORegion);
            WriteTlvObject(writer, 142, OPetMana);
            WriteTlvObject(writer, 143, OPetMaxMana);
            WriteTlvObject(writer, 145, OAnimusHide);
            WriteTlvObject(writer, 146, ONoInterupt);
        }
    }
}
