using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;
using Arrowgene.MonsterHunterOnline.ClientTools.Flash;
using Arrowgene.Logging;

// FlaArchive is no longer used for minimap rendering — the authoritative
// source is the compiled SWF rendered via SwfSceneRenderer.

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

/// <summary>
/// Loads the client-authored minimap config and its paired visual asset from the MHO client files tree.
/// </summary>
public sealed class MinimapAssetLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(MinimapAssetLoader));
    private static readonly StringComparer PathComparer = StringComparer.OrdinalIgnoreCase;

    public LevelClientMiniMapAsset? LoadForLevel(IFileProvider provider, string levelName)
    {
        string? configPath = FindConfigPath(provider, levelName);
        if (configPath != null)
        {
            string assetName = Path.GetFileNameWithoutExtension(configPath);
            string? visualAssetPath = FindVisualAssetPath(provider, assetName);
            Logger.Info($"Minimap for '{levelName}': config={configPath}, asset={assetName}, visual={visualAssetPath ?? "(none)"}");
            return Load(provider, configPath, levelName, assetName, visualAssetPath);
        }

        Logger.Debug($"No minimap config for '{levelName}', trying SWF-only");
        string? visualPath = FindVisualAssetPath(provider, levelName);
        if (visualPath == null)
        {
            Logger.Debug($"No minimap visual asset for '{levelName}'");
            return null;
        }

        LevelClientMiniMapAsset asset = new()
        {
            LevelName = levelName,
            AssetName = levelName,
        };
        TryLoadVisualAsset(asset, provider, visualPath);
        return asset.RenderedImage != null || asset.CanvasWidth > 0 ? asset : null;
    }

    public string? FindConfigPath(IFileProvider provider, string levelName)
    {
        string exactPath = $"libs/minimap/{levelName}.xml";
        if (provider.Exists(exactPath))
            return exactPath;

        return ResolveBestMatch(provider.EnumerateFiles("libs/minimap", "*.xml"), levelName);
    }

    public string? FindVisualAssetPath(IFileProvider provider, string assetName)
    {
        string flaPath = $"libs/ui/flashassets/images/minimap/{assetName}.fla";
        if (provider.Exists(flaPath))
            return flaPath;

        string swfPath = $"libs/ui/flashassets/images/minimap/{assetName}.swf";
        if (provider.Exists(swfPath))
            return swfPath;

        return ResolveBestMatch(provider.EnumerateFiles("libs/ui/flashassets/images/minimap", "*.fla"), assetName)
            ?? ResolveBestMatch(provider.EnumerateFiles("libs/ui/flashassets/images/minimap", "*.swf"), assetName);
    }

    public LevelClientMiniMapAsset Load(IFileProvider provider, string configPath, string? levelName = null,
        string? assetName = null, string? visualAssetPath = null)
    {
        XDocument doc = MhoCryXmlCodec.LoadDocument(provider.ReadAllBytes(configPath));
        XElement root = doc.Root ?? throw new InvalidDataException($"Minimap config '{configPath}' has no root element.");

        LevelClientMiniMapAsset asset = new()
        {
            LevelName = levelName ?? Path.GetFileNameWithoutExtension(configPath),
            AssetName = assetName ?? Path.GetFileNameWithoutExtension(configPath),
            ConfigPath = configPath,
            VisualAssetPath = visualAssetPath,
        };

        XElement? regionGroup = root.Element("MiniMapRegions");
        if (regionGroup != null)
        {
            asset.Alpha = GetIntAttr(regionGroup, "alpha");
            foreach (XElement regionEl in regionGroup.Elements("MiniMapRegion"))
            {
                asset.Regions.Add(ParseRegion(regionEl));
            }
        }

        if (!string.IsNullOrEmpty(visualAssetPath))
        {
            TryLoadVisualAsset(asset, provider, visualAssetPath);
        }

        return asset;
    }

    private static void TryLoadVisualAsset(LevelClientMiniMapAsset asset, IFileProvider provider, string visualAssetPath)
    {
        string swfPath = Path.ChangeExtension(visualAssetPath, ".swf");
        if (provider.Exists(swfPath))
        {
            try
            {
                SwfFile swf = SwfFile.Open(provider.ReadAllBytes(swfPath), swfPath);

                if (asset.CanvasWidth <= 0 && swf.FrameSize.WidthPixels > 0)
                    asset.CanvasWidth = swf.FrameSize.WidthPixels;
                if (asset.CanvasHeight <= 0 && swf.FrameSize.HeightPixels > 0)
                    asset.CanvasHeight = swf.FrameSize.HeightPixels;

                byte[]? rendered = SwfSceneRenderer.RenderFirstFrame(swf);
                if (rendered != null && rendered.Length > 0)
                {
                    asset.VisualAssetPath = swfPath;
                    asset.RenderedImage = rendered;
                    Logger.Info($"SWF rendered: {swfPath} ({asset.CanvasWidth}x{asset.CanvasHeight}, {rendered.Length} bytes)");
                    return;
                }

                Logger.Error($"SWF rendering produced no output for '{swfPath}'");
            }
            catch (Exception ex)
            {
                Logger.Error($"SWF rendering failed for '{swfPath}': {ex.Message}");
            }
        }
    }

    private static LevelClientMiniMapRegion ParseRegion(XElement regionEl)
    {
        XAttribute? idAttr = regionEl.Attribute("groupid") ?? regionEl.Attribute("roomid");
        string identifierName = idAttr?.Name.LocalName ?? string.Empty;
        int? identifierValue = idAttr != null ? ParseNullableInt(idAttr.Value) : null;

        return new LevelClientMiniMapRegion
        {
            IdentifierName = identifierName,
            IdentifierValue = identifierValue,
            TopLeft3D = ParseVec2(regionEl.Attribute("TL_3D")?.Value),
            BottomRight3D = ParseVec2(regionEl.Attribute("BR_3D")?.Value),
            TopLeft2D = ParseVec2(regionEl.Attribute("TL_2D")?.Value),
            BottomRight2D = ParseVec2(regionEl.Attribute("BR_2D")?.Value),
            MinZ3D = ParseNullableFloat(regionEl.Attribute("MINZ_3D")?.Value),
        };
    }

    private static string? ResolveBestMatch(IEnumerable<string> paths, string levelName)
    {
        string normalizedLevelName = Normalize(levelName);
        if (normalizedLevelName.Length == 0)
        {
            return null;
        }

        List<(string path, int score, int lengthDelta)> candidates = [];
        foreach (string path in paths)
        {
            string stem = Path.GetFileNameWithoutExtension(path);
            int score = ScoreCandidate(stem, levelName, normalizedLevelName);
            if (score == int.MaxValue)
            {
                continue;
            }

            candidates.Add((path, score, Math.Abs(stem.Length - levelName.Length)));
        }

        if (candidates.Count == 0)
        {
            return null;
        }

        candidates.Sort((left, right) =>
        {
            int scoreCompare = left.score.CompareTo(right.score);
            if (scoreCompare != 0)
            {
                return scoreCompare;
            }

            int lengthCompare = left.lengthDelta.CompareTo(right.lengthDelta);
            if (lengthCompare != 0)
            {
                return lengthCompare;
            }

            return PathComparer.Compare(left.path, right.path);
        });

        if (candidates.Count > 1 &&
            candidates[0].score == candidates[1].score &&
            candidates[0].lengthDelta == candidates[1].lengthDelta)
        {
            Logger.Info($"Ambiguous minimap asset match for '{levelName}': '{candidates[0].path}' and '{candidates[1].path}'.");
            return null;
        }

        return candidates[0].path;
    }

    private static int ScoreCandidate(string stem, string levelName, string normalizedLevelName)
    {
        if (string.Equals(stem, levelName, StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }

        string normalizedStem = Normalize(stem);
        if (normalizedStem == normalizedLevelName)
        {
            return 1;
        }

        if (normalizedStem.EndsWith(normalizedLevelName, StringComparison.Ordinal))
        {
            return 2;
        }

        if (normalizedStem.StartsWith(normalizedLevelName, StringComparison.Ordinal))
        {
            return 3;
        }

        return int.MaxValue;
    }

    private static string Normalize(string value)
    {
        return new string(value
            .Where(char.IsLetterOrDigit)
            .Select(char.ToLowerInvariant)
            .ToArray());
    }

    private static Vec2 ParseVec2(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return default;
        }

        string[] parts = value.Split(',');
        if (parts.Length < 2)
        {
            return default;
        }

        return new Vec2(ParseFloat(parts[0]), ParseFloat(parts[1]));
    }

    private static float ParseFloat(string value)
    {
        return float.Parse(value.Trim(), CultureInfo.InvariantCulture);
    }

    private static float? ParseNullableFloat(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return ParseFloat(value);
    }

    private static int GetIntAttr(XElement element, string attrName, int defaultValue = 0)
    {
        XAttribute? attr = element.Attribute(attrName);
        int? value = attr != null ? ParseNullableInt(attr.Value) : null;
        return value ?? defaultValue;
    }

    private static int? ParseNullableInt(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return int.Parse(value.Trim(), CultureInfo.InvariantCulture);
    }
}

