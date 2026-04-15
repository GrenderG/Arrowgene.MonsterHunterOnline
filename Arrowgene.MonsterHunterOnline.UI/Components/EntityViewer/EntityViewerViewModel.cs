using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Arrowgene.MonsterHunterOnline.ClientTools.Entity;
using Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;
using Arrowgene.MonsterHunterOnline.UI.Infrastructure;
using Arrowgene.MonsterHunterOnline.UI.ViewModels;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public sealed class EntityListItemViewModel
{
    public int Id { get; }
    public string Name { get; }
    public string Kind { get; }
    public string Summary { get; }
    public object Data { get; }
    public Bitmap? Icon { get; }

    public EntityListItemViewModel(MonsterDef m, Bitmap? icon)
    {
        Id = m.Id;
        Name = m.Name;
        Kind = "Monster";
        Data = m;
        Icon = icon;
        List<string> parts = [];
        if (!string.IsNullOrEmpty(m.MonsterTypeLabel)) parts.Add(m.MonsterTypeLabel);
        if (m.Parts.Count > 0) parts.Add($"{m.Parts.Count} parts");
        if (m.DifficultyEntries.Count > 0) parts.Add($"{m.DifficultyEntries.Count} difficulties");
        Summary = parts.Count > 0 ? string.Join(" | ", parts) : "";
    }

    public EntityListItemViewModel(NpcDef n)
    {
        Id = n.Id;
        Name = n.Name;
        Kind = "NPC";
        Data = n;
        Icon = null;
        List<string> parts = [];
        if (!string.IsNullOrEmpty(n.Title)) parts.Add(n.Title);
        if (n.Services.Count > 0) parts.Add($"{n.Services.Count} services");
        Summary = parts.Count > 0 ? string.Join(" | ", parts) : "";
    }

    public EntityListItemViewModel(PetDef p, Bitmap? icon)
    {
        Id = p.Id;
        Name = p.Name;
        Kind = "Pet";
        Data = p;
        Icon = icon;
        List<string> parts = [];
        if (!string.IsNullOrEmpty(p.Type)) parts.Add(p.Type);
        if (!string.IsNullOrEmpty(p.Quality)) parts.Add(p.Quality);
        if (!string.IsNullOrEmpty(p.Personality)) parts.Add(p.Personality);
        Summary = parts.Count > 0 ? string.Join(" | ", parts) : "";
    }
}

public sealed partial class EntityViewerViewModel : ViewModelBase
{
    private readonly EntityDataLoader _loader = new();
    private readonly GameIconLoader _icons = new();
    private List<EntityListItemViewModel> _allEntities = [];

    [ObservableProperty] private string _statusText = "Open a data source from the toolbar.";
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private EntityListItemViewModel? _selectedEntity;
    [ObservableProperty] private string _filterText = string.Empty;
    [ObservableProperty] private string _selectedKindFilter = "All";

    public ObservableCollection<EntityListItemViewModel> Entities { get; } = [];
    public ObservableCollection<string> KindFilters { get; } = ["All", "Monster", "NPC", "Pet"];
    public bool HasEntities => Entities.Count > 0;
    public bool HasSelection => _selectedEntity != null;

    // ── Which detail panel to show ──
    public bool IsMonster => _selectedEntity?.Data is MonsterDef;
    public bool IsNpc => _selectedEntity?.Data is NpcDef;
    public bool IsPet => _selectedEntity?.Data is PetDef;

    // ── Common ──
    public string DetailName => _selectedEntity?.Name ?? string.Empty;
    public int DetailId => _selectedEntity?.Id ?? 0;
    public string DetailKind => _selectedEntity?.Kind ?? string.Empty;
    public Bitmap? DetailIcon => _selectedEntity?.Icon;
    public bool HasDetailIcon => _selectedEntity?.Icon != null;

