using System.Collections.Generic;
using UnityEngine;

public static class Inventory 
{
    public static Dictionary<string, int> GetSkillsByRarityCount(List<Skill> fighterSkills)
    {
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
}
