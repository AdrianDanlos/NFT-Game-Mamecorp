using System.Collections.Generic;

public static class Chest
{
    public enum ChestTypes
    {
        BATTLECHEST,
        SHOPCHEST
    }

    public enum BattleChestRarities
    {
        COMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    public enum ShopChestTypes
    {
        NORMAL,
        EPIC,
        LEGENDARY,
        SPECIAL
    }

    // Chance of receiving a chest of each one of the rarities on level up
    public static readonly Dictionary<BattleChestRarities, float> battleChestsProbabilities =
        new Dictionary<BattleChestRarities, float>()
        {
            { BattleChestRarities.COMMON, 1},
            { BattleChestRarities.RARE, 1},
            { BattleChestRarities.EPIC, 98},
            { BattleChestRarities.LEGENDARY, 2}
        };

    //Battle chests contain either gold & gems OR a skill
    public static readonly Dictionary<BattleChestRarities, Dictionary<string, int>> battleChestCurrencyRewards =
        new Dictionary<BattleChestRarities, Dictionary<string, int>>
        {
            {
                BattleChestRarities.COMMON, new Dictionary<string, int>
                {
                    {"minGold", 10},
                    {"maxGold", 51},
                    {"minGems", 1},
                    {"maxGems", 6},
                }

            },
            {
                BattleChestRarities.RARE, new Dictionary<string, int>
                {
                    {"minGold", 40},
                    {"maxGold", 81},
                    {"minGems", 7},
                    {"maxGems", 14},
                }
            },
        };

    public static readonly Dictionary<BattleChestRarities, Dictionary<SkillCollection.SkillRarity, float>> battleChestSkillRewardProbability =
        new Dictionary<BattleChestRarities, Dictionary<SkillCollection.SkillRarity, float>>
        {
            {BattleChestRarities.EPIC, new Dictionary<SkillCollection.SkillRarity, float>
                {
                    {SkillCollection.SkillRarity.COMMON, 70},
                    {SkillCollection.SkillRarity.RARE, 30},
                }
            },
            {BattleChestRarities.LEGENDARY, new Dictionary<SkillCollection.SkillRarity, float>
                {
                    {SkillCollection.SkillRarity.EPIC, 70},
                    {SkillCollection.SkillRarity.LEGENDARY, 30},
                }
            },
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
            {
                ShopChestTypes.SPECIAL, new Dictionary<string, float>
                {
                    // chances of SkillRarity
                    {SkillCollection.SkillRarity.COMMON.ToString(), 0},
                    {SkillCollection.SkillRarity.RARE.ToString(), 30},
                    {SkillCollection.SkillRarity.EPIC.ToString(), 60},
                    {SkillCollection.SkillRarity.LEGENDARY.ToString(), 10}
                }
            },
        };

    public static readonly Dictionary<ShopChestTypes, Dictionary<string, int>> shopChestsValue =
        new Dictionary<ShopChestTypes, Dictionary<string, int>>
        {
                {
                    ShopChestTypes.NORMAL, new Dictionary<string, int>
                    {
                        {"gold", 1000},
                    }
                },
                {
                    ShopChestTypes.EPIC, new Dictionary<string, int>
                    {
                        {"gold", 2500},
                    }
                },
                {
                    ShopChestTypes.LEGENDARY, new Dictionary<string, int>
                    {
                        {"gold", 10000},
                    }
                },
                {
                    ShopChestTypes.SPECIAL, new Dictionary<string, int>
                    {
                        {"gold", 7500},
                    }
                },
        };
}
