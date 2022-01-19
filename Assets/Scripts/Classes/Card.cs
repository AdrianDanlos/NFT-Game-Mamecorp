using UnityEngine;
enum CardTypes
{
    Basic,
    Neutral,
    Species,
}

public class Card
{
    public string cardName { get; set; }
    public int mana { get; set; }
    public string text { get; set; }
    public string rarity { get; set; }
    public string type { get; set; }

    public Card(string cardName, int mana, string text, string rarity, string type){
        this.cardName = cardName;
        this.mana = mana;
        this.text = text;
        this.rarity = rarity;
        this.type = type;
    }
}
