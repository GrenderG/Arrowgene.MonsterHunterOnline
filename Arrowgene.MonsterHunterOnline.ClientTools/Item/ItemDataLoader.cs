using System;
using System.Globalization;
using System.IO;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.Dat;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Item;

public sealed class ItemDataLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(ItemDataLoader));

    private static readonly string[] ItemFiles =
    [
        "itemdata_quest.dat",
        "itemdata_material.dat",
        "itemdata.dat",
        "itemdata_tool.dat",
        "itemdata_skillpearl.dat",
        "itemdata_legendpearl.dat",
        "itemdata_mart.dat",
        "itemdata_levelgroup.dat",
        "equipdata.dat",
        "equipdata2.dat",
        "equipdata_armor.dat",
        "equipdata_clothes.dat",
        "equipdata_jewelry.dat",
    ];

    public ItemDatabase Load(string clientFilesRoot)
    {
        string staticDir = Path.Combine(clientFilesRoot, "common", "staticdata");
        if (!Directory.Exists(staticDir))
        {
            Logger.Error($"Static data directory not found: {staticDir}");
            return new ItemDatabase();
        }

        ItemDatabase db = new();

        foreach (string fileName in ItemFiles)
        {
            string path = Path.Combine(staticDir, fileName);
            if (!File.Exists(path)) continue;
            LoadFile(db, path, Path.GetFileNameWithoutExtension(fileName));
        }

        Logger.Info($"Loaded {db.Items.Count} items");
        return db;
    }

    private void LoadFile(ItemDatabase db, string path, string sourceFile)
    {
        try
        {
            DatFile dat = new();
            dat.Open(path);

            foreach (TsvSheet sheet in dat.Sheets)
            {
                if (sheet.Table == null || sheet.TableHead == null) continue;

                // Find columns by header name
                int idCol = FindCol(sheet.TableHead, "总表ID", "ID");
                int nameCol = FindCol(sheet.TableHead, "物品名称", "Name");
                int descCol = FindCol(sheet.TableHead, "说明");
                int levelCol = FindCol(sheet.TableHead, "物品等级");
                int rarityCol = FindCol(sheet.TableHead, "稀有度");
                int iconCol = FindCol(sheet.TableHead, "图标ID");
                int keepCol = FindCol(sheet.TableHead, "离开副本是否保留");
                int rankCol = FindCol(sheet.TableHead, "RANK");
                int mainCatCol = FindCol(sheet.TableHead, "物品主类");
                int subCatCol = FindCol(sheet.TableHead, "物品中类");
                int detailCatCol = FindCol(sheet.TableHead, "物品子类");
                int bindCol = FindCol(sheet.TableHead, "绑定类型");
                int ownLimitCol = FindCol(sheet.TableHead, "拥有上限");
                int carryLimitCol = FindCol(sheet.TableHead, "携带上限");
                int stackLimitCol = FindCol(sheet.TableHead, "堆叠上限");
                int destroyCol = FindCol(sheet.TableHead, "可否摧毁");
                int buyCol = FindCol(sheet.TableHead, "购买价格");
                int sellCol = FindCol(sheet.TableHead, "出售价格");
                int mallCol = FindCol(sheet.TableHead, "是否商城道具");

                // Equipment-specific columns (present in equipdata files)
                int attackCol = FindCol(sheet.TableHead, "攻击力");
                int defenseCol = FindCol(sheet.TableHead, "防御力");
                int affinityCol = FindCol(sheet.TableHead, "会心等级");
                int slotsCol = FindCol(sheet.TableHead, "初始孔数");
                int maxSlotsCol = FindCol(sheet.TableHead, "最大孔数");
                int waterAtkCol = FindCol(sheet.TableHead, "水属性攻击力");
                int fireAtkCol = FindCol(sheet.TableHead, "火属性攻击力");
                int thunderAtkCol = FindCol(sheet.TableHead, "雷属性攻击力");
                int dragonAtkCol = FindCol(sheet.TableHead, "龙属性攻击力");
                int iceAtkCol = FindCol(sheet.TableHead, "冰属性攻击力");
                int waterResCol = FindCol(sheet.TableHead, "水抗性");
                int fireResCol = FindCol(sheet.TableHead, "火抗性");
                int thunderResCol = FindCol(sheet.TableHead, "雷抗性");
                int dragonResCol = FindCol(sheet.TableHead, "龙抗性");
                int iceResCol = FindCol(sheet.TableHead, "冰抗性");

                if (idCol < 0 || nameCol < 0) continue;

                foreach (string[] row in sheet.Table)
                {
                    int id = GetInt(row, idCol);
                    if (id == 0) continue;
                    string name = GetStr(row, nameCol);
                    if (string.IsNullOrEmpty(name)) continue;

                    db.Items.Add(new ItemDef
                    {
                        Id = id,
                        Name = name,
                        Description = StripHtml(GetStr(row, descCol)),
                        ItemLevel = GetInt(row, levelCol),
                        Rarity = GetInt(row, rarityCol),
                        Icon = GetStr(row, iconCol),
                        Rank = GetInt(row, rankCol),
                        MainCategory = GetInt(row, mainCatCol),
                        SubCategory = GetInt(row, subCatCol),
                        DetailCategory = GetInt(row, detailCatCol),
                        BindType = GetInt(row, bindCol),
                        OwnLimit = GetInt(row, ownLimitCol),
                        CarryLimit = GetInt(row, carryLimitCol),
                        StackLimit = GetInt(row, stackLimitCol),
                        BuyPrice = GetInt(row, buyCol),
                        SellPrice = GetInt(row, sellCol),
                        IsMallItem = GetInt(row, mallCol) != 0,
                        KeepOnLeaveLevel = GetInt(row, keepCol) != 0,
                        CanDestroy = GetInt(row, destroyCol) != 0,
                        Attack = GetInt(row, attackCol),
                        Defense = GetInt(row, defenseCol),
                        Affinity = GetInt(row, affinityCol),
                        Slots = GetInt(row, slotsCol),
                        MaxSlots = GetInt(row, maxSlotsCol),
                        WaterAttack = GetInt(row, waterAtkCol),
                        FireAttack = GetInt(row, fireAtkCol),
                        ThunderAttack = GetInt(row, thunderAtkCol),
                        DragonAttack = GetInt(row, dragonAtkCol),
                        IceAttack = GetInt(row, iceAtkCol),
                        WaterRes = GetInt(row, waterResCol),
                        FireRes = GetInt(row, fireResCol),
                        ThunderRes = GetInt(row, thunderResCol),
                        DragonRes = GetInt(row, dragonResCol),
                        IceRes = GetInt(row, iceResCol),
                        SourceFile = sourceFile,
                        SourceSheet = sheet.Name ?? "",
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load {path}: {ex.Message}");
        }
    }

    private static int FindCol(string[] header, params string[] names)
    {
        for (int i = 0; i < header.Length; i++)
        {
            string h = header[i].Trim();
            foreach (string n in names)
            {
                if (h == n) return i;
            }
        }
        return -1;
    }

    private static int GetInt(string[] row, int col)
    {
        if (col < 0 || col >= row.Length) return 0;
        string val = row[col].Trim();
        return int.TryParse(val, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v) ? v : 0;
    }

    private static string GetStr(string[] row, int col)
    {
        if (col < 0 || col >= row.Length) return string.Empty;
        return row[col].Trim();
    }

    private static string StripHtml(string html)
    {
        if (string.IsNullOrEmpty(html)) return html;
        string result = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
        result = result.Replace("&lt;", "<").Replace("&gt;", ">")
                       .Replace("&amp;", "&").Replace("&quot;", "\"")
                       .Replace("\"", "");
        return result.Trim();
    }
}
