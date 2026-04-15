using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Arrowgene.MonsterHunterOnline.ClientTools.Quest;
using Arrowgene.MonsterHunterOnline.UI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public sealed partial class QuestListItemViewModel : ViewModelBase
{
    public QuestDef Data { get; }

    public int Id => Data.Id;
    public string Name => Data.Name;
    public string Note => Data.Note;
    public int Star => Data.Star;
    public string TypeLabel => GetTypeLabel(Data.Type);
    public string SourceFile => Data.SourceFile;

    public string Summary
    {
        get
        {
            List<string> parts = [];
            if (Data.Star > 0) parts.Add($"{Data.Star} star");
            string tl = TypeLabel;
            if (!string.IsNullOrEmpty(tl)) parts.Add(tl);
            if (Data.Contents.Count > 0) parts.Add($"{Data.Contents.Count} objective(s)");
            if (Data.Rewards.Count > 0) parts.Add($"{Data.Rewards.Count} reward(s)");
            return parts.Count > 0 ? string.Join(" | ", parts) : "No details";
        }
    }

    public QuestListItemViewModel(QuestDef data)
    {
        Data = data;
    }

    internal static string GetTypeLabel(int type) => type switch
    {
        1 => "Main",
        2 => "Side",
        4 => "Daily",
        8 => "Guide",
        16 => "Event",
        32 => "Farm",
        64 => "Guild",
        128 => "Challenge",
        256 => "Arena",
        512 => "Weekly",
        1024 => "Bounty",
        2048 => "Achievement",
        _ => type > 0 ? $"Type {type}" : "",
    };
}

public sealed partial class QuestViewerViewModel : ViewModelBase
{
    private readonly QuestDataLoader _loader = new();
    private QuestDatabase? _database;
    private List<QuestListItemViewModel> _allQuests = [];

    [ObservableProperty]
    private string _clientFilesPath = string.Empty;

    [ObservableProperty]
    private string _statusText = "Enter the MHO client files directory path and click Load.";

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private QuestListItemViewModel? _selectedQuest;

    [ObservableProperty]
    private string _filterText = string.Empty;

    [ObservableProperty]
    private string _selectedSourceFilter = "All";

    [ObservableProperty]
    private string _selectedTypeFilter = "All";

    public ObservableCollection<QuestListItemViewModel> Quests { get; } = [];
    public ObservableCollection<string> SourceFilters { get; } = ["All"];
    public ObservableCollection<string> TypeFilters { get; } = ["All"];

    public bool HasQuests => Quests.Count > 0;

    // Detail panel properties
    public string DetailName => _selectedQuest?.Data.Name ?? string.Empty;
    public string DetailNote => _selectedQuest?.Data.Note ?? string.Empty;
    public string DetailDescription => StripHtml(_selectedQuest?.Data.Description ?? string.Empty);
    public string DetailCompleteNote => StripHtml(_selectedQuest?.Data.CompleteNote ?? string.Empty);
    public int DetailId => _selectedQuest?.Data.Id ?? 0;
    public int DetailType => _selectedQuest?.Data.Type ?? 0;
    public string DetailTypeLabel => _selectedQuest?.TypeLabel ?? string.Empty;
    public int DetailStar => _selectedQuest?.Data.Star ?? 0;
    public int DetailTimeout => _selectedQuest?.Data.Timeout ?? 0;
    public int DetailAcceptNpc => _selectedQuest?.Data.AcceptNpc ?? 0;
    public int DetailSubmitNpc => _selectedQuest?.Data.SubmitNpc ?? 0;
    public int DetailRelatedNpc => _selectedQuest?.Data.RelatedNpc ?? 0;
    public int DetailPreTask => _selectedQuest?.Data.PreTask ?? 0;
    public int DetailGroup => _selectedQuest?.Data.Group ?? 0;
    public int DetailSeries => _selectedQuest?.Data.Series ?? 0;
    public int DetailStage => _selectedQuest?.Data.Stage ?? 0;
    public bool DetailCanRepeat => _selectedQuest?.Data.CanRepeat ?? false;
    public bool DetailCanShare => _selectedQuest?.Data.CanShare ?? false;
    public bool DetailAutoAccept => _selectedQuest?.Data.AutoAccept ?? false;
    public bool DetailAutoComplete => _selectedQuest?.Data.AutoComplete ?? false;
    public bool DetailIsAlone => _selectedQuest?.Data.IsAlone ?? false;
    public bool DetailCantGiveup => _selectedQuest?.Data.CantGiveup ?? false;
    public bool DetailInvalid => _selectedQuest?.Data.Invalid ?? false;
    public string DetailSourceFile => _selectedQuest?.Data.SourceFile ?? string.Empty;
    public bool HasSelection => _selectedQuest != null;

