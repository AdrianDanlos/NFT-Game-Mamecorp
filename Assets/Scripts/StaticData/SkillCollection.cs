//This should be in a database in the future

using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public struct SkillNames
{
    public const string DangerousStrength = "DangerousStrength";
    public const string Heavyweight = "Heavyweight";
    public const string Lightning = "Lightning";
    public const string Persistant = "Persistant";
    public const string FelineAgility = "FelineAgility";
    public const string CriticalBleeding = "CriticalBleeding";
    public const string Reversal = "Reversal";
    public const string CounterAttack = "CounterAttack";
    public const string Survival = "Survival";
    public const string BalletShoes = "BalletShoes";
    public const string Initiator = "Initiator";
    public const string CosmicKicks = "CosmicKicks";
    public const string ShurikenFury = "ShurikenFury";
    public const string LowBlow = "LowBlow";
    public const string JumpStrike = "JumpStrike";
    public const string GloriousShield = "GloriousShield";
    public const string ExplosiveBomb = "ExplosiveBomb";
    public const string InterdimensionalTravel = "InterdimensionalTravel";
    public const string HealingPotion = "HealingPotion";
    public const string ViciousTheft = "ViciousTheft";

}

public static class SkillCollection
{
    public enum SkillType
    {
        //Boost fighter stats by a % at the start of the combat and remain until the end of the combat.
        PASSIVES, 
        //These are casted at a specific time during the combat and can be used from 1 to n times. Unlike Supers they dont take a full turn as they are not the main attack but rather an IN-COMBAT like passive.
        SPONTANEOUS, 
        //These are a substitute for the default autoattack. 
        //They take a full turn and can only be used once per combat and are removed from the fighter skills list once used.
        SUPERS,
    }
    public enum SkillRarity
    {
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
            {"icon", "1" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Heavyweight},
            {"description", "Increase the health by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "2" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Lightning},
            {"description", "Increase the speed by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "3" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Persistant},
            {"description", "Increase the chances of attacking multiple times by 5%"},
            {"skillRarity", SkillRarity.COMMON.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "4" }
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
            {"icon", "6" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Reversal},
            {"description", "Increase the chance of attacking your opponent before he has finished his turn by 5%"},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "7" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.CounterAttack},
            {"description", "Increase the chance of hitting the opponent before it hits you by 5%"},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.PASSIVES.ToString()},
            {"icon", "8" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.Initiator},
            {"description", "You attack first every game."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SPONTANEOUS.ToString()},
            {"icon", "9" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.GloriousShield},
            {"description", "Whenever your opponent attacks you have a chance of invoking a shield that will block the attack."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SPONTANEOUS.ToString()},
            {"icon", "10" }
        },
        // new OrderedDictionary
        // {
        //     {"name", SkillNames.BalletShoes},
        //     {"description", "The opponent has a very high chance of missing it's first attack."},
        //     {"skillRarity", SkillRarity.COMMON.ToString()},
        //     {"category", SkillType.SPONTANEOUS.ToString()},
        //     {"icon", "11" }
        // },
        new OrderedDictionary
        {
            {"name", SkillNames.Survival},
            {"description", "Whenever you take lethal damage you survive with 1 health point."},
            {"skillRarity", SkillRarity.EPIC.ToString()},
            {"category", SkillType.SPONTANEOUS.ToString()},
            {"icon", "12" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.CosmicKicks},
            {"description", "Land between 4 and 8 deadly kicks that can't be dodged."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "13" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.ShurikenFury},
            {"description", "Throw between 4 and 8 ninja shurikens at high speed to your opponent."},
            {"skillRarity", SkillRarity.EPIC.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "14" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.LowBlow},
            {"description", "Run and slide towards your opponent to hit a low blow that deals critical damage."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "15" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.JumpStrike},
            {"description", "Jump towards the opponent to execute a sequence of lightning fast attacks that grant lifesteal and can't be dodged."},
            {"skillRarity", SkillRarity.EPIC.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "16" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.ExplosiveBomb},
            {"description", "Throw an explosive bomb towards your opponent that instantly detonates to inflict severe damage."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "17" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.InterdimensionalTravel},
            {"description", "Become invisble for a brief time to catch your opponent by surprise and deal extra damage."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "18" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.HealingPotion},
            {"description", "Use a magic potion that heals a 30% of the maximum health."},
            {"skillRarity", SkillRarity.RARE.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "19" }
        },
        new OrderedDictionary
        {
            {"name", SkillNames.ViciousTheft},
            {"description", "Steal one of the opponent skills and use it immediately."},
            {"skillRarity", SkillRarity.LEGENDARY.ToString()},
            {"category", SkillType.SUPERS.ToString()},
            {"icon", "20" }
        }
    };

    public static OrderedDictionary GetSkillByName(string skillname)
    {
        //FIXME: We can access with key, we dont need to iterate
        foreach(OrderedDictionary skill in skills)
        {
            if(skillname == (string)skill["name"])
            {
                return skill;
            }
        }

        Debug.Log("Error");
        return null;
    }
}
