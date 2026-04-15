using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.ClientTools.Dat;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Quest;

public sealed class QuestDataLoader
{
    private static readonly ILogger Logger = LogProvider.Logger(typeof(QuestDataLoader));

    public QuestDatabase Load(string clientFilesRoot)
    {
        string staticDir = Path.Combine(clientFilesRoot, "common", "staticdata");
        if (!Directory.Exists(staticDir))
        {
            Logger.Error($"Static data directory not found: {staticDir}");
            return new QuestDatabase();
        }

        QuestDatabase db = new();

        LoadQuestFiles(db, staticDir);
        LoadLibrary(db, Path.Combine(staticDir, "questlib.dat"));
        LoadGroups(db, Path.Combine(staticDir, "questgroup.dat"));
        LoadChapters(db, Path.Combine(staticDir, "questchapter.dat"));
        LoadSeries(db, Path.Combine(staticDir, "questseries.dat"));
        LoadLoot(db, Path.Combine(staticDir, "questloot.dat"));
        LoadNpcs(db, Path.Combine(staticDir, "npcdatanew.dat"));
        LoadMonsterNames(db, Path.Combine(staticDir, "monsterdata.dat"));
        LoadItemNames(db, staticDir);
        LoadLevelNames(db, Path.Combine(staticDir, "lin_entrust.dat"));

        Logger.Info($"Loaded {db.Quests.Count} quests, {db.Libraries.Count} libraries, " +
                    $"{db.Groups.Count} groups, {db.Chapters.Count} chapters, {db.Series.Count} series, " +
                    $"{db.Npcs.Count} NPCs, {db.MonsterNames.Count} monsters, " +
                    $"{db.ItemNames.Count} items, {db.LevelNames.Count} levels, " +
                    $"{db.LootEntries.Count} loot entries");
        return db;
    }

    private void LoadQuestFiles(QuestDatabase db, string staticDir)
    {
        string[] questFiles = Directory.GetFiles(staticDir, "quest_*.dat");
        Array.Sort(questFiles, StringComparer.OrdinalIgnoreCase);

        foreach (string file in questFiles)
        {
            try
            {
                string xml = DecryptToXml(file);
                if (xml == null) continue;

                string fileName = Path.GetFileNameWithoutExtension(file);
                XDocument doc = XDocument.Parse(xml);
                if (doc.Root == null) continue;

                foreach (XElement obj in doc.Root.Elements("Obj"))
                {
                    if (obj.Attribute("Id")?.Value != "TaskDef") continue;
                    QuestDef quest = ParseQuestDef(obj);
                    quest.SourceFile = fileName;
                    db.Quests.Add(quest);
                }

                Logger.Info($"Loaded {fileName}: found quests up to {db.Quests.Count} total");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load {file}: {ex.Message}");
            }
        }
    }

    private void LoadLibrary(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            string xml = DecryptToXml(path);
            if (xml == null) return;
            XDocument doc = XDocument.Parse(xml);
            if (doc.Root == null) return;

            foreach (XElement obj in doc.Root.Elements("Obj"))
            {
                if (obj.Attribute("Id")?.Value != "TaskLibDef") continue;
                QuestLibDef lib = new()
                {
                    Id = GetInt(obj, "Id"),
                    Name = GetStr(obj, "Name"),
                    Note = GetStr(obj, "Note"),
                    Type = GetInt(obj, "Type"),
                    RefreshPeriod = GetInt(obj, "RefreshPeriod"),
                    RefreshTime = GetStr(obj, "RefreshTime"),
                    CanSelectCount = GetInt(obj, "CanSelectCount"),
                    CanAcceptCount = GetInt(obj, "CanAcceptCount"),
                    CanCompleteCount = GetInt(obj, "CanCompleteCount"),
                };

                XElement? groups = obj.Element("Groups");
                if (groups != null)
                {
                    foreach (XElement val in groups.Elements("Val"))
                    {
                        if (int.TryParse(val.Value, out int gid))
                            lib.Groups.Add(gid);
                    }
                }

                db.Libraries.Add(lib);
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load questlib: {ex.Message}");
        }
    }

