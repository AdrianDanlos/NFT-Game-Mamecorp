using System.Collections.Generic;

public class Fighter
{
    public string name { get; set; }
    public float hp { get; set; }
    public float damage { get; set; }
    public float speed { get; set; }
    public string species { get; set; }

    public int level { get; set; }

    //FIXME: This needs to be discussed as a part of the design. Does each fighter have a specific amount of variable manaSlots.
    public int manaSlots { get; set; }

    //Passive skills
    public int repeatAttackChance { get; } = 5;
    public int dodgeChance { get; } = 5;
    public int criticalChance { get; } = 5;
    public List<string> cards { get; set; }

    // Constructor
    public Fighter(string name, float hp, float damage, float speed, string species, int level, int manaSlots)
    {
        this.name = name;
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
        this.species = species;
        this.level = level;
        this.manaSlots = manaSlots;
    }
}