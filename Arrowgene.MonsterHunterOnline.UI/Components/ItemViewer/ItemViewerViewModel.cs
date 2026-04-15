using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Arrowgene.MonsterHunterOnline.ClientTools.Item;
using Arrowgene.MonsterHunterOnline.UI.Infrastructure;
using Arrowgene.MonsterHunterOnline.UI.ViewModels;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public sealed partial class ItemListItemViewModel : ViewModelBase
{
    public ItemDef Data { get; }
    public Bitmap? Icon { get; }

    public int Id => Data.Id;
    public string Name => Data.Name;
    public string RarityLabel => Data.RarityLabel;
    public string CategoryLabel => Data.MainCategoryLabel;

    public string Summary
    {
        get
        {
            List<string> parts = [];
            if (!string.IsNullOrEmpty(Data.RarityLabel)) parts.Add(Data.RarityLabel);
            if (!string.IsNullOrEmpty(Data.MainCategoryLabel)) parts.Add(Data.MainCategoryLabel);
            if (Data.SellPrice > 0) parts.Add($"Sell: {Data.SellPrice}z");
            return parts.Count > 0 ? string.Join(" | ", parts) : "";
        }
    }

    public ItemListItemViewModel(ItemDef data, Bitmap? icon)
    {
        Data = data;
        Icon = icon;
    }
}

public sealed partial class ItemViewerViewModel : ViewModelBase
{
    private readonly ItemDataLoader _loader = new();
    private readonly GameIconLoader _icons = new();
    private List<ItemListItemViewModel> _allItems = [];

    [ObservableProperty]
    private string _statusText = "Open a data source from the toolbar.";

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private ItemListItemViewModel? _selectedItem;

    [ObservableProperty]
    private string _filterText = string.Empty;

    [ObservableProperty]
    private string _selectedCategoryFilter = "All";

    [ObservableProperty]
    private string _selectedRarityFilter = "All";

    [ObservableProperty]
    private string _selectedSourceFilter = "All";

    public ObservableCollection<ItemListItemViewModel> Items { get; } = [];
    public ObservableCollection<string> CategoryFilters { get; } = ["All"];
    public ObservableCollection<string> RarityFilters { get; } = ["All"];
    public ObservableCollection<string> SourceFilters { get; } = ["All"];

    public bool HasItems => Items.Count > 0;

    // Detail properties
    public bool HasSelection => _selectedItem != null;
    public string DetailName => _selectedItem?.Data.Name ?? string.Empty;
    public string DetailDescription => _selectedItem?.Data.Description ?? string.Empty;
    public int DetailId => _selectedItem?.Data.Id ?? 0;
    public int DetailItemLevel => _selectedItem?.Data.ItemLevel ?? 0;
    public string DetailRarity => _selectedItem?.Data.RarityLabel ?? string.Empty;
    public int DetailRarityValue => _selectedItem?.Data.Rarity ?? 0;
    public string DetailCategory => _selectedItem?.Data.MainCategoryLabel ?? string.Empty;
    public int DetailSubCategory => _selectedItem?.Data.SubCategory ?? 0;
    public int DetailDetailCategory => _selectedItem?.Data.DetailCategory ?? 0;
    public string DetailIconKey => _selectedItem?.Data.Icon ?? string.Empty;
    public int DetailRank => _selectedItem?.Data.Rank ?? 0;
    public string DetailBindType => _selectedItem?.Data.BindTypeLabel ?? string.Empty;
    public int DetailOwnLimit => _selectedItem?.Data.OwnLimit ?? 0;
    public int DetailCarryLimit => _selectedItem?.Data.CarryLimit ?? 0;
    public int DetailStackLimit => _selectedItem?.Data.StackLimit ?? 0;
    public int DetailBuyPrice => _selectedItem?.Data.BuyPrice ?? 0;
    public int DetailSellPrice => _selectedItem?.Data.SellPrice ?? 0;
    public bool DetailIsMallItem => _selectedItem?.Data.IsMallItem ?? false;
    public bool DetailKeepOnLeave => _selectedItem?.Data.KeepOnLeaveLevel ?? false;
    public bool DetailCanDestroy => _selectedItem?.Data.CanDestroy ?? false;
    public Bitmap? DetailIcon => _selectedItem?.Icon;
    public bool HasDetailIcon => _selectedItem?.Icon != null;
    public string DetailSourceFile => _selectedItem?.Data.SourceFile ?? string.Empty;
    public string DetailSourceSheet => _selectedItem?.Data.SourceSheet ?? string.Empty;

