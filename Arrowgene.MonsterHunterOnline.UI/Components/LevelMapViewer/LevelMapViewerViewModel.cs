using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;
using Arrowgene.MonsterHunterOnline.ClientTools.Level;
using Arrowgene.MonsterHunterOnline.UI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public sealed partial class LevelListItemViewModel : ViewModelBase
{
    public LevelData Data { get; }
    public string Name => Data.Name;
    public string TerrainSize => Data.Terrain.WorldSize > 0 ? $"{Data.Terrain.WorldSize}m" : "?";
    public int EntityCount => Data.Entities.Count;
    public int MonsterCount => Data.Entities.Count(e => e.EntityClass == "MHMonsterSpawnPoint" && e.FixedMonsterID > 0);
    public int ResourceCount => Data.Entities.Count(e => e.EntityClass == "MHCollectSpawner");
    public int RegionCount => Data.Regions.Count;

    public string Summary
    {
        get
        {
            List<string> parts = [];
            int monsters = Data.Entities.Count(e =>
                e.EntityClass == "MHMonsterSpawnPoint" && e.FixedMonsterID >= 50000);
            int npcs = Data.Entities.Count(e =>
                e.EntityClass == "MHMonsterSpawnPoint" && e.FixedMonsterID >= 30000 && e.FixedMonsterID < 40000);
            int resources = ResourceCount;
            int playerSpawns = Data.Entities.Count(e => e.EntityClass == "MHPlayerSpawnPoint");

            if (monsters > 0) parts.Add($"{monsters} monsters");
            if (npcs > 0) parts.Add($"{npcs} NPCs");
            if (resources > 0) parts.Add($"{resources} resources");
            if (playerSpawns > 0) parts.Add($"{playerSpawns} spawns");

            return parts.Count > 0 ? string.Join(", ", parts) : "No entities";
        }
    }

    public LevelListItemViewModel(LevelData data)
    {
        Data = data;
    }
}

public sealed partial class LevelMapViewerViewModel : ViewModelBase
{
    private readonly LevelDataLoader _loader = new();

    [ObservableProperty]
    private string _statusText = "Open a data source from the toolbar.";

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private LevelListItemViewModel? _selectedLevel;

    [ObservableProperty]
    private LevelData? _selectedLevelData;

    [ObservableProperty]
    private bool _showMonsters = true;

    [ObservableProperty]
    private bool _showResources = true;

    [ObservableProperty]
    private bool _showPlayerSpawns = true;

    [ObservableProperty]
    private bool _showRegions = true;

    [ObservableProperty]
    private bool _useMinimap = true;

    [ObservableProperty]
    private string _selectedLevelInfo = string.Empty;

    public ObservableCollection<LevelListItemViewModel> Levels { get; } = [];

    public bool HasLevels => Levels.Count > 0;

    partial void OnSelectedLevelChanged(LevelListItemViewModel? value)
    {
        SelectedLevelData = value?.Data;
        if (value != null)
        {
            var d = value.Data;
            string mapSource = d.ClientMiniMap != null
                ? $"Client Minimap: {d.ClientMiniMap.AssetName}"
                : $"Terrain: {d.Terrain.WorldSize}m";
            SelectedLevelInfo = $"{d.Name}  |  {mapSource}  |  Regions: {d.Regions.Count}  |  Entities: {d.Entities.Count}";
        }
        else
        {
            SelectedLevelInfo = string.Empty;
        }
    }

    public async Task LoadClientFilesAsync(IFileProvider provider)
    {
        IsLoading = true;
        StatusText = "Loading levels...";
        Levels.Clear();
        OnPropertyChanged(nameof(HasLevels));

        try
        {
            // LevelDataLoader uses filesystem directly (XML, terrain, minimap) — needs a directory path
            string? root = (provider as DirectoryFileProvider)?.Root;
            if (root == null)
            {
                StatusText = "Level Map requires a local directory source.";
                return;
            }

            List<LevelData> levels = await Task.Run(() => _loader.LoadAll(root));

            foreach (LevelData level in levels.OrderBy(l => l.Name, StringComparer.OrdinalIgnoreCase))
            {
                Levels.Add(new LevelListItemViewModel(level));
            }

            OnPropertyChanged(nameof(HasLevels));
            int minimapBackedLevels = levels.Count(level => level.ClientMiniMap != null);
            StatusText = $"Loaded {levels.Count} levels from {root} ({minimapBackedLevels} with client minimap assets)";

            if (Levels.Count > 0)
            {
                SelectedLevel = Levels[0];
            }
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
