using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Level;

/// <summary>
/// Parsed data for a single game level/map.
/// </summary>
public sealed class LevelData
{
    public string Name { get; set; } = string.Empty;
    public string DirectoryPath { get; set; } = string.Empty;
    public LevelTerrainInfo Terrain { get; set; } = new();
    public LevelMiniMapInfo MiniMap { get; set; } = new();
    public LevelClientMiniMapAsset? ClientMiniMap { get; set; }
    public List<LevelRegion> Regions { get; } = [];
    public List<LevelEntity> Entities { get; } = [];

    /// <summary>BGRA pixel data for the terrain heightmap. Null if not loaded.</summary>
    public byte[]? HeightmapPixels { get; set; }
    public int HeightmapWidth { get; set; }
    public int HeightmapHeight { get; set; }
}

/// <summary>
/// Terrain dimensions from leveldata.xml LevelInfo element.
/// </summary>
public sealed class LevelTerrainInfo
{
    public int HeightmapSize { get; set; }
    public int HeightmapUnitSize { get; set; } = 1;
    public int HeightmapMaxHeight { get; set; }
    public int TerrainSectorSizeInMeters { get; set; }

    /// <summary>Total terrain extent in world units (HeightmapSize * HeightmapUnitSize).</summary>
    public float WorldSize => HeightmapSize * HeightmapUnitSize;
}

/// <summary>
/// MiniMap element from mission_mission0.xml.
/// </summary>
public sealed class LevelMiniMapInfo
{
    public float CenterX { get; set; }
    public float CenterY { get; set; }
    public float ExtendsX { get; set; }
    public float ExtendsY { get; set; }
    public int TexWidth { get; set; }
    public int TexHeight { get; set; }
}

/// <summary>
/// A region polygon from the mission Regions section.
/// </summary>
public sealed class LevelRegion
{
    public int Id { get; set; }
    public int Type { get; set; }
    public float Height { get; set; }
    public List<Vec3> Points { get; } = [];
}

/// <summary>
/// An entity/spawner parsed from the MHSpawners or Objects section.
/// </summary>
public sealed class LevelEntity
{
    public string Name { get; set; } = string.Empty;
    public string EntityClass { get; set; } = string.Empty;
    public Vec3 Pos { get; set; }
    public Vec4 Rotate { get; set; }
    public int EntityId { get; set; }
    public int RegionId { get; set; } = -1;
    public int FixedMonsterID { get; set; }
    public string LevelInfo { get; set; } = string.Empty;
    public string LayerPath { get; set; } = string.Empty;
    public bool SpawnerEnable { get; set; }

    // MHCollectSpawner
    public int ResourceType { get; set; } = -1;
    public int ResourceQuality { get; set; }

    // MHMonsterSpawnPoint
    public int SpawnAppearType { get; set; }
    public bool OnlyForBoss { get; set; }
}

public struct Vec3
{
    public float X;
    public float Y;
    public float Z;

    public Vec3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString() => $"{X},{Y},{Z}";
}

public struct Vec2
{
    public float X;
    public float Y;

    public Vec2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"{X},{Y}";
}

public struct Vec4
{
    public float X;
    public float Y;
    public float Z;
    public float W;

    public Vec4(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public override string ToString() => $"{X},{Y},{Z},{W}";
}
