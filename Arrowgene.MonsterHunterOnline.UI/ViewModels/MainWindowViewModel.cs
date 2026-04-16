using CommunityToolkit.Mvvm.ComponentModel;

namespace Arrowgene.MonsterHunterOnline.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Title { get; } = "MHO Tools";

    [ObservableProperty]
    private string _statusText = "No data source loaded.";

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _hasDataSource;

    [ObservableProperty]
    private bool _isArchiveOnlyMode;

    [ObservableProperty]
    private string _dataSourceLabel = "No data loaded";
}
