//This should be in a database in the future

using System.Collections.Generic;
using System.Collections.Specialized;

public static class CardCollection
{
    public static List<OrderedDictionary> cards = new List<OrderedDictionary>()
    {
        new OrderedDictionary
        {
            {"cardName", "A"},
            {"mana", 5},
            {"text", "B"},
            {"rarity", "C"},
            {"type", "D" }
        },
        new OrderedDictionary
        {
            {"cardName", "A"},
            {"mana", 5},
            {"text", "B"},
            {"rarity", "C"},
            {"type", "D" }
        },
    };
}
