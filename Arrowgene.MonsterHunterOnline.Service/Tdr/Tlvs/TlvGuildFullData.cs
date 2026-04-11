using Rathalos.Core.Utils.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild full data.
    /// C++ Reader: crygame.dll+sub_10135C20 (UnkTlv0047)
    /// C++ Printer: crygame.dll+sub_10136E50
    /// </summary>
    public class TlvGuildFullData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Field ID: 2</summary>
        public long Id { get; set; }

        /// <summary>Field ID: 3</summary>
        public int Seed { get; set; }

        /// <summary>Field ID: 4</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Field ID: 5</summary>
        public int Icon { get; set; }

        /// <summary>Field ID: 6</summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>Field ID: 7</summary>
        public int Level { get; set; }

        /// <summary>Field ID: 8</summary>
        public int Exp { get; set; }

        /// <summary>Field ID: 9</summary>
        public int Repute { get; set; }

        /// <summary>Field ID: 10</summary>
        public int Fund { get; set; }

        /// <summary>Field ID: 11</summary>
        public int Arena { get; set; }

        /// <summary>Field ID: 12</summary>
        public int ResA { get; set; }

        /// <summary>Field ID: 13</summary>
        public int ResB { get; set; }

        /// <summary>Field ID: 14</summary>
        public int JoinLevel { get; set; }

        /// <summary>Field ID: 15</summary>
        public long Leader { get; set; }

        /// <summary>Field ID: 16</summary>
        public TlvGuildTitleList Titles { get; set; } = new();

        /// <summary>Field ID: 17</summary>
        public TlvGuildMemberList Guilders { get; set; } = new();

        /// <summary>Field ID: 18</summary>
        public TlvGuildApplyList Applys { get; set; } = new();

        /// <summary>Field ID: 19</summary>
        public long FundMax { get; set; }

        /// <summary>Field ID: 20</summary>
        public byte HallLevel { get; set; }

        /// <summary>Field ID: 21</summary>
        public byte RecruitingLevel { get; set; }

        /// <summary>Field ID: 22</summary>
        public byte ResourcingLevel { get; set; }

        /// <summary>Field ID: 23</summary>
        public byte TradingLevel { get; set; }

        /// <summary>Field ID: 24</summary>
        public byte TrainingLevel { get; set; }

        /// <summary>Field ID: 25</summary>
        public int GuilderCountMax { get; set; }

        /// <summary>Field ID: 26</summary>
        public long FundDaily { get; set; }

        /// <summary>Field ID: 27</summary>
        public long FundWeekly { get; set; }

        /// <summary>Field ID: 28</summary>
        public long CommodityUnlock1 { get; set; }

        /// <summary>Field ID: 29</summary>
        public long CommodityUnlock2 { get; set; }

        /// <summary>Field ID: 30</summary>
        public long CommodityUnlock3 { get; set; }

        /// <summary>Field ID: 31</summary>
        public int FundDailyTime { get; set; }

        /// <summary>Field ID: 32</summary>
        public int FundWeeklyTime { get; set; }

        /// <summary>Field ID: 33</summary>
        public int ResC { get; set; }

        /// <summary>Field ID: 34</summary>
        public int ResD { get; set; }

        /// <summary>Field ID: 35</summary>
        public int ResADaily { get; set; }

        /// <summary>Field ID: 36</summary>
        public int ResBDaily { get; set; }

        /// <summary>Field ID: 37</summary>
        public int ResCDaily { get; set; }

        /// <summary>Field ID: 38</summary>
        public int ResDDaily { get; set; }

        /// <summary>Field ID: 39</summary>
        public int ResAWeekly { get; set; }

        /// <summary>Field ID: 40</summary>
        public int ResBWeekly { get; set; }

        /// <summary>Field ID: 41</summary>
        public int ResCWeekly { get; set; }

        /// <summary>Field ID: 42</summary>
        public int ResDWeekly { get; set; }

        /// <summary>Field ID: 43</summary>
        public int ResAMax { get; set; }

        /// <summary>Field ID: 44</summary>
        public int ResBMax { get; set; }

        /// <summary>Field ID: 45</summary>
        public int ResCMax { get; set; }

        /// <summary>Field ID: 46</summary>
        public int ResDMax { get; set; }

        /// <summary>Field ID: 47</summary>
        public int AddFundWithDay { get; set; }

        /// <summary>Field ID: 48</summary>
        public int ConsumeFundWithDay { get; set; }

        /// <summary>Field ID: 49</summary>
        public int Plot { get; set; }

        /// <summary>Field ID: 50</summary>
        public TlvQuestScheduleData Tasks { get; set; } = new();

        /// <summary>Field ID: 51</summary>
        public TlvSkills Skills { get; set; } = new();

        /// <summary>Field ID: 52</summary>
        public int SignUpCount { get; set; }

        /// <summary>Field ID: 53</summary>
        public TlvGuildTeam Stores { get; set; } = new();

        /// <summary>Field ID: 54</summary>
        public TlvStoreData DepotOpenFlag { get; set; } = new();

        /// <summary>Field ID: 55</summary>
        public byte RandCommodities { get; set; }

        /// <summary>Field ID: 56</summary>
        public TlvCountCtxsB Quest { get; set; } = new();

        /// <summary>Field ID: 57</summary>
        public TlvCountPrizesB QuestPrize { get; set; } = new();

        /// <summary>Field ID: 58</summary>
        public int Contribution { get; set; }

        /// <summary>Field ID: 59</summary>
        public int CreateTime { get; set; }

        /// <summary>Field ID: 60</summary>
        public int HuntSoul { get; set; }

        /// <summary>Field ID: 61</summary>
        public long WildHuntCamp { get; set; }

        /// <summary>Field ID: 62</summary>
        public int WildHuntPhase { get; set; }

        /// <summary>Field ID: 63</summary>
        public int CelebrationTaskA { get; set; }

        /// <summary>Field ID: 64</summary>
        public int CelebrationTaskB { get; set; }

        /// <summary>Field ID: 65</summary>
        public int CelebrationTaskC { get; set; }

        /// <summary>Field ID: 66</summary>
        public int CelebrationTaskD { get; set; }

        /// <summary>Field ID: 67</summary>
        public int CelebrationScore { get; set; }

        /// <summary>Field ID: 68</summary>
        public int CelebrationDailyUpdate { get; set; }

        /// <summary>Field ID: 69</summary>
        public int CelebrationWeeklyScoreUpdate { get; set; }

        /// <summary>Field ID: 70</summary>
        public int CelebrationDailyScore { get; set; }

        /// <summary>Field ID: 71</summary>
        public int CelebrationWeeklyRewardUpdate { get; set; }

        /// <summary>Field ID: 72</summary>
        public int GuildWar { get; set; }

        /// <summary>Field ID: 73</summary>
        public TlvGuildCommerceData GuildOperateRecord { get; set; } = new();

        /// <summary>Field ID: 74</summary>
        public TlvGuildFuncRecords GuildBuyRecord { get; set; } = new();

        /// <summary>Field ID: 75</summary>
        public TlvGuildBuyRecords SignUps { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: Id = reader.ReadLong(); break;
                    case 3: Seed = reader.ReadInt(); break;
                    case 4: { int len = reader.ReadInt(); Name = len > 0 ? System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 5: Icon = reader.ReadInt(); break;
                    case 6: { int len = reader.ReadInt(); Note = len > 0 ? System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len)) : string.Empty; } break;
                    case 7: Level = reader.ReadInt(); break;
                    case 8: Exp = reader.ReadInt(); break;
                    case 9: Repute = reader.ReadInt(); break;
                    case 10: Fund = reader.ReadInt(); break;
                    case 11: Arena = reader.ReadInt(); break;
                    case 12: ResA = reader.ReadInt(); break;
                    case 13: ResB = reader.ReadInt(); break;
                    case 14: JoinLevel = reader.ReadInt(); break;
                    case 15: Leader = reader.ReadLong(); break;
                    case 16: Titles = ReadTlvObject<TlvGuildTitleList>(reader); break;
                    case 17: Guilders = ReadTlvObject<TlvGuildMemberList>(reader); break;
                    case 18: Applys = ReadTlvObject<TlvGuildApplyList>(reader); break;
                    case 19: FundMax = reader.ReadLong(); break;
                    case 20: HallLevel = reader.ReadByte(); break;
                    case 21: RecruitingLevel = reader.ReadByte(); break;
                    case 22: ResourcingLevel = reader.ReadByte(); break;
                    case 23: TradingLevel = reader.ReadByte(); break;
                    case 24: TrainingLevel = reader.ReadByte(); break;
                    case 25: GuilderCountMax = reader.ReadInt(); break;
                    case 26: FundDaily = reader.ReadLong(); break;
                    case 27: FundWeekly = reader.ReadLong(); break;
                    case 28: CommodityUnlock1 = reader.ReadLong(); break;
                    case 29: CommodityUnlock2 = reader.ReadLong(); break;
                    case 30: CommodityUnlock3 = reader.ReadLong(); break;
                    case 31: FundDailyTime = reader.ReadInt(); break;
                    case 32: FundWeeklyTime = reader.ReadInt(); break;
                    case 33: ResC = reader.ReadInt(); break;
                    case 34: ResD = reader.ReadInt(); break;
                    case 35: ResADaily = reader.ReadInt(); break;
                    case 36: ResBDaily = reader.ReadInt(); break;
                    case 37: ResCDaily = reader.ReadInt(); break;
                    case 38: ResDDaily = reader.ReadInt(); break;
                    case 39: ResAWeekly = reader.ReadInt(); break;
                    case 40: ResBWeekly = reader.ReadInt(); break;
                    case 41: ResCWeekly = reader.ReadInt(); break;
                    case 42: ResDWeekly = reader.ReadInt(); break;
                    case 43: ResAMax = reader.ReadInt(); break;
                    case 44: ResBMax = reader.ReadInt(); break;
                    case 45: ResCMax = reader.ReadInt(); break;
                    case 46: ResDMax = reader.ReadInt(); break;
                    case 47: AddFundWithDay = reader.ReadInt(); break;
                    case 48: ConsumeFundWithDay = reader.ReadInt(); break;
                    case 49: Plot = reader.ReadInt(); break;
                    case 50: Tasks = ReadTlvObject<TlvQuestScheduleData>(reader); break;
                    case 51: Skills = ReadTlvObject<TlvSkills>(reader); break;
                    case 52: SignUpCount = reader.ReadInt(); break;
                    case 53: Stores = ReadTlvObject<TlvGuildTeam>(reader); break;
                    case 54: DepotOpenFlag = ReadTlvObject<TlvStoreData>(reader); break;
                    case 55: RandCommodities = reader.ReadByte(); break;
                    case 56: Quest = ReadTlvObject<TlvCountCtxsB>(reader); break;
                    case 57: QuestPrize = ReadTlvObject<TlvCountPrizesB>(reader); break;
                    case 58: Contribution = reader.ReadInt(); break;
                    case 59: CreateTime = reader.ReadInt(); break;
                    case 60: HuntSoul = reader.ReadInt(); break;
                    case 61: WildHuntCamp = reader.ReadLong(); break;
                    case 62: WildHuntPhase = reader.ReadInt(); break;
                    case 63: CelebrationTaskA = reader.ReadInt(); break;
                    case 64: CelebrationTaskB = reader.ReadInt(); break;
                    case 65: CelebrationTaskC = reader.ReadInt(); break;
                    case 66: CelebrationTaskD = reader.ReadInt(); break;
                    case 67: CelebrationScore = reader.ReadInt(); break;
                    case 68: CelebrationDailyUpdate = reader.ReadInt(); break;
                    case 69: CelebrationWeeklyScoreUpdate = reader.ReadInt(); break;
                    case 70: CelebrationDailyScore = reader.ReadInt(); break;
                    case 71: CelebrationWeeklyRewardUpdate = reader.ReadInt(); break;
                    case 72: GuildWar = reader.ReadInt(); break;
                    case 73: GuildOperateRecord = ReadTlvObject<TlvGuildCommerceData>(reader); break;
                    case 74: GuildBuyRecord = ReadTlvObject<TlvGuildFuncRecords>(reader); break;
                    case 75: SignUps = ReadTlvObject<TlvGuildBuyRecords>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvLong(writer, 2, Id);
            WriteTlvInt(writer, 3, Seed);
            WriteTlvString(writer, 4, Name);
            WriteTlvInt(writer, 5, Icon);
            WriteTlvString(writer, 6, Note);
            WriteTlvInt(writer, 7, Level);
            WriteTlvInt(writer, 8, Exp);
            WriteTlvInt(writer, 9, Repute);
            WriteTlvInt(writer, 10, Fund);
            WriteTlvInt(writer, 11, Arena);
            WriteTlvInt(writer, 12, ResA);
            WriteTlvInt(writer, 13, ResB);
            WriteTlvInt(writer, 14, JoinLevel);
            WriteTlvLong(writer, 15, Leader);
            WriteTlvObject(writer, 16, Titles);
            WriteTlvObject(writer, 17, Guilders);
            WriteTlvObject(writer, 18, Applys);
            WriteTlvLong(writer, 19, FundMax);
            WriteTlvByte(writer, 20, HallLevel);
            WriteTlvByte(writer, 21, RecruitingLevel);
            WriteTlvByte(writer, 22, ResourcingLevel);
            WriteTlvByte(writer, 23, TradingLevel);
            WriteTlvByte(writer, 24, TrainingLevel);
            WriteTlvInt(writer, 25, GuilderCountMax);
            WriteTlvLong(writer, 26, FundDaily);
            WriteTlvLong(writer, 27, FundWeekly);
            WriteTlvLong(writer, 28, CommodityUnlock1);
            WriteTlvLong(writer, 29, CommodityUnlock2);
            WriteTlvLong(writer, 30, CommodityUnlock3);
            WriteTlvInt(writer, 31, FundDailyTime);
            WriteTlvInt(writer, 32, FundWeeklyTime);
            WriteTlvInt(writer, 33, ResC);
            WriteTlvInt(writer, 34, ResD);
            WriteTlvInt(writer, 35, ResADaily);
            WriteTlvInt(writer, 36, ResBDaily);
            WriteTlvInt(writer, 37, ResCDaily);
            WriteTlvInt(writer, 38, ResDDaily);
            WriteTlvInt(writer, 39, ResAWeekly);
            WriteTlvInt(writer, 40, ResBWeekly);
            WriteTlvInt(writer, 41, ResCWeekly);
            WriteTlvInt(writer, 42, ResDWeekly);
            WriteTlvInt(writer, 43, ResAMax);
            WriteTlvInt(writer, 44, ResBMax);
            WriteTlvInt(writer, 45, ResCMax);
            WriteTlvInt(writer, 46, ResDMax);
            WriteTlvInt(writer, 47, AddFundWithDay);
            WriteTlvInt(writer, 48, ConsumeFundWithDay);
            WriteTlvInt(writer, 49, Plot);
            WriteTlvObject(writer, 50, Tasks);
            WriteTlvObject(writer, 51, Skills);
            WriteTlvInt(writer, 52, SignUpCount);
            WriteTlvObject(writer, 53, Stores);
            WriteTlvObject(writer, 54, DepotOpenFlag);
            WriteTlvByte(writer, 55, RandCommodities);
            WriteTlvObject(writer, 56, Quest);
            WriteTlvObject(writer, 57, QuestPrize);
            WriteTlvInt(writer, 58, Contribution);
            WriteTlvInt(writer, 59, CreateTime);
            WriteTlvInt(writer, 60, HuntSoul);
            WriteTlvLong(writer, 61, WildHuntCamp);
            WriteTlvInt(writer, 62, WildHuntPhase);
            WriteTlvInt(writer, 63, CelebrationTaskA);
            WriteTlvInt(writer, 64, CelebrationTaskB);
            WriteTlvInt(writer, 65, CelebrationTaskC);
            WriteTlvInt(writer, 66, CelebrationTaskD);
            WriteTlvInt(writer, 67, CelebrationScore);
            WriteTlvInt(writer, 68, CelebrationDailyUpdate);
            WriteTlvInt(writer, 69, CelebrationWeeklyScoreUpdate);
            WriteTlvInt(writer, 70, CelebrationDailyScore);
            WriteTlvInt(writer, 71, CelebrationWeeklyRewardUpdate);
            WriteTlvInt(writer, 72, GuildWar);
            WriteTlvObject(writer, 73, GuildOperateRecord);
            WriteTlvObject(writer, 74, GuildBuyRecord);
            WriteTlvObject(writer, 75, SignUps);
        }
    }
}
