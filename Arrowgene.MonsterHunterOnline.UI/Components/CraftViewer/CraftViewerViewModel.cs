using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Arrowgene.MonsterHunterOnline.ClientTools.Craft;
using Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;
using Arrowgene.MonsterHunterOnline.UI.Infrastructure;
using Arrowgene.MonsterHunterOnline.UI.ViewModels;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public sealed class CraftListItemViewModel
{
    public CraftRecipe Data { get; }
    public int Id => Data.Id;
    public string Name => Data.Name;
    public string EquipType => Data.EquipTypeLabel;

    public string Summary
    {
        get
        {
            List<string> parts = [];
            if (!string.IsNullOrEmpty(Data.EquipTypeLabel)) parts.Add(Data.EquipTypeLabel);
            parts.Add(Data.RecipeTypeLabel);
            if (Data.Materials.Count > 0) parts.Add($"{Data.Materials.Count} mats");
            if (Data.GoldCost > 0) parts.Add($"{Data.GoldCost}z");
            return string.Join(" | ", parts);
        }
    }

    public CraftListItemViewModel(CraftRecipe data) { Data = data; }
}

public sealed class CraftMaterialViewModel
{
    public int ItemId { get; }
    public string Name { get; }
    public int Count { get; }
    public Bitmap? Icon { get; }

    public CraftMaterialViewModel(int itemId, string name, int count, Bitmap? icon)
    {
        ItemId = itemId;
        Name = name;
        Count = count;
        Icon = icon;
    }
}

public sealed class CraftOutputViewModel
{
    public int ItemId { get; }
    public string Name { get; }
    public int Count { get; }
    public int Rate { get; }
    public Bitmap? Icon { get; }

    public CraftOutputViewModel(int itemId, string name, int count, int rate, Bitmap? icon)
    {
        ItemId = itemId;
        Name = name;
        Count = count;
        Rate = rate;
        Icon = icon;
    }
}

public sealed partial class CraftViewerViewModel : ViewModelBase
{
    private readonly CraftDataLoader _loader = new();
    private readonly GameIconLoader _icons = new();
    private CraftDatabase? _database;
    private List<CraftListItemViewModel> _allRecipes = [];

    [ObservableProperty] private string _statusText = "Open a data source from the toolbar.";
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private CraftListItemViewModel? _selectedRecipe;
    [ObservableProperty] private string _filterText = string.Empty;
    [ObservableProperty] private string _selectedEquipFilter = "All";
    [ObservableProperty] private string _selectedTypeFilter = "All";

    public ObservableCollection<CraftListItemViewModel> Recipes { get; } = [];
    public ObservableCollection<string> EquipFilters { get; } = ["All"];
    public ObservableCollection<string> TypeFilters { get; } = ["All"];
    public bool HasRecipes => Recipes.Count > 0;
    public bool HasSelection => _selectedRecipe != null;

    // Detail
    public string DetailName => _selectedRecipe?.Data.Name ?? string.Empty;
    public int DetailId => _selectedRecipe?.Data.Id ?? 0;
    public string DetailRecipeType => _selectedRecipe?.Data.RecipeTypeLabel ?? string.Empty;
    public string DetailEquipType => _selectedRecipe?.Data.EquipTypeLabel ?? string.Empty;
    public int DetailRequiredLevel => _selectedRecipe?.Data.RequiredLevel ?? 0;
    public int DetailRequiredStar => _selectedRecipe?.Data.RequiredStar ?? 0;
    public int DetailGoldCost => _selectedRecipe?.Data.GoldCost ?? 0;
    public string DetailSourceSheet => _selectedRecipe?.Data.SourceSheet ?? string.Empty;

    public ObservableCollection<CraftOutputViewModel> DetailOutputs { get; } = [];
    public ObservableCollection<CraftMaterialViewModel> DetailMaterials { get; } = [];
    public CraftOutputViewModel? DetailByproduct { get; private set; }
    public bool HasByproduct => DetailByproduct != null;

    partial void OnSelectedRecipeChanged(CraftListItemViewModel? value) => UpdateDetailPanel();
    partial void OnFilterTextChanged(string value) => ApplyFilter();
    partial void OnSelectedEquipFilterChanged(string value) => ApplyFilter();
    partial void OnSelectedTypeFilterChanged(string value) => ApplyFilter();