public sealed class LevelClientMiniMapAsset
{
    public string LevelName { get; set; } = string.Empty;
    public string AssetName { get; set; } = string.Empty;
    public string ConfigPath { get; set; } = string.Empty;
    public string? VisualAssetPath { get; set; }
    public float CanvasWidth { get; set; }
    public float CanvasHeight { get; set; }
    public int Alpha { get; set; }
    public List<LevelClientMiniMapRegion> Regions { get; } = [];
    /// <summary>Pre-rendered minimap image (PNG) from the SWF, covering the full canvas.</summary>
    public byte[]? RenderedImage { get; set; }

    public bool TryProjectWorldPosition(Vec3 worldPosition, out Vec2 mapPosition)
    {
        LevelClientMiniMapRegion? region = FindRegion(worldPosition);
        if (region == null)
        {
            mapPosition = default;
            return false;
        }

        mapPosition = region.Project(worldPosition.X, worldPosition.Y);
        return true;
    }

    public LevelClientMiniMapRegion? FindRegion(Vec3 worldPosition)
    {
        LevelClientMiniMapRegion? fallback = null;
        LevelClientMiniMapRegion? bestZMatch = null;
        float bestMinZ = float.NegativeInfinity;

        foreach (LevelClientMiniMapRegion region in Regions)
        {
            if (!region.ContainsWorldXY(worldPosition.X, worldPosition.Y))
            {
                continue;
            }

            if (fallback == null)
            {
                fallback = region;
            }

            if (!region.MinZ3D.HasValue || worldPosition.Z < region.MinZ3D.Value)
            {
                continue;
            }

            if (region.MinZ3D.Value >= bestMinZ)
            {
                bestMinZ = region.MinZ3D.Value;
                bestZMatch = region;
            }
        }

        return bestZMatch ?? fallback;
    }
}

