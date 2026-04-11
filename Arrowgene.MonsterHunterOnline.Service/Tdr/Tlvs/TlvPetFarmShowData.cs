using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for pet farm show data.
    /// C++ Reader: crygame.dll+sub_10201E60 (UnkTlv0215)
    /// C++ Printer: crygame.dll+sub_102026E0
    /// </summary>
    public class TlvPetFarmShowData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxSPFData = 6;
        public const int MaxPetAvatars = 30;

        /// <summary>Pet start time data. Field ID: 1</summary>
        public List<TlvPetIdStartTime> SPFData { get; set; }

        /// <summary>Pet avatar count (derived). Field ID: 2</summary>
        public short PetAvatarCount => (short)(PetAvatarInfo?.Count ?? 0);

        /// <summary>Pet avatar data entries. Field ID: 3</summary>
        public List<TlvPetAvatarData> PetAvatarInfo { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: SPFData = ReadTlvList<TlvPetIdStartTime>(reader); break;
                    case 2: reader.ReadShort(); break;
                    case 3: PetAvatarInfo = ReadTlvList<TlvPetAvatarData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((SPFData?.Count ?? 0) > MaxSPFData)
                throw new InvalidDataException($"[TlvPetFarmShowData] SPFData exceeds {MaxSPFData}.");
            if ((PetAvatarInfo?.Count ?? 0) > MaxPetAvatars)
                throw new InvalidDataException($"[TlvPetFarmShowData] PetAvatarInfo exceeds {MaxPetAvatars}.");

            WriteTlvList(writer, 1, SPFData);
            WriteTlvShort(writer, 2, PetAvatarCount);
            WriteTlvList(writer, 3, PetAvatarInfo);
        }
    }
}
