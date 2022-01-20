using UnityEngine;
enum CardTypes
{
    Basic,
    Neutral,
    Species,
}

public class Card
{
    private string _cardName;
    private int _mana;
    private string _description;
    private string _rarity;
    private string _type;
    public string cardName { get => _cardName; set => _cardName = value; }
    public int mana { get => _mana; set => _mana = value; }
    public string description { get => _description; set => _description = value; }
    public string rarity { get => _rarity; set => _rarity = value; }
    public string type { get => _type; set => _type = value; }

    public Card(string cardName, int mana, string description, string rarity, string type)
    {
        this.cardName = cardName;
        this.mana = mana;
        this.description = description;
        this.rarity = rarity;
        this.type = type;
    }
}
