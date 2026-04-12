using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvPetInfo.
    /// C++ Reader: crygame.dll+sub_101A03C0 (UnkTlv0178)
    /// </summary>
    public class TlvPets : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Pets).
        /// Field ID: 1
        /// </summary>
        public int Count => Pets?.Count ?? 0;

        /// <summary>
        /// List of TlvPetInfo.
        /// Field ID: 2
        /// </summary>
        public List<TlvPetInfo> Pets { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Pets.Count, Pets);
        }
    }
}
