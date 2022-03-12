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
            {"Title", SkillsList.COSMIC_KICKS},
            {"Description", "Land between 4 and 8 deadly kicks that can't be dodged."},
            {"Rarity", Rarity.RARE.ToString()},
            {"Category", SkillType.SUPERS.ToString()},
            {"Icon", "5" }
        },
        new OrderedDictionary
        {
            {"Title", SkillsList.COSMIC_KICKS},
            {"Description", "Throw between 4 and 8 ninja shurikens at high speed to your opponent."},
            {"Rarity", Rarity.RARE.ToString()},
            {"Category", SkillType.SUPERS.ToString()},
            {"Icon", "5" }
        }
    };
}


