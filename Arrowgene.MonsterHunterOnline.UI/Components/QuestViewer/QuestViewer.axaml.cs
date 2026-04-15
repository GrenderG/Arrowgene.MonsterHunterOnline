using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class QuestViewer : UserControl
{
    private static readonly IBrush FlagOnBrush = SolidColorBrush.Parse("#DDD9CF");
    private static readonly IBrush FlagOffBrush = SolidColorBrush.Parse("#EDEAE4");

    public static readonly IValueConverter FlagBgConverter =
        new FuncValueConverter<bool, IBrush>(v => v ? FlagOnBrush : FlagOffBrush);

    public QuestViewer()
    {
        InitializeComponent();
        DataContext = new QuestViewerViewModel();
    }

    private async void LoadClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not QuestViewerViewModel vm) return;

        string path = ClientFilesPathBox.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
        {
            vm.StatusText = $"Directory not found: {path}";
            return;
        }

        await vm.LoadAsync(path);
    }
}
