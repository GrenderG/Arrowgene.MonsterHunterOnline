using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for miscellaneous game data (items, cuisines, titles, settings, etc).
    /// C++ Reader: crygame.dll+sub_10196960 (UnkTlv0170)
    /// C++ Printer: crygame.dll+sub_10199DE0
    /// </summary>
    public class TlvMiscGameData : Structure, ITlvStructure
    {
        /// <summary>Field ID: 6</summary>
        public TlvEquipPlanList ItemColumnData { get; set; } = new();

        /// <summary>Field ID: 10</summary>
        public int CreditCount { get; set; }

        /// <summary>Field ID: 11</summary>
        public int CreditValue { get; set; }

        /// <summary>Field ID: 12</summary>
        public short GuideStepCount { get; set; }

        /// <summary>Field ID: 13</summary>
        public TlvStepState GuideSteps { get; set; } = new();

        /// <summary>Field ID: 16</summary>
        public TlvMailSendStats MailInfo { get; set; } = new();

        /// <summary>Field ID: 17</summary>
        public TlvPrizeState SchedulePrizeInfo { get; set; } = new();

        /// <summary>Field ID: 18</summary>
        public TlvGiftData GiftBag { get; set; } = new();

        /// <summary>Field ID: 19</summary>
        public TlvNpcOrgPrefsContainer NpcAtd { get; set; } = new();

        /// <summary>Field ID: 20</summary>
        public int CatCuisineId { get; set; }

        /// <summary>Field ID: 21</summary>
        public short CatCuisineCount { get; set; }

        /// <summary>Field ID: 22</summary>
        public byte CatCuisineLevel { get; set; }

        /// <summary>Field ID: 23</summary>
        public byte CatCuisineBuffs { get; set; }

        /// <summary>Field ID: 24</summary>
        public int CatCuisineLastTime { get; set; }

        /// <summary>Field ID: 28</summary>
        public int EquipTitle { get; set; }

        /// <summary>Field ID: 29</summary>
        public int EquipTitleBuff { get; set; }

        /// <summary>Field ID: 30</summary>
        public TlvGuildContributionData Guild { get; set; } = new();

        /// <summary>Field ID: 31</summary>
        public short VideoSize { get; set; }

        /// <summary>Field ID: 32</summary>
        public byte Video { get; set; }

        /// <summary>Field ID: 33</summary>
        public TlvClientSettingsData ClientSettings { get; set; } = new();

        /// <summary>Field ID: 42</summary>
        public TlvPointsCtxPrizes Spoor { get; set; } = new();

        /// <summary>Field ID: 43</summary>
        public TlvAwardsState RapidHunt { get; set; } = new();

        /// <summary>Field ID: 47</summary>
        public TlvScriptProcData Activity { get; set; } = new();

        /// <summary>Field ID: 52</summary>
        public TlvItemRebuildData ItemRebuild { get; set; } = new();

        /// <summary>Field ID: 53</summary>
        public TlvRelicBoxesContainer ItemBox { get; set; } = new();

        /// <summary>Field ID: 54</summary>
        public TlvShopDataContainer Shop { get; set; } = new();

        /// <summary>Field ID: 55</summary>
        public TlvCatCuisineData CatTreature { get; set; } = new();

        /// <summary>Field ID: 56</summary>
        public TlvBuyLimitContainer NormalLimitInfo { get; set; } = new();

        /// <summary>Field ID: 57</summary>
        public TlvPlayerReportData ReportInfo { get; set; } = new();

        /// <summary>Field ID: 58</summary>
        public TlvTypeTraceList Trace { get; set; } = new();

        /// <summary>Field ID: 59</summary>
        public TlvEquipPlanList EquipPlan { get; set; } = new();

        /// <summary>Count (derived). Field ID: 60</summary>
        public int ShortcutCount => ShortcutData?.Length ?? 0;

        /// <summary>Field ID: 61</summary>
        public int[] ShortcutData { get; set; }

        /// <summary>Count (derived). Field ID: 62</summary>
        public int CatCuisineFormulaCount => CatCuisineFormulaIds?.Length ?? 0;

        /// <summary>Field ID: 63</summary>
        public int[] CatCuisineFormulaIds { get; set; }

        /// <summary>Field ID: 64</summary>
        public int[] CatCuisineFormulaState { get; set; }

        /// <summary>Count (derived). Field ID: 69</summary>
        public int TitleInfoCount => TitleIds?.Length ?? 0;

        /// <summary>Field ID: 70</summary>
        public int[] TitleIds { get; set; }

        /// <summary>Field ID: 71</summary>
        public int[] TitleUnlockTimes { get; set; }

        /// <summary>Field ID: 72</summary>
        public TlvElementExp StarStoneData { get; set; } = new();

        /// <summary>Field ID: 73</summary>
        public long DataRepairFlag { get; set; }

        /// <summary>Field ID: 74</summary>
        public TlvSnapItemData ItemSnapshot { get; set; } = new();

        /// <summary>Field ID: 75</summary>
        public TlvSeasonStats PersonalLeagueData { get; set; } = new();

        /// <summary>Field ID: 76</summary>
        public TlvChatSpeakData Speak { get; set; } = new();

        /// <summary>Field ID: 77</summary>
        public TlvSupplyPlanList SupplyPlanData { get; set; } = new();

        /// <summary>Count (derived). Field ID: 78</summary>
        public int CdCount => CdGroupIds?.Length ?? 0;

        /// <summary>Field ID: 79</summary>
        public int[] CdGroupIds { get; set; }

        /// <summary>Field ID: 80</summary>
        public byte[] CdTypes { get; set; }

        /// <summary>Field ID: 81</summary>
        public int[] CdTimes { get; set; }

        /// <summary>Field ID: 82</summary>
        public int CreditVersion { get; set; }

        /// <summary>Field ID: 83</summary>
        public TlvSuitSkillGroups EquipSuitSkill { get; set; } = new();

        /// <summary>Field ID: 84</summary>
        public TlvWeaponRecord WeaponTrial { get; set; } = new();

        /// <summary>Field ID: 85</summary>
        public TlvArenaSeasonTaskData Astrolabe { get; set; } = new();

        /// <summary>Field ID: 86</summary>
        public int CreditNoChangeCount { get; set; }

        /// <summary>Field ID: 87</summary>
        public TlvTaskResetData WildHunt { get; set; } = new();

        /// <summary>Field ID: 88</summary>
        public TlvSoulBeastSystemData SoulStone { get; set; } = new();

        /// <summary>Field ID: 89</summary>
        public TlvFixedTimesBlock Monolopy { get; set; } = new();

        /// <summary>Field ID: 90</summary>
        public TlvDailyStats GrowHigher { get; set; } = new();

        /// <summary>Field ID: 91</summary>
        public TlvCompleteBit Achieve { get; set; } = new();

        /// <summary>Field ID: 92</summary>
        public TlvCompleteBitCards Illustrate { get; set; } = new();

        /// <summary>Field ID: 93</summary>
        public TlvWeaponStyleData WeaponStyle { get; set; } = new();

        /// <summary>Count (derived). Field ID: 94</summary>
        public int WeaponHavenInfoCount => WeaponHavenInfo?.Length ?? 0;

        /// <summary>Field ID: 95</summary>
        public byte[] WeaponHavenInfo { get; set; }

        /// <summary>Field ID: 96</summary>
        public TlvSilverStats SilverStorageBox { get; set; } = new();

        /// <summary>Field ID: 97</summary>
        public TlvGuideBookData GuideBook { get; set; } = new();

        /// <summary>Field ID: 98</summary>
        public TlvSearchItemPool MonsterTalkData { get; set; } = new();

        /// <summary>Field ID: 99</summary>
        public TlvLotteryBoxContainer SecretResearchData { get; set; } = new();

        /// <summary>Field ID: 100</summary>
        public int DragonBoxShopId { get; set; }

        /// <summary>Field ID: 102</summary>
        public int RewardActivityCost { get; set; }

        /// <summary>Count (derived). Field ID: 103</summary>
        public int RewardActivityCount => RewardActivityState?.Count ?? 0;

        /// <summary>Field ID: 104</summary>
        public List<TlvIndexState> RewardActivityState { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 6, ItemColumnData);
            WriteTlvInt32(buffer, 10, CreditCount);
            WriteTlvInt32(buffer, 11, CreditValue);
            WriteTlvInt16(buffer, 12, GuideStepCount);
            WriteTlvSubStructure(buffer, 13, GuideSteps);
            WriteTlvSubStructure(buffer, 16, MailInfo);
            WriteTlvSubStructure(buffer, 17, SchedulePrizeInfo);
            WriteTlvSubStructure(buffer, 18, GiftBag);
            WriteTlvSubStructure(buffer, 19, NpcAtd);
            WriteTlvInt32(buffer, 20, CatCuisineId);
            WriteTlvInt16(buffer, 21, CatCuisineCount);
            WriteTlvByte(buffer, 22, CatCuisineLevel);
            WriteTlvByte(buffer, 23, CatCuisineBuffs);
            WriteTlvInt32(buffer, 24, CatCuisineLastTime);
            WriteTlvInt32(buffer, 28, EquipTitle);
            WriteTlvInt32(buffer, 29, EquipTitleBuff);
            WriteTlvSubStructure(buffer, 30, Guild);
            WriteTlvInt16(buffer, 31, VideoSize);
            WriteTlvByte(buffer, 32, Video);
            WriteTlvSubStructure(buffer, 33, ClientSettings);
            WriteTlvSubStructure(buffer, 42, Spoor);
            WriteTlvSubStructure(buffer, 43, RapidHunt);
            WriteTlvSubStructure(buffer, 47, Activity);
            WriteTlvSubStructure(buffer, 52, ItemRebuild);
            WriteTlvSubStructure(buffer, 53, ItemBox);
            WriteTlvSubStructure(buffer, 54, Shop);
            WriteTlvSubStructure(buffer, 55, CatTreature);
            WriteTlvSubStructure(buffer, 56, NormalLimitInfo);
            WriteTlvSubStructure(buffer, 57, ReportInfo);
            WriteTlvSubStructure(buffer, 58, Trace);
            WriteTlvSubStructure(buffer, 59, EquipPlan);
            WriteTlvInt32(buffer, 60, ShortcutCount);
            WriteTlvInt32Arr(buffer, 61, ShortcutData);
            WriteTlvInt32(buffer, 62, CatCuisineFormulaCount);
            WriteTlvInt32Arr(buffer, 63, CatCuisineFormulaIds);
            WriteTlvInt32Arr(buffer, 64, CatCuisineFormulaState);
            WriteTlvInt32(buffer, 69, TitleInfoCount);
            WriteTlvInt32Arr(buffer, 70, TitleIds);
            WriteTlvInt32Arr(buffer, 71, TitleUnlockTimes);
            WriteTlvSubStructure(buffer, 72, StarStoneData);
            WriteTlvInt64(buffer, 73, DataRepairFlag);
            WriteTlvSubStructure(buffer, 74, ItemSnapshot);
            WriteTlvSubStructure(buffer, 75, PersonalLeagueData);
            WriteTlvSubStructure(buffer, 76, Speak);
            WriteTlvSubStructure(buffer, 77, SupplyPlanData);
            WriteTlvInt32(buffer, 78, CdCount);
            WriteTlvInt32Arr(buffer, 79, CdGroupIds);
            WriteTlvByteArr(buffer, 80, CdTypes);
            WriteTlvInt32Arr(buffer, 81, CdTimes);
            WriteTlvInt32(buffer, 82, CreditVersion);
            WriteTlvSubStructure(buffer, 83, EquipSuitSkill);
            WriteTlvSubStructure(buffer, 84, WeaponTrial);
            WriteTlvSubStructure(buffer, 85, Astrolabe);
            WriteTlvInt32(buffer, 86, CreditNoChangeCount);
            WriteTlvSubStructure(buffer, 87, WildHunt);
            WriteTlvSubStructure(buffer, 88, SoulStone);
            WriteTlvSubStructure(buffer, 89, Monolopy);
            WriteTlvSubStructure(buffer, 90, GrowHigher);
            WriteTlvSubStructure(buffer, 91, Achieve);
            WriteTlvSubStructure(buffer, 92, Illustrate);
            WriteTlvSubStructure(buffer, 93, WeaponStyle);
            WriteTlvInt32(buffer, 94, WeaponHavenInfoCount);
            WriteTlvByteArr(buffer, 95, WeaponHavenInfo);
            WriteTlvSubStructure(buffer, 96, SilverStorageBox);
            WriteTlvSubStructure(buffer, 97, GuideBook);
            WriteTlvSubStructure(buffer, 98, MonsterTalkData);
            WriteTlvSubStructure(buffer, 99, SecretResearchData);
            WriteTlvInt32(buffer, 100, DragonBoxShopId);
            WriteTlvInt32(buffer, 102, RewardActivityCost);
            WriteTlvInt32(buffer, 103, RewardActivityCount);
            WriteTlvSubStructureList(buffer, 104, RewardActivityState.Count, RewardActivityState);
        }
    }
}
