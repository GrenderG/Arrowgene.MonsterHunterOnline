using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvSkillLevel.
    /// C++ Reader: crygame.dll+sub_10128180 (UnkTlv0033)
    /// </summary>
    public class TlvSkills : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from Skills).
        /// Field ID: 1
        /// </summary>
        public int Count => Skills?.Count ?? 0;

        /// <summary>
        /// List of TlvSkillLevel.
        /// Field ID: 2
        /// </summary>
        public List<TlvSkillLevel> Skills { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Skills.Count, Skills);
        }
    }
}
