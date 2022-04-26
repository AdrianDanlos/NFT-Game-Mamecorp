//This should be in a database in the future

using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public struct SkillNames
{
    public const string DangerousStrength = "Dangerous Strength";
    public const string Heavyweight = "Heavyweight";
    public const string Lightning = "Lightning";
    public const string Persistant = "Persistant";
    public const string FelineAgility = "Feline Agility";
    public const string CriticalBleeding = "Critical Bleeding";
    public const string Reversal = "Reversal";
    public const string CounterAttack = "Counter Attack";
    public const string Survival = "Survival";
    public const string BalletShoes = "Ballet Shoes";
    public const string Initiator = "Initiator";
    public const string CosmicKicks = "Cosmic Kicks";
    public const string ShurikenFury = "Shuriken Fury";
    public const string LowBlow = "Low Blow";
    public const string JumpStrike = "Jump Strike";
    public const string GloriousShield = "Glorious Shield";

}


public static class SkillCollection
{
    enum SkillType
    {
        PASSIVES, //Stats granted at the start of the combat and remain for the rest of the combat
        SUPERS, //Can only be used once per combat and are removed from the fighter skills list once used
    }
    public enum SkillRarity
    {
        // standard practice for enums that cannot be null by having the FIRST value in the enum
        // (aka 0) be the default value.
        NONE,
        COMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    public static List<OrderedDictionary> skills =
    new List<OrderedDictionary>
    {
        new OrderedDictionary
        {
            {"name", SkillNames.DangerousStrength},
            {"description", "Increase the attack damage by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Heavyweight},
            {"description", "Increase the health by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Lightning},
            {"description", "Increase the speed by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Persistant},
            {"description", "Increase the chances of attacking multiple times by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.FelineAgility},
            {"description", "Increase the chance of dodging attacks by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.CriticalBleeding},
            {"description", "Increase the chance of landing a critical hit by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Reversal},
            {"description", "Increase the chance of attacking your opponent before he has finished his turn by 5%"},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.CounterAttack},
            {"description", "Increase the chance of hitting the opponent before it hits you by 5%"},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Initiator},
            {"description", "You attack first every game."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.GloriousShield},
            {"description", "Whenever your opponent attacks you have a chance of invoking a shield that will block the attack."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.BalletShoes},
            {"description", "The opponent has a very high chance of missing it's first attack."},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Survival},
            {"description", "Whenever you take lethal damage you survive with 1 health point."},
            {"skillRarity", SkillRarity.EPIC.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.CosmicKicks},
            {"description", "Land between 4 and 8 deadly kicks that can't be dodged."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.ShurikenFury},
            {"description", "Throw between 4 and 8 ninja shurikens at high speed to your opponent."},
            {"skillRarity", SkillRarity.EPIC.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.LowBlow},
            {"description", "Run and slide towards your opponent to hit a low blow that deals critical damage."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.JumpStrike},
            {"description", "Jump towards the opponent to execute a sequence of lightning fast attacks that grant lifesteal and can't be dodged."},
            {"skillRarity", SkillRarity.EPIC.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "5" }
        }
    };

    public static Dictionary<string, int> GetFighterSkillsData(List<Skill> fighterSkills)
    {
        //Skills should never be null. Skills should be an empty list. If skills are null we have a bug that needs to be fixed.
        //Skills are set to an empty list when the fighter is created
        Dictionary<string, int> skillsByRarityCount = new Dictionary<string, int>()
        {
            {"COMMON", 0 },
            {"RARE", 0 },
            {"EPIC", 0 },
            {"LEGENDARY", 0 },
        };

        foreach (Skill skill in fighterSkills)
        {

            switch (skill.rarity.ToUpper())
            {
                case "COMMON":
                    skillsByRarityCount["COMMON"]++;
                    break;
                case "RARE":
                    skillsByRarityCount["RARE"]++;
                    break;
                case "EPIC":
                    skillsByRarityCount["EPIC"]++;
                    break;
                case "LEGENDARY":
                    skillsByRarityCount["LEGENDARY"]++;
                    break;
            }
        }


        Debug.Log("COMMON: " + skillsByRarityCount["COMMON"] +
            " | RARE: " + skillsByRarityCount["RARE"] +
            " | EPIC: " + skillsByRarityCount["EPIC"] +
            " | LEGENDARY: " + skillsByRarityCount["LEGENDARY"]);

        return skillsByRarityCount;
    }

    public static void GetSkillsNameData(List<Skill> fighterSkills)
    {
        //Skills should never be null. Skills should be an empty list. If skills are null we have a bug that needs to be fixed.
        //Skills are set to an empty list when the fighter is created
        List<string> skillsnames = new List<string>();
        string message = "";

        foreach (Skill skill in fighterSkills)
        {
            skillsnames.Add(skill.skillName);
            message += skill.skillName + " - ";
        }

        Debug.Log(message);
    }

    public static Dictionary<string, int> GetAllRaritySkillCount()
    {
        Dictionary<string, int> skillsByRarityCount = new Dictionary<string, int>()
        {
            {"COMMON", 0 },
            {"RARE", 0 },
            {"EPIC", 0 },
            {"LEGENDARY", 0 },
        };

        foreach (OrderedDictionary skill in skills)
        {

            switch (skill["skillRarity"])
            {
                case "COMMON":
                    skillsByRarityCount["COMMON"]++;
                    break;
                case "RARE":
                    skillsByRarityCount["RARE"]++;
                    break;
                case "EPIC":
                    skillsByRarityCount["EPIC"]++;
                    break;
                case "LEGENDARY":
                    skillsByRarityCount["LEGENDARY"]++;
                    break;
            }
        }

        Debug.Log("COMMON: " + skillsByRarityCount["COMMON"] + 
            " | RARE: " + skillsByRarityCount["RARE"] + 
            " | EPIC: " + skillsByRarityCount["EPIC"] + 
            " | LEGENDARY: " + skillsByRarityCount["LEGENDARY"]);

        return skillsByRarityCount;
    }
}
