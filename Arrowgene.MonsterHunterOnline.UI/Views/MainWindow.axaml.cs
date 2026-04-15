using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;
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
    private IIPSUnifiedArchive? _unifiedArchive;

    public MainWindow()
    {
        InitializeComponent();
    }

    private MainWindowViewModel Vm => (MainWindowViewModel)DataContext!;

    // ── Single Load button ──

    private async void LoadClick(object? sender, RoutedEventArgs e)
    {
        // Let user pick: folder, .lst file, or single .ifs archive
        // Use a file picker that accepts all three — on macOS/Linux the folder picker is separate
        // Strategy: show file picker first (for .lst and .ifs), with a "folder" option

        // Try folder picker first via a simple choice: pick file or folder
        // For simplicity, use file picker that accepts .lst, .ifs, and all files
        // If user picks a directory (via folder selection), treat as directory mode
        // If user picks .lst, treat as IIPS list mode
        // If user picks .ifs, treat as single archive mode

        string? path = await PickFileOrFolderAsync();
        if (string.IsNullOrEmpty(path)) return;

        if (Directory.Exists(path))
        {
            await LoadFromDirectoryAsync(path);
        }
        else if (path.EndsWith(".lst", StringComparison.OrdinalIgnoreCase))
        {
            await LoadFromIIPSListAsync(path);
        }
        else if (path.EndsWith(".ifs", StringComparison.OrdinalIgnoreCase))
        {
            await LoadFromSingleArchiveAsync(path);
        }
        else
        {
            Vm.StatusText = $"Unsupported file type: {Path.GetFileName(path)}";
        }
    }

    // ── Directory mode ──

    private async Task LoadFromDirectoryAsync(string dirPath)
    {
        CleanupPreviousSource();
        IFileProvider provider = new DirectoryFileProvider(dirPath);
        Vm.DataSourceLabel = $"Directory: {dirPath}";
        await LoadAllViewersAsync(provider);
    }

    // ── IIPS list mode ──

    private async Task LoadFromIIPSListAsync(string lstPath)
    {
        CleanupPreviousSource();
        Vm.IsLoading = true;
        Vm.HasDataSource = false;
        Vm.StatusText = "Opening IIPS file list...";

        try
        {
            _unifiedArchive = await Task.Run(() => IIPSUnifiedArchive.Open(lstPath));

            // Feed to IIPS explorer
            if (IIPSExplorer.DataContext is IIPSArchiveFileExplorerViewModel explorerVm)
                explorerVm.TryOpenFileList(lstPath);

            IFileProvider provider = new IIPSFileProvider(_unifiedArchive);
            Vm.DataSourceLabel = $"IIPS List: {Path.GetFileName(lstPath)} ({_unifiedArchive.LoadedArchives.Count} archives, {_unifiedArchive.MergedEntries.Count} entries)";

            await LoadAllViewersAsync(provider);
        }
        catch (Exception ex)
        {
            Vm.StatusText = $"Error: {ex.Message}";
            Vm.IsLoading = false;
        }
    }

    // ── Single archive mode ──

    private async Task LoadFromSingleArchiveAsync(string ifsPath)
    {
        CleanupPreviousSource();
        Vm.IsLoading = true;
        Vm.StatusText = "Opening archive...";

        try
        {
            IIPSArchive archive = await Task.Run(() => IIPSArchive.Open(ifsPath));

            // Feed to IIPS explorer
            if (IIPSExplorer.DataContext is IIPSArchiveFileExplorerViewModel explorerVm)
                explorerVm.TryOpenArchive(ifsPath);

            // Try to load data from this single archive too
            // Create a mini unified archive wrapper for the file provider
            _unifiedArchive = null; // single archive, managed by explorer
            IFileProvider provider = CreateSingleArchiveProvider(archive);

            // Check if this archive has useful data
            bool hasStaticData = provider.Exists("common/staticdata/itemdata.dat") ||
                                 provider.Exists("common/staticdata/monsterdata.dat");

            Vm.DataSourceLabel = $"Archive: {Path.GetFileName(ifsPath)} ({archive.Entries.Count} entries)";

            if (hasStaticData)
            {
                await LoadAllViewersAsync(provider);
            }
            else
            {
                Vm.HasDataSource = false;
                Vm.StatusText = "Single archive mode — data viewer tabs disabled (no static data found).";
                Vm.IsLoading = false;
                MainTabs.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Vm.StatusText = $"Error: {ex.Message}";
            Vm.IsLoading = false;
        }
    }

    private static IFileProvider CreateSingleArchiveProvider(IIPSArchive archive)
    {
        return new SingleArchiveFileProvider(archive);
    }

    // ── Load all viewers ──

    private async Task LoadAllViewersAsync(IFileProvider provider)
    {
        Vm.IsLoading = true;
        Vm.HasDataSource = false;
        Vm.StatusText = "Loading all data...";

        try
        {
            List<Task> tasks = [];

            if (ItemControl.DataContext is ItemViewerViewModel itemVm)
                tasks.Add(itemVm.LoadAsync(provider));
            if (EntityControl.DataContext is EntityViewerViewModel entityVm)
                tasks.Add(entityVm.LoadAsync(provider));
            if (CraftControl.DataContext is CraftViewerViewModel craftVm)
                tasks.Add(craftVm.LoadAsync(provider));
            if (QuestControl.DataContext is QuestViewerViewModel questVm)
                tasks.Add(questVm.LoadAsync(provider));
            if (LevelMapControl.DataContext is LevelMapViewerViewModel levelVm)
                tasks.Add(levelVm.LoadClientFilesAsync(provider));

            await Task.WhenAll(tasks);

            Vm.HasDataSource = true;
            Vm.StatusText = "All data loaded.";
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

    // ── Cleanup ──

    private void CleanupPreviousSource()
    {
        _unifiedArchive?.Dispose();
        _unifiedArchive = null;
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        CleanupPreviousSource();
        base.OnClosing(e);
    }

    // ── File picker ──

    private async Task<string?> PickFileOrFolderAsync()
    {
        if (OperatingSystem.IsMacOS())
        {
            // macOS native picker supports selecting both files and folders
            return await MacNativePicker.PickFileOrFolderAsync("Select data source (directory, .lst, or .ifs)");
        }

        // Non-macOS: show file picker with option to pick folders
        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider == null) return null;

        // Try file picker first
        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = "Select data source (.lst, .ifs, or directory)",
                AllowMultiple = false,
                FileTypeFilter =
                [
                    new FilePickerFileType("IIPS Files") { Patterns = ["*.lst", "*.ifs"] },
                    new FilePickerFileType("All Files") { Patterns = ["*"] },
                ],
            });

        if (files.Count > 0)
            return files[0].TryGetLocalPath();

        // If no file selected, try folder picker
        IReadOnlyList<IStorageFolder> folders = await topLevel.StorageProvider.OpenFolderPickerAsync(
            new FolderPickerOpenOptions { Title = "Select MHO client files directory", AllowMultiple = false });

        return folders.Count == 0 ? null : folders[0].TryGetLocalPath();
    }
}