    public ObservableCollection<string> DetailContents { get; } = [];
    public ObservableCollection<string> DetailRewards { get; } = [];
    public ObservableCollection<string> DetailPreRewards { get; } = [];
    public ObservableCollection<string> DetailChecks { get; } = [];
    public ObservableCollection<string> DetailLoot { get; } = [];

    // NPC name lookups
    public string DetailAcceptNpcDisplay => FormatNpcDisplay(_selectedQuest?.Data.AcceptNpc ?? 0);
    public string DetailSubmitNpcDisplay => FormatNpcDisplay(_selectedQuest?.Data.SubmitNpc ?? 0);
    public string DetailRelatedNpcDisplay => FormatNpcDisplay(_selectedQuest?.Data.RelatedNpc ?? 0);

    // Pre-quest name lookup
    public string DetailPreTaskDisplay
    {
        get
        {
            int preTask = _selectedQuest?.Data.PreTask ?? 0;
            if (preTask == 0) return "None";
            var q = _allQuests.FirstOrDefault(x => x.Id == preTask);
            return q != null ? $"#{preTask} {q.Name}" : $"#{preTask}";
        }
    }

    // Group name lookup
    public string DetailGroupName
    {
        get
        {
            if (_selectedQuest == null || _database == null) return string.Empty;
            var g = _database.Groups.FirstOrDefault(x => x.Id == _selectedQuest.Data.Group);
            return g?.Name ?? string.Empty;
        }
    }

    public string DetailSeriesName
    {
        get
        {
            if (_selectedQuest == null || _database == null) return string.Empty;
            var s = _database.Series.FirstOrDefault(x => x.Id == _selectedQuest.Data.Series);
            return s?.Name ?? string.Empty;
        }
    }

    // Chapter lookup (find chapter that contains this quest's series)
    public string DetailChapterName
    {
        get
        {
            if (_selectedQuest == null || _database == null) return string.Empty;
            int seriesId = _selectedQuest.Data.Series;
            if (seriesId == 0) return string.Empty;
            var ch = _database.Chapters.FirstOrDefault(c => c.Series.Contains(seriesId));
            return ch?.Name ?? string.Empty;
        }
    }

    public int DetailChapterId
    {
        get
        {
            if (_selectedQuest == null || _database == null) return 0;
            int seriesId = _selectedQuest.Data.Series;
            if (seriesId == 0) return 0;
            var ch = _database.Chapters.FirstOrDefault(c => c.Series.Contains(seriesId));
            return ch?.Id ?? 0;
        }
    }

    // Library lookup (find library whose groups contain this quest's group)
    public string DetailLibraryName
    {
        get
        {
            if (_selectedQuest == null || _database == null) return string.Empty;
            int groupId = _selectedQuest.Data.Group;
            if (groupId == 0) return string.Empty;
            var lib = _database.Libraries.FirstOrDefault(l => l.Groups.Contains(groupId));
            return lib?.Name ?? string.Empty;
        }
    }

    public bool HasPreRewards => DetailPreRewards.Count > 0;
    public bool HasLoot => DetailLoot.Count > 0;

    partial void OnSelectedQuestChanged(QuestListItemViewModel? value)
    {
        UpdateDetailPanel();
    }

    partial void OnFilterTextChanged(string value) => ApplyFilter();
    partial void OnSelectedSourceFilterChanged(string value) => ApplyFilter();
    partial void OnSelectedTypeFilterChanged(string value) => ApplyFilter();

