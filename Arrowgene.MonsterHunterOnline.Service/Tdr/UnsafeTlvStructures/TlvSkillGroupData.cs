using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for skill group with skill IDs and active flags.
    /// C++ Reader: crygame.dll+sub_101890F0 (UnkTlv0154)
    /// C++ Printer: crygame.dll+sub_10189670
    /// </summary>
    public class TlvSkillGroupData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxSkills = 30;

        /// <summary>
        /// Skill group ID.
        /// Field ID: 1
        /// </summary>
        public int SkillGroup { get; set; }

        /// <summary>
        /// Left edit count.
        /// Field ID: 2
        /// </summary>
        public int LeftEditCnt { get; set; }

        /// <summary>
        /// Skill count (byte).
        /// Field ID: 3
        /// </summary>
        public byte SkillCnt => (byte)(SkillId?.Length ?? 0);

        /// <summary>
        /// Skill IDs (int array).
        /// Field ID: 4
        /// </summary>
        public int[] SkillId { get; set; }

        /// <summary>
        /// Active flag bytes.
        /// Field ID: 5
        /// </summary>
        public byte[] ActFlag { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((SkillId?.Length ?? 0) > MaxSkills)
                throw new InvalidDataException($"[TlvSkillGroupData] SkillId exceeds the maximum of {MaxSkills} elements.");
            if ((ActFlag?.Length ?? 0) > MaxSkills)
                throw new InvalidDataException($"[TlvSkillGroupData] ActFlag exceeds the maximum of {MaxSkills} elements.");

            WriteTlvInt32(buffer, 1, SkillGroup);
            WriteTlvInt32(buffer, 2, LeftEditCnt);
            WriteTlvByte(buffer, 3, SkillCnt);
            WriteTlvInt32Arr(buffer, 4, SkillId);
            WriteTlvByteArr(buffer, 5, ActFlag);
        }
    }
}
