//This should be in a database in the future

using System.Collections.Generic;
using System.Collections.Specialized;

public enum SkillsList
{
    COSMIC_KICKS,
    SHURIKEN_FURY,
}

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
            {"name", SkillsList.COSMIC_KICKS},
            {"description", "Land between 4 and 8 deadly kicks that can't be dodged."},
            {"rarity", Rarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillsList.SHURIKEN_FURY},
            {"description", "Throw between 4 and 8 ninja shurikens at high speed to your opponent."},
            {"rarity", Rarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        }
    };
}