    // ── Monster detail ──
    private MonsterDef? Mon => _selectedEntity?.Data as MonsterDef;
    public string MonType => Mon?.MonsterTypeLabel ?? string.Empty;
    public int MonGroup => Mon?.MonsterGroup ?? 0;
    public string MonEntityName => Mon?.EntityName ?? string.Empty;
    public string MonEntityClass => Mon?.EntityClass ?? string.Empty;
    public int MonRace => Mon?.Race ?? 0;
    public int MonSize => Mon?.Size ?? 0;
    public int MonLootSize => Mon?.LootSize ?? 0;
    public int MonCaptureHp => Mon?.CaptureHpPercent ?? 0;
    public int MonExistTime => Mon?.ExistTime ?? 0;
    public ObservableCollection<string> MonParts { get; } = [];
    public ObservableCollection<string> MonStats { get; } = [];
    public bool HasMonParts => MonParts.Count > 0;
    public bool HasMonStats => MonStats.Count > 0;

    // ── NPC detail ──
    private NpcDef? Npc => _selectedEntity?.Data as NpcDef;
    public string NpcTitle => Npc?.Title ?? string.Empty;
    public string NpcEntityName => Npc?.EntityName ?? string.Empty;
    public string NpcEntityClass => Npc?.EntityClass ?? string.Empty;
    public string NpcCustomType => Npc?.EntityCustomType ?? string.Empty;
    public int NpcAppearTask => Npc?.AppearTask ?? 0;
    public int NpcDisappearTask => Npc?.DisappearTask ?? 0;
    public ObservableCollection<string> NpcServices { get; } = [];
    public bool HasNpcServices => NpcServices.Count > 0;

    // ── Pet detail ──
    private PetDef? Pet => _selectedEntity?.Data as PetDef;
    public string PetType => Pet?.Type ?? string.Empty;
    public string PetQuality => Pet?.Quality ?? string.Empty;
    public string PetPersonality => Pet?.Personality ?? string.Empty;
    public string PetAttackPref => Pet?.AttackPreference ?? string.Empty;
    public string PetAttackStyle => Pet?.AttackStyle ?? string.Empty;
    public int PetLevel => Pet?.Level ?? 0;
    public int PetBurrowHp => Pet?.BurrowHpPercent ?? 0;
    public int PetHealSpeed => Pet?.HealSpeed ?? 0;
    public string PetEntityName => Pet?.EntityName ?? string.Empty;
    public string PetIcon => Pet?.Icon ?? string.Empty;
    public int PetAptitude => Pet?.Aptitude ?? 0;
    public int PetSupportSkill => Pet?.SupportSkill ?? 0;

    partial void OnSelectedEntityChanged(EntityListItemViewModel? value) => UpdateDetailPanel();
    partial void OnFilterTextChanged(string value) => ApplyFilter();
    partial void OnSelectedKindFilterChanged(string value) => ApplyFilter();

