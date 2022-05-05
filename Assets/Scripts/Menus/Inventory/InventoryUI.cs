using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // UI
    GameObject skillsContainer;
    [SerializeField] private List<Sprite> frameColors = new List<Sprite>();


    // Skill Description 
    GameObject skillIcon;
    GameObject skillRarityFrame;
    GameObject skillName;
    GameObject skillRarity;
    GameObject skillDescription;
    GameObject skillQuote;
    Fighter player;

    // variables
    List<Transform> skillsGameObjectList = new List<Transform>();


    private void Awake()
    {
        player = PlayerUtils.FindInactiveFighter();
        skillsContainer = GameObject.FindGameObjectWithTag("SkillsContainer");
        skillIcon = GameObject.Find("SkillIcon");
        skillRarityFrame = GameObject.Find("SkillFrame");
        skillName = GameObject.Find("Text_Name");
        skillRarity = GameObject.Find("Text_Rarity");
        skillDescription = GameObject.Find("Text_Description");
        skillQuote = GameObject.Find("Lore");

        AddSkillsGameObjectsToList();
        ShowOwnedSkills();
    }

    private void AddSkillsGameObjectsToList()
    {
        foreach (Transform skill in skillsContainer.transform) skillsGameObjectList.Add(skill);
    }

    private void ShowOwnedSkills()
    {
        foreach (Transform skill in skillsGameObjectList)
        {
            //show skill on UI
            if (player.HasSkill(skill.gameObject.name)) ShowQuestionMarkOrSkill(skill, false, true);
            //show question mark on UI
            else ShowQuestionMarkOrSkill(skill, true, false);
        }
    }

    private void ShowQuestionMarkOrSkill(Transform skill, bool showQuestionMark, bool showSkill)
    {
        skill.GetChild(0).gameObject.SetActive(showQuestionMark);
        skill.GetChild(1).gameObject.SetActive(showSkill);
    }

    // private void ShowOwnedSkills()
    // {
    //     int i = 0;

    //     foreach (Transform skill in skillsGameObjectList)
    //     {
    //         Debug.Log(skillsGameObjectList[i].gameObject.name);
    //         Debug.Log(skillsGameObjectList[i].gameObject.name);
    //         if (skillsGameObjectList[i].gameObject.name != (string)SkillCollection.skills[i]["name"])
    //         {
    //             HideSkill(skillsGameObjectList[i].name);
    //         }

    //         i++;
    //     }
    // }

    public void HideSkill(string skillname)
    {
        foreach (Transform child in skillsGameObjectList)
        {
            if (child.gameObject.name == skillname)
            {
                child.GetChild(0).gameObject.SetActive(true);
                child.GetChild(1).gameObject.SetActive(false);
            }
        }
    }


    public void SetSideBarSkillInfo(string skillname)
    {
        OrderedDictionary skillDictionary = SkillCollection.GetSkillByName(skillname);

        foreach (Transform child in skillsGameObjectList)
        {
            if (child.gameObject.name == skillname)
            {

                ChangeIcon(skillDictionary["icon"].ToString());
                ChangeFrameColor(skillDictionary["skillRarity"].ToString());
                skillName.GetComponent<TextMeshProUGUI>().text = skillDictionary["name"].ToString();
                skillRarity.GetComponent<TextMeshProUGUI>().text = skillDictionary["skillRarity"].ToString();
                skillDescription.GetComponent<TextMeshProUGUI>().text = skillDictionary["description"].ToString();
                skillQuote.GetComponent<TextMeshProUGUI>().text = "none";
            }
        }
    }

    // public bool HasSkill(string skillname)
    // {
    //     int i = 0;

    //     foreach (Transform skill in skillsGameObjectList)
    //     {
    //         Debug.Log(SkillCollection.skills[i]);
    //         if (skillname == (string)SkillCollection.skills[i]["name"])
    //         {
    //             return true;
    //         }

    //         i++;
    //     }

    //     return false;
    // }

    //Change name to OnSkillClicked
    public void GetSkillClicked()
    {
        string clickedSkillName = EventSystem.current.currentSelectedGameObject.name;
        //if (HasSkill(clickedSkillName)) SetSideBarSkillInfo(clickedSkillName);
    }

    public void ChangeFrameColor(string rarity)
    {
        switch (rarity)
        {
            case "COMMON":
                skillRarityFrame.GetComponent<Image>().sprite = frameColors[0];
                break;
            case "RARE":
                skillRarityFrame.GetComponent<Image>().sprite = frameColors[1];
                break;
            case "EPIC":
                skillRarityFrame.GetComponent<Image>().sprite = frameColors[2];
                break;
            case "LEGENDARY":
                skillRarityFrame.GetComponent<Image>().sprite = frameColors[3];
                break;
        }
    }

    public void ChangeIcon(string iconNumber)
    {
        switch (iconNumber)
        {
            case "1":
                skillIcon.GetComponent<Image>().sprite = frameColors[0];
                break;
        }
    }
}
