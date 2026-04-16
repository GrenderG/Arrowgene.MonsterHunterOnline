using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal enum RecentEntryKind
{
    Archive,
    FileList,
    Directory
}

internal sealed class RecentEntry
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = "";

    [JsonPropertyName("kind")]
    public RecentEntryKind Kind { get; set; }

    [JsonPropertyName("openedUtc")]
    public DateTime OpenedUtc { get; set; }

    [JsonIgnore]
    public string DisplayName => System.IO.Path.GetFileName(Path.TrimEnd('/', '\\'));

    [JsonIgnore]
    public string KindLabel => Kind switch
    {
        RecentEntryKind.Archive => "Archive",
        RecentEntryKind.FileList => "File List",
        RecentEntryKind.Directory => "Directory",
        _ => ""
    };
}

internal static class RecentFilesStore
{
    private const int MaxEntries = 15;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static List<RecentEntry> Load()
    {
        string filePath = GetFilePath();
        if (!File.Exists(filePath))
            return [];

        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<RecentEntry>>(json, JsonOpts) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public static bool Add(string path, RecentEntryKind kind)
    {
        try
        {
            List<RecentEntry> entries = Load();

            entries.RemoveAll(e => string.Equals(e.Path, path, StringComparison.OrdinalIgnoreCase));

            entries.Insert(0, new RecentEntry
            {
                Path = path,
                Kind = kind,
                OpenedUtc = DateTime.UtcNow
            });

            if (entries.Count > MaxEntries)
                entries.RemoveRange(MaxEntries, entries.Count - MaxEntries);

            Save(entries);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void Clear()
    {
        try { Save([]); } catch { /* best effort */ }
    }

    private static void Save(List<RecentEntry> entries)
    {
        string filePath = GetFilePath();
        string? dir = Path.GetDirectoryName(filePath);
        if (dir != null)
            Directory.CreateDirectory(dir);

        string json = JsonSerializer.Serialize(entries, JsonOpts);
        File.WriteAllText(filePath, json);
    }

    private static string GetFilePath()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Arrowgene.MonsterHunterOnline",
            "UI",
            "recent-files.json");
    }
}
