using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Quest;

public sealed class QuestDatabase
{
    public List<QuestDef> Quests { get; } = [];
    public List<QuestLibDef> Libraries { get; } = [];
    public List<QuestGroupDef> Groups { get; } = [];
    public List<QuestChapterDef> Chapters { get; } = [];
    public List<QuestSeriesDef> Series { get; } = [];
    public List<QuestLootEntry> LootEntries { get; } = [];

    /// <summary>NPC ID → NPC info lookup loaded from npcdatanew.dat.</summary>
    public Dictionary<int, NpcInfo> Npcs { get; } = [];

    /// <summary>Monster ID → name lookup loaded from monsterdata.dat.</summary>
    public Dictionary<int, string> MonsterNames { get; } = [];

    /// <summary>Item ID → name lookup loaded from itemdata*.dat.</summary>
    public Dictionary<int, string> ItemNames { get; } = [];

    /// <summary>Level ID → name lookup loaded from lin_entrust.dat.</summary>
    public Dictionary<int, string> LevelNames { get; } = [];
}

public sealed class NpcInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public sealed class QuestDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public int Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Series { get; set; }
    public bool CantGiveup { get; set; }
    public bool AutoAccept { get; set; }
    public int AcceptNpc { get; set; }
    public int SubmitNpc { get; set; }
    public int RelatedNpc { get; set; }
    public int ContentsType { get; set; }
    public bool AutoComplete { get; set; }
    public int Timeout { get; set; }
    public int ResetPeriod { get; set; }
    public string ResetTime { get; set; } = string.Empty;
    public int CountDownType { get; set; }
    public bool CanRepeat { get; set; }
    public bool CanShare { get; set; }
    public bool IsAlone { get; set; }
    public int RepeatCount { get; set; }
    public int AcceptMutexGroup { get; set; }
    public int CompleteMutexGroup { get; set; }
    public string CompleteNote { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int Star { get; set; }
    public int Group { get; set; }
    public int Weight { get; set; }
    public int Rate { get; set; }
    public int Stage { get; set; }
    public bool Invalid { get; set; }
    public int AcceptNpcLocation { get; set; }
    public int PreTask { get; set; }
    public string SourceFile { get; set; } = string.Empty;

    public List<QuestContent> Contents { get; } = [];
    public List<QuestReward> Rewards { get; } = [];
    public List<QuestReward> PreRewards { get; } = [];
    public List<QuestCheck> AcceptChecks { get; } = [];
}

public sealed class QuestContent
{
    public string ContentType { get; set; } = string.Empty;
    public int Id { get; set; }
    public int Order { get; set; }

    // TaskLevelFinishContentDef
    public int Level { get; set; }
    public int Appraisal { get; set; }
    public int Count { get; set; }
    public int Time { get; set; }

    // TaskTalkContentDef
    public int Npc { get; set; }

    // TaskUseItemContentDef / TaskSubmitContentDef / TaskCollectContentDef / TaskGatherContentDef
    public int Item { get; set; }
    public int ItemCount { get; set; }

    // TaskMonsterPartBrokenContentDef / TaskMonsterAbnormalContentDef / TaskCaptureContentDef / TaskHunterContentDef
    public int Monster { get; set; }
    public int Part { get; set; }
    public int Abnormal { get; set; }

    // TaskGatherContentDef
    public int Source { get; set; }

    // TaskCommonContentDef
    public int Content { get; set; }
    public int Arg1 { get; set; }
    public int Arg2 { get; set; }
    public int Arg3 { get; set; }

    // TaskFarmGatherContentDef / TaskFarmLevelUpContentDef
    public int ColletionPoint { get; set; }
    public bool IsFriend { get; set; }

    // TaskPetTrainingContentDef / TaskFarmPetLevelUpContentDef
    public int Facility { get; set; }

    // TaskHunterStarFinishCardContentDef / TaskHRCardCheckDef
    public int Card { get; set; }

    // TaskStatisticsContentDef
    public int StatId { get; set; }

    // EventFilters (for monster-related objectives)
    public List<QuestLocationFilter> EventFilters { get; } = [];
}

public sealed class QuestLocationFilter
{
    public int Map { get; set; }
    public int Group { get; set; }
    public int Scene { get; set; }
    public int Region { get; set; }
}

public sealed class QuestReward
{
    public string RewardType { get; set; } = string.Empty;
    public int Ratio { get; set; }

    // TaskGoldPrizeDef
    public int Money { get; set; }
    public int BoundMoney { get; set; }

    // TaskExpPrizeDef
    public int Exp { get; set; }

    // TaskFarmExpPrizeDef
    public int FarmExp { get; set; }

    // TaskItemsPrizeDef
    public List<QuestItemReward> Items { get; } = [];

    // TaskItemPrizeDef (single item)
    public QuestItemReward? SingleItem { get; set; }

    // TaskHuntSoulPrizeDef
    public int HuntSoul { get; set; }

    // TaskSpoorPrizeDef
    public int SpoorPoint { get; set; }

    // TaskContributionPrizeDef
    public int Contribution { get; set; }

    // TaskGuildPrizeDef
    public int GuildContribution { get; set; }
    public int GuildExp { get; set; }
    public int GuildFund { get; set; }

    // TaskGuildCelebrationScoreDef
    public int AddScore { get; set; }
}

public sealed class QuestItemReward
{
    public int Item { get; set; }
    public int Count { get; set; }
    public int BindType { get; set; }
}

public sealed class QuestCheck
{
    public string CheckType { get; set; } = string.Empty;

    // TaskCharLevelCheckDef
    public int LevelMin { get; set; }
    public int LevelMax { get; set; }

    // TaskPreTaskCheckDef
    public List<QuestPreTask> PreTasks { get; } = [];

    // TaskMerLvCheckDef
    public int MerLvMin { get; set; }
    public int MerLvMax { get; set; }

    // TaskHRLevelCheckDef
    public int HRLevelMin { get; set; }
    public int HRLevelMax { get; set; }

    // TaskActivityCheckDef
    public int Activity { get; set; }

    // TaskHRCardCheckDef
    public int Card { get; set; }

    // TaskGroupMutexDef
    public int GroupLimit { get; set; }
}

public sealed class QuestPreTask
{
    public int Task { get; set; }
    public int Count { get; set; }
}

public sealed class QuestLibDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public int Type { get; set; }
    public int RefreshPeriod { get; set; }
    public string RefreshTime { get; set; } = string.Empty;
    public int CanSelectCount { get; set; }
    public int CanAcceptCount { get; set; }
    public int CanCompleteCount { get; set; }
    public List<int> Groups { get; } = [];
}

public sealed class QuestGroupDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public int LevelMin { get; set; }
    public int LevelMax { get; set; }
    public int Star { get; set; }
    public int Group { get; set; }
    public int FreeHuntStage { get; set; }
    public int Stage { get; set; }
}

public sealed class QuestChapterDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public int LevelMin { get; set; }
    public int LevelMax { get; set; }
    public string Image { get; set; } = string.Empty;
    public List<int> Series { get; } = [];
}

public sealed class QuestSeriesDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}

public sealed class QuestLootEntry
{
    public int MonsterId { get; set; }
    public int LevelMode { get; set; }
    public int Difficulty { get; set; }
    public int QuestId { get; set; }
    public int ItemId { get; set; }
    public int ItemAmountMin { get; set; }
    public int ItemAmountMax { get; set; }
}
