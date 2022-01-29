using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

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
    private int _repeatAttackChance = 10;
    private int _dodgeChance = 100;
    private int _criticalChance = 10;

    public string fighterName
    {
        get => _fighterName; set
        {
            _fighterName = value;
            SaveFighter();
        }
    }
    public float hp
    {
        get => _hp; set
        {
            _hp = value;
        }
    }
    public float damage
    {
        get => _damage; set
        {
            _damage = value;
        }
    }
    public float speed
    {
        get => _speed; set
        {
            _speed = value;
        }
    }
    public string species
    {
        get => _species; set
        {
            _species = value;
        }
    }
    public int level
    {
        get => _level; set
        {
            _level = value;
            SaveFighter();
        }
    }
    public int manaSlots
    {
        get => _manaSlots; set
        {
            _manaSlots = value;
            SaveFighter();
        }
    }
    public List<Card> cards
    {
        get => _cards; set
        {
            _cards = value;
            SaveFighter();
        }
    }
    public Vector2 initialPosition { get => _initialPosition; set => _initialPosition = value; }
    public Vector2 destinationPosition { get => _destinationPosition; set => _destinationPosition = value; }
    public int repeatAttackChance
    {
        get => _repeatAttackChance;
    }
    public int dodgeChance
    {
        get => _dodgeChance;
    }
    public int criticalChance
    {
        get => _criticalChance;
    }
    

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

    private void SaveFighter()
    {
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(this));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FIGHTER_FILE_NAME);
    }
}