    // Equipment stats
    public bool DetailIsEquipment => _selectedItem?.Data.IsEquipment ?? false;
    public int DetailAttack => _selectedItem?.Data.Attack ?? 0;
    public int DetailDefense => _selectedItem?.Data.Defense ?? 0;
    public int DetailAffinity => _selectedItem?.Data.Affinity ?? 0;
    public int DetailSlots => _selectedItem?.Data.Slots ?? 0;
    public int DetailMaxSlots => _selectedItem?.Data.MaxSlots ?? 0;
    public int DetailWaterAttack => _selectedItem?.Data.WaterAttack ?? 0;
    public int DetailFireAttack => _selectedItem?.Data.FireAttack ?? 0;
    public int DetailThunderAttack => _selectedItem?.Data.ThunderAttack ?? 0;
    public int DetailDragonAttack => _selectedItem?.Data.DragonAttack ?? 0;
    public int DetailIceAttack => _selectedItem?.Data.IceAttack ?? 0;
    public int DetailWaterRes => _selectedItem?.Data.WaterRes ?? 0;
    public int DetailFireRes => _selectedItem?.Data.FireRes ?? 0;
    public int DetailThunderRes => _selectedItem?.Data.ThunderRes ?? 0;
    public int DetailDragonRes => _selectedItem?.Data.DragonRes ?? 0;
    public int DetailIceRes => _selectedItem?.Data.IceRes ?? 0;

    partial void OnSelectedItemChanged(ItemListItemViewModel? value) => UpdateDetailPanel();
    partial void OnFilterTextChanged(string value) => ApplyFilter();
    partial void OnSelectedCategoryFilterChanged(string value) => ApplyFilter();
    partial void OnSelectedRarityFilterChanged(string value) => ApplyFilter();
    partial void OnSelectedSourceFilterChanged(string value) => ApplyFilter();

    private void UpdateDetailPanel()
    {
        OnPropertyChanged(nameof(HasSelection));
        OnPropertyChanged(nameof(DetailIcon));
        OnPropertyChanged(nameof(HasDetailIcon));
        OnPropertyChanged(nameof(DetailName));
        OnPropertyChanged(nameof(DetailDescription));
        OnPropertyChanged(nameof(DetailId));
        OnPropertyChanged(nameof(DetailItemLevel));
        OnPropertyChanged(nameof(DetailRarity));
        OnPropertyChanged(nameof(DetailRarityValue));
        OnPropertyChanged(nameof(DetailCategory));
        OnPropertyChanged(nameof(DetailSubCategory));
        OnPropertyChanged(nameof(DetailDetailCategory));
        OnPropertyChanged(nameof(DetailIconKey));
        OnPropertyChanged(nameof(DetailRank));
        OnPropertyChanged(nameof(DetailBindType));
        OnPropertyChanged(nameof(DetailOwnLimit));
        OnPropertyChanged(nameof(DetailCarryLimit));
        OnPropertyChanged(nameof(DetailStackLimit));
        OnPropertyChanged(nameof(DetailBuyPrice));
        OnPropertyChanged(nameof(DetailSellPrice));
        OnPropertyChanged(nameof(DetailIsMallItem));
        OnPropertyChanged(nameof(DetailKeepOnLeave));
        OnPropertyChanged(nameof(DetailCanDestroy));
        OnPropertyChanged(nameof(DetailSourceFile));
        OnPropertyChanged(nameof(DetailSourceSheet));
        OnPropertyChanged(nameof(DetailIsEquipment));
        OnPropertyChanged(nameof(DetailAttack));
        OnPropertyChanged(nameof(DetailDefense));
        OnPropertyChanged(nameof(DetailAffinity));
        OnPropertyChanged(nameof(DetailSlots));
        OnPropertyChanged(nameof(DetailMaxSlots));
        OnPropertyChanged(nameof(DetailWaterAttack));
        OnPropertyChanged(nameof(DetailFireAttack));
        OnPropertyChanged(nameof(DetailThunderAttack));
        OnPropertyChanged(nameof(DetailDragonAttack));
        OnPropertyChanged(nameof(DetailIceAttack));
        OnPropertyChanged(nameof(DetailWaterRes));
        OnPropertyChanged(nameof(DetailFireRes));
        OnPropertyChanged(nameof(DetailThunderRes));
        OnPropertyChanged(nameof(DetailDragonRes));
        OnPropertyChanged(nameof(DetailIceRes));
    }

