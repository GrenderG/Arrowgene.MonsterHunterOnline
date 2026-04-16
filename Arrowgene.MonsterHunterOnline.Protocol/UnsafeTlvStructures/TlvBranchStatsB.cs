using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for branch stats (variant B with short level).
    /// C++ Reader: crygame.dll+sub_10216470 (inner of UnkTlv0236)
    /// C++ Printer: crygame.dll+sub_102166E0
    /// </summary>
    public class TlvBranchStatsB : Structure, ITlvStructure
    {
        /// <summary>
        /// Branch type.
        /// Field ID: 1
        /// </summary>
        public byte BranchType { get; set; }

        /// <summary>
        /// Branch level.
        /// Field ID: 2
        /// </summary>
        public short BranchLevel { get; set; }

        /// <summary>
        /// Branch total score.
        /// Field ID: 3
        /// </summary>
        public uint BranchAllScore { get; set; }

        /// <summary>
        /// Branch daily score.
        /// Field ID: 4
        /// </summary>
        public uint BranchDayScore { get; set; }

        /// <summary>
        /// Branch record card level-up times.
        /// Field ID: 5
        /// </summary>
        public int BranchRecordCardLevelUpTimes { get; set; }

        /// <summary>
        /// Branch challenge score.
        /// Field ID: 6
        /// </summary>
        public uint BranchChallengeScore { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, BranchType);
            WriteTlvInt16(buffer, 2, BranchLevel);
            WriteTlvInt32(buffer, 3, (int)BranchAllScore);
            WriteTlvInt32(buffer, 4, (int)BranchDayScore);
            WriteTlvInt32(buffer, 5, BranchRecordCardLevelUpTimes);
            WriteTlvInt32(buffer, 6, (int)BranchChallengeScore);
        }
    }
}
