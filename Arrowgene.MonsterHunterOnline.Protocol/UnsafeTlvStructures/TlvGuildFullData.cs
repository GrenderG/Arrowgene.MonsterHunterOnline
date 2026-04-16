using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for guild full data.
    /// C++ Reader: crygame.dll+sub_10135C20 (UnkTlv0047)
    /// C++ Printer: crygame.dll+sub_10136E50
    /// </summary>
    public class TlvGuildFullData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt64(buffer, 2, Id);
            WriteTlvInt32(buffer, 3, Seed);
            WriteTlvString(buffer, 4, Name);
            WriteTlvInt32(buffer, 5, Icon);
            WriteTlvString(buffer, 6, Note);
            WriteTlvInt32(buffer, 7, Level);
            WriteTlvInt32(buffer, 8, Exp);
            WriteTlvInt32(buffer, 9, Repute);
            WriteTlvInt32(buffer, 10, Fund);
            WriteTlvInt32(buffer, 11, Arena);
            WriteTlvInt32(buffer, 12, ResA);
            WriteTlvInt32(buffer, 13, ResB);
            WriteTlvInt32(buffer, 14, JoinLevel);
            WriteTlvInt64(buffer, 15, Leader);
            WriteTlvSubStructure(buffer, 16, Titles);
            WriteTlvSubStructure(buffer, 17, Guilders);
            WriteTlvSubStructure(buffer, 18, Applys);
            WriteTlvInt64(buffer, 19, FundMax);
            WriteTlvByte(buffer, 20, HallLevel);
            WriteTlvByte(buffer, 21, RecruitingLevel);
            WriteTlvByte(buffer, 22, ResourcingLevel);
            WriteTlvByte(buffer, 23, TradingLevel);
            WriteTlvByte(buffer, 24, TrainingLevel);
            WriteTlvInt32(buffer, 25, GuilderCountMax);
            WriteTlvInt64(buffer, 26, FundDaily);
            WriteTlvInt64(buffer, 27, FundWeekly);
            WriteTlvInt64(buffer, 28, CommodityUnlock1);
            WriteTlvInt64(buffer, 29, CommodityUnlock2);
            WriteTlvInt64(buffer, 30, CommodityUnlock3);
            WriteTlvInt32(buffer, 31, FundDailyTime);
            WriteTlvInt32(buffer, 32, FundWeeklyTime);
            WriteTlvInt32(buffer, 33, ResC);
            WriteTlvInt32(buffer, 34, ResD);
            WriteTlvInt32(buffer, 35, ResADaily);
            WriteTlvInt32(buffer, 36, ResBDaily);
            WriteTlvInt32(buffer, 37, ResCDaily);
            WriteTlvInt32(buffer, 38, ResDDaily);
            WriteTlvInt32(buffer, 39, ResAWeekly);
            WriteTlvInt32(buffer, 40, ResBWeekly);
            WriteTlvInt32(buffer, 41, ResCWeekly);
            WriteTlvInt32(buffer, 42, ResDWeekly);
            WriteTlvInt32(buffer, 43, ResAMax);
            WriteTlvInt32(buffer, 44, ResBMax);
            WriteTlvInt32(buffer, 45, ResCMax);
            WriteTlvInt32(buffer, 46, ResDMax);
            WriteTlvInt32(buffer, 47, AddFundWithDay);
            WriteTlvInt32(buffer, 48, ConsumeFundWithDay);
            WriteTlvInt32(buffer, 49, Plot);
            WriteTlvSubStructure(buffer, 50, Tasks);
            WriteTlvSubStructure(buffer, 51, Skills);
            WriteTlvInt32(buffer, 52, SignUpCount);
            WriteTlvSubStructure(buffer, 53, Stores);
            WriteTlvSubStructure(buffer, 54, DepotOpenFlag);
            WriteTlvByte(buffer, 55, RandCommodities);
            WriteTlvSubStructure(buffer, 56, Quest);
            WriteTlvSubStructure(buffer, 57, QuestPrize);
            WriteTlvInt32(buffer, 58, Contribution);
            WriteTlvInt32(buffer, 59, CreateTime);
            WriteTlvInt32(buffer, 60, HuntSoul);
            WriteTlvInt64(buffer, 61, WildHuntCamp);
            WriteTlvInt32(buffer, 62, WildHuntPhase);
            WriteTlvInt32(buffer, 63, CelebrationTaskA);
            WriteTlvInt32(buffer, 64, CelebrationTaskB);
            WriteTlvInt32(buffer, 65, CelebrationTaskC);
            WriteTlvInt32(buffer, 66, CelebrationTaskD);
            WriteTlvInt32(buffer, 67, CelebrationScore);
            WriteTlvInt32(buffer, 68, CelebrationDailyUpdate);
            WriteTlvInt32(buffer, 69, CelebrationWeeklyScoreUpdate);
            WriteTlvInt32(buffer, 70, CelebrationDailyScore);
            WriteTlvInt32(buffer, 71, CelebrationWeeklyRewardUpdate);
            WriteTlvInt32(buffer, 72, GuildWar);
            WriteTlvSubStructure(buffer, 73, GuildOperateRecord);
            WriteTlvSubStructure(buffer, 74, GuildBuyRecord);
            WriteTlvSubStructure(buffer, 75, SignUps);
        }
    }
}
