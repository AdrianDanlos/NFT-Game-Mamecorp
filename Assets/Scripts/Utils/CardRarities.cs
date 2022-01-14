using System.Collections.Generic;

class CardRarity
{
    public Dictionary<string, Dictionary<string, string>> cardRarities = new Dictionary<string, Dictionary<string, string>>
    {
        {"Common", new Dictionary<string, string>{{"color", "#e0e5ee"}}},
        {"Rare", new Dictionary<string, string>{{"color", "#3887f2"}}},
        {"Epic", new Dictionary<string, string>{{"color", "#b533da"}}},
        {"Legendary", new Dictionary<string, string>{{"color", "#eb8307"}}},
    };
}

