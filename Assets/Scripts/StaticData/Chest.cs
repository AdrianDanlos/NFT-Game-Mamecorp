using System.Collections.Generic;

public class Chest 
{
    public enum ChestTypes
    {
        COMMON,
        UNCOMMON,
        RARE,
        EPIC
    }

    // TODO: need to copy popup chest ui to combat after fight results
    // TODO: balance out skills dropchance & values
    public static readonly Dictionary<ChestTypes, Dictionary<string, float>> chests =
    new Dictionary<ChestTypes, Dictionary<string, float>>
    {
        {
            ChestTypes.COMMON, new Dictionary<string, float>
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
                {Rarity.LEGENDARY.ToString(), 1},
            }
        }
    };

}
