using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media.Imaging;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

/// <summary>
/// Loads game icons from MHO client files with caching.
/// </summary>
public sealed class GameIconLoader
{
    private readonly Dictionary<string, Bitmap?> _cache = [];

    // Populated after Initialize()
    private string _itemIconDir = string.Empty;
    private string _monsterIconDir = string.Empty;

    // Monster icons are named {id}_{name}.png — build a lookup by ID
    private readonly Dictionary<int, string> _monsterIconPaths = [];

    public void Initialize(string clientFilesRoot)
    {
        _cache.Clear();
        _monsterIconPaths.Clear();

        _itemIconDir = Path.Combine(clientFilesRoot, "libs", "ui", "flashassets", "images", "icon");
        _monsterIconDir = Path.Combine(clientFilesRoot, "libs", "ui", "flashassets", "images", "illustratebook", "monstericon");

        // Scan monster icon directory and index by ID prefix
        if (Directory.Exists(_monsterIconDir))
        {
            foreach (string file in Directory.GetFiles(_monsterIconDir, "*.png"))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                int sep = name.IndexOf('_');
                if (sep > 0 && int.TryParse(name.AsSpan(0, sep), out int id))
                {
                    _monsterIconPaths[id] = file;
                }
            }
        }
    }

    /// <summary>
    /// Load an item/equipment icon by its icon key (e.g. "item_icon_01", "ep_lslw_14").
    /// </summary>
    public Bitmap? LoadItemIcon(string? iconKey)
    {
        if (string.IsNullOrEmpty(iconKey) || string.IsNullOrEmpty(_itemIconDir))
            return null;

        string cacheKey = $"item:{iconKey}";
        if (_cache.TryGetValue(cacheKey, out var cached))
            return cached;

        string path = Path.Combine(_itemIconDir, $"{iconKey}.png");
        Bitmap? bmp = LoadBitmap(path);
        _cache[cacheKey] = bmp;
        return bmp;
    }

    /// <summary>
    /// Load a monster icon by monster ID (looks up {id}_{name}.png in illustratebook/monstericon/).
    /// </summary>
    public Bitmap? LoadMonsterIcon(int monsterId)
    {
        if (monsterId == 0)
            return null;

        string cacheKey = $"mon:{monsterId}";
        if (_cache.TryGetValue(cacheKey, out var cached))
            return cached;

        Bitmap? bmp = null;
        if (_monsterIconPaths.TryGetValue(monsterId, out string? path))
            bmp = LoadBitmap(path);

        _cache[cacheKey] = bmp;
        return bmp;
    }

    private static Bitmap? LoadBitmap(string path)
    {
        if (!File.Exists(path)) return null;
        try { return new Bitmap(path); }
        catch { return null; }
    }
}
