using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Arrowgene.MonsterHunterOnline.UI.Infrastructure;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class IIPSArchiveFileExplorer : UserControl
{
    public IIPSArchiveFileExplorer()
    {
        InitializeComponent();
        DataContext = new IIPSArchiveFileExplorerViewModel();
    }

    private IIPSArchiveFileExplorerViewModel ViewModel => (IIPSArchiveFileExplorerViewModel)DataContext!;

    private async void OpenArchiveClick(object? sender, RoutedEventArgs e)
    {
        string? path = await PickFileAsync("Open IIPS archive", CreateArchiveFileTypes());

        if (!string.IsNullOrEmpty(path))
        {
            ViewModel.TryOpenArchive(path);
        }
    }

    private void SaveArchiveClick(object? sender, RoutedEventArgs e)
    {
        ViewModel.TrySaveArchive();
    }

    private void ClearFilterClick(object? sender, RoutedEventArgs e)
    {
        ViewModel.FilterText = string.Empty;
    }

    private async void AddFileClick(object? sender, RoutedEventArgs e)
    {
        string? sourcePath = await PickFileAsync("Choose a local file to add");
        if (string.IsNullOrEmpty(sourcePath))
        {
            return;
        }

        Window? owner = TopLevel.GetTopLevel(this) as Window;
        if (owner == null)
        {
            return;
        }

        TextInputDialog dialog = new TextInputDialog(
            "Add File",
            "Archive path",
            ViewModel.SuggestArchivePath(sourcePath),
            "Add");

        string? archivePath = await dialog.ShowDialog<string?>(owner);
        if (!string.IsNullOrEmpty(archivePath))
        {
            ViewModel.TryAddFile(sourcePath, archivePath);
        }
    }

    private async void ModifySelectionClick(object? sender, RoutedEventArgs e)
    {
        string? sourcePath = await PickFileAsync("Choose replacement file");
        if (!string.IsNullOrEmpty(sourcePath))
        {
            ViewModel.TryModifySelection(sourcePath);
        }
    }

    private async void RemoveSelectionClick(object? sender, RoutedEventArgs e)
    {
        Window? owner = TopLevel.GetTopLevel(this) as Window;
        if (owner == null || ViewModel.SelectedNode == null)
        {
            return;
        }

        int selectedCount = ViewModel.SelectedNode.EnumerateFileNodes().Count(static node => node.Entry != null);
        if (selectedCount == 0)
        {
            return;
        }

        string subject = selectedCount == 1 ? "1 archive entry" : $"{selectedCount} archive entries";
        ConfirmationDialog dialog = new ConfirmationDialog(
            "Remove Selection",
            $"Remove {subject} from the archive? Changes stay local until you save.",
            "Remove");

        bool confirmed = await dialog.ShowDialog<bool>(owner);
        if (confirmed)
        {
            ViewModel.TryRemoveSelection();
        }
    }

    private async void ExtractSelectionClick(object? sender, RoutedEventArgs e)
    {
        string? path = await PickFolderAsync("Extract selected archive entry");
        if (!string.IsNullOrEmpty(path))
        {
            ViewModel.TryExtractSelection(path);
        }
    }

    private async void ExtractAllClick(object? sender, RoutedEventArgs e)
    {
        string? path = await PickFolderAsync("Extract archive");
        if (!string.IsNullOrEmpty(path))
        {
            ViewModel.TryExtractAll(path);
        }
    }

    private async System.Threading.Tasks.Task<string?> PickFileAsync(string title, IReadOnlyList<FilePickerFileType>? fileTypes = null)
    {
        if (OperatingSystem.IsMacOS())
        {
            return await MacNativePicker.PickFileAsync(title);
        }

        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider == null)
        {
            return null;
        }

        FilePickerOpenOptions options = new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false
        };

        if (fileTypes is { Count: > 0 })
        {
            options.FileTypeFilter = fileTypes;
        }

        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(options);

        return files.Count == 0 ? null : files[0].TryGetLocalPath();
    }

    private async System.Threading.Tasks.Task<string?> PickFolderAsync(string title)
    {
        if (OperatingSystem.IsMacOS())
        {
            return await MacNativePicker.PickFolderAsync(title);
        }

        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider == null)
        {
            return null;
        }

        IReadOnlyList<IStorageFolder> folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = title,
            AllowMultiple = false
        });

        return folders.Count == 0 ? null : folders[0].TryGetLocalPath();
    }

    private static IReadOnlyList<FilePickerFileType> CreateArchiveFileTypes()
    {
        return
        [
            new FilePickerFileType("IIPS Archive")
            {
                Patterns = ["*.iips", "*.ifs", "*.mpq"]
            }
        ];
    }
}
