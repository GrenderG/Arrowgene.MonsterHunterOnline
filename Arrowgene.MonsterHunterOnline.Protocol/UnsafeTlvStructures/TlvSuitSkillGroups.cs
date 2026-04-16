using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Container for list of TlvSkillGroupData.
    /// C++ Reader: crygame.dll+sub_10189400 (UnkTlv0155)
    /// </summary>
    public class TlvSuitSkillGroups : Structure, ITlvStructure
    {
        /// <summary>
        /// Count (derived from SuitSkillGroupData).
        /// Field ID: 1
        /// </summary>
        public int Count => SuitSkillGroupData?.Count ?? 0;

        /// <summary>
        /// List of TlvSkillGroupData.
        /// Field ID: 2
        /// </summary>
        public List<TlvSkillGroupData> SuitSkillGroupData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, SuitSkillGroupData.Count, SuitSkillGroupData);
        }
    }
}
