using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Arrowgene.Logging;

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
    public List<LevelData> LoadAll(string clientFilesRoot)
    {
        string levelsDir = Path.Combine(clientFilesRoot, "levels");
        if (!Directory.Exists(levelsDir))
        {
            Logger.Error($"Levels directory not found: {levelsDir}");
            return [];
        }

        List<LevelData> levels = [];
        string[] dirs = Directory.GetDirectories(levelsDir);
        Array.Sort(dirs, StringComparer.OrdinalIgnoreCase);

        foreach (string dir in dirs)
        {
            LevelData? level = LoadLevel(dir, clientFilesRoot);
            if (level != null)
            {
                levels.Add(level);
            }
        }

        Logger.Info($"Loaded {levels.Count} levels from {levelsDir}");
        return levels;
    }

    /// <summary>
    /// Loads a single level from its directory.
    /// </summary>
    public LevelData? LoadLevel(string levelDir)
    {
        return LoadLevel(levelDir, ResolveClientFilesRoot(levelDir));
    }

    private LevelData? LoadLevel(string levelDir, string? clientFilesRoot)
    {
        string levelName = Path.GetFileName(levelDir);
        LevelData level = new()
        {
            Name = levelName,
            DirectoryPath = levelDir,
        };

        string leveldataPath = Path.Combine(levelDir, "leveldata.xml");
        if (File.Exists(leveldataPath))
        {
            try
            {
                ParseLevelData(level, leveldataPath);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to parse leveldata.xml for {levelName}: {ex.Message}");
            }
        }

        string missionPath = Path.Combine(levelDir, "mission_mission0.xml");
        if (File.Exists(missionPath))
        {
            try
            {
                ParseMission(level, missionPath);
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

        if (!string.IsNullOrEmpty(clientFilesRoot))
        {
            try
            {
                level.ClientMiniMap = _minimapAssetLoader.LoadForLevel(clientFilesRoot, levelName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load client minimap asset for {levelName}: {ex.Message}");
            }
        }

        string coverPath = Path.Combine(levelDir, "terrain", "cover.ctc");
        string terrainDatPath = Path.Combine(levelDir, "terrain", "terrain.dat");
        if (File.Exists(coverPath))
        {
            try
            {
                // Use terrain.dat header for heightmap dimensions and unit scale
                var (heightmapSize, unitSize) = TerrainTextureLoader.ReadTerrainInfo(terrainDatPath);
                if (heightmapSize <= 0)
                    heightmapSize = level.Terrain.HeightmapSize > 0 ? level.Terrain.HeightmapSize : 2048;

                level.Terrain.HeightmapSize = heightmapSize;
                level.Terrain.HeightmapUnitSize = unitSize;

                // Render CTC at native heightmap resolution (1:1 with DXT tile pixels).
                // The UI scales the bitmap to fill WorldSize via bilinear filtering.
                byte[]? pixels = _textureLoader.LoadTexture(coverPath, heightmapSize, out int w, out int h);
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

    private static string? ResolveClientFilesRoot(string levelDir)
    {
        DirectoryInfo? directory = new DirectoryInfo(levelDir);
        if (!directory.Exists)
        {
            return null;
        }

        DirectoryInfo? levelsDirectory = directory.Parent;
        if (levelsDirectory == null || !string.Equals(levelsDirectory.Name, "levels", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return levelsDirectory.Parent?.FullName;
    }

    private void ParseLevelData(LevelData level, string path)
    {
        XDocument doc = MhoCryXmlCodec.LoadFile(path);
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

    private void ParseMission(LevelData level, string path)
    {
        XDocument doc = MhoCryXmlCodec.LoadFile(path);
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
