#nullable enable
using System;
using System.IO;
using System.Linq;
using Arrowgene.MonsterHunterOnline.ClientTools.Level;
using Xunit;

namespace Arrowgene.MonsterHunterOnline.Test.ClientTools.Level;

public sealed class MinimapAssetLoaderTest : IDisposable
{
    private readonly string _rootPath;

    public MinimapAssetLoaderTest()
    {
        _rootPath = Path.Combine(Path.GetTempPath(), $"mho-minimap-loader-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_rootPath);
    }

    [Fact]
    public void LoadForLevel_LoadsRegionMappingsAndProjectsWorldPosition()
    {
        WriteMinimapFiles("level_012", """
            <?xml version="1.0" encoding="utf-8"?>
            <MiniMapDatas>
              <MiniMapRegions alpha="50">
                <MiniMapRegion groupid="1" TL_3D="2021.6657,3205.5757" TL_2D="127,76" BR_3D="2112.7665,3301.5134" BR_2D="187,12" />
              </MiniMapRegions>
            </MiniMapDatas>
            """);

        MinimapAssetLoader loader = new();
        LevelClientMiniMapAsset? asset = loader.LoadForLevel(new Arrowgene.MonsterHunterOnline.ClientTools.FileProvider.DirectoryFileProvider(_rootPath), "level_012");

        Assert.NotNull(asset);
        Assert.Equal("level_012", asset!.AssetName);
        Assert.Equal(50, asset.Alpha);
        Assert.Single(asset.Regions);
        Assert.EndsWith(Path.Combine("libs", "ui", "flashassets", "images", "minimap", "level_012.swf"), asset.VisualAssetPath, StringComparison.OrdinalIgnoreCase);

        bool projected = asset.TryProjectWorldPosition(new Vec3(2067.2161f, 3253.5446f, 35f), out Vec2 mapPosition);

        Assert.True(projected);
        Assert.InRange(mapPosition.X, 156.8f, 157.8f);
        Assert.InRange(mapPosition.Y, 43.5f, 44.5f);
    }

    [Fact]
    public void LoadForLevel_UsesFallbackAssetNameMatchWhenExactNameIsMissing()
    {
        WriteMinimapFiles("level_pvp01", """
            <?xml version="1.0" encoding="utf-8"?>
            <MiniMapDatas>
              <MiniMapRegions alpha="80">
                <MiniMapRegion roomid="0" TL_3D="100,200" TL_2D="10,20" BR_3D="200,300" BR_2D="110,120" MINZ_3D="5" />
              </MiniMapRegions>
            </MiniMapDatas>
            """);

        MinimapAssetLoader loader = new();
        LevelClientMiniMapAsset? asset = loader.LoadForLevel(new Arrowgene.MonsterHunterOnline.ClientTools.FileProvider.DirectoryFileProvider(_rootPath), "pvp_01");

        Assert.NotNull(asset);
        Assert.Equal("level_pvp01", asset!.AssetName);
        Assert.Equal("roomid", asset.Regions.Single().IdentifierName);
        Assert.Equal(0, asset.Regions.Single().IdentifierValue);
        Assert.Equal(5f, asset.Regions.Single().MinZ3D);
    }

    [Fact]
    public void LevelDataLoader_LoadLevel_PopulatesClientMinimap()
    {
        string levelName = "level_001";
        string levelDir = Path.Combine(_rootPath, "levels", levelName);
        Directory.CreateDirectory(levelDir);

        File.WriteAllText(Path.Combine(levelDir, "leveldata.xml"), """
            <?xml version="1.0" encoding="utf-8"?>
            <Level>
              <LevelInfo HeightmapSize="1024" HeightmapUnitSize="2" HeightmapMaxHeight="512" TerrainSectorSizeInMeters="64" />
            </Level>
            """);

        File.WriteAllText(Path.Combine(levelDir, "mission_mission0.xml"), """
            <?xml version="1.0" encoding="utf-8"?>
            <Mission>
              <MiniMap CenterX="512" CenterY="512" ExtendsX="512" ExtendsY="512" TexWidth="1024" TexHeight="1024" />
              <RegionsInfo>
                <Regions>
                  <Region ID="7" Type="1" Height="0">
                    <Points>
                      <Point pos="0,0,0" />
                      <Point pos="10,0,0" />
                      <Point pos="10,10,0" />
                    </Points>
                  </Region>
                </Regions>
              </RegionsInfo>
              <MHSpawners>
                <Entity Name="SpawnA" EntityClass="MHPlayerSpawnPoint" Pos="5,6,7" Rotate="0,0,0,1" EntityId="100" />
              </MHSpawners>
            </Mission>
            """);

        WriteMinimapFiles(levelName, """
            <?xml version="1.0" encoding="utf-8"?>
            <MiniMapDatas>
              <MiniMapRegions alpha="60">
                <MiniMapRegion groupid="7" TL_3D="0,0" TL_2D="0,100" BR_3D="10,10" BR_2D="100,0" />
              </MiniMapRegions>
            </MiniMapDatas>
            """);

        LevelDataLoader loader = new();
        LevelData? level = loader.LoadLevel(levelDir);

        Assert.NotNull(level);
        Assert.NotNull(level!.ClientMiniMap);
        Assert.Equal(60, level.ClientMiniMap!.Alpha);
        Assert.Equal(levelName, level.ClientMiniMap.AssetName);
        Assert.Single(level.Entities);
        Assert.True(level.ClientMiniMap.TryProjectWorldPosition(level.Entities[0].Pos, out Vec2 mapPosition));
        Assert.InRange(mapPosition.X, 49.9f, 50.1f);
        Assert.InRange(mapPosition.Y, 39.9f, 40.1f);
    }

    public void Dispose()
    {
        if (Directory.Exists(_rootPath))
        {
            Directory.Delete(_rootPath, recursive: true);
        }
    }

    private void WriteMinimapFiles(string assetName, string xml)
    {
        string minimapDir = Path.Combine(_rootPath, "libs", "minimap");
        string artDir = Path.Combine(_rootPath, "libs", "ui", "flashassets", "images", "minimap");
        Directory.CreateDirectory(minimapDir);
        Directory.CreateDirectory(artDir);

        File.WriteAllText(Path.Combine(minimapDir, $"{assetName}.xml"), xml);
        File.WriteAllBytes(Path.Combine(artDir, $"{assetName}.swf"), [0x46, 0x57, 0x53]);
    }
}
