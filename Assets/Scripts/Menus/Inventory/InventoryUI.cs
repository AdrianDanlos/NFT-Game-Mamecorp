using System.Collections.Generic;
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
    }

    private void GetAllSkills()
    {
        foreach (Transform child in skillsContainer.transform)
        {
            skillsList.Add(child);
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

    public void GetSkillClicked()
    {
        lastButtonClicked =  EventSystem.current.currentSelectedGameObject.name;
        HideSkill(lastButtonClicked);
    }

}
