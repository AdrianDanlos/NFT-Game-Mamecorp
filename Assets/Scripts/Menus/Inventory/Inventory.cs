using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public static class Inventory 
{
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

        foreach (OrderedDictionary skill in SkillCollection.skills)
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
