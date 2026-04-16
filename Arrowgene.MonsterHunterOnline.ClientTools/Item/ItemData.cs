using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.ClientTools.Item;

public sealed class ItemDatabase
{
    public List<ItemDef> Items { get; } = [];
}

public sealed class ItemDef
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ItemLevel { get; set; }
    public int Rarity { get; set; }
    public string Icon { get; set; } = string.Empty;
    public int Rank { get; set; }
    public int MainCategory { get; set; }
    public int SubCategory { get; set; }
    public int DetailCategory { get; set; }
    public int BindType { get; set; }
    public int OwnLimit { get; set; }
    public int CarryLimit { get; set; }
    public int StackLimit { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public bool IsMallItem { get; set; }
    public bool KeepOnLeaveLevel { get; set; }
    public bool CanDestroy { get; set; }
    // Equipment-specific stats (0 for non-equipment items)
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Affinity { get; set; }
    public int Slots { get; set; }
    public int MaxSlots { get; set; }
    public int WaterAttack { get; set; }
    public int FireAttack { get; set; }
    public int ThunderAttack { get; set; }
    public int DragonAttack { get; set; }
    public int IceAttack { get; set; }
    public int WaterRes { get; set; }
    public int FireRes { get; set; }
    public int ThunderRes { get; set; }
    public int DragonRes { get; set; }
    public int IceRes { get; set; }

    public string SourceFile { get; set; } = string.Empty;
    public string SourceSheet { get; set; } = string.Empty;

    public bool IsEquipment => MainCategory == 2;

    public string RarityLabel => Rarity switch
    {
        1 => "Common",
        2 => "Uncommon",
        3 => "Rare",
        4 => "Epic",
        5 => "Legendary",
        _ => Rarity > 0 ? $"Rarity {Rarity}" : "",
    };

    /// <summary>
    /// MainCategory (物品主类) is ItemClass: 1=Item, 2=Equipment.
    /// SubCategory (物品中类) gives the specific type within each class.
    /// </summary>
    public string MainCategoryLabel => MainCategory switch
    {
        1 => SubCategory switch
        {
            1 => "Consumable",
            2 => "Quest",
            3 => "Tool",
            4 => "Ammo",
            5 => "Revival",
            6 => "Book",
            7 => "Gem",
            8 => "Material",
            9 => "Farm",
            10 => "Charm",
            11 => "Gift",
            12 => "Skill Pearl",
            13 => "Legend Pearl",
            _ => SubCategory > 0 ? $"Item ({SubCategory})" : "Item",
        },
        2 => SubCategory switch
        {
            1 => "Weapon",
            2 => "Armor",
            3 => "Jewelry",
            4 => "Clothes",
            5 => "Fashion Weapon",
            _ => SubCategory > 0 ? $"Equip ({SubCategory})" : "Equipment",
        },
        _ => MainCategory > 0 ? $"Class {MainCategory}" : "",
    };

    public string BindTypeLabel => BindType switch
    {
        0 => "None",
        1 => "BoE",
        2 => "BoP",
        _ => $"Bind {BindType}",
    };
}