    private void LoadGroups(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            string xml = DecryptToXml(path);
            if (xml == null) return;
            XDocument doc = XDocument.Parse(xml);
            if (doc.Root == null) return;

            foreach (XElement obj in doc.Root.Elements("Obj"))
            {
                if (obj.Attribute("Id")?.Value != "TaskGroupDef") continue;

                int levelMin = 0, levelMax = 0;
                XElement? levelEl = obj.Element("Level")?.Element("Obj");
                if (levelEl != null)
                {
                    levelMin = GetInt(levelEl, "Min");
                    levelMax = GetInt(levelEl, "Max");
                }

                db.Groups.Add(new QuestGroupDef
                {
                    Id = GetInt(obj, "Id"),
                    Name = GetStr(obj, "Name"),
                    Note = GetStr(obj, "Note"),
                    LevelMin = levelMin,
                    LevelMax = levelMax,
                    Star = GetInt(obj, "Star"),
                    Group = GetInt(obj, "Group"),
                    FreeHuntStage = GetInt(obj, "FreeHuntStage"),
                    Stage = GetInt(obj, "Stage"),
                });
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load questgroup: {ex.Message}");
        }
    }

    private void LoadChapters(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            string xml = DecryptToXml(path);
            if (xml == null) return;
            XDocument doc = XDocument.Parse(xml);
            if (doc.Root == null) return;

            foreach (XElement obj in doc.Root.Elements("Obj"))
            {
                if (obj.Attribute("Id")?.Value != "TaskChapterDef") continue;

                int levelMin = 0, levelMax = 0;
                XElement? levelEl = obj.Element("Level")?.Element("Obj");
                if (levelEl != null)
                {
                    levelMin = GetInt(levelEl, "Min");
                    levelMax = GetInt(levelEl, "Max");
                }

                QuestChapterDef chapter = new()
                {
                    Id = GetInt(obj, "Id"),
                    Name = GetStr(obj, "Name"),
                    Note = GetStr(obj, "Note"),
                    LevelMin = levelMin,
                    LevelMax = levelMax,
                    Image = GetStr(obj, "Image"),
                };

                XElement? series = obj.Element("Series");
                if (series != null)
                {
                    foreach (XElement val in series.Elements("Val"))
                    {
                        if (int.TryParse(val.Value, out int sid))
                            chapter.Series.Add(sid);
                    }
                }

                db.Chapters.Add(chapter);
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load questchapter: {ex.Message}");
        }
    }

    private void LoadSeries(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            string xml = DecryptToXml(path);
            if (xml == null) return;
            XDocument doc = XDocument.Parse(xml);
            if (doc.Root == null) return;

            foreach (XElement obj in doc.Root.Elements("Obj"))
            {
                if (obj.Attribute("Id")?.Value != "TaskSeriesDef") continue;
                db.Series.Add(new QuestSeriesDef
                {
                    Id = GetInt(obj, "Id"),
                    Name = GetStr(obj, "Name"),
                    Note = GetStr(obj, "Note"),
                });
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load questseries: {ex.Message}");
        }
    }