public sealed class LevelClientMiniMapRegion
{
    public string IdentifierName { get; set; } = string.Empty;
    public int? IdentifierValue { get; set; }
    public Vec2 TopLeft3D { get; set; }
    public Vec2 BottomRight3D { get; set; }
    public Vec2 TopLeft2D { get; set; }
    public Vec2 BottomRight2D { get; set; }
    public float? MinZ3D { get; set; }

    public bool ContainsWorldXY(float x, float y)
    {
        float minX = Math.Min(TopLeft3D.X, BottomRight3D.X);
        float maxX = Math.Max(TopLeft3D.X, BottomRight3D.X);
        float minY = Math.Min(TopLeft3D.Y, BottomRight3D.Y);
        float maxY = Math.Max(TopLeft3D.Y, BottomRight3D.Y);
        return x >= minX && x <= maxX && y >= minY && y <= maxY;
    }

    public Vec2 Project(float worldX, float worldY)
    {
        float tx = InverseLerp(TopLeft3D.X, BottomRight3D.X, worldX);
        float ty = InverseLerp(TopLeft3D.Y, BottomRight3D.Y, worldY);
        return new Vec2(
            Lerp(TopLeft2D.X, BottomRight2D.X, tx),
            Lerp(TopLeft2D.Y, BottomRight2D.Y, ty));
    }

    private static float InverseLerp(float start, float end, float value)
    {
        if (Math.Abs(end - start) < float.Epsilon)
        {
            return 0f;
        }

        return Math.Clamp((value - start) / (end - start), 0f, 1f);
    }

    private static float Lerp(float start, float end, float t)
    {
        return start + ((end - start) * t);
    }
}
