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

    public Card(string name, int mana, string text, string rarity, string type){
        this.name = name;
        this.mana = mana;
        this.text = text;
        this.rarity = rarity;
        this.type = type;
    }
}
