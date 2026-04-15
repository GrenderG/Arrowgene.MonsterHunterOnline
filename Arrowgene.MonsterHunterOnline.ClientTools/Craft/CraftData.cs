using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Craft;

public sealed class CraftDatabase
{
    public List<CraftRecipe> Recipes { get; } = [];

    /// <summary>Item ID → name lookup (shared from item loading).</summary>
    public Dictionary<int, string> ItemNames { get; } = [];

    /// <summary>Item ID → icon key lookup (e.g. "item_icon_59").</summary>
    public Dictionary<int, string> ItemIcons { get; } = [];
}

public sealed class CraftRecipe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RecipeType { get; set; }
    public int EquipType { get; set; }
    public int CraftKind { get; set; }
    public int RequiredLevel { get; set; }
    public int RequiredStar { get; set; }
    public int MeleeRanged { get; set; }
    public int GoldCost { get; set; }
    public string SourceSheet { get; set; } = string.Empty;

    // Outputs
    public List<CraftOutput> Outputs { get; } = [];
    public CraftOutput? Byproduct { get; set; }

    // Material inputs
    public List<CraftMaterial> Materials { get; } = [];

    public string RecipeTypeLabel => RecipeType switch
    {
        0 => "Forge",
        1 => "Upgrade",
        _ => $"Type {RecipeType}",
    };

    public string EquipTypeLabel => EquipType switch
    {
        1 => "Great Sword",
        2 => "Dual Blades",
        3 => "Long Sword",
        4 => "Sword & Shield",
        5 => "Hammer",
        6 => "Hunting Horn",
        7 => "Lance",
        8 => "Gunlance",
        9 => "Switch Axe",
        10 => "Bow",
        11 => "Bowgun",
        20 => "Head",
        21 => "Chest",
        22 => "Arms",
        23 => "Waist",
        24 => "Legs",
        _ => EquipType > 0 ? $"Equip {EquipType}" : "",
    };
}

public sealed class CraftOutput
{
    public int ItemId { get; set; }
    public int Count { get; set; }
    public int Rate { get; set; }
}

public sealed class CraftMaterial
{
    public int ItemId { get; set; }
    public int Count { get; set; }
}
