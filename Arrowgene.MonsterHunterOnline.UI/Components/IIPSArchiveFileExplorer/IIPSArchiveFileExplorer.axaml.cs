using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Arrowgene.MonsterHunterOnline.UI.Infrastructure;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class IIPSArchiveFileExplorer : UserControl
{
    public IIPSArchiveFileExplorer()
    {
        InitializeComponent();
        IIPSArchiveFileExplorerViewModel vm = new IIPSArchiveFileExplorerViewModel();
        DataContext = vm;
        vm.PropertyChanged += OnViewModelPropertyChanged;
    }

    private IIPSArchiveFileExplorerViewModel ViewModel => (IIPSArchiveFileExplorerViewModel)DataContext!;

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IIPSArchiveFileExplorerViewModel.TablePreviewHeaders))
        {
            RebuildCsvDataGridColumns();
        }

        if (e.PropertyName == nameof(IIPSArchiveFileExplorerViewModel.DatPreviewSheets) ||
            e.PropertyName == nameof(IIPSArchiveFileExplorerViewModel.DatSelectedSheetIndex))
        {
            RebuildDatDataGrid();
        }
    }

    private void ToggleHexModeClick(object? sender, RoutedEventArgs e)
    {
        ViewModel.ToggleHexMode();
    }

    private void RebuildCsvDataGridColumns()
    {
        DataGrid grid = CsvPreviewDataGrid;
        grid.Columns.Clear();

        string[] headers = ViewModel.TablePreviewHeaders;
        for (int i = 0; i < headers.Length; i++)
        {
            grid.Columns.Add(new DataGridTextColumn
            {
                Header = headers[i],
                Binding = new Binding($"[{i}]"),
                MaxWidth = 200,
            });
        }

        grid.ItemsSource = ViewModel.TablePreviewRows;
    }

    private void RebuildDatDataGrid()
    {
        List<DatSheetViewModel> sheets = ViewModel.DatPreviewSheets;
        int index = ViewModel.DatSelectedSheetIndex;
        if (sheets.Count == 0 || index < 0 || index >= sheets.Count)
        {
            return;
        }

        // The DataGrid lives inside the TabControl's content template.
        // After the TabControl updates, walk the visual tree to find it.
        DatPreviewTabControl.UpdateLayout();
        DataGrid? grid = FindDatSheetDataGrid(DatPreviewTabControl);
        if (grid == null)
        {
            return;
        }

        DatSheetViewModel sheet = sheets[index];
        grid.Columns.Clear();
        for (int i = 0; i < sheet.Headers.Length; i++)
        {
            grid.Columns.Add(new DataGridTextColumn
            {
                Header = sheet.Headers[i],
                Binding = new Binding($"[{i}]"),
                MaxWidth = 200,
            });
        }

        grid.ItemsSource = sheet.Rows;
    }

    private static DataGrid? FindDatSheetDataGrid(Control root)
    {
        if (root is DataGrid dg && dg.Name == "DatSheetDataGrid")
        {
            return dg;
        }

        if (root is Avalonia.Visual visual)
        {
            int count = Avalonia.VisualTree.VisualExtensions.GetVisualChildren(visual).Count();
            foreach (Avalonia.Visual child in Avalonia.VisualTree.VisualExtensions.GetVisualChildren(visual))
            {
                if (child is Control childControl)
                {
                    DataGrid? found = FindDatSheetDataGrid(childControl);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
        }

        return null;
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

}
