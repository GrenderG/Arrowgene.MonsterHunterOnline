using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Arrowgene.MonsterHunterOnline.ClientTools.IIPS;
using Arrowgene.MonsterHunterOnline.UI.Components;
using Arrowgene.MonsterHunterOnline.UI.Infrastructure;
using Arrowgene.MonsterHunterOnline.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

namespace Arrowgene.MonsterHunterOnline.UI.Views;

public partial class MainWindow : Window
{
    private string? _tempExtractDir;
    private IIPSUnifiedArchive? _unifiedArchive;

    private static readonly string[] ExtractPrefixes =
    [
        "common/staticdata/",
        "common\\staticdata\\",
        "libs/ui/flashassets/images/icon/",
        "libs\\ui\\flashassets\\images\\icon\\",
        "libs/ui/flashassets/images/illustratebook/monstericon/",
        "libs\\ui\\flashassets\\images\\illustratebook\\monstericon\\",
        "levels/",
        "levels\\",
    ];

    public MainWindow()
    {
        InitializeComponent();
    }

    private MainWindowViewModel Vm => (MainWindowViewModel)DataContext!;

    // ── Open Directory ──

    private async void OpenDirectoryClick(object? sender, RoutedEventArgs e)
    {
        string? path = await PickFolderAsync("Select MHO client files directory");
        if (string.IsNullOrEmpty(path)) return;

        if (!Directory.Exists(path))
        {
            Vm.StatusText = $"Directory not found: {path}";
            return;
        }

        CleanupPreviousSource();
        Vm.IsArchiveOnlyMode = false;
        Vm.DataSourceLabel = $"Directory: {path}";
        await LoadAllViewersAsync(path);
    }

    // ── Open IIPS List (.lst) ──

    private async void OpenIIPSListClick(object? sender, RoutedEventArgs e)
    {
        string? path = await PickFileAsync("Open IIPS file list");
        if (string.IsNullOrEmpty(path)) return;

        CleanupPreviousSource();
        Vm.IsLoading = true;
        Vm.IsArchiveOnlyMode = false;
        Vm.StatusText = "Opening IIPS file list...";

        try
        {
            // Open unified archive
            _unifiedArchive = await Task.Run(() => IIPSUnifiedArchive.Open(path));

            Vm.StatusText = $"Extracting data from {_unifiedArchive.LoadedArchives.Count} archives...";

            // Feed to IIPS explorer
            if (IIPSExplorer.DataContext is IIPSArchiveFileExplorerViewModel explorerVm)
                explorerVm.TryOpenFileList(path);

            // Extract needed files to temp
            string tempDir = await ExtractToTempAsync(_unifiedArchive);
            _tempExtractDir = tempDir;

            Vm.DataSourceLabel = $"IIPS List: {Path.GetFileName(path)} ({_unifiedArchive.LoadedArchives.Count} archives, {_unifiedArchive.MergedEntries.Count} entries)";

            // Load all viewers from temp dir
            await LoadAllViewersAsync(tempDir);
        }
        catch (Exception ex)
        {
            Vm.StatusText = $"Error: {ex.Message}";
            Vm.IsLoading = false;
        }
    }

    // ── Open single .ifs archive (archive-only mode) ──

    private async void OpenSingleArchiveClick(object? sender, RoutedEventArgs e)
    {
        string? path = await PickFileAsync("Open IIPS archive");
        if (string.IsNullOrEmpty(path)) return;

        CleanupPreviousSource();

        if (IIPSExplorer.DataContext is IIPSArchiveFileExplorerViewModel explorerVm)
        {
            explorerVm.TryOpenArchive(path);
        }

        Vm.IsArchiveOnlyMode = true;
        Vm.HasDataSource = false;
        Vm.DataSourceLabel = $"Archive: {Path.GetFileName(path)}";
        Vm.StatusText = "Single archive mode — data viewer tabs are disabled.";

        // Switch to the IIPS explorer tab
        MainTabs.SelectedIndex = 0;
    }

