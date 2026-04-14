using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for pet farm show data.
    /// C++ Reader: crygame.dll+sub_10201E60 (UnkTlv0215)
    /// C++ Printer: crygame.dll+sub_102026E0
    /// </summary>
    public class TlvPetFarmShowData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxSPFData = 6;
        public const int MaxPetAvatars = 30;

        /// <summary>Pet start time data. Field ID: 1</summary>
        public List<TlvPetIdStartTime> SPFData { get; set; }

        /// <summary>Pet avatar count (derived). Field ID: 2</summary>
        public short PetAvatarCount => (short)(PetAvatarInfo?.Count ?? 0);

        /// <summary>Pet avatar data entries. Field ID: 3</summary>
        public List<TlvPetAvatarData> PetAvatarInfo { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((SPFData?.Count ?? 0) > MaxSPFData)
                throw new InvalidDataException($"[TlvPetFarmShowData] SPFData exceeds {MaxSPFData}.");
            if ((PetAvatarInfo?.Count ?? 0) > MaxPetAvatars)
                throw new InvalidDataException($"[TlvPetFarmShowData] PetAvatarInfo exceeds {MaxPetAvatars}.");

            WriteTlvSubStructureList(buffer, 1, SPFData.Count, SPFData);
            WriteTlvInt16(buffer, 2, PetAvatarCount);
            WriteTlvSubStructureList(buffer, 3, PetAvatarInfo.Count, PetAvatarInfo);
        }
    }
}
