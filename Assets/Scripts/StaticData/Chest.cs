using System.Collections.Generic;
public enum ChestTypes
{
    BATTLECHEST,
    SHOPCHEST
}

public enum BattleChestTypes
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC
}

public enum ShopChestTypes
{
    REGULAR,
    MAGIC,
    VOID,
    LEGENDARY
}

public static class Chest 
{

    // TODO: need to copy popup chest ui to combat after fight results
    // TODO: balance out skills dropchance & values
    public static readonly Dictionary<BattleChestTypes, Dictionary<string, float>> battleChests =
    new Dictionary<BattleChestTypes, Dictionary<string, float>>
    {
        {
            BattleChestTypes.COMMON, new Dictionary<string, float>
            {
                {"minGold", 10},
                {"maxGold", 50},
                {"minGems", 0},
                {"maxGems", 0},
                {"skillChance", 25},
                // chances of skill rarity
                {Rarity.COMMON.ToString(), 80},
                {Rarity.RARE.ToString(), 14},
                {Rarity.EPIC.ToString(), 5},
                {Rarity.LEGENDARY.ToString(), 1}
            }
            
        },
        {
            BattleChestTypes.UNCOMMON, new Dictionary<string, float>
            {
                {"minGold", 30},
                {"maxGold", 100},
                {"minGems", 1},
                {"maxGems", 5},
                {"skillChance", 25},
                // chances of skill rarity
                {Rarity.COMMON.ToString(), 60},
                {Rarity.RARE.ToString(), 34},
                {Rarity.EPIC.ToString(), 5},
                {Rarity.LEGENDARY.ToString(), 1}
            }
        }
    };

    public static readonly Dictionary<ShopChestTypes, Dictionary<string, float>> shopChests =
    new Dictionary<ShopChestTypes, Dictionary<string, float>>
    {
        {
            ShopChestTypes.LEGENDARY, new Dictionary<string, float>
            {
                // chances of skill rarity
                {Rarity.COMMON.ToString(), 0},
                {Rarity.RARE.ToString(), 94},
                {Rarity.EPIC.ToString(), 5},
                {Rarity.LEGENDARY.ToString(), 1},
            }
        }
    };

}
