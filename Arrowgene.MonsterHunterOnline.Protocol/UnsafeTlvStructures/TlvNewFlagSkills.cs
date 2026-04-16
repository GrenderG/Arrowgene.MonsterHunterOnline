using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvIdNewFlag.
    /// C++ Reader: crygame.dll+sub_101A2EA0 (UnkTlv0183)
    /// </summary>
    public class TlvNewFlagSkills : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Skill).
        /// Field ID: 1
        /// </summary>
        public int Count => Skill?.Count ?? 0;

        /// <summary>
        /// List of TlvIdNewFlag.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdNewFlag> Skill { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Skill.Count, Skill);
        }
    }
}
