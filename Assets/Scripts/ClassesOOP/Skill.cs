using UnityEngine;
enum SkillTypes
{
    Basic,
    Neutral,
    Species,
}

public class Skill
{
    private string _skillName;
    private int _mana;
    private string _description;
    private string _rarity;
    private string _type;
    public string skillName { get => _skillName; set => _skillName = value; }
    public int mana { get => _mana; set => _mana = value; }
    public string description { get => _description; set => _description = value; }
    public string rarity { get => _rarity; set => _rarity = value; }
    public string type { get => _type; set => _type = value; }

    public Skill(string skillName, int mana, string description, string rarity, string type)
    {
        this.skillName = skillName;
        this.mana = mana;
        this.description = description;
        this.rarity = rarity;
        this.type = type;
    }
}
