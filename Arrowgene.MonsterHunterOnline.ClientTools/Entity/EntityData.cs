using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Entity;

public sealed class EntityDatabase
{
    public List<MonsterDef> Monsters { get; } = [];
    public List<NpcDef> Npcs { get; } = [];
    public List<PetDef> Pets { get; } = [];
}

public sealed class MonsterDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MonsterType { get; set; }
    public int MonsterGroup { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string EntityClass { get; set; } = string.Empty;
    public int Race { get; set; }
    public int Difficulty { get; set; }
    public int Size { get; set; }
    public int LootSize { get; set; }
    public int CaptureHpPercent { get; set; }
    public int ExistTime { get; set; }

    // From monsterdifficulty.dat
    public List<MonsterDifficultyEntry> DifficultyEntries { get; } = [];

    // From monsterdata.dat Parts sheet
    public List<MonsterPartDef> Parts { get; } = [];

    public string MonsterTypeLabel => MonsterType switch
    {
        1 => "Boss",
        2 => "Small",
        3 => "Intruder",
        _ => MonsterType > 0 ? $"Type {MonsterType}" : "",
    };
}

public sealed class MonsterDifficultyEntry
{
    public int Difficulty { get; set; }
    public int MaxHealth { get; set; }
    public int PhyAtk { get; set; }
    public int Defence { get; set; }
    public int WaterAtk { get; set; }
    public int FireAtk { get; set; }
    public int LightningAtk { get; set; }
    public int DragonAtk { get; set; }
    public int IceAtk { get; set; }
}

public sealed class MonsterPartDef
{
    public string PartId { get; set; } = string.Empty;
    public string PartName { get; set; } = string.Empty;
    public string StateId { get; set; } = string.Empty;
}

public sealed class NpcDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityClass { get; set; } = string.Empty;
    public string EntityCustomType { get; set; } = string.Empty;
    public int AppearTask { get; set; }
    public int DisappearTask { get; set; }

    // Services (parsed from boolean columns)
    public List<string> Services { get; } = [];
}

public sealed class PetDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Quality { get; set; } = string.Empty;
    public string Personality { get; set; } = string.Empty;
    public string AttackPreference { get; set; } = string.Empty;
    public string AttackStyle { get; set; } = string.Empty;
    public int Level { get; set; }
    public int BurrowHpPercent { get; set; }
    public int HealSpeed { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int Aptitude { get; set; }
    public int SupportSkill { get; set; }
}
