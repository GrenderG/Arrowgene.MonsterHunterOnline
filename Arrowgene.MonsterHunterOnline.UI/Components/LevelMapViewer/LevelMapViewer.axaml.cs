using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class LevelMapViewer : UserControl
{
    public LevelMapViewer()
    {
        InitializeComponent();
        DataContext = new LevelMapViewerViewModel();
    }

    private async void LoadClientFilesClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not LevelMapViewerViewModel vm) return;

        string path = ClientFilesPathBox.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
        {
            vm.StatusText = $"Directory not found: {path}";
            return;
        }

        await vm.LoadClientFilesAsync(path);
    }
}