    // ── Load all viewers ──

    private async Task LoadAllViewersAsync(string clientFilesRoot)
    {
        Vm.IsLoading = true;
        Vm.HasDataSource = false;
        Vm.StatusText = "Loading all data...";

        try
        {
            List<Task> tasks = [];

            if (ItemControl.DataContext is ItemViewerViewModel itemVm)
                tasks.Add(itemVm.LoadAsync(clientFilesRoot));
            if (EntityControl.DataContext is EntityViewerViewModel entityVm)
                tasks.Add(entityVm.LoadAsync(clientFilesRoot));
            if (CraftControl.DataContext is CraftViewerViewModel craftVm)
                tasks.Add(craftVm.LoadAsync(clientFilesRoot));
            if (QuestControl.DataContext is QuestViewerViewModel questVm)
                tasks.Add(questVm.LoadAsync(clientFilesRoot));
            if (LevelMapControl.DataContext is LevelMapViewerViewModel levelVm)
                tasks.Add(levelVm.LoadClientFilesAsync(clientFilesRoot));

            await Task.WhenAll(tasks);

            Vm.HasDataSource = true;
            Vm.StatusText = $"All data loaded from {clientFilesRoot}";
        }
        catch (Exception ex)
        {
            Vm.StatusText = $"Error loading data: {ex.Message}";
        }
        finally
        {
            Vm.IsLoading = false;
        }
    }

    // ── IIPS extraction ──

    private static async Task<string> ExtractToTempAsync(IIPSUnifiedArchive archive)
    {
        string tempDir = Path.Combine(Path.GetTempPath(), "MHOTools", Guid.NewGuid().ToString("N")[..8]);

        await Task.Run(() =>
        {
            int count = 0;
            foreach (IIPSArchiveEntry entry in archive.MergedEntries)
            {
                if (!entry.Exists || entry.Length == 0 || string.IsNullOrEmpty(entry.ArchivePath))
                    continue;

                string archivePath = entry.ArchivePath!;
                bool match = false;
                foreach (string prefix in ExtractPrefixes)
                {
                    if (archivePath.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        match = true;
                        break;
                    }
                }

                if (!match) continue;

                string outputPath = Path.Combine(tempDir, archivePath.Replace('\\', Path.DirectorySeparatorChar));
                string? dir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);

                try
                {
                    File.WriteAllBytes(outputPath, entry.ReadAllBytes());
                    count++;
                }
                catch
                {
                    // Skip files that fail to extract
                }
            }
        });

        return tempDir;
    }

    // ── Cleanup ──

    private void CleanupPreviousSource()
    {
        CleanupTempDir();
        CleanupUnifiedArchive();
    }

    private void CleanupTempDir()
    {
        if (_tempExtractDir != null && Directory.Exists(_tempExtractDir))
        {
            try { Directory.Delete(_tempExtractDir, true); }
            catch { /* best effort */ }
            _tempExtractDir = null;
        }
    }

    private void CleanupUnifiedArchive()
    {
        _unifiedArchive?.Dispose();
        _unifiedArchive = null;
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        CleanupPreviousSource();
        base.OnClosing(e);
    }

    // ── File pickers ──

    private async Task<string?> PickFileAsync(string title)
    {
        if (OperatingSystem.IsMacOS())
        {
            return await MacNativePicker.PickFileAsync(title);
        }

        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider == null) return null;

        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions { Title = title, AllowMultiple = false });

        return files.Count == 0 ? null : files[0].TryGetLocalPath();
    }

    private async Task<string?> PickFolderAsync(string title)
    {
        if (OperatingSystem.IsMacOS())
        {
            return await MacNativePicker.PickFolderAsync(title);
        }

        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider == null) return null;

        IReadOnlyList<IStorageFolder> folders = await topLevel.StorageProvider.OpenFolderPickerAsync(
            new FolderPickerOpenOptions { Title = title, AllowMultiple = false });

        return folders.Count == 0 ? null : folders[0].TryGetLocalPath();
    }
}
