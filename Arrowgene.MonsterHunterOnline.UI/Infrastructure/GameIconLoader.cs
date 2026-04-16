using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;
using Avalonia.Media.Imaging;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

/// <summary>
/// Loads game icons from an IFileProvider with caching.
/// </summary>
public sealed class GameIconLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(GameIconLoader));

    private readonly Dictionary<string, Bitmap?> _cache = [];
    private IFileProvider? _provider;

    // Monster icons are named {id}_{name}.png — build a lookup by ID
    private readonly Dictionary<int, string> _monsterIconPaths = [];

    public void Initialize(IFileProvider provider)
    {
        _cache.Clear();
        _monsterIconPaths.Clear();
        _provider = provider;

        // Scan monster icon directory and index by ID prefix
        foreach (string file in provider.EnumerateFiles("libs/ui/flashassets/images/illustratebook/monstericon", "*.png"))
        {
            string name = Path.GetFileNameWithoutExtension(file);
            int sep = name.IndexOf('_');
            if (sep > 0 && int.TryParse(name.AsSpan(0, sep), out int id))
            {
                _monsterIconPaths[id] = file;
            }
        }

        Logger.Info($"GameIconLoader initialized: {_monsterIconPaths.Count} monster icons indexed, provider={provider.GetType().Name}");

        // Quick probe: check if the item icon directory has any files
        int itemIconCount = 0;
        foreach (string _ in provider.EnumerateFiles("libs/ui/flashassets/images/icon", "*.png"))
        {
            itemIconCount++;
            if (itemIconCount >= 3) break; // just probe a few
        }
        Logger.Info($"GameIconLoader item icon probe: found {(itemIconCount >= 3 ? "3+" : itemIconCount.ToString())} PNGs");
    }

    /// <summary>
    /// Load an item/equipment icon by its icon key (e.g. "item_icon_01", "ep_lslw_14").
    /// </summary>
    public Bitmap? LoadItemIcon(string? iconKey)
    {
        if (string.IsNullOrEmpty(iconKey) || _provider == null)
            return null;

        string cacheKey = $"item:{iconKey}";
        if (_cache.TryGetValue(cacheKey, out var cached))
            return cached;

        string rel = $"libs/ui/flashassets/images/icon/{iconKey}.png";
        Bitmap? bmp = LoadBitmap(rel);
        _cache[cacheKey] = bmp;
        return bmp;
    }

    /// <summary>
    /// Load a monster icon by monster ID (looks up {id}_{name}.png in illustratebook/monstericon/).
    /// </summary>
    public Bitmap? LoadMonsterIcon(int monsterId)
    {
        if (monsterId == 0 || _provider == null)
            return null;

        string cacheKey = $"mon:{monsterId}";
        if (_cache.TryGetValue(cacheKey, out var cached))
            return cached;

        Bitmap? bmp = null;
        if (_monsterIconPaths.TryGetValue(monsterId, out string? rel))
            bmp = LoadBitmap(rel);

        _cache[cacheKey] = bmp;
        return bmp;
    }

    private Bitmap? LoadBitmap(string relativePath)
    {
        if (_provider == null) return null;
        if (!_provider.Exists(relativePath))
        {
            Logger.Debug($"Icon not found: {relativePath}");
            return null;
        }

        try
        {
            byte[] data = _provider.ReadAllBytes(relativePath);
            // Do not dispose the MemoryStream — Avalonia Bitmap may hold a reference to it
            MemoryStream ms = new(data, writable: false);
            return new Bitmap(ms);
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load icon {relativePath}: {ex.Message}");
            return null;
        }
    }
}
