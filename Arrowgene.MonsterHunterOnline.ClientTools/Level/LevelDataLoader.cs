using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.FileProvider;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

/// <summary>
/// Loads level data from the MHO client files directory.
/// Parses encrypted CryXML files: leveldata.xml and mission_mission0.xml per level.
/// </summary>
public sealed class LevelDataLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(LevelDataLoader));
    private readonly MinimapAssetLoader _minimapAssetLoader = new();
    private readonly TerrainTextureLoader _textureLoader = new();

    /// <summary>
    /// Discovers and loads all levels from the client files root directory.
    /// Expects <paramref name="clientFilesRoot"/>/levels/ to contain level subdirectories.
    /// </summary>
    /// <summary>
    /// Compatibility overload for loading from a filesystem directory path.
    /// </summary>
    public List<LevelData> LoadAll(string clientFilesRoot) => LoadAll(new DirectoryFileProvider(clientFilesRoot));

    /// <summary>
    /// Compatibility overload for loading a single level from its directory.
    /// </summary>
    public LevelData? LoadLevel(string levelDir)
    {
        // Walk up to find the client root (levelDir → levels → root)
        DirectoryInfo? di = new DirectoryInfo(levelDir);
        string? root = di.Parent?.Parent?.FullName;
        if (root == null) return null;
        string levelName = di.Name;
        return LoadLevel(new DirectoryFileProvider(root), $"levels/{levelName}", levelName);
    }

    public List<LevelData> LoadAll(IFileProvider provider)
    {
        List<LevelData> levels = [];
        List<string> dirs = provider.EnumerateDirectories("levels")
            .OrderBy(d => d, StringComparer.OrdinalIgnoreCase).ToList();

        foreach (string dir in dirs)
        {
            string levelName = dir.Split('/').Last();
            LevelData? level = LoadLevel(provider, dir, levelName);
            if (level != null)
                levels.Add(level);
        }

        Logger.Info($"Loaded {levels.Count} levels");
        return levels;
    }

    private LevelData? LoadLevel(IFileProvider provider, string levelDir, string levelName)
    {
        LevelData level = new()
        {
            Name = levelName,
            DirectoryPath = levelDir,
        };

        string leveldataPath = $"{levelDir}/leveldata.xml";
        if (provider.Exists(leveldataPath))
        {
            try
            {
                ParseLevelData(level, provider.ReadAllBytes(leveldataPath));
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to parse leveldata.xml for {levelName}: {ex.Message}");
            }
        }

        string missionPath = $"{levelDir}/mission_mission0.xml";
        if (provider.Exists(missionPath))
        {
            try
            {
                ParseMission(level, provider.ReadAllBytes(missionPath));
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to parse mission_mission0.xml for {levelName}: {ex.Message}");
            }
        }
        else
        {
            Logger.Debug($"No mission file for {levelName}");
            return null;
        }

        try
        {
            level.ClientMiniMap = _minimapAssetLoader.LoadForLevel(provider, levelName);
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load client minimap asset for {levelName}: {ex.Message}");
        }

        string coverPath = $"{levelDir}/terrain/cover.ctc";
        string terrainDatPath = $"{levelDir}/terrain/terrain.dat";
        if (provider.Exists(coverPath))
        {
            try
            {
                var (heightmapSize, unitSize) = provider.Exists(terrainDatPath)
                    ? TerrainTextureLoader.ReadTerrainInfo(provider.ReadAllBytes(terrainDatPath))
                    : (0, 1);
                if (heightmapSize <= 0)
                    heightmapSize = level.Terrain.HeightmapSize > 0 ? level.Terrain.HeightmapSize : 2048;

                level.Terrain.HeightmapSize = heightmapSize;
                level.Terrain.HeightmapUnitSize = unitSize;

                byte[]? pixels = _textureLoader.LoadTexture(provider.ReadAllBytes(coverPath), heightmapSize, out int w, out int h);
                if (pixels != null)
                {
                    level.HeightmapPixels = pixels;
                    level.HeightmapWidth = w;
                    level.HeightmapHeight = h;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load terrain texture for {levelName}: {ex.Message}");
            }
        }

        return level;
    }

    private void ParseLevelData(LevelData level, byte[] data)
    {
        XDocument doc = MhoCryXmlCodec.LoadDocument(data);
        XElement? levelInfo = doc.Root?.Element("LevelInfo");
        if (levelInfo == null) return;

        level.Terrain = new LevelTerrainInfo
        {
            HeightmapSize = GetIntAttr(levelInfo, "HeightmapSize"),
            HeightmapUnitSize = GetIntAttr(levelInfo, "HeightmapUnitSize", 1),
            HeightmapMaxHeight = GetIntAttr(levelInfo, "HeightmapMaxHeight"),
            TerrainSectorSizeInMeters = GetIntAttr(levelInfo, "TerrainSectorSizeInMeters"),
        };
    }

    private void ParseMission(LevelData level, byte[] data)
    {
        XDocument doc = MhoCryXmlCodec.LoadDocument(data);
        XElement root = doc.Root!;

        // MiniMap
        XElement? minimap = root.Element("MiniMap");
        if (minimap != null)
        {
            level.MiniMap = new LevelMiniMapInfo
            {
                CenterX = GetFloatAttr(minimap, "CenterX"),
                CenterY = GetFloatAttr(minimap, "CenterY"),
                ExtendsX = GetFloatAttr(minimap, "ExtendsX"),
                ExtendsY = GetFloatAttr(minimap, "ExtendsY"),
                TexWidth = GetIntAttr(minimap, "TexWidth"),
                TexHeight = GetIntAttr(minimap, "TexHeight"),
            };
        }

        // Regions
        XElement? regionsInfo = root.Element("RegionsInfo");
        XElement? regions = regionsInfo?.Element("Regions");
        if (regions != null)
        {
            foreach (XElement regionEl in regions.Elements("Region"))
            {
                LevelRegion region = new()
                {
                    Id = GetIntAttr(regionEl, "ID"),
                    Type = GetIntAttr(regionEl, "Type"),
                    Height = GetFloatAttr(regionEl, "Height"),
                };

                XElement? points = regionEl.Element("Points");
                if (points != null)
                {
                    foreach (XElement pt in points.Elements("Point"))
                    {
                        region.Points.Add(ParseVec3(pt.Attribute("pos")?.Value));
                    }
                }

                level.Regions.Add(region);
            }
        }

        // MHSpawners (the primary entity data)
        XElement? spawners = root.Element("MHSpawners");
        if (spawners != null)
        {
            foreach (XElement entity in spawners.Elements("Entity"))
            {
                LevelEntity ent = ParseEntity(entity);
                level.Entities.Add(ent);
            }
        }

        // Also scan Objects for MHPlayerSpawnPoint (they appear here, not in MHSpawners)
        XElement? objects = root.Element("Objects");
        if (objects != null)
        {
            foreach (XElement el in objects.Elements())
            {
                string? entityClass = el.Attribute("EntityClass")?.Value;
                if (entityClass == "MHPlayerSpawnPoint")
                {
                    LevelEntity ent = ParseEntity(el);
                    level.Entities.Add(ent);
                }
            }
        }
    }

    private LevelEntity ParseEntity(XElement el)
    {
        return new LevelEntity
        {
            Name = el.Attribute("Name")?.Value ?? string.Empty,
            EntityClass = el.Attribute("EntityClass")?.Value ?? string.Empty,
            Pos = ParseVec3(el.Attribute("Pos")?.Value),
            Rotate = ParseVec4(el.Attribute("Rotate")?.Value),
            EntityId = GetIntAttr(el, "EntityId"),
            RegionId = GetIntAttr(el, "RegionID", -1),
            FixedMonsterID = GetIntAttr(el, "FixedMonsterID"),
            LevelInfo = el.Attribute("LevelInfo")?.Value ?? string.Empty,
            LayerPath = el.Attribute("LayerPath")?.Value ?? string.Empty,
            SpawnerEnable = el.Attribute("SpawnerEnable")?.Value == "1",
            ResourceType = GetIntAttr(el, "ResourceType", -1),
            ResourceQuality = GetIntAttr(el, "ResourceQuality"),
            SpawnAppearType = GetIntAttr(el, "SpawnAppearType"),
            OnlyForBoss = el.Attribute("OnlyForBoss")?.Value == "1",
        };
    }

    private static Vec3 ParseVec3(string? value)
    {
        if (string.IsNullOrEmpty(value)) return default;
        string[] parts = value.Split(',');
        if (parts.Length < 3) return default;
        return new Vec3(
            ParseFloat(parts[0]),
            ParseFloat(parts[1]),
            ParseFloat(parts[2])
        );
    }

    private static Vec4 ParseVec4(string? value)
    {
        if (string.IsNullOrEmpty(value)) return default;
        string[] parts = value.Split(',');
        if (parts.Length < 4) return default;
        return new Vec4(
            ParseFloat(parts[0]),
            ParseFloat(parts[1]),
            ParseFloat(parts[2]),
            ParseFloat(parts[3])
        );
    }

    private static float ParseFloat(string s)
    {
        float.TryParse(s.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float v);
        return v;
    }

    private static int GetIntAttr(XElement el, string name, int defaultValue = 0)
    {
        string? val = el.Attribute(name)?.Value;
        if (val == null) return defaultValue;
        return int.TryParse(val, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v) ? v : defaultValue;
    }

    private static float GetFloatAttr(XElement el, string name, float defaultValue = 0f)
    {
        string? val = el.Attribute(name)?.Value;
        if (val == null) return defaultValue;
        return float.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out float v) ? v : defaultValue;
    }
}
