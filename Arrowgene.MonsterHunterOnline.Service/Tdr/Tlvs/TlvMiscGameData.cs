using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for miscellaneous game data (items, cuisines, titles, settings, etc).
    /// C++ Reader: crygame.dll+sub_10196960 (UnkTlv0170)
    /// C++ Printer: crygame.dll+sub_10199DE0
    /// </summary>
    public class TlvMiscGameData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 6: ItemColumnData = ReadTlvObject<TlvEquipPlanList>(reader); break;
                    case 10: CreditCount = reader.ReadInt(); break;
                    case 11: CreditValue = reader.ReadInt(); break;
                    case 12: GuideStepCount = reader.ReadShort(); break;
                    case 13: GuideSteps = ReadTlvObject<TlvStepState>(reader); break;
                    case 16: MailInfo = ReadTlvObject<TlvMailSendStats>(reader); break;
                    case 17: SchedulePrizeInfo = ReadTlvObject<TlvPrizeState>(reader); break;
                    case 18: GiftBag = ReadTlvObject<TlvGiftData>(reader); break;
                    case 19: NpcAtd = ReadTlvObject<TlvNpcOrgPrefsContainer>(reader); break;
                    case 20: CatCuisineId = reader.ReadInt(); break;
                    case 21: CatCuisineCount = reader.ReadShort(); break;
                    case 22: CatCuisineLevel = reader.ReadByte(); break;
                    case 23: CatCuisineBuffs = reader.ReadByte(); break;
                    case 24: CatCuisineLastTime = reader.ReadInt(); break;
                    case 28: EquipTitle = reader.ReadInt(); break;
                    case 29: EquipTitleBuff = reader.ReadInt(); break;
                    case 30: Guild = ReadTlvObject<TlvGuildContributionData>(reader); break;
                    case 31: VideoSize = reader.ReadShort(); break;
                    case 32: Video = reader.ReadByte(); break;
                    case 33: ClientSettings = ReadTlvObject<TlvClientSettingsData>(reader); break;
                    case 42: Spoor = ReadTlvObject<TlvPointsCtxPrizes>(reader); break;
                    case 43: RapidHunt = ReadTlvObject<TlvAwardsState>(reader); break;
                    case 47: Activity = ReadTlvObject<TlvScriptProcData>(reader); break;
                    case 52: ItemRebuild = ReadTlvObject<TlvItemRebuildData>(reader); break;
                    case 53: ItemBox = ReadTlvObject<TlvRelicBoxesContainer>(reader); break;
                    case 54: Shop = ReadTlvObject<TlvShopDataContainer>(reader); break;
                    case 55: CatTreature = ReadTlvObject<TlvCatCuisineData>(reader); break;
                    case 56: NormalLimitInfo = ReadTlvObject<TlvBuyLimitContainer>(reader); break;
                    case 57: ReportInfo = ReadTlvObject<TlvPlayerReportData>(reader); break;
                    case 58: Trace = ReadTlvObject<TlvTypeTraceList>(reader); break;
                    case 59: EquipPlan = ReadTlvObject<TlvEquipPlanList>(reader); break;
                    case 60: reader.ReadInt(); break;
                    case 61: ShortcutData = ReadTlvIntArray(reader); break;
                    case 62: reader.ReadInt(); break;
                    case 63: CatCuisineFormulaIds = ReadTlvIntArray(reader); break;
                    case 64: CatCuisineFormulaState = ReadTlvIntArray(reader); break;
                    case 69: reader.ReadInt(); break;
                    case 70: TitleIds = ReadTlvIntArray(reader); break;
                    case 71: TitleUnlockTimes = ReadTlvIntArray(reader); break;
                    case 72: StarStoneData = ReadTlvObject<TlvElementExp>(reader); break;
                    case 73: DataRepairFlag = reader.ReadLong(); break;
                    case 74: ItemSnapshot = ReadTlvObject<TlvSnapItemData>(reader); break;
                    case 75: PersonalLeagueData = ReadTlvObject<TlvSeasonStats>(reader); break;
                    case 76: Speak = ReadTlvObject<TlvChatSpeakData>(reader); break;
                    case 77: SupplyPlanData = ReadTlvObject<TlvSupplyPlanList>(reader); break;
                    case 78: reader.ReadInt(); break;
                    case 79: CdGroupIds = ReadTlvIntArray(reader); break;
                    case 80: CdTypes = ReadTlvByteArray(reader); break;
                    case 81: CdTimes = ReadTlvIntArray(reader); break;
                    case 82: CreditVersion = reader.ReadInt(); break;
                    case 83: EquipSuitSkill = ReadTlvObject<TlvSuitSkillGroups>(reader); break;
                    case 84: WeaponTrial = ReadTlvObject<TlvWeaponRecord>(reader); break;
                    case 85: Astrolabe = ReadTlvObject<TlvArenaSeasonTaskData>(reader); break;
                    case 86: CreditNoChangeCount = reader.ReadInt(); break;
                    case 87: WildHunt = ReadTlvObject<TlvTaskResetData>(reader); break;
                    case 88: SoulStone = ReadTlvObject<TlvSoulBeastSystemData>(reader); break;
                    case 89: Monolopy = ReadTlvObject<TlvFixedTimesBlock>(reader); break;
                    case 90: GrowHigher = ReadTlvObject<TlvDailyStats>(reader); break;
                    case 91: Achieve = ReadTlvObject<TlvCompleteBit>(reader); break;
                    case 92: Illustrate = ReadTlvObject<TlvCompleteBitCards>(reader); break;
                    case 93: WeaponStyle = ReadTlvObject<TlvWeaponStyleData>(reader); break;
                    case 94: reader.ReadInt(); break;
                    case 95: WeaponHavenInfo = ReadTlvByteArray(reader); break;
                    case 96: SilverStorageBox = ReadTlvObject<TlvSilverStats>(reader); break;
                    case 97: GuideBook = ReadTlvObject<TlvGuideBookData>(reader); break;
                    case 98: MonsterTalkData = ReadTlvObject<TlvSearchItemPool>(reader); break;
                    case 99: SecretResearchData = ReadTlvObject<TlvLotteryBoxContainer>(reader); break;
                    case 100: DragonBoxShopId = reader.ReadInt(); break;
                    case 102: RewardActivityCost = reader.ReadInt(); break;
                    case 103: reader.ReadInt(); break;
                    case 104: RewardActivityState = ReadTlvList<TlvIndexState>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 6, ItemColumnData);
            WriteTlvInt(writer, 10, CreditCount);
            WriteTlvInt(writer, 11, CreditValue);
            WriteTlvShort(writer, 12, GuideStepCount);
            WriteTlvObject(writer, 13, GuideSteps);
            WriteTlvObject(writer, 16, MailInfo);
            WriteTlvObject(writer, 17, SchedulePrizeInfo);
            WriteTlvObject(writer, 18, GiftBag);
            WriteTlvObject(writer, 19, NpcAtd);
            WriteTlvInt(writer, 20, CatCuisineId);
            WriteTlvShort(writer, 21, CatCuisineCount);
            WriteTlvByte(writer, 22, CatCuisineLevel);
            WriteTlvByte(writer, 23, CatCuisineBuffs);
            WriteTlvInt(writer, 24, CatCuisineLastTime);
            WriteTlvInt(writer, 28, EquipTitle);
            WriteTlvInt(writer, 29, EquipTitleBuff);
            WriteTlvObject(writer, 30, Guild);
            WriteTlvShort(writer, 31, VideoSize);
            WriteTlvByte(writer, 32, Video);
            WriteTlvObject(writer, 33, ClientSettings);
            WriteTlvObject(writer, 42, Spoor);
            WriteTlvObject(writer, 43, RapidHunt);
            WriteTlvObject(writer, 47, Activity);
            WriteTlvObject(writer, 52, ItemRebuild);
            WriteTlvObject(writer, 53, ItemBox);
            WriteTlvObject(writer, 54, Shop);
            WriteTlvObject(writer, 55, CatTreature);
            WriteTlvObject(writer, 56, NormalLimitInfo);
            WriteTlvObject(writer, 57, ReportInfo);
            WriteTlvObject(writer, 58, Trace);
            WriteTlvObject(writer, 59, EquipPlan);
            WriteTlvInt(writer, 60, ShortcutCount);
            WriteTlvIntArray(writer, 61, ShortcutData);
            WriteTlvInt(writer, 62, CatCuisineFormulaCount);
            WriteTlvIntArray(writer, 63, CatCuisineFormulaIds);
            WriteTlvIntArray(writer, 64, CatCuisineFormulaState);
            WriteTlvInt(writer, 69, TitleInfoCount);
            WriteTlvIntArray(writer, 70, TitleIds);
            WriteTlvIntArray(writer, 71, TitleUnlockTimes);
            WriteTlvObject(writer, 72, StarStoneData);
            WriteTlvLong(writer, 73, DataRepairFlag);
            WriteTlvObject(writer, 74, ItemSnapshot);
            WriteTlvObject(writer, 75, PersonalLeagueData);
            WriteTlvObject(writer, 76, Speak);
            WriteTlvObject(writer, 77, SupplyPlanData);
            WriteTlvInt(writer, 78, CdCount);
            WriteTlvIntArray(writer, 79, CdGroupIds);
            WriteTlvByteArray(writer, 80, CdTypes);
            WriteTlvIntArray(writer, 81, CdTimes);
            WriteTlvInt(writer, 82, CreditVersion);
            WriteTlvObject(writer, 83, EquipSuitSkill);
            WriteTlvObject(writer, 84, WeaponTrial);
            WriteTlvObject(writer, 85, Astrolabe);
            WriteTlvInt(writer, 86, CreditNoChangeCount);
            WriteTlvObject(writer, 87, WildHunt);
            WriteTlvObject(writer, 88, SoulStone);
            WriteTlvObject(writer, 89, Monolopy);
            WriteTlvObject(writer, 90, GrowHigher);
            WriteTlvObject(writer, 91, Achieve);
            WriteTlvObject(writer, 92, Illustrate);
            WriteTlvObject(writer, 93, WeaponStyle);
            WriteTlvInt(writer, 94, WeaponHavenInfoCount);
            WriteTlvByteArray(writer, 95, WeaponHavenInfo);
            WriteTlvObject(writer, 96, SilverStorageBox);
            WriteTlvObject(writer, 97, GuideBook);
            WriteTlvObject(writer, 98, MonsterTalkData);
            WriteTlvObject(writer, 99, SecretResearchData);
            WriteTlvInt(writer, 100, DragonBoxShopId);
            WriteTlvInt(writer, 102, RewardActivityCost);
            WriteTlvInt(writer, 103, RewardActivityCount);
            WriteTlvList(writer, 104, RewardActivityState);
        }
    }
}
