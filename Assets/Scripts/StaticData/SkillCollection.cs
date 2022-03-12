//This should be in a database in the future

using System.Collections.Generic;
using System.Collections.Specialized;

enum SkillType
{
    PASSIVES,
    SUPERS, //Generally can only be used once per combat
}

public enum Rarity
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}


public static class SkillCollection
{
    public static List<OrderedDictionary> skills =
    new List<OrderedDictionary>
    {
        new OrderedDictionary
        {
            {"name", "Cosmic kicks"},
            {"description", "Land between 4 and 8 deadly kicks that can't be dodged."},
            {"rarity", Rarity.COMMON.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", "Shuriken fury"},
            {"description", "Throw between 4 and 8 ninja shurikens at high speed to your opponent."},
            {"rarity", Rarity.EPIC.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", "Low blow"},
            {"description", "Run and slide towards your opponent to hit a low blow that deals critical damage."},
            {"rarity", Rarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        }
    };
}


