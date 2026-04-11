using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for pet option data (12 typed base-or-bonus fields).
    /// C++ Reader: crygame.dll+sub_101F9E00 (UnkTlv0205)
    /// C++ Printer: crygame.dll+sub_101FAEE0
    /// </summary>
    public class TlvPetOptionData : TlvStructure
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

        /// <summary>Field ID: 91</summary>
        public TlvTypedBaseOrBonus ORegion { get; set; } = new();

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
                    case 10: OPetLevel = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 85: OPetWeaponID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 86: OPetHatID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 87: OPetBodyID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 88: OFashionWeaponID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 89: OFashionHatID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 90: OFashionBodyID = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
                    case 91: ORegion = ReadTlvObject<TlvTypedBaseOrBonus>(reader); break;
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
            WriteTlvObject(writer, 10, OPetLevel);
            WriteTlvObject(writer, 85, OPetWeaponID);
            WriteTlvObject(writer, 86, OPetHatID);
            WriteTlvObject(writer, 87, OPetBodyID);
            WriteTlvObject(writer, 88, OFashionWeaponID);
            WriteTlvObject(writer, 89, OFashionHatID);
            WriteTlvObject(writer, 90, OFashionBodyID);
            WriteTlvObject(writer, 91, ORegion);
        }
    }
}
