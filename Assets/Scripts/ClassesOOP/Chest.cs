using System.Collections.Generic;

public enum ChestTypes
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic
}


public class Chest 
{
    // need to copy popup chest ui to combat after fight results
    public static readonly Dictionary<ChestTypes, Dictionary<string, float>> chestLoot =
    new Dictionary<ChestTypes, Dictionary<string, float>>
    {
        {
            ChestTypes.Common, new Dictionary<string, float>{
            {"gold", 80}}
        }
    };
}
