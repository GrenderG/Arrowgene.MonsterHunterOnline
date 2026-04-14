using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for guild member data entry.
    /// C++ Reader: crygame.dll+sub_10121660 (UnkTlv0024)
    /// C++ Printer: crygame.dll+sub_10121D40
    /// </summary>
    public class TlvGuildMemberData : Structure, ITlvStructure
    {
        /// <summary>Member identity. Field ID: 1</summary>
        public TlvGuildMemberId Id { get; set; } = new();

        /// <summary>Field ID: 2</summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>Field ID: 3</summary>
        public int Level { get; set; }

        /// <summary>Field ID: 4</summary>
        public string HunterStar { get; set; } = string.Empty;

        /// <summary>Field ID: 5</summary>
        public int Contribution { get; set; }

        /// <summary>Field ID: 6</summary>
        public int ContributionAcc { get; set; }

        /// <summary>Field ID: 7</summary>
        public int Wage { get; set; }

        /// <summary>Field ID: 8</summary>
        public int Title { get; set; }

        /// <summary>Field ID: 9</summary>
        public int OfflineTime { get; set; }

        /// <summary>Field ID: 10</summary>
        public int DepotOpCount { get; set; }

        /// <summary>Field ID: 11</summary>
        public int HRLevel { get; set; }

        /// <summary>Field ID: 12</summary>
        public int JoinTime { get; set; }

        /// <summary>Field ID: 13</summary>
        public int WildHuntSoul { get; set; }

        /// <summary>Field ID: 14</summary>
        public int WildHuntPhase { get; set; }

        /// <summary>Field ID: 15</summary>
        public int CelebrationTask { get; set; }

        /// <summary>Field ID: 16</summary>
        public int PreCelebrationTask { get; set; }

        /// <summary>Field ID: 17</summary>
        public int CelebrationScore { get; set; }

        /// <summary>Field ID: 18</summary>
        public int CelebrationReward { get; set; }

        /// <summary>Field ID: 19</summary>
        public long ContributionWeekAcc { get; set; }

        /// <summary>Field ID: 20</summary>
        public int LevelupAll { get; set; }

        /// <summary>Field ID: 21</summary>
        public int HunterCount { get; set; }

        /// <summary>Field ID: 22</summary>
        public int TaskCount { get; set; }

        /// <summary>Field ID: 23</summary>
        public int IsBaned { get; set; }

        /// <summary>Field ID: 24</summary>
        public int BanedTime { get; set; }

        /// <summary>Guild war data. Field ID: 25</summary>
        public TlvCommerceBoatContribution GuildWar { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 1, Id);
            WriteTlvString(buffer, 2, Note);
            WriteTlvInt32(buffer, 3, Level);
            WriteTlvString(buffer, 4, HunterStar);
            WriteTlvInt32(buffer, 5, Contribution);
            WriteTlvInt32(buffer, 6, ContributionAcc);
            WriteTlvInt32(buffer, 7, Wage);
            WriteTlvInt32(buffer, 8, Title);
            WriteTlvInt32(buffer, 9, OfflineTime);
            WriteTlvInt32(buffer, 10, DepotOpCount);
            WriteTlvInt32(buffer, 11, HRLevel);
            WriteTlvInt32(buffer, 12, JoinTime);
            WriteTlvInt32(buffer, 13, WildHuntSoul);
            WriteTlvInt32(buffer, 14, WildHuntPhase);
            WriteTlvInt32(buffer, 15, CelebrationTask);
            WriteTlvInt32(buffer, 16, PreCelebrationTask);
            WriteTlvInt32(buffer, 17, CelebrationScore);
            WriteTlvInt32(buffer, 18, CelebrationReward);
            WriteTlvInt64(buffer, 19, ContributionWeekAcc);
            WriteTlvInt32(buffer, 20, LevelupAll);
            WriteTlvInt32(buffer, 21, HunterCount);
            WriteTlvInt32(buffer, 22, TaskCount);
            WriteTlvInt32(buffer, 23, IsBaned);
            WriteTlvInt32(buffer, 24, BanedTime);
            WriteTlvSubStructure(buffer, 25, GuildWar);
        }
    }
}
