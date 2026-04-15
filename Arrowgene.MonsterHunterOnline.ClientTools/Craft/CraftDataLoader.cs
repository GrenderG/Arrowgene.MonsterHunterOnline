using System;
using System.Globalization;
using System.IO;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.Dat;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Craft;

public sealed class CraftDataLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(CraftDataLoader));

    public CraftDatabase Load(string clientFilesRoot)
    {
        string staticDir = Path.Combine(clientFilesRoot, "common", "staticdata");
        if (!Directory.Exists(staticDir))
        {
            Logger.Error($"Static data directory not found: {staticDir}");
            return new CraftDatabase();
        }

        CraftDatabase db = new();
        LoadItemLookups(db, staticDir);
        LoadCraftData(db, Path.Combine(staticDir, "craftdata.dat"));

        Logger.Info($"Loaded {db.Recipes.Count} recipes, {db.ItemNames.Count} item names, {db.ItemIcons.Count} icons");
        return db;
    }

    private void LoadItemLookups(CraftDatabase db, string staticDir)
    {
        string[] itemFiles =
        [
            "itemdata_quest.dat", "itemdata_material.dat", "itemdata.dat",
            "itemdata_tool.dat", "itemdata_skillpearl.dat", "itemdata_legendpearl.dat",
            "itemdata_mart.dat", "itemdata_levelgroup.dat",
            "equipdata.dat", "equipdata2.dat", "equipdata_armor.dat",
            "equipdata_clothes.dat", "equipdata_jewelry.dat",
        ];

        foreach (string fileName in itemFiles)
        {
            string path = Path.Combine(staticDir, fileName);
            if (!File.Exists(path)) continue;
            try
            {
                DatFile dat = new();
                dat.Open(path);
                foreach (TsvSheet sheet in dat.Sheets)
                {
                    if (sheet.Table == null || sheet.TableHead == null) continue;
                    int idCol = FindCol(sheet.TableHead, "总表ID", "ID");
                    int nameCol = FindCol(sheet.TableHead, "物品名称", "Name");
                    int iconCol = FindCol(sheet.TableHead, "图标ID");
                    if (idCol < 0 || nameCol < 0) continue;

                    foreach (string[] row in sheet.Table)
                    {
                        int id = GetInt(row, idCol);
                        if (id == 0) continue;
                        string name = GetStr(row, nameCol);
                        if (!string.IsNullOrEmpty(name) && !db.ItemNames.ContainsKey(id))
                            db.ItemNames[id] = name;
                        if (iconCol >= 0)
                        {
                            string icon = GetStr(row, iconCol);
                            if (!string.IsNullOrEmpty(icon) && !db.ItemIcons.ContainsKey(id))
                                db.ItemIcons[id] = icon;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load {fileName} for lookups: {ex.Message}");
            }
        }
    }

    private void LoadCraftData(CraftDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            foreach (TsvSheet sheet in dat.Sheets)
            {
                if (sheet.Table == null || sheet.TableHead == null) continue;

                int idCol = FindCol(sheet.TableHead, "配方ID");
                int nameCol = FindCol(sheet.TableHead, "配方名称");
                int typeCol = FindCol(sheet.TableHead, "配方类型");
                int equipCol = FindCol(sheet.TableHead, "装备类型");
                int kindCol = FindCol(sheet.TableHead, "打造种类");
                int levelCol = FindCol(sheet.TableHead, "使用等级");
                int starCol = FindCol(sheet.TableHead, "使用星级");
                int rangeCol = FindCol(sheet.TableHead, "远近类型");
                int goldCol = FindCol(sheet.TableHead, "消耗金钱");

                // Output columns (up to 2 outputs + byproduct)
                int out1IdCol = FindCol(sheet.TableHead, "生成物品1ID");
                int out1CntCol = FindCol(sheet.TableHead, "生成数量1");
                int out1RateCol = FindCol(sheet.TableHead, "生成率1");
                int out2IdCol = FindCol(sheet.TableHead, "生成物品2ID");
                int out2CntCol = FindCol(sheet.TableHead, "生成数量2");
                int out2RateCol = FindCol(sheet.TableHead, "生成率2");
                int byIdCol = FindCol(sheet.TableHead, "副产物ID");
                int byCntCol = FindCol(sheet.TableHead, "副产数量");
                int byRateCol = FindCol(sheet.TableHead, "副产生成率");

                // Material columns (up to 10)
                int[] matIdCols = new int[10];
                int[] matCntCols = new int[10];
                for (int i = 0; i < 10; i++)
                {
                    matIdCols[i] = FindCol(sheet.TableHead, $"原料{i + 1}ID");
                    matCntCols[i] = FindCol(sheet.TableHead, $"数量{i + 1}");
                }

                if (idCol < 0) continue;

                foreach (string[] row in sheet.Table)
                {
                    int id = GetInt(row, idCol);
                    if (id == 0) continue;

                    CraftRecipe recipe = new()
                    {
                        Id = id,
                        Name = GetStr(row, nameCol),
                        RecipeType = GetInt(row, typeCol),
                        EquipType = GetInt(row, equipCol),
                        CraftKind = GetInt(row, kindCol),
                        RequiredLevel = GetInt(row, levelCol),
                        RequiredStar = GetInt(row, starCol),
                        MeleeRanged = GetInt(row, rangeCol),
                        GoldCost = GetInt(row, goldCol),
                        SourceSheet = sheet.Name ?? "",
                    };

                    // Outputs
                    int o1 = GetInt(row, out1IdCol);
                    if (o1 > 0) recipe.Outputs.Add(new CraftOutput { ItemId = o1, Count = GetInt(row, out1CntCol), Rate = GetInt(row, out1RateCol) });
                    int o2 = GetInt(row, out2IdCol);
                    if (o2 > 0) recipe.Outputs.Add(new CraftOutput { ItemId = o2, Count = GetInt(row, out2CntCol), Rate = GetInt(row, out2RateCol) });
                    int by = GetInt(row, byIdCol);
                    if (by > 0) recipe.Byproduct = new CraftOutput { ItemId = by, Count = GetInt(row, byCntCol), Rate = GetInt(row, byRateCol) };

                    // Materials
                    for (int i = 0; i < 10; i++)
                    {
                        int matId = GetInt(row, matIdCols[i]);
                        if (matId > 0)
                            recipe.Materials.Add(new CraftMaterial { ItemId = matId, Count = GetInt(row, matCntCols[i]) });
                    }

                    db.Recipes.Add(recipe);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load craftdata: {ex.Message}");
        }
    }

    private static int FindCol(string[] header, params string[] names)
    {
        for (int i = 0; i < header.Length; i++)
        {
            string h = header[i].Trim();
            foreach (string n in names)
                if (h == n) return i;
        }
        return -1;
    }

    private static int GetInt(string[] row, int col)
    {
        if (col < 0 || col >= row.Length) return 0;
        return int.TryParse(row[col].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int v) ? v : 0;
    }

    private static string GetStr(string[] row, int col)
    {
        if (col < 0 || col >= row.Length) return string.Empty;
        return row[col].Trim();
    }
}
