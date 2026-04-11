using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for season stats tracking.
    /// C++ Reader: crygame.dll+sub_1017EE60 (UnkTlv0142)
    /// C++ Printer: crygame.dll+sub_1017F1E0
    /// </summary>
    public class TlvSeasonStats : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CurSeason = reader.ReadInt(); break;
                    case 2: Score = reader.ReadInt(); break;
                    case 3: ScoreTop = reader.ReadInt(); break;
                    case 4: WeekReward = reader.ReadInt(); break;
                    case 5: Streak = reader.ReadInt(); break;
                    case 6: WinNum = reader.ReadInt(); break;
                    case 7: LoseNum = reader.ReadInt(); break;
                    case 8: TotalNum = reader.ReadInt(); break;
                    case 9: RewardMask = reader.ReadInt(); break;
                    case 10: ExRewardCount = reader.ReadInt(); break;
                    case 11: StepReward = reader.ReadInt(); break;
                    case 12: ExMedalCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, CurSeason);
            WriteTlvInt(writer, 2, Score);
            WriteTlvInt(writer, 3, ScoreTop);
            WriteTlvInt(writer, 4, WeekReward);
            WriteTlvInt(writer, 5, Streak);
            WriteTlvInt(writer, 6, WinNum);
            WriteTlvInt(writer, 7, LoseNum);
            WriteTlvInt(writer, 8, TotalNum);
            WriteTlvInt(writer, 9, RewardMask);
            WriteTlvInt(writer, 10, ExRewardCount);
            WriteTlvInt(writer, 11, StepReward);
            WriteTlvInt(writer, 12, ExMedalCount);
        }
    }
}