    private void UpdateDetailPanel()
    {
        OnPropertyChanged(nameof(HasSelection));
        OnPropertyChanged(nameof(DetailName));
        OnPropertyChanged(nameof(DetailId));
        OnPropertyChanged(nameof(DetailRecipeType));
        OnPropertyChanged(nameof(DetailEquipType));
        OnPropertyChanged(nameof(DetailRequiredLevel));
        OnPropertyChanged(nameof(DetailRequiredStar));
        OnPropertyChanged(nameof(DetailGoldCost));
        OnPropertyChanged(nameof(DetailSourceSheet));

        DetailOutputs.Clear();
        DetailMaterials.Clear();
        DetailByproduct = null;

        if (_selectedRecipe != null && _database != null)
        {
            foreach (var o in _selectedRecipe.Data.Outputs)
                DetailOutputs.Add(MakeOutputVm(o));

            if (_selectedRecipe.Data.Byproduct != null)
                DetailByproduct = MakeOutputVm(_selectedRecipe.Data.Byproduct);

            foreach (var m in _selectedRecipe.Data.Materials)
                DetailMaterials.Add(MakeMaterialVm(m));
        }

        OnPropertyChanged(nameof(HasByproduct));
        OnPropertyChanged(nameof(DetailByproduct));
    }

    private CraftOutputViewModel MakeOutputVm(CraftOutput o)
    {
        string name = _database!.ItemNames.TryGetValue(o.ItemId, out var n) ? n : $"Item #{o.ItemId}";
        return new CraftOutputViewModel(o.ItemId, name, o.Count, o.Rate, LoadIcon(o.ItemId));
    }

    private CraftMaterialViewModel MakeMaterialVm(CraftMaterial m)
    {
        string name = _database!.ItemNames.TryGetValue(m.ItemId, out var n) ? n : $"Item #{m.ItemId}";
        return new CraftMaterialViewModel(m.ItemId, name, m.Count, LoadIcon(m.ItemId));
    }

    private Bitmap? LoadIcon(int itemId)
    {
        if (_database == null) return null;
        if (!_database.ItemIcons.TryGetValue(itemId, out string? iconKey)) return null;
        return _icons.LoadItemIcon(iconKey);
    }

    private void ApplyFilter()
    {
        Recipes.Clear();
        string filter = FilterText?.Trim() ?? string.Empty;
        string equipF = SelectedEquipFilter;
        string typeF = SelectedTypeFilter;

        foreach (var r in _allRecipes)
        {
            if (equipF != "All" && r.EquipType != equipF) continue;
            if (typeF != "All" && r.Data.RecipeTypeLabel != typeF) continue;

            if (!string.IsNullOrEmpty(filter))
            {
                bool matchId = r.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
                bool matchName = r.Name.Contains(filter, StringComparison.OrdinalIgnoreCase);
                if (!matchId && !matchName) continue;
            }

            Recipes.Add(r);
        }

        OnPropertyChanged(nameof(HasRecipes));
        StatusText = $"Showing {Recipes.Count} of {_allRecipes.Count} recipes";
    }

    public async Task LoadAsync(IFileProvider provider)
    {
        IsLoading = true;
        StatusText = "Loading craft data...";
        Recipes.Clear();
        _allRecipes.Clear();
        EquipFilters.Clear();
        EquipFilters.Add("All");
        TypeFilters.Clear();
        TypeFilters.Add("All");
        OnPropertyChanged(nameof(HasRecipes));

        _icons.Initialize(provider);

        try
        {
            _database = await Task.Run(() => _loader.Load(provider));

            HashSet<string> equips = [];
            HashSet<string> types = [];

            foreach (CraftRecipe recipe in _database.Recipes.OrderBy(r => r.Id))
            {
                var vm = new CraftListItemViewModel(recipe);
                _allRecipes.Add(vm);

                if (!string.IsNullOrEmpty(recipe.EquipTypeLabel)) equips.Add(recipe.EquipTypeLabel);
                types.Add(recipe.RecipeTypeLabel);
            }

            foreach (string e in equips.OrderBy(x => x)) EquipFilters.Add(e);
            foreach (string t in types.OrderBy(x => x)) TypeFilters.Add(t);

            SelectedEquipFilter = "All";
            SelectedTypeFilter = "All";
            FilterText = string.Empty;
            ApplyFilter();

            StatusText = $"Loaded {_allRecipes.Count} recipes ({_database.ItemNames.Count} item names, {_database.ItemIcons.Count} icons)";

            if (Recipes.Count > 0)
                SelectedRecipe = Recipes[0];
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
