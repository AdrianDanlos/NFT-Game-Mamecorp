using System.Collections.Generic;
using UnityEngine;

// Difference between dontdestroyonload instance and static class?

public enum ChestTypes
{
    BATTLECHEST,
    SHOPCHEST
}

public enum BattleChestTypes
{
    // standard practice for enums that cannot be null by having the FIRST value in the enum
    // (aka 0) be the default value.
    NONE,
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
    // TODO: balance out skills dropchance & values
    public static readonly Dictionary<BattleChestTypes, float> battleChestsProbabilities =
    new Dictionary<BattleChestTypes, float>()
    {
        { BattleChestTypes.COMMON, 60},
        { BattleChestTypes.UNCOMMON, 30},
        { BattleChestTypes.RARE, 8},
        { BattleChestTypes.EPIC, 2}
    };

    // TODO: need to copy popup chest ui to combat after fight results
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

    public static string OpenChest(string chestType)
    {
        // TODO: make code reusable in post combat chest

        float totalWeight = 100f;
        float diceRoll = Random.Range(0f, totalWeight);

        foreach (KeyValuePair<string, float> chest in
            Chest.shopChests[(ShopChestTypes)System.Enum.Parse(typeof(ShopChestTypes), chestType.ToUpper())])
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        return null;
    }

    public static BattleChestTypes GetRandomBattleChest()
    {
        float totalWeight = 100f;
        float diceRoll = Random.Range(0f, totalWeight);

        foreach (KeyValuePair<BattleChestTypes, float> chest in
            Chest.battleChestsProbabilities)
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        return BattleChestTypes.NONE;
    }

    public static void GetBattleChestRewards()
    {
        string battlechest = GetRandomBattleChest().ToString();
        float goldAmount = Random.Range(
            battleChests[(BattleChestTypes)System.Enum.Parse(typeof(BattleChestTypes), battlechest)]["minGold"],
            battleChests[(BattleChestTypes)System.Enum.Parse(typeof(BattleChestTypes), battlechest)]["maxGold"]);
        float gemAmount = Random.Range(
                    battleChests[(BattleChestTypes)System.Enum.Parse(typeof(BattleChestTypes), battlechest)]["minGems"],
                    battleChests[(BattleChestTypes)System.Enum.Parse(typeof(BattleChestTypes), battlechest)]["maxGems"]);

        bool hasSkill = Random.value < 0.01 * battleChests[(BattleChestTypes)System.Enum.Parse(typeof(BattleChestTypes), battlechest)]["skillChance"];

        if (hasSkill)
        {
            // cant iterate
            // should chest have 2 dictionaries? gold and gems + skill chances
        }
    }
}
