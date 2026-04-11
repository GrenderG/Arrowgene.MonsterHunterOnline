using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild member data entry.
    /// C++ Reader: crygame.dll+sub_10121660 (UnkTlv0024)
    /// C++ Printer: crygame.dll+sub_10121D40
    /// </summary>
    public class TlvGuildMemberData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = ReadTlvObject<TlvGuildMemberId>(reader); break;
                    case 2: { int len = reader.ReadInt(); Note = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 3: Level = reader.ReadInt(); break;
                    case 4: { int len = reader.ReadInt(); HunterStar = len > 0 ? Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 5: Contribution = reader.ReadInt(); break;
                    case 6: ContributionAcc = reader.ReadInt(); break;
                    case 7: Wage = reader.ReadInt(); break;
                    case 8: Title = reader.ReadInt(); break;
                    case 9: OfflineTime = reader.ReadInt(); break;
                    case 10: DepotOpCount = reader.ReadInt(); break;
                    case 11: HRLevel = reader.ReadInt(); break;
                    case 12: JoinTime = reader.ReadInt(); break;
                    case 13: WildHuntSoul = reader.ReadInt(); break;
                    case 14: WildHuntPhase = reader.ReadInt(); break;
                    case 15: CelebrationTask = reader.ReadInt(); break;
                    case 16: PreCelebrationTask = reader.ReadInt(); break;
                    case 17: CelebrationScore = reader.ReadInt(); break;
                    case 18: CelebrationReward = reader.ReadInt(); break;
                    case 19: ContributionWeekAcc = reader.ReadLong(); break;
                    case 20: LevelupAll = reader.ReadInt(); break;
                    case 21: HunterCount = reader.ReadInt(); break;
                    case 22: TaskCount = reader.ReadInt(); break;
                    case 23: IsBaned = reader.ReadInt(); break;
                    case 24: BanedTime = reader.ReadInt(); break;
                    case 25: GuildWar = ReadTlvObject<TlvCommerceBoatContribution>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 1, Id);
            WriteTlvString(writer, 2, Note);
            WriteTlvInt(writer, 3, Level);
            WriteTlvString(writer, 4, HunterStar);
            WriteTlvInt(writer, 5, Contribution);
            WriteTlvInt(writer, 6, ContributionAcc);
            WriteTlvInt(writer, 7, Wage);
            WriteTlvInt(writer, 8, Title);
            WriteTlvInt(writer, 9, OfflineTime);
            WriteTlvInt(writer, 10, DepotOpCount);
            WriteTlvInt(writer, 11, HRLevel);
            WriteTlvInt(writer, 12, JoinTime);
            WriteTlvInt(writer, 13, WildHuntSoul);
            WriteTlvInt(writer, 14, WildHuntPhase);
            WriteTlvInt(writer, 15, CelebrationTask);
            WriteTlvInt(writer, 16, PreCelebrationTask);
            WriteTlvInt(writer, 17, CelebrationScore);
            WriteTlvInt(writer, 18, CelebrationReward);
            WriteTlvLong(writer, 19, ContributionWeekAcc);
            WriteTlvInt(writer, 20, LevelupAll);
            WriteTlvInt(writer, 21, HunterCount);
            WriteTlvInt(writer, 22, TaskCount);
            WriteTlvInt(writer, 23, IsBaned);
            WriteTlvInt(writer, 24, BanedTime);
            WriteTlvObject(writer, 25, GuildWar);
        }
    }
}