    private void LoadLoot(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            foreach (TsvSheet sheet in dat.Sheets)
            {
                if (sheet.Table == null) continue;

                foreach (string[] row in sheet.Table)
                {
                    if (row.Length < 10) continue;

                    int monsterId = ParseTsvInt(row[0]);
                    int levelMode = ParseTsvInt(row[1]);
                    int difficulty = ParseTsvInt(row[2]);
                    int questId = ParseTsvInt(row[5]);
                    int itemId = ParseTsvInt(row[7]);
                    int amountMin = ParseTsvInt(row[8]);
                    int amountMax = ParseTsvInt(row[9]);

                    if (monsterId == 0 && questId == 0) continue;

                    db.LootEntries.Add(new QuestLootEntry
                    {
                        MonsterId = monsterId,
                        LevelMode = levelMode,
                        Difficulty = difficulty,
                        QuestId = questId,
                        ItemId = itemId,
                        ItemAmountMin = amountMin,
                        ItemAmountMax = amountMax,
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load questloot: {ex.Message}");
        }
    }

    private void LoadNpcs(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            foreach (TsvSheet sheet in dat.Sheets)
            {
                if (sheet.Name != "NPCData") continue;
                if (sheet.Table == null) continue;

                foreach (string[] row in sheet.Table)
                {
                    if (row.Length < 3) continue;

                    int id = ParseTsvInt(row[0]);
                    if (id == 0) continue;

                    string name = row[1].Trim();
                    string title = row.Length > 2 ? row[2].Trim() : string.Empty;

                    db.Npcs[id] = new NpcInfo
                    {
                        Id = id,
                        Name = name,
                        Title = title,
                    };
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load npcdatanew: {ex.Message}");
        }
    }

    private void LoadMonsterNames(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            foreach (TsvSheet sheet in dat.Sheets)
            {
                if (sheet.Name != "Monsters") continue;
                if (sheet.Table == null) continue;

                foreach (string[] row in sheet.Table)
                {
                    if (row.Length < 3) continue;
                    int id = ParseTsvInt(row[0]);
                    if (id == 0) continue;
                    string name = row[2].Trim();
                    if (!string.IsNullOrEmpty(name))
                        db.MonsterNames[id] = name;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load monsterdata: {ex.Message}");
        }
    }

    private void LoadItemNames(QuestDatabase db, string staticDir)
    {
        string[] itemFiles =
        [
            "itemdata_quest.dat",
            "itemdata_material.dat",
            "itemdata.dat",
            "itemdata_tool.dat",
            "itemdata_skillpearl.dat",
            "itemdata_legendpearl.dat",
            "itemdata_mart.dat",
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

                    // Find the ID and Name columns by header
                    int idCol = -1, nameCol = -1;
                    for (int i = 0; i < sheet.TableHead.Length; i++)
                    {
                        string h = sheet.TableHead[i].Trim();
                        if (h == "总表ID" || h == "ID") idCol = i;
                        else if (h == "物品名称" || h == "Name") nameCol = i;
                    }

                    // Fallback: first two columns
                    if (idCol < 0) idCol = 0;
                    if (nameCol < 0) nameCol = 1;

                    foreach (string[] row in sheet.Table)
                    {
                        if (row.Length <= nameCol) continue;
                        int id = ParseTsvInt(row[idCol]);
                        if (id == 0) continue;
                        string name = row[nameCol].Trim();
                        if (!string.IsNullOrEmpty(name) && !db.ItemNames.ContainsKey(id))
                            db.ItemNames[id] = name;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load {fileName}: {ex.Message}");
            }
        }
    }

    private void LoadLevelNames(QuestDatabase db, string path)
    {
        if (!File.Exists(path)) return;
        try
        {
            DatFile dat = new();
            dat.Open(path);

            foreach (TsvSheet sheet in dat.Sheets)
            {
                if (sheet.Name != "LevelInfo") continue;
                if (sheet.Table == null || sheet.TableHead == null) continue;

                // Find LevelID and LevelName columns
                int idCol = 0, nameCol = 4;
                for (int i = 0; i < sheet.TableHead.Length; i++)
                {
                    string h = sheet.TableHead[i].Trim();
                    if (h == "LevelID") idCol = i;
                    else if (h == "LevelName") nameCol = i;
                }

                foreach (string[] row in sheet.Table)
                {
                    if (row.Length <= nameCol) continue;
                    int id = ParseTsvInt(row[idCol]);
                    if (id == 0) continue;
                    string name = row[nameCol].Trim();
                    if (!string.IsNullOrEmpty(name))
                        db.LevelNames[id] = name;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load lin_entrust: {ex.Message}");
        }
    }

    private QuestDef ParseQuestDef(XElement obj)
    {
        QuestDef quest = new()
        {
            Id = GetInt(obj, "Id"),
            Name = GetStr(obj, "Name"),
            Note = GetStr(obj, "Note"),
            Type = GetInt(obj, "Type"),
            Description = GetStr(obj, "Description"),
            Series = GetInt(obj, "Series"),
            CantGiveup = GetBool(obj, "CantGiveup"),
            AutoAccept = GetBool(obj, "AutoAccept"),
            AcceptNpc = GetInt(obj, "AcceptNpc"),
            SubmitNpc = GetInt(obj, "SubmitNpc"),
            RelatedNpc = GetInt(obj, "RelatedNpc"),
            ContentsType = GetInt(obj, "ContentsType"),
            AutoComplete = GetBool(obj, "AutoComplete"),
            Timeout = GetInt(obj, "Timeout"),
            ResetPeriod = GetInt(obj, "ResetPeriod"),
            ResetTime = GetStr(obj, "ResetTime"),
            CountDownType = GetInt(obj, "CountDownType"),
            CanRepeat = GetBool(obj, "CanRepeat"),
            CanShare = GetBool(obj, "CanShare"),
            IsAlone = GetBool(obj, "IsAlone"),
            RepeatCount = GetInt(obj, "RepeatCount"),
            AcceptMutexGroup = GetInt(obj, "AcceptMutexGroup"),
            CompleteMutexGroup = GetInt(obj, "CompleteMutexGroup"),
            CompleteNote = GetStr(obj, "CompleteNote"),
            Image = GetStr(obj, "Image"),
            Star = GetInt(obj, "Star"),
            Group = GetInt(obj, "Group"),
            Weight = GetInt(obj, "Weight"),
            Rate = GetInt(obj, "Rate"),
            Stage = GetInt(obj, "Stage"),
            Invalid = GetBool(obj, "Invalid"),
            AcceptNpcLocation = GetInt(obj, "AcceptNpcLocation"),
            PreTask = GetInt(obj, "PreTask"),
        };

        // Parse AcceptChecks
        XElement? checks = obj.Element("AcceptChecks");
        if (checks != null)
        {
            foreach (XElement checkObj in checks.Elements("Obj"))
            {
                quest.AcceptChecks.Add(ParseCheck(checkObj));
            }
        }

        // Parse Contents (objectives)
        XElement? contents = obj.Element("Contents");
        if (contents != null)
        {
            foreach (XElement contentObj in contents.Elements("Obj"))
            {
                quest.Contents.Add(ParseContent(contentObj));
            }
        }

        // Parse PostProcs (rewards)
        XElement? postProcs = obj.Element("PostProcs");
        if (postProcs != null)
        {
            foreach (XElement rewardObj in postProcs.Elements("Obj"))
            {
                quest.Rewards.Add(ParseReward(rewardObj));
            }
        }

        // Parse PreProcs (pre-rewards given on accept)
        XElement? preProcs = obj.Element("PreProcs");
        if (preProcs != null)
        {
            foreach (XElement rewardObj in preProcs.Elements("Obj"))
            {
                quest.PreRewards.Add(ParseReward(rewardObj));
            }
        }

        return quest;
    }

    private static QuestContent ParseContent(XElement obj)
    {
        string type = obj.Attribute("Id")?.Value ?? string.Empty;
        QuestContent content = new()
        {
            ContentType = type,
            Id = GetInt(obj, "Id"),
            Order = GetInt(obj, "Order"),
        };

        switch (type)
        {
            case "TaskLevelFinishContentDef":
                content.Level = GetInt(obj, "Level");
                content.Appraisal = GetInt(obj, "Appraisal");
                content.Count = GetInt(obj, "Count");
                content.Time = GetInt(obj, "Time");
                break;
            case "TaskTalkContentDef":
                content.Npc = GetInt(obj, "Npc");
                break;
            case "TaskUseItemContentDef":
                content.Item = GetInt(obj, "Item");
                content.ItemCount = GetInt(obj, "Count");
                break;
            case "TaskSubmitContentDef":
                content.Item = GetInt(obj, "Item");
                content.ItemCount = GetInt(obj, "Count");
                break;
            case "TaskKillContentDef":
                content.Count = GetInt(obj, "Count");
                break;
            case "TaskCollectContentDef":
                content.Item = GetInt(obj, "Item");
                content.ItemCount = GetInt(obj, "Count");
                break;
            case "TaskMonsterPartBrokenContentDef":
                content.Monster = GetInt(obj, "Monster");
                content.Part = GetInt(obj, "Part");
                ParseEventFilters(obj, content);
                break;
            case "TaskMonsterAbnormalContentDef":
                content.Monster = GetInt(obj, "Monster");
                content.Abnormal = GetInt(obj, "Abnormal");
                ParseEventFilters(obj, content);
                break;
            case "TaskCaptureContentDef":
                content.Monster = GetInt(obj, "Monster");
                content.Count = GetInt(obj, "Count");
                ParseEventFilters(obj, content);
                break;
            case "TaskHunterContentDef":
                content.Monster = GetInt(obj, "Monster");
                content.Count = GetInt(obj, "Count");
                break;
            case "TaskCommonContentDef":
                content.Content = GetInt(obj, "Content");
                content.Count = GetInt(obj, "Count");
                content.Arg1 = GetInt(obj, "Arg1");
                content.Arg2 = GetInt(obj, "Arg2");
                content.Arg3 = GetInt(obj, "Arg3");
                break;
            case "TaskGatherContentDef":
                content.Source = GetInt(obj, "Source");
                content.Item = GetInt(obj, "Item");
                content.Count = GetInt(obj, "Count");
                break;
            case "TaskFarmGatherContentDef":
                content.ColletionPoint = GetInt(obj, "ColletionPoint");
                content.Count = GetInt(obj, "Count");
                content.IsFriend = GetBool(obj, "IsFriend");
                break;
            case "TaskFarmGatherAnyContentDef":
                content.Count = GetInt(obj, "Count");
                content.IsFriend = GetBool(obj, "IsFriend");
                break;
            case "TaskLevelUpContentDef":
                content.Level = GetInt(obj, "Level");
                break;
            case "TaskFarmLevelUpContentDef":
                content.ColletionPoint = GetInt(obj, "ColletionPoint");
                content.Level = GetInt(obj, "Level");
                break;
            case "TaskFarmLevelUpAnyContentDef":
                content.Level = GetInt(obj, "Level");
                break;
            case "TaskPetTrainingContentDef":
                content.Facility = GetInt(obj, "Facility");
                content.Count = GetInt(obj, "Count");
                break;
            case "TaskPetEmploymentContentDef":
                break;
            case "TaskFarmPetLevelUpContentDef":
                content.Facility = GetInt(obj, "Facility");
                content.Level = GetInt(obj, "Level");
                break;
            case "TaskFarmPetLevelUpAnyContentDef":
                content.Level = GetInt(obj, "Level");
                break;
            case "TaskHunterStarFinishCardContentDef":
                content.Card = GetInt(obj, "Card");
                break;
            case "TaskStatisticsContentDef":
                content.StatId = GetInt(obj, "StatId");
                content.Count = GetInt(obj, "Count");
                break;
            case "TaskAccWeaponTrialContentDef":
                content.Count = GetInt(obj, "Count");
                break;
            case "TaskSoulStoneLevelContentDef":
                content.Level = GetInt(obj, "Level");
                break;
            case "TaskAccExpressionLearnContentDef":
                content.Count = GetInt(obj, "Count");
                break;
        }

        return content;
    }

    private static void ParseEventFilters(XElement obj, QuestContent content)
    {
        XElement? filters = obj.Element("EventFilters");
        if (filters == null) return;

        foreach (XElement filterObj in filters.Elements("Obj"))
        {
            if (filterObj.Attribute("Id")?.Value != "TaskLocationFilterDef") continue;
            content.EventFilters.Add(new QuestLocationFilter
            {
                Map = GetInt(filterObj, "Map"),
                Group = GetInt(filterObj, "Group"),
                Scene = GetInt(filterObj, "Scene"),
                Region = GetInt(filterObj, "Region"),
            });
        }
    }

    private static QuestReward ParseReward(XElement obj)
    {
        string type = obj.Attribute("Id")?.Value ?? string.Empty;
        QuestReward reward = new()
        {
            RewardType = type,
            Ratio = GetInt(obj, "Ratio"),
        };

        switch (type)
        {
            case "TaskGoldPrizeDef":
                reward.Money = GetInt(obj, "Money");
                reward.BoundMoney = GetInt(obj, "BoundMoney");
                break;
            case "TaskExpPrizeDef":
                reward.Exp = GetInt(obj, "Exp");
                break;
            case "TaskFarmExpPrizeDef":
                reward.FarmExp = GetInt(obj, "FarmExp");
                break;
            case "TaskItemsPrizeDef":
                XElement? items = obj.Element("Items");
                if (items != null)
                {
                    foreach (XElement itemObj in items.Elements("Obj"))
                    {
                        reward.Items.Add(new QuestItemReward
                        {
                            Item = GetInt(itemObj, "Item"),
                            Count = GetInt(itemObj, "Count"),
                            BindType = GetInt(itemObj, "BindType"),
                        });
                    }
                }
                break;
            case "TaskItemPrizeDef":
                XElement? singleItem = obj.Element("Item")?.Element("Obj");
                if (singleItem != null)
                {
                    reward.SingleItem = new QuestItemReward
                    {
                        Item = GetInt(singleItem, "Item"),
                        Count = GetInt(singleItem, "Count"),
                        BindType = GetInt(singleItem, "BindType"),
                    };
                }
                break;
            case "TaskHuntSoulPrizeDef":
                reward.HuntSoul = GetInt(obj, "HuntSoul");
                break;
            case "TaskSpoorPrizeDef":
                reward.SpoorPoint = GetInt(obj, "SpoorPoint");
                break;
            case "TaskContributionPrizeDef":
                reward.Contribution = GetInt(obj, "Contribution");
                break;
            case "TaskGuildPrizeDef":
                reward.GuildContribution = GetInt(obj, "Contribution");
                reward.GuildExp = GetInt(obj, "Exp");
                reward.GuildFund = GetInt(obj, "Fund");
                break;
            case "TaskGuildCelebrationScoreDef":
                reward.AddScore = GetInt(obj, "AddScore");
                break;
        }

        return reward;
    }

    private static QuestCheck ParseCheck(XElement obj)
    {
        string type = obj.Attribute("Id")?.Value ?? string.Empty;
        QuestCheck check = new() { CheckType = type };

        switch (type)
        {
            case "TaskCharLevelCheckDef":
                XElement? levelObj = obj.Element("CharLevel")?.Element("Obj");
                if (levelObj != null)
                {
                    check.LevelMin = GetInt(levelObj, "Min");
                    check.LevelMax = GetInt(levelObj, "Max");
                }
                break;
            case "TaskPreTaskCheckDef":
                XElement? preTasks = obj.Element("PreTasks");
                if (preTasks != null)
                {
                    foreach (XElement ptObj in preTasks.Elements("Obj"))
                    {
                        check.PreTasks.Add(new QuestPreTask
                        {
                            Task = GetInt(ptObj, "Task"),
                            Count = GetInt(ptObj, "Count"),
                        });
                    }
                }
                break;
            case "TaskMerLvCheckDef":
                XElement? merLvObj = obj.Element("MerLv")?.Element("Obj");
                if (merLvObj != null)
                {
                    check.MerLvMin = GetInt(merLvObj, "Min");
                    check.MerLvMax = GetInt(merLvObj, "Max");
                }
                break;
            case "TaskHRLevelCheckDef":
                XElement? hrLevelObj = obj.Element("HRLevel")?.Element("Obj");
                if (hrLevelObj != null)
                {
                    check.HRLevelMin = GetInt(hrLevelObj, "Min");
                    check.HRLevelMax = GetInt(hrLevelObj, "Max");
                }
                break;
            case "TaskActivityCheckDef":
                check.Activity = GetInt(obj, "Activity");
                break;
            case "TaskHRCardCheckDef":
                check.Card = GetInt(obj, "Card");
                break;
            case "TaskGroupMutexDef":
                check.GroupLimit = GetInt(obj, "GroupLimit");
                break;
        }

        return check;
    }

    private static string? DecryptToXml(string path)
    {
        DatFile dat = new();
        dat.Open(path);
        if (dat.Content == null || dat.ContentType != DatFile.DatContentType.Content)
            return null;
        // Strip trailing null bytes from AES padding, then trim whitespace
        string content = dat.Content.TrimEnd('\0').Trim();
        if (!content.StartsWith("<"))
            return null;
        return content;
    }

    private static int GetInt(XElement el, string name)
    {
        string? val = el.Element(name)?.Value;
        if (val == null) return 0;
        return int.TryParse(val, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v) ? v : 0;
    }

    private static string GetStr(XElement el, string name)
    {
        return el.Element(name)?.Value ?? string.Empty;
    }

    private static bool GetBool(XElement el, string name)
    {
        return string.Equals(el.Element(name)?.Value, "true", StringComparison.OrdinalIgnoreCase);
    }

    private static int ParseTsvInt(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return 0;
        return int.TryParse(value.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int v) ? v : 0;
    }
}
