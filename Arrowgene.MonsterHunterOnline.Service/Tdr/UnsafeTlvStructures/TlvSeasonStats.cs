using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for season stats tracking.
    /// C++ Reader: crygame.dll+sub_1017EE60 (UnkTlv0142)
    /// C++ Printer: crygame.dll+sub_1017F1E0
    /// </summary>
    public class TlvSeasonStats : Structure, ITlvStructure
    {
        /// <summary>
        /// Current season.
        /// Field ID: 1
        /// </summary>
        public int CurSeason { get; set; }

        /// <summary>
        /// Score.
        /// Field ID: 2
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Top score.
        /// Field ID: 3
        /// </summary>
        public int ScoreTop { get; set; }

        /// <summary>
        /// Week reward.
        /// Field ID: 4
        /// </summary>
        public int WeekReward { get; set; }

        /// <summary>
        /// Streak.
        /// Field ID: 5
        /// </summary>
        public int Streak { get; set; }

        /// <summary>
        /// Win number.
        /// Field ID: 6
        /// </summary>
        public int WinNum { get; set; }

        /// <summary>
        /// Lose number.
        /// Field ID: 7
        /// </summary>
        public int LoseNum { get; set; }

        /// <summary>
        /// Total number.
        /// Field ID: 8
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// Reward mask.
        /// Field ID: 9
        /// </summary>
        public int RewardMask { get; set; }

        /// <summary>
        /// Extra reward count.
        /// Field ID: 10
        /// </summary>
        public int ExRewardCount { get; set; }

        /// <summary>
        /// Step reward.
        /// Field ID: 11
        /// </summary>
        public int StepReward { get; set; }

        /// <summary>
        /// Extra medal count.
        /// Field ID: 12
        /// </summary>
        public int ExMedalCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, CurSeason);
            WriteTlvInt32(buffer, 2, Score);
            WriteTlvInt32(buffer, 3, ScoreTop);
            WriteTlvInt32(buffer, 4, WeekReward);
            WriteTlvInt32(buffer, 5, Streak);
            WriteTlvInt32(buffer, 6, WinNum);
            WriteTlvInt32(buffer, 7, LoseNum);
            WriteTlvInt32(buffer, 8, TotalNum);
            WriteTlvInt32(buffer, 9, RewardMask);
            WriteTlvInt32(buffer, 10, ExRewardCount);
            WriteTlvInt32(buffer, 11, StepReward);
            WriteTlvInt32(buffer, 12, ExMedalCount);
        }
    }
}
