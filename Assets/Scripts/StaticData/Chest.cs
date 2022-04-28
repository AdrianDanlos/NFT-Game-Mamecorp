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
        UNCOMMON,
        RARE,
        EPIC
    }

    public enum ShopChestTypes
    {
        NORMAL,
        EPIC,
        LEGENDARY,
        SPECIAL
    }

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
