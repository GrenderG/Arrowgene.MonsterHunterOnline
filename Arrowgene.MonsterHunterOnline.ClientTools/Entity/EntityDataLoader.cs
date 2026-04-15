using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.Dat;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Entity;

public sealed class EntityDataLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(EntityDataLoader));

    // NPC service column names mapped to display labels
    private static readonly (string Column, string Label)[] NpcServiceColumns =
    [
        ("ForgeEnabled", "Forge"),
        ("SupplyBoxEnabled", "Supply Box"),
        ("TurnInBoxEnabled", "Turn-in Box"),
        ("Mail", "Mail"),
        ("Auction", "Auction"),
        ("Storage", "Storage"),
        ("Daily", "Daily"),
        ("Enhance", "Enhance"),
        ("RankUp", "Rank Up"),
        ("ManufactureLearn", "Manufacture"),
        ("MaterialDecompose", "Decompose"),
        ("PetForge", "Pet Forge"),
        ("PetShop", "Pet Shop"),
        ("LevelEntrustList", "Entrust List"),
        ("LevelEntrustGo", "Entrust Go"),
        ("EquipEmbed", "Equip Embed"),
        ("EquipUpgrade", "Equip Upgrade"),
        ("EquipQuench", "Equip Quench"),
        ("EquipReborn", "Equip Reborn"),
        ("Exchange", "Exchange"),
        ("WeaponWake", "Weapon Wake"),
        ("WeaponTrial", "Weapon Trial"),
        ("ItemExchange", "Item Exchange"),
        ("Transfer", "Transfer"),
        ("WorldMap", "World Map"),
    ];

    public EntityDatabase Load(string clientFilesRoot)
    {
        string staticDir = Path.Combine(clientFilesRoot, "common", "staticdata");
        if (!Directory.Exists(staticDir))
        {
            Logger.Error($"Static data directory not found: {staticDir}");
            return new EntityDatabase();
        }

        EntityDatabase db = new();

        LoadMonsters(db, Path.Combine(staticDir, "monsterdata.dat"));
        LoadMonsterDifficulty(db, Path.Combine(staticDir, "monsterdifficulty.dat"));
        LoadNpcs(db, Path.Combine(staticDir, "npcdatanew.dat"));
        LoadPets(db, Path.Combine(staticDir, "pet.dat"));

        Logger.Info($"Loaded {db.Monsters.Count} monsters, {db.Npcs.Count} NPCs, {db.Pets.Count} pets");
        return db;
    }

    private void LoadMonsters(EntityDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            // Load Monsters sheet
            TsvSheet? monstersSheet = FindSheet(dat, "Monsters");
            if (monstersSheet?.Table != null && monstersSheet.TableHead != null)
            {
                int idCol = FindCol(monstersSheet.TableHead, "MonsterID");
                int nameCol = FindCol(monstersSheet.TableHead, "Name");
                int typeCol = FindCol(monstersSheet.TableHead, "MonsterType");
                int groupCol = FindCol(monstersSheet.TableHead, "MonsterGroup");
                int entityCol = FindCol(monstersSheet.TableHead, "EntityName");
                int classCol = FindCol(monstersSheet.TableHead, "EntityClass");
                int raceCol = FindCol(monstersSheet.TableHead, "Race");
                int diffCol = FindCol(monstersSheet.TableHead, "Difficulty");
                int sizeCol = FindCol(monstersSheet.TableHead, "Size");
                int lootCol = FindCol(monstersSheet.TableHead, "LootSize");
                int captureCol = FindCol(monstersSheet.TableHead, "CaptureHPPercent");
                int existCol = FindCol(monstersSheet.TableHead, "ExistTime");

                foreach (string[] row in monstersSheet.Table)
                {
                    int id = GetInt(row, idCol);
                    if (id == 0) continue;
                    string name = GetStr(row, nameCol);
                    if (string.IsNullOrEmpty(name)) continue;

                    db.Monsters.Add(new MonsterDef
                    {
                        Id = id,
                        Name = name,
                        MonsterType = GetInt(row, typeCol),
                        MonsterGroup = GetInt(row, groupCol),
                        EntityName = GetStr(row, entityCol),
                        EntityClass = GetStr(row, classCol),
                        Race = GetInt(row, raceCol),
                        Difficulty = GetInt(row, diffCol),
                        Size = GetInt(row, sizeCol),
                        LootSize = GetInt(row, lootCol),
                        CaptureHpPercent = GetInt(row, captureCol),
                        ExistTime = GetInt(row, existCol),
                    });
                }
            }

            // Load Parts sheet
            TsvSheet? partsSheet = FindSheet(dat, "Parts");
            if (partsSheet?.Table != null && partsSheet.TableHead != null)
            {
                int pidCol = FindCol(partsSheet.TableHead, "MonsterID");
                int partIdCol = FindCol(partsSheet.TableHead, "PartID");
                int partNameCol = FindCol(partsSheet.TableHead, "PartName");
                int stateCol = FindCol(partsSheet.TableHead, "StateID");

                Dictionary<int, MonsterDef> lookup = [];
                foreach (var m in db.Monsters) lookup[m.Id] = m;

                foreach (string[] row in partsSheet.Table)
                {
                    int mId = GetInt(row, pidCol);
                    if (mId == 0 || !lookup.TryGetValue(mId, out var monster)) continue;

                    string partId = GetStr(row, partIdCol);
                    string partName = GetStr(row, partNameCol);
                    if (string.IsNullOrEmpty(partId) && string.IsNullOrEmpty(partName)) continue;

                    monster.Parts.Add(new MonsterPartDef
                    {
                        PartId = partId,
                        PartName = partName,
                        StateId = GetStr(row, stateCol),
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load monsterdata: {ex.Message}");
        }
    }

    private void LoadMonsterDifficulty(EntityDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            TsvSheet? sheet = FindSheet(dat, "Monsters");
            if (sheet?.Table == null || sheet.TableHead == null) return;

            int idCol = FindCol(sheet.TableHead, "MonsterID");
            int diffCol = FindCol(sheet.TableHead, "Difficulty");
            int hpCol = FindCol(sheet.TableHead, "MaxHealth");
            int atkCol = FindCol(sheet.TableHead, "PhyAtk");
            int defCol = FindCol(sheet.TableHead, "Defence");
            int waterCol = FindCol(sheet.TableHead, "WaterAtk");
            int fireCol = FindCol(sheet.TableHead, "FireAtk");
            int lightCol = FindCol(sheet.TableHead, "LightningAtk");
            int dragonCol = FindCol(sheet.TableHead, "DragonAtk");
            int iceCol = FindCol(sheet.TableHead, "IceAtk");

            Dictionary<int, MonsterDef> lookup = [];
            foreach (var m in db.Monsters) lookup[m.Id] = m;

            foreach (string[] row in sheet.Table)
            {
                int mId = GetInt(row, idCol);
                if (mId == 0 || !lookup.TryGetValue(mId, out var monster)) continue;

                monster.DifficultyEntries.Add(new MonsterDifficultyEntry
                {
                    Difficulty = GetInt(row, diffCol),
                    MaxHealth = GetInt(row, hpCol),
                    PhyAtk = GetInt(row, atkCol),
                    Defence = GetInt(row, defCol),
                    WaterAtk = GetInt(row, waterCol),
                    FireAtk = GetInt(row, fireCol),
                    LightningAtk = GetInt(row, lightCol),
                    DragonAtk = GetInt(row, dragonCol),
                    IceAtk = GetInt(row, iceCol),
                });
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load monsterdifficulty: {ex.Message}");
        }
    }

    private void LoadNpcs(EntityDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            TsvSheet? sheet = FindSheet(dat, "NPCData");
            if (sheet?.Table == null || sheet.TableHead == null) return;

            int idCol = FindCol(sheet.TableHead, "ID");
            int nameCol = FindCol(sheet.TableHead, "Name");
            int titleCol = FindCol(sheet.TableHead, "Title");
            int entityCol = FindCol(sheet.TableHead, "EntityName");
            int classCol = FindCol(sheet.TableHead, "EntityClass");
            int customCol = FindCol(sheet.TableHead, "EntityCustomType");
            int appearCol = FindCol(sheet.TableHead, "AppearTask");
            int disappearCol = FindCol(sheet.TableHead, "DisappearTask");

            // Build service column indices
            List<(int Col, string Label)> serviceCols = [];
            foreach (var (column, label) in NpcServiceColumns)
            {
                int col = FindCol(sheet.TableHead, column);
                if (col >= 0) serviceCols.Add((col, label));
            }

            foreach (string[] row in sheet.Table)
            {
                int id = GetInt(row, idCol);
                if (id == 0) continue;
                string name = GetStr(row, nameCol);

                NpcDef npc = new()
                {
                    Id = id,
                    Name = name,
                    Title = GetStr(row, titleCol),
                    EntityName = GetStr(row, entityCol),
                    EntityClass = GetStr(row, classCol),
                    EntityCustomType = GetStr(row, customCol),
                    AppearTask = GetInt(row, appearCol),
                    DisappearTask = GetInt(row, disappearCol),
                };

                foreach (var (col, label) in serviceCols)
                {
                    if (GetInt(row, col) != 0)
                        npc.Services.Add(label);
                }

                db.Npcs.Add(npc);
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load npcdatanew: {ex.Message}");
        }
    }

    private void LoadPets(EntityDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            TsvSheet? sheet = FindSheet(dat, "宠物信息");
            if (sheet?.Table == null || sheet.TableHead == null) return;

            int idCol = FindCol(sheet.TableHead, "ID");
            int nameCol = FindCol(sheet.TableHead, "默认名字");
            int typeCol = FindCol(sheet.TableHead, "种类");
            int qualCol = FindCol(sheet.TableHead, "品质");
            int persCol = FindCol(sheet.TableHead, "性格");
            int prefCol = FindCol(sheet.TableHead, "攻击倾向");
            int styleCol = FindCol(sheet.TableHead, "攻击方式");
            int lvlCol = FindCol(sheet.TableHead, "等级");
            int burrowCol = FindCol(sheet.TableHead, "钻地血量百分比");
            int healCol = FindCol(sheet.TableHead, "回血速度");
            int entityCol = FindCol(sheet.TableHead, "EntityName");
            int iconCol = FindCol(sheet.TableHead, "Icon");
            int aptCol = FindCol(sheet.TableHead, "资质");
            int suppCol = FindCol(sheet.TableHead, "支援技能");

            foreach (string[] row in sheet.Table)
            {
                int id = GetInt(row, idCol);
                if (id == 0) continue;

                db.Pets.Add(new PetDef
                {
                    Id = id,
                    Name = GetStr(row, nameCol),
                    Type = GetStr(row, typeCol),
                    Quality = GetStr(row, qualCol),
                    Personality = GetStr(row, persCol),
                    AttackPreference = GetStr(row, prefCol),
                    AttackStyle = GetStr(row, styleCol),
                    Level = GetInt(row, lvlCol),
                    BurrowHpPercent = GetInt(row, burrowCol),
                    HealSpeed = GetInt(row, healCol),
                    EntityName = GetStr(row, entityCol),
                    Icon = GetStr(row, iconCol),
                    Aptitude = GetInt(row, aptCol),
                    SupportSkill = GetInt(row, suppCol),
                });
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load pet: {ex.Message}");
        }
    }

    private static TsvSheet? FindSheet(DatFile dat, string name)
    {
        foreach (TsvSheet sheet in dat.Sheets)
        {
            if (sheet.Name == name) return sheet;
        }
        return null;
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
}
