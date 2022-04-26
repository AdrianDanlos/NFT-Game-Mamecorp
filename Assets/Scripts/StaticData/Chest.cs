using System.Collections.Generic;
using UnityEngine;

// Difference between dontdestroyonload instance and static class?

public enum ChestTypes
{
    BATTLECHEST,
    SHOPCHEST
}

public enum BattleChestRarities
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
    NORMAL,
    EPIC,
    LEGENDARY
}

public static class Chest 
{
    // Chance of gettin chest after level up
    public static readonly Dictionary<BattleChestRarities, float> battleChestsProbabilities =
    new Dictionary<BattleChestRarities, float>()
    {
        { BattleChestRarities.COMMON, 60},
        { BattleChestRarities.UNCOMMON, 30},
        { BattleChestRarities.RARE, 8},
        { BattleChestRarities.EPIC, 2}
    };

    // TODO: need to copy popup chest ui to combat after fight results
    // chests loot 
    public static readonly Dictionary<BattleChestRarities, Dictionary<string, int>> battleChestsRewards =
    new Dictionary<BattleChestRarities, Dictionary<string, int>>
    {
        {
            BattleChestRarities.COMMON, new Dictionary<string, int>
            {
                {"minGold", 10},
                {"maxGold", 51},
                {"minGems", 0},
                {"maxGems", 0},
            }
            
        },
        {
            BattleChestRarities.UNCOMMON, new Dictionary<string, int>
            {
                {"minGold", 40},
                {"maxGold", 81},
                {"minGems", 1},
                {"maxGems", 6},
            }
        },
        {
            BattleChestRarities.RARE, new Dictionary<string, int>
            {
                {"minGold", 80},
                {"maxGold", 141},
                {"minGems", 10},
                {"maxGems", 16},
            }
        },
        {
            BattleChestRarities.EPIC, new Dictionary<string, int>
            {
                {"minGold", 500},
                {"maxGold", 601},
                {"minGems", 50},
                {"maxGems", 101},
            }
        }
    };

    public static readonly Dictionary<BattleChestRarities, Dictionary<string, float>> battleChestsSkillProbabilities =
    new Dictionary<BattleChestRarities, Dictionary<string, float>>
    {
            {
                BattleChestRarities.COMMON, new Dictionary<string, float>
                {
                    // chances of SkillRarity
                    {SkillCollection.SkillRarity.COMMON.ToString(), 80},
                    {SkillCollection.SkillRarity.RARE.ToString(), 14},
                    {SkillCollection.SkillRarity.EPIC.ToString(), 5},
                    {SkillCollection.SkillRarity.LEGENDARY.ToString(), 1}
                }

            },
            {
                BattleChestRarities.UNCOMMON, new Dictionary<string, float>
                {
                    // chances of SkillRarity
                    {SkillCollection.SkillRarity.COMMON.ToString(), 60},
                    {SkillCollection.SkillRarity.RARE.ToString(), 34},
                    {SkillCollection.SkillRarity.EPIC.ToString(), 5},
                    {SkillCollection.SkillRarity.LEGENDARY.ToString(), 1}
                }
            },
            {
                BattleChestRarities.RARE, new Dictionary<string, float>
                {
                    // chances of SkillRarity
                    {SkillCollection.SkillRarity.COMMON.ToString(), 50},
                    {SkillCollection.SkillRarity.RARE.ToString(), 40},
                    {SkillCollection.SkillRarity.EPIC.ToString(), 9},
                    {SkillCollection.SkillRarity.LEGENDARY.ToString(), 1}
                }
            },
            {
                BattleChestRarities.EPIC, new Dictionary<string, float>
                {
                    // chances of SkillRarity
                    {SkillCollection.SkillRarity.COMMON.ToString(), 30},
                    {SkillCollection.SkillRarity.RARE.ToString(), 53},
                    {SkillCollection.SkillRarity.EPIC.ToString(), 16},
                    {SkillCollection.SkillRarity.LEGENDARY.ToString(), 1}
                }
            }
    };

    public static readonly Dictionary<ShopChestTypes, Dictionary<string, float>> shopChests =
    new Dictionary<ShopChestTypes, Dictionary<string, float>>
    {
        {
            ShopChestTypes.NORMAL, new Dictionary<string, float>
            {
                // chances of SkillRarity
                {SkillCollection.SkillRarity.COMMON.ToString(), 0},
                {SkillCollection.SkillRarity.RARE.ToString(), 94},
                {SkillCollection.SkillRarity.EPIC.ToString(), 5},
                {SkillCollection.SkillRarity.LEGENDARY.ToString(), 1}
            } 
        },
        {
            ShopChestTypes.EPIC, new Dictionary<string, float>
            {
                // chances of SkillRarity
                {SkillCollection.SkillRarity.COMMON.ToString(), 0},
                {SkillCollection.SkillRarity.RARE.ToString(), 0},
                {SkillCollection.SkillRarity.EPIC.ToString(), 95},
                {SkillCollection.SkillRarity.LEGENDARY.ToString(), 5}
            }
        },
        {
            ShopChestTypes.LEGENDARY, new Dictionary<string, float>
            {
                // chances of SkillRarity
                {SkillCollection.SkillRarity.COMMON.ToString(), 0},
                {SkillCollection.SkillRarity.RARE.ToString(), 0},
                {SkillCollection.SkillRarity.EPIC.ToString(), 0},
                {SkillCollection.SkillRarity.LEGENDARY.ToString(), 100}
            }
        },
    };

    public static string OpenChest(string chestType)
    {
        chestType = chestType.ToUpper();
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

    public static BattleChestRarities GetRandomBattleChest()
    {
        float totalWeight = 100f;
        float diceRoll = Random.Range(0f, totalWeight);

        foreach (KeyValuePair<BattleChestRarities, float> chest in
            Chest.battleChestsProbabilities)
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        return BattleChestRarities.NONE;
    }

    public static int GetBattleChestGold(string battleChest)
    {
        battleChest = battleChest.ToUpper();
        return Random.Range(
            battleChestsRewards[(BattleChestRarities)System.Enum.Parse(typeof(BattleChestRarities), battleChest)]["minGold"],
            battleChestsRewards[(BattleChestRarities)System.Enum.Parse(typeof(BattleChestRarities), battleChest)]["maxGold"]);
    }

    public static int GetBattleChestGems(string battleChest)
    {
        battleChest = battleChest.ToUpper();
        return Random.Range(
                    battleChestsRewards[(BattleChestRarities)System.Enum.Parse(typeof(BattleChestRarities), battleChest)]["minGems"],
                    battleChestsRewards[(BattleChestRarities)System.Enum.Parse(typeof(BattleChestRarities), battleChest)]["maxGems"]);
    }

    public static SkillCollection.SkillRarity GetBattleChestSkillReward(string battleChest)
    {
        battleChest.ToUpper();
        float totalWeight = 100f;
        float diceRoll = Random.Range(0f, totalWeight);

        foreach (KeyValuePair<string, float> raity in
        Chest.battleChestsSkillProbabilities
        [(BattleChestRarities)System.Enum.Parse(typeof(BattleChestRarities), battleChest)])
        {
            if (raity.Value >= diceRoll)
                return (SkillCollection.SkillRarity)System.Enum.Parse(typeof(SkillCollection.SkillRarity), raity.Key);

            diceRoll -= raity.Value;
        }

        return SkillCollection.SkillRarity.NONE;
    }
}