/// <summary>
/// IFileProvider backed by a single IIPSArchive (not unified).
/// </summary>
file sealed class SingleArchiveFileProvider : IFileProvider
{
    private readonly IIPSArchive _archive;
    private readonly Dictionary<string, IIPSArchiveEntry> _lookup;

    public SingleArchiveFileProvider(IIPSArchive archive)
    {
        _archive = archive;
        _lookup = new Dictionary<string, IIPSArchiveEntry>(StringComparer.OrdinalIgnoreCase);
        foreach (IIPSArchiveEntry entry in archive.Entries)
        {
            if (entry.Exists && !string.IsNullOrEmpty(entry.ArchivePath))
                _lookup[Normalize(entry.ArchivePath!)] = entry;
        }
    }

    public bool Exists(string relativePath) => _lookup.ContainsKey(Normalize(relativePath));

    public byte[] ReadAllBytes(string relativePath)
    {
        if (_lookup.TryGetValue(Normalize(relativePath), out var entry))
            return entry.ReadAllBytes();
        throw new FileNotFoundException($"Entry not found: {relativePath}");
    }

    public Stream OpenRead(string relativePath) => new MemoryStream(ReadAllBytes(relativePath), writable: false);

    public IEnumerable<string> EnumerateFiles(string relativeDir, string pattern)
    {
        string prefix = Normalize(relativeDir);
        if (!prefix.EndsWith('/')) prefix += '/';
        string ext = Path.GetExtension(pattern);
        string namePrefix = pattern.Contains('*') ? pattern[..pattern.IndexOf('*')] : pattern;

        return _lookup.Keys.Where(k =>
        {
            if (!k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) return false;
            string remainder = k[prefix.Length..];
            if (remainder.Contains('/')) return false;
            if (!string.IsNullOrEmpty(namePrefix) && !remainder.StartsWith(namePrefix, StringComparison.OrdinalIgnoreCase)) return false;
            if (!string.IsNullOrEmpty(ext) && !remainder.EndsWith(ext, StringComparison.OrdinalIgnoreCase)) return false;
            return true;
        });
    }

    public IEnumerable<string> EnumerateDirectories(string relativeDir)
    {
        string prefix = Normalize(relativeDir);
        if (!prefix.EndsWith('/')) prefix += '/';
        HashSet<string> dirs = new(StringComparer.OrdinalIgnoreCase);
        foreach (string key in _lookup.Keys)
        {
            if (!key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) continue;
            string remainder = key[prefix.Length..];
            int sep = remainder.IndexOf('/');
            if (sep > 0) dirs.Add(prefix + remainder[..sep]);
        }
        return dirs;
    }

    private static string Normalize(string path) => path.Replace('\\', '/');
}