    private void ApplyFilter()
    {
        Items.Clear();

        string filter = FilterText?.Trim() ?? string.Empty;
        string catFilter = SelectedCategoryFilter;
        string rarFilter = SelectedRarityFilter;
        string srcFilter = SelectedSourceFilter;

        foreach (var item in _allItems)
        {
            if (catFilter != "All" && item.CategoryLabel != catFilter)
                continue;
            if (rarFilter != "All" && item.RarityLabel != rarFilter)
                continue;
            if (srcFilter != "All" && item.Data.SourceFile != srcFilter)
                continue;

            if (!string.IsNullOrEmpty(filter))
            {
                bool matchId = item.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
                bool matchName = item.Name.Contains(filter, StringComparison.OrdinalIgnoreCase);
                if (!matchId && !matchName)
                    continue;
            }

            Items.Add(item);
        }

        OnPropertyChanged(nameof(HasItems));
        StatusText = $"Showing {Items.Count} of {_allItems.Count} items";
    }

    public async Task LoadAsync(string path)
    {
        IsLoading = true;
        StatusText = "Loading item data...";
        Items.Clear();
        _allItems.Clear();
        CategoryFilters.Clear();
        CategoryFilters.Add("All");
        RarityFilters.Clear();
        RarityFilters.Add("All");
        SourceFilters.Clear();
        SourceFilters.Add("All");
        OnPropertyChanged(nameof(HasItems));

        try
        {
            ItemDatabase db = await Task.Run(() => _loader.Load(path));
            _icons.Initialize(path);

            HashSet<string> cats = [];
            HashSet<string> rars = [];
            HashSet<string> srcs = [];

            foreach (ItemDef item in db.Items.OrderBy(i => i.Id))
            {
                var vm = new ItemListItemViewModel(item, _icons.LoadItemIcon(item.Icon));
                _allItems.Add(vm);

                if (!string.IsNullOrEmpty(item.MainCategoryLabel))
                    cats.Add(item.MainCategoryLabel);
                if (!string.IsNullOrEmpty(item.RarityLabel))
                    rars.Add(item.RarityLabel);
                if (!string.IsNullOrEmpty(item.SourceFile))
                    srcs.Add(item.SourceFile);
            }

            foreach (string c in cats.OrderBy(x => x))
                CategoryFilters.Add(c);
            foreach (string r in rars.OrderBy(x => x))
                RarityFilters.Add(r);
            foreach (string s in srcs.OrderBy(x => x))
                SourceFilters.Add(s);

            SelectedCategoryFilter = "All";
            SelectedRarityFilter = "All";
            SelectedSourceFilter = "All";
            FilterText = string.Empty;
            ApplyFilter();

            StatusText = $"Loaded {_allItems.Count} items";

            if (Items.Count > 0)
                SelectedItem = Items[0];
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
