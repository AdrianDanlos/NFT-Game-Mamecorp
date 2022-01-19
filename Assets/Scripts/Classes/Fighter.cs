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

    public List<Card> cards { get; set; }
    
    // Fighter position
    public Vector2 initialPosition { get; set; }
    public Vector2 destinationPosition { get; set; }

    //Passive skills
    public int repeatAttackChance { get; } = 5;
    public int dodgeChance { get; } = 5;
    public int criticalChance { get; } = 5;
    

    // When adding a script through the AddComponent method it is not possible to use the default constructor for the class.
    // That's why we create the following FighterConstructor method and use it as a constructor.
    public void FighterConstructor(string fighterName, float hp, float damage, float speed, string species, int level, int manaSlots, List<Card> cards, Vector2 initialPosition, Vector2 destinationPosition)
    {
        this.fighterName = fighterName;
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
        this.species = species;
        this.level = level;
        this.manaSlots = manaSlots;
        this.cards = cards;
        this.initialPosition = initialPosition;
        this.destinationPosition = destinationPosition;
    }
}