    private void UpdateDetailPanel()
    {
        OnPropertyChanged(nameof(DetailName));
        OnPropertyChanged(nameof(DetailNote));
        OnPropertyChanged(nameof(DetailDescription));
        OnPropertyChanged(nameof(DetailCompleteNote));
        OnPropertyChanged(nameof(DetailId));
        OnPropertyChanged(nameof(DetailType));
        OnPropertyChanged(nameof(DetailTypeLabel));
        OnPropertyChanged(nameof(DetailStar));
        OnPropertyChanged(nameof(DetailTimeout));
        OnPropertyChanged(nameof(DetailAcceptNpc));
        OnPropertyChanged(nameof(DetailSubmitNpc));
        OnPropertyChanged(nameof(DetailRelatedNpc));
        OnPropertyChanged(nameof(DetailAcceptNpcDisplay));
        OnPropertyChanged(nameof(DetailSubmitNpcDisplay));
        OnPropertyChanged(nameof(DetailRelatedNpcDisplay));
        OnPropertyChanged(nameof(DetailPreTask));
        OnPropertyChanged(nameof(DetailPreTaskDisplay));
        OnPropertyChanged(nameof(DetailGroup));
        OnPropertyChanged(nameof(DetailGroupName));
        OnPropertyChanged(nameof(DetailSeries));
        OnPropertyChanged(nameof(DetailSeriesName));
        OnPropertyChanged(nameof(DetailChapterName));
        OnPropertyChanged(nameof(DetailChapterId));
        OnPropertyChanged(nameof(DetailLibraryName));
        OnPropertyChanged(nameof(DetailStage));
        OnPropertyChanged(nameof(DetailCanRepeat));
        OnPropertyChanged(nameof(DetailCanShare));
        OnPropertyChanged(nameof(DetailAutoAccept));
        OnPropertyChanged(nameof(DetailAutoComplete));
        OnPropertyChanged(nameof(DetailIsAlone));
        OnPropertyChanged(nameof(DetailCantGiveup));
        OnPropertyChanged(nameof(DetailInvalid));
        OnPropertyChanged(nameof(DetailSourceFile));
        OnPropertyChanged(nameof(HasSelection));

        DetailContents.Clear();
        DetailRewards.Clear();
        DetailPreRewards.Clear();
        DetailChecks.Clear();
        DetailLoot.Clear();

        if (_selectedQuest == null) return;

        foreach (var c in _selectedQuest.Data.Contents)
        {
            DetailContents.Add(FormatContent(c));
        }

        foreach (var r in _selectedQuest.Data.Rewards)
        {
            DetailRewards.Add(FormatReward(r));
        }

        foreach (var r in _selectedQuest.Data.PreRewards)
        {
            DetailPreRewards.Add(FormatReward(r));
        }

        foreach (var ch in _selectedQuest.Data.AcceptChecks)
        {
            DetailChecks.Add(FormatCheck(ch));
        }

        // Loot entries for this quest
        if (_database != null)
        {
            foreach (var loot in _database.LootEntries.Where(l => l.QuestId == _selectedQuest.Data.Id))
            {
                string amount = loot.ItemAmountMin == loot.ItemAmountMax
                    ? $"x{loot.ItemAmountMin}"
                    : $"x{loot.ItemAmountMin}-{loot.ItemAmountMax}";
                string diff = loot.Difficulty >= 0 ? $" (Difficulty {loot.Difficulty})" : "";
                string monsterName = LookupMonster(loot.MonsterId);
                string itemName = LookupItem(loot.ItemId);
                DetailLoot.Add($"{monsterName} drops {itemName} {amount}{diff}");
            }
        }

        OnPropertyChanged(nameof(HasPreRewards));
        OnPropertyChanged(nameof(HasLoot));
    }

    private void ApplyFilter()
    {
        Quests.Clear();

        string filter = FilterText?.Trim() ?? string.Empty;
        string sourceFilter = SelectedSourceFilter;
        string typeFilter = SelectedTypeFilter;

        foreach (var q in _allQuests)
        {
            if (sourceFilter != "All" && q.SourceFile != sourceFilter)
                continue;

            if (typeFilter != "All" && q.TypeLabel != typeFilter)
                continue;

            if (!string.IsNullOrEmpty(filter))
            {
                bool matchId = q.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
                bool matchName = q.Name.Contains(filter, StringComparison.OrdinalIgnoreCase);
                bool matchNote = q.Note.Contains(filter, StringComparison.OrdinalIgnoreCase);
                if (!matchId && !matchName && !matchNote)
                    continue;
            }

            Quests.Add(q);
        }

        OnPropertyChanged(nameof(HasQuests));
        StatusText = $"Showing {Quests.Count} of {_allQuests.Count} quests";
    }

    public async Task LoadAsync(string path)
    {
        ClientFilesPath = path;
        IsLoading = true;
        StatusText = "Loading quest data...";
        Quests.Clear();
        _allQuests.Clear();
        SourceFilters.Clear();
        SourceFilters.Add("All");
        TypeFilters.Clear();
        TypeFilters.Add("All");
        OnPropertyChanged(nameof(HasQuests));

        try
        {
            _database = await Task.Run(() => _loader.Load(path));

            HashSet<string> sources = [];
            HashSet<string> types = [];

            foreach (QuestDef quest in _database.Quests.OrderBy(q => q.Id))
            {
                var vm = new QuestListItemViewModel(quest);
                _allQuests.Add(vm);

                if (!string.IsNullOrEmpty(quest.SourceFile))
                    sources.Add(quest.SourceFile);

                string tl = vm.TypeLabel;
                if (!string.IsNullOrEmpty(tl))
                    types.Add(tl);
            }

            foreach (string s in sources.OrderBy(x => x))
                SourceFilters.Add(s);

            foreach (string t in types.OrderBy(x => x))
                TypeFilters.Add(t);

            SelectedSourceFilter = "All";
            SelectedTypeFilter = "All";
            FilterText = string.Empty;
            ApplyFilter();

            StatusText = $"Loaded {_allQuests.Count} quests, {_database.Npcs.Count} NPCs, " +
                         $"{_database.MonsterNames.Count} monsters, {_database.ItemNames.Count} items, " +
                         $"{_database.LevelNames.Count} levels, {_database.LootEntries.Count} loot entries";

            if (Quests.Count > 0)
                SelectedQuest = Quests[0];
        }
        catch (Exception ex)
        {
            StatusText = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private string FormatNpcDisplay(int npcId)
    {
        if (npcId == 0) return "None";
        if (_database != null && _database.Npcs.TryGetValue(npcId, out var npc))
        {
            string display = $"#{npcId} {npc.Name}";
            if (!string.IsNullOrEmpty(npc.Title))
                display += $" {npc.Title}";
            return display;
        }
        return $"#{npcId}";
    }

    private string FormatContent(QuestContent c)
    {
        string location = FormatLocationFilters(c.EventFilters);

        return c.ContentType switch
        {
            "TaskLevelFinishContentDef" =>
                $"Complete Level {LookupLevel(c.Level)}" +
                FormatAppraisal(c.Appraisal) +
                (c.Count > 1 ? $" x{c.Count}" : "") +
                (c.Time > 0 ? $" within {c.Time}s" : ""),
            "TaskTalkContentDef" =>
                $"Talk to NPC {FormatNpcDisplay(c.Npc)}",
            "TaskUseItemContentDef" =>
                $"Use {LookupItem(c.Item)} x{c.ItemCount}",
            "TaskSubmitContentDef" =>
                $"Submit {LookupItem(c.Item)} x{c.ItemCount}",
            "TaskKillContentDef" =>
                $"Kill x{c.Count}",
            "TaskCollectContentDef" =>
                $"Collect {LookupItem(c.Item)} x{c.ItemCount}",
            "TaskMonsterPartBrokenContentDef" =>
                $"Break {LookupMonster(c.Monster)} Part {c.Part}{location}",
            "TaskMonsterAbnormalContentDef" =>
                $"Inflict {FormatAbnormal(c.Abnormal)} on {LookupMonster(c.Monster)}{location}",
            "TaskCaptureContentDef" =>
                $"Capture {LookupMonster(c.Monster)} x{c.Count}{location}",
            "TaskHunterContentDef" =>
                $"Hunt {LookupMonster(c.Monster)} x{c.Count}",
            "TaskCommonContentDef" =>
                $"Complete Objective (type={c.Content}) x{c.Count}" +
                (c.Arg1 != 0 ? $" [args: {c.Arg1},{c.Arg2},{c.Arg3}]" : ""),
            "TaskGatherContentDef" =>
                $"Gather {LookupItem(c.Item)} x{c.Count}" +
                (c.Source > 0 ? $" from Source {c.Source}" : ""),
            "TaskFarmGatherContentDef" =>
                $"Farm Gather x{c.Count}" +
                (c.ColletionPoint > 0 ? $" at Point {c.ColletionPoint}" : "") +
                (c.IsFriend ? " (friend's farm)" : ""),
            "TaskFarmGatherAnyContentDef" =>
                $"Farm Gather (any) x{c.Count}" +
                (c.IsFriend ? " (friend's farm)" : ""),
            "TaskLevelUpContentDef" =>
                $"Reach Level {c.Level}",
            "TaskFarmLevelUpContentDef" =>
                $"Upgrade Farm to Level {c.Level}" +
                (c.ColletionPoint > 0 ? $" (Point {c.ColletionPoint})" : ""),
            "TaskFarmLevelUpAnyContentDef" =>
                $"Upgrade any Farm Facility to Level {c.Level}",
            "TaskPetTrainingContentDef" =>
                $"Train Pet at Facility {c.Facility} x{c.Count}",
            "TaskPetEmploymentContentDef" =>
                "Employ a Pet",
            "TaskFarmPetLevelUpContentDef" =>
                $"Upgrade Pet Facility {c.Facility} to Level {c.Level}",
            "TaskFarmPetLevelUpAnyContentDef" =>
                $"Upgrade any Pet Facility to Level {c.Level}",
            "TaskHunterStarFinishCardContentDef" =>
                $"Complete Hunter Star Card #{c.Card}",
            "TaskStatisticsContentDef" =>
                $"Achieve Statistic #{c.StatId} x{c.Count}",
            "TaskAccWeaponTrialContentDef" =>
                $"Complete Weapon Trial x{c.Count}",
            "TaskSoulStoneLevelContentDef" =>
                $"Reach Soul Stone Level {c.Level}",
            "TaskAccExpressionLearnContentDef" =>
                $"Learn Expressions x{c.Count}",
            _ => $"{c.ContentType} (id={c.Id})",
        };
    }

    private static string FormatAppraisal(int appraisal) => appraisal switch
    {
        0 => "",
        1 => " (any rating)",
        2 => " (Silver+)",
        3 => " (Gold+)",
        4 => " (Platinum)",
        _ => $" (rating >= {appraisal})",
    };

    private static string FormatAbnormal(int abnormal) => abnormal switch
    {
        1 => "Poison",
        2 => "Paralysis",
        3 => "Sleep",
        4 => "Stun",
        5 => "Blast",
        6 => "Exhaustion",
        7 => "Mount",
        8 => "Pitfall",
        9 => "Shock Trap",
        10 => "Flash",
        11 => "Knockdown",
        12 => "Flinch",
        _ => $"#{abnormal}",
    };

    private static string FormatLocationFilters(List<QuestLocationFilter> filters)
    {
        if (filters.Count == 0) return "";
        var parts = new List<string>();
        foreach (var f in filters)
        {
            if (f.Scene > 0) parts.Add($"Scene {f.Scene}");
            else if (f.Map > 0) parts.Add($"Map {f.Map}");
        }
        return parts.Count > 0 ? $" in {string.Join(", ", parts)}" : "";
    }

    private string FormatReward(QuestReward r)
    {
        return r.RewardType switch
        {
            "TaskGoldPrizeDef" =>
                $"Gold: {r.Money}" +
                (r.BoundMoney > 0 ? $" + {r.BoundMoney} bound" : "") +
                (r.Ratio > 0 ? $" (ratio {r.Ratio})" : ""),
            "TaskExpPrizeDef" =>
                $"EXP: {r.Exp}" +
                (r.Ratio > 0 ? $" (ratio {r.Ratio})" : ""),
            "TaskFarmExpPrizeDef" =>
                $"Farm EXP: {r.FarmExp}" +
                (r.Ratio > 0 ? $" (ratio {r.Ratio})" : ""),
            "TaskItemsPrizeDef" =>
                $"Items: {string.Join(", ", r.Items.Select(i => FormatItemReward(i)))}",
            "TaskItemPrizeDef" =>
                r.SingleItem != null
                    ? $"Item: {FormatItemReward(r.SingleItem)}"
                    : "Item: (empty)",
            "TaskHuntSoulPrizeDef" =>
                $"Hunt Soul: {r.HuntSoul}",
            "TaskSpoorPrizeDef" =>
                $"Spoor Points: {r.SpoorPoint}",
            "TaskContributionPrizeDef" =>
                $"Contribution: {r.Contribution}",
            "TaskGuildPrizeDef" =>
                $"Guild: Contribution {r.GuildContribution}, EXP {r.GuildExp}, Fund {r.GuildFund}",
            "TaskGuildCelebrationScoreDef" =>
                $"Guild Celebration Score: +{r.AddScore}",
            "TaskItemReclaimDef" =>
                $"Reclaim Items: {string.Join(", ", r.Items.Select(i => FormatItemReward(i)))}",
            _ => r.RewardType,
        };
    }

    private string FormatItemReward(QuestItemReward i)
    {
        string bind = i.BindType switch
        {
            1 => " [BoE]",
            2 => " [BoP]",
            _ => "",
        };
        return $"{LookupItem(i.Item)} x{i.Count}{bind}";
    }

    private string FormatCheck(QuestCheck ch)
    {
        return ch.CheckType switch
        {
            "TaskCharLevelCheckDef" =>
                $"Character Level {ch.LevelMin}-{ch.LevelMax}",
            "TaskPreTaskCheckDef" =>
                $"Pre-quests: {string.Join(", ", ch.PreTasks.Select(p => FormatPreTask(p)))}",
            "TaskMerLvCheckDef" =>
                $"Mercenary Level {ch.MerLvMin}-{ch.MerLvMax}",
            "TaskHRLevelCheckDef" =>
                $"HR Level {ch.HRLevelMin}-{ch.HRLevelMax}",
            "TaskActivityCheckDef" =>
                $"Activity #{ch.Activity} active",
            "TaskHRCardCheckDef" =>
                $"HR Card #{ch.Card} required",
            "TaskGroupMutexDef" =>
                $"Group Limit: {ch.GroupLimit}",
            _ => ch.CheckType,
        };
    }

    private string FormatPreTask(QuestPreTask p)
    {
        var q = _allQuests.FirstOrDefault(x => x.Id == p.Task);
        string name = q != null ? $" {q.Name}" : "";
        return $"#{p.Task}{name} x{p.Count}";
    }

    private string LookupMonster(int monsterId)
    {
        if (monsterId == 0) return "Unknown";
        if (_database != null && _database.MonsterNames.TryGetValue(monsterId, out var name))
            return $"{name} (#{monsterId})";
        return $"Monster #{monsterId}";
    }

    private string LookupItem(int itemId)
    {
        if (itemId == 0) return "Unknown";
        if (_database != null && _database.ItemNames.TryGetValue(itemId, out var name))
            return $"{name} (#{itemId})";
        return $"Item #{itemId}";
    }

    private string LookupLevel(int levelId)
    {
        if (levelId == 0) return "Unknown";
        if (_database != null && _database.LevelNames.TryGetValue(levelId, out var name))
            return $"{name} (#{levelId})";
        return $"#{levelId}";
    }

    private static string StripHtml(string html)
    {
        if (string.IsNullOrEmpty(html)) return html;
        string result = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
        result = result.Replace("&lt;", "<").Replace("&gt;", ">")
                       .Replace("&amp;", "&").Replace("&quot;", "\"");
        return result;
    }
}
