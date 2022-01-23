using System.Collections.Generic;
using UnityEngine;

// MonoBehaviours are scripts that are attached to an object in the scene, and run in the scene as long as the object they are attached to is active.
public class Fighter : MonoBehaviour
{
    private string _fighterName;
    private float _hp;
    private float _damage;
    private float _speed;
    private string _species;
    private int _level;
    private int _manaSlots;
    private List<Card> _cards;
    private Vector2 _initialPosition;
    private Vector2 _destinationPosition;

    public string fighterName { get => _fighterName; set => _fighterName = value; }
    public float hp { get => _hp; set => _hp = value; }
    public float damage { get => _damage; set => _damage = value; }
    public float speed { get => _speed; set => _speed = value; }
    public string species { get => _species; set => _species = value; }
    public int level { get => _level; set => _level = value; }
    //FIXME: This needs to be discussed as a part of the design. Does each fighter have a specific amount of variable manaSlots.
    public int manaSlots { get => _manaSlots; set => _manaSlots = value; }
    public List<Card> cards { get => _cards; set => _cards = value; }

    // Fighter position
    public Vector2 initialPosition { get => _initialPosition; set => _initialPosition = value; }
    public Vector2 destinationPosition { get => _destinationPosition; set => _destinationPosition = value; }

    //Passive skills
    public int repeatAttackChance = 5;
    public int dodgeChance = 5;
    public int criticalChance = 5;

    // When a class is attached to a gameobject (Monobehaviour) it is not possible to use the default constructor for the class.
    // That's why we create the following FighterConstructor method and use it as a constructor.
    public void FighterConstructor(string fighterName, float hp, float damage, float speed, string species, int level, int manaSlots, List<Card> cards)
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