//This should be in a database in the future

using System.Collections.Generic;
using System.Collections.Specialized;

public static class SkillCollection
{
    public static readonly List<OrderedDictionary> skills = new List<OrderedDictionary>()
    {
        new OrderedDictionary
        {
            {"skillName", "A"},
            {"mana", 5},
            {"text", "B"},
            {"rarity", "C"},
            {"type", "D" }
        },
        new OrderedDictionary
        {
            {"skillName", "A"},
            {"mana", 5},
            {"text", "B"},
            {"rarity", "C"},
            {"type", "D" }
        },
    };
}
