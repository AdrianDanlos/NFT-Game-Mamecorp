using System.Collections.Generic;
using UnityEngine;

// MonoBehaviours are scripts that are attached to an object in the scene, and run in the scene as long as the object they are attached to is active.
public class Fighter : MonoBehaviour
{
    public string fighterName { get; set; }
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

    public void FighterConstructor(string fighterName, float hp, float damage, float speed, string species, int level, int manaSlots)
    {
        this.fighterName = fighterName;
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
        this.species = species;
        this.level = level;
        this.manaSlots = manaSlots;
    }
}