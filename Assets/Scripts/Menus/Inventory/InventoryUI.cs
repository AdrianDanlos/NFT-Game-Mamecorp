using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    // UI
    GameObject skillsContainer;

    // Skill Description 
    GameObject skillIcon;
    GameObject skillRarityFrame;
    GameObject skillName;
    GameObject skillRarity;
    GameObject skillDescription;
    GameObject skillQuote;

    // variables
    List<Transform> skillsList = new List<Transform>();
    string lastButtonClicked = "";

    private void Awake()
    {
        // 
        skillsContainer = GameObject.FindGameObjectWithTag("SkillsContainer");
        skillIcon = GameObject.Find("SkillIcon");
        skillRarityFrame = GameObject.Find("SkillFrame");
        skillName = GameObject.Find("Text_Name");
        skillRarity = GameObject.Find("Text_Rarity");
        skillDescription = GameObject.Find("Text_Description");
        skillQuote = GameObject.Find("Lore");

        // gather all gameobjects
        GetAllSkills();
        ShowOwnedSkills();
    }

    private void GetAllSkills()
    {
        foreach (Transform child in skillsContainer.transform)
        {
            skillsList.Add(child);
        }
    }

    private void ShowOwnedSkills()
    {
        int i = 0;

        foreach (Transform skill in skillsList)
        {
            if (skillsList[i].gameObject.name != (string) SkillCollection.skills[i]["name"])
            {
                HideSkill(skillsList[i].name);
            }

            i++;
        }
    }

    public void HideSkill(string skillname)
    {
        foreach (Transform child in skillsList)
        {
            if(child.gameObject.name == skillname)
            {
                child.GetChild(0).gameObject.SetActive(true);
                child.GetChild(1).gameObject.SetActive(false);
            }
        }
    }


    public void ChangeSkillInfo(string skillname)
    {
        OrderedDictionary skillDictionary = SkillCollection.GetSkillByName(skillname);

        foreach (Transform child in skillsList)
        {
            if (child.gameObject.name == skillname)
            {
                // skillIcon = GameObject.Find("SkillIcon");
                // skillRarityFrame = GameObject.Find("SkillFrame");
                skillName.GetComponent<TextMeshProUGUI>().text = skillDictionary["name"].ToString();
                skillRarity.GetComponent<TextMeshProUGUI>().text = skillDictionary["skillRarity"].ToString();
                skillDescription.GetComponent<TextMeshProUGUI>().text = skillDictionary["description"].ToString();
                skillQuote.GetComponent<TextMeshProUGUI>().text = "none";
            }
        }
    }

    public void GetSkillClicked()
    {
        lastButtonClicked =  EventSystem.current.currentSelectedGameObject.name;
        ChangeSkillInfo(lastButtonClicked);
    }

}