    private void UpdateDetailPanel()
    {
        // Notify all detail properties
        OnPropertyChanged(nameof(HasSelection));
        OnPropertyChanged(nameof(IsMonster));
        OnPropertyChanged(nameof(IsNpc));
        OnPropertyChanged(nameof(IsPet));
        OnPropertyChanged(nameof(DetailName));
        OnPropertyChanged(nameof(DetailId));
        OnPropertyChanged(nameof(DetailKind));
        OnPropertyChanged(nameof(DetailIcon));
        OnPropertyChanged(nameof(HasDetailIcon));

        // Monster
        OnPropertyChanged(nameof(MonType));
        OnPropertyChanged(nameof(MonGroup));
        OnPropertyChanged(nameof(MonEntityName));
        OnPropertyChanged(nameof(MonEntityClass));
        OnPropertyChanged(nameof(MonRace));
        OnPropertyChanged(nameof(MonSize));
        OnPropertyChanged(nameof(MonLootSize));
        OnPropertyChanged(nameof(MonCaptureHp));
        OnPropertyChanged(nameof(MonExistTime));

        MonParts.Clear();
        MonStats.Clear();
        if (Mon != null)
        {
            // Deduplicate parts by PartName
            HashSet<string> seen = [];
            foreach (var p in Mon.Parts)
            {
                string label = !string.IsNullOrEmpty(p.PartName) ? p.PartName : p.PartId;
                if (!string.IsNullOrEmpty(label) && seen.Add(label))
                    MonParts.Add($"{label} ({p.PartId}) — {p.StateId}");
            }
            foreach (var d in Mon.DifficultyEntries.OrderBy(x => x.Difficulty))
            {
                string elems = "";
                List<string> el = [];
                if (d.FireAtk > 0) el.Add($"Fire {d.FireAtk}");
                if (d.WaterAtk > 0) el.Add($"Water {d.WaterAtk}");
                if (d.LightningAtk > 0) el.Add($"Thunder {d.LightningAtk}");
                if (d.IceAtk > 0) el.Add($"Ice {d.IceAtk}");
                if (d.DragonAtk > 0) el.Add($"Dragon {d.DragonAtk}");
                if (el.Count > 0) elems = $" | {string.Join(", ", el)}";
                MonStats.Add($"Diff {d.Difficulty}: HP {d.MaxHealth}, ATK {d.PhyAtk}, DEF {d.Defence}{elems}");
            }
        }

        OnPropertyChanged(nameof(HasMonParts));
        OnPropertyChanged(nameof(HasMonStats));

        // NPC
        OnPropertyChanged(nameof(NpcTitle));
        OnPropertyChanged(nameof(NpcEntityName));
        OnPropertyChanged(nameof(NpcEntityClass));
        OnPropertyChanged(nameof(NpcCustomType));
        OnPropertyChanged(nameof(NpcAppearTask));
        OnPropertyChanged(nameof(NpcDisappearTask));

        NpcServices.Clear();
        if (Npc != null)
        {
            foreach (string s in Npc.Services)
                NpcServices.Add(s);
        }
        OnPropertyChanged(nameof(HasNpcServices));

        // Pet
        OnPropertyChanged(nameof(PetType));
        OnPropertyChanged(nameof(PetQuality));
        OnPropertyChanged(nameof(PetPersonality));
        OnPropertyChanged(nameof(PetAttackPref));
        OnPropertyChanged(nameof(PetAttackStyle));
        OnPropertyChanged(nameof(PetLevel));
        OnPropertyChanged(nameof(PetBurrowHp));
        OnPropertyChanged(nameof(PetHealSpeed));
        OnPropertyChanged(nameof(PetEntityName));
        OnPropertyChanged(nameof(PetIcon));
        OnPropertyChanged(nameof(PetAptitude));
        OnPropertyChanged(nameof(PetSupportSkill));
    }

    private void ApplyFilter()
    {
        Entities.Clear();
        string filter = FilterText?.Trim() ?? string.Empty;
        string kind = SelectedKindFilter;

        foreach (var e in _allEntities)
        {
            if (kind != "All" && e.Kind != kind) continue;

            if (!string.IsNullOrEmpty(filter))
            {
                bool matchId = e.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
                bool matchName = e.Name.Contains(filter, StringComparison.OrdinalIgnoreCase);
                bool matchSummary = e.Summary.Contains(filter, StringComparison.OrdinalIgnoreCase);
                if (!matchId && !matchName && !matchSummary) continue;
            }

            Entities.Add(e);
        }

        OnPropertyChanged(nameof(HasEntities));
        StatusText = $"Showing {Entities.Count} of {_allEntities.Count} entities";
    }

    public async Task LoadAsync(IFileProvider provider)
    {
        IsLoading = true;
        StatusText = "Loading entity data...";
        Entities.Clear();
        _allEntities.Clear();
        OnPropertyChanged(nameof(HasEntities));

        try
        {
            EntityDatabase db = await Task.Run(() => _loader.Load(provider));
            _icons.Initialize(provider);

            foreach (var m in db.Monsters.OrderBy(x => x.Id))
                _allEntities.Add(new EntityListItemViewModel(m, _icons.LoadMonsterIcon(m.Id)));
            foreach (var n in db.Npcs.OrderBy(x => x.Id))
                _allEntities.Add(new EntityListItemViewModel(n));
            foreach (var p in db.Pets.OrderBy(x => x.Id))
                _allEntities.Add(new EntityListItemViewModel(p, _icons.LoadItemIcon(p.Icon)));

            SelectedKindFilter = "All";
            FilterText = string.Empty;
            ApplyFilter();

            StatusText = $"Loaded {db.Monsters.Count} monsters, {db.Npcs.Count} NPCs, {db.Pets.Count} pets";

            if (Entities.Count > 0)
                SelectedEntity = Entities[0];
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
}
