using System.Collections.Generic;

public class SerializableFighter {
    private string _fighterName;
    private float _hp;
    private float _damage;
    private float _speed;
    private string _species;
    private int _level;
    private int _manaSlots;
    private List<Card> _cards;

    public string fighterName { get => _fighterName; set => _fighterName = value; }
    public float hp { get => _hp; set => _hp = value; }
    public float damage { get => _damage; set => _damage = value; }
    public float speed { get => _speed; set => _speed = value; }
    public string species { get => _species; set => _species = value; }
    public int level { get => _level; set => _level = value; }
    public int manaSlots { get => _manaSlots; set => _manaSlots = value; }
    public List<Card> cards { get => _cards; set => _cards = value; }

    public SerializableFighter(string fighterName, float hp, float damage, float speed, string species, int level, int manaSlots, List<Card> cards)
    {
        this.fighterName = fighterName;
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
        this.species = species;
        this.level = level;
        this.manaSlots = manaSlots;
        this.cards = cards;
    }
}