
var cardRarities = new Dictionary<string, Dictionary<string, string>>
{
    {"Common", new Dictionary {"color", "#e0e5ee"}},
    {"Rare", new Dictionary {"color", "#3887f2"}},
    {"Epic", new Dictionary {"color", "#b533da"}},
    {"Legendary", new Dictionary {"color", "#eb8307"}},
};

enum CardTypes
{
    Basic,
    Neutral,
    Species,      
}

class Card
{
  private string name { get; set; }
  private int mana { get; set; }
  private string text { get; set; }
  private string rarity { get; set; }
  private string type { get; set; }
}
