using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System;

public class LevelUp : MonoBehaviour
{
    public GameObject RewardStats;
    public GameObject RewardItems;
    public GameObject buttonShowChest;
    public GameObject buttonOpenChest;
    public GameObject buttonGoToMainMenu;
    public GameObject skillRewardPopUp;
    public GameObject currenciesRewardPopUp;
    public GameObject attackNumber;
    public GameObject healthNumber;
    public GameObject speedNumber;
    public GameObject commonChest;
    public GameObject rareChest;
    public GameObject epicChest;
    public GameObject legendaryChest;
    public GameObject goldText;
    public GameObject gemsText;
    public GameObject skillTitle;
    public GameObject skillType;
    public GameObject skillRarity;
    public GameObject skillDescription;
    public GameObject skillIcon;
    public GameObject commonSkill;
    public GameObject rareSkill;
    public GameObject epicSkill;
    public GameObject legendarySkill;
    private Chest.BattleChestRarities chestRarityAwarded;
    void Start()
    {
        AddListenerToButtons();
        SetDefaultVisibilityOfUIElements();
        MenuUtils.DisplayLevelIcon(Combat.player.level, GameObject.Find("Levels"));
        SetStatRewardValue(attackNumber, "damage");
        SetStatRewardValue(healthNumber, "hp");
        SetStatRewardValue(speedNumber, "speed");
        chestRarityAwarded = ChestManager.GetRandomBattleChestRarity();
    }

    private void SetStatRewardValue(GameObject element, string stat)
    {
        SpeciesNames species = GeneralUtils.StringToSpeciesNamesEnum(Combat.player.species);
        element.GetComponent<TextMeshProUGUI>().text = Species.statsPerLevel[species][stat].ToString();
    }

    private void SetDefaultVisibilityOfUIElements()
    {
        RewardStats.SetActive(true);
        RewardItems.SetActive(false);
        buttonShowChest.SetActive(true);
        buttonOpenChest.SetActive(false);
        skillRewardPopUp.SetActive(false);
        currenciesRewardPopUp.SetActive(false);
        HideChests();
    }

    private void HideChests()
    {
        commonChest.SetActive(false);
        rareChest.SetActive(false);
        epicChest.SetActive(false);
        legendaryChest.SetActive(false);
    }

    private void AddListenerToButtons()
    {
        buttonShowChest.GetComponent<Button>().onClick.AddListener(() => OnClickShowChest());
        buttonOpenChest.GetComponent<Button>().onClick.AddListener(() => OnClickOpenChest());
    }

    private void OnClickShowChest()
    {
        RewardStats.SetActive(false);
        RewardItems.SetActive(true);
        buttonOpenChest.SetActive(true);
        buttonShowChest.SetActive(false);
        ShowChest();
    }
    private void ShowChest()
    {
        commonChest.SetActive(Chest.BattleChestRarities.COMMON == chestRarityAwarded);
        rareChest.SetActive(Chest.BattleChestRarities.RARE == chestRarityAwarded);
        epicChest.SetActive(Chest.BattleChestRarities.EPIC == chestRarityAwarded);
        legendaryChest.SetActive(Chest.BattleChestRarities.LEGENDARY == chestRarityAwarded);
    }
    private void OnClickOpenChest()
    {
        if (PlayerHasAllSkills())
        {
            Debug.Log("User has all skills in the game.");
            CurrenciesPopUpLogic();
            currenciesRewardPopUp.SetActive(true);
            return;
        }

        switch (chestRarityAwarded)
        {
            case Chest.BattleChestRarities.COMMON:
            case Chest.BattleChestRarities.RARE:
                CurrenciesPopUpLogic();
                currenciesRewardPopUp.SetActive(true);
                break;
            case Chest.BattleChestRarities.EPIC:
            case Chest.BattleChestRarities.LEGENDARY:
                SkillPopUpLogic();
                skillRewardPopUp.SetActive(true);
                break;
        }
    }

    private void CurrenciesPopUpLogic()
    {
        int awardedGold = UnityEngine.Random.Range(GetCurrencyAmount("minGold"), GetCurrencyAmount("maxGold"));
        int awardedGems = UnityEngine.Random.Range(GetCurrencyAmount("minGems"), GetCurrencyAmount("maxGems"));

        ShowCurrenciesData(awardedGold, awardedGems);

        //Save data
        User.Instance.gold = awardedGold;
        User.Instance.gems = awardedGems;
    }

    private void SkillPopUpLogic()
    {
        SkillCollection.SkillRarity skillRarityAwarded = GetRandomSkillRarityBasedOnChest();
        Skill skillInstance = GetAwardedSkill(skillRarityAwarded);
        PlayerUtils.FindInactiveFighter().skills.Add(skillInstance);
        PlayerUtils.FindInactiveFighter().skills = PlayerUtils.FindInactiveFighter().skills;
        Notifications.TurnOnNotification();
        Notifications.IncreaseCardsUnseen();

        ShowSkillData(skillInstance);
        ShowSkillIcon(skillInstance);
    }

    private static Func<bool> PlayerHasAllSkills = () => SkillCollection.skills.Count() == Combat.player.skills.Count();

    private void ShowSkillData(Skill skill)
    {
        skillTitle.GetComponent<TextMeshProUGUI>().text = skill.skillName;
        skillType.GetComponent<TextMeshProUGUI>().text = skill.category;
        skillRarity.GetComponent<TextMeshProUGUI>().text = skill.rarity;
        skillDescription.GetComponent<TextMeshProUGUI>().text = skill.description;
    }
    private void ShowCurrenciesData(int awardedGold, int awardedGems)
    {
        goldText.GetComponent<TextMeshProUGUI>().text = awardedGold.ToString();
        gemsText.GetComponent<TextMeshProUGUI>().text = awardedGems.ToString();
    }
    private int GetCurrencyAmount(string minMaxCurrency)
    {
        return Chest.battleChestCurrencyRewards[chestRarityAwarded][minMaxCurrency];
    }
    private bool HasSkillAlready(OrderedDictionary skill)
    {
        Debug.Log(Combat.player);
        Debug.Log(Combat.player.skills);
#pragma warning disable CS0253 // Posible comparación de referencias involuntaria. El lado de la mano derecha necesita conversión
        // if .ToString() is added as warning message says it doesn't return bool
        // and breaks the code
        return Combat.player.skills.Any(playerSkill => playerSkill.skillName == skill["skillName"]);
#pragma warning restore CS0253 // Posible comparación de referencias involuntaria. El lado de la mano derecha necesita conversión
    }
    private Skill GetAwardedSkill(SkillCollection.SkillRarity skillRarityAwarded)
    {
        //List of OrderedDictionaries
        //Filter the ones that are from another rarity and the ones the player already has
        var skills = SkillCollection.skills
            .Where(skill => (string)skill["skillRarity"] == skillRarityAwarded.ToString())
            .Where(skill => !HasSkillAlready(skill))
            .ToList();

        Debug.Log(SkillCollection.skills
            .Where(skill => !HasSkillAlready(skill)).ToList().Count);

        //If player has all skill for the current rarity get skills from a rarity above. 
        //Does not matter that they might not belong to the current chest
        if (!skills.Any())
        {
            Debug.Log("User has all skills for the given rarity.");

            //Cast enum to int
            int skillRarityIndex = (int)skillRarityAwarded;

            //If value for the next index in the enum exists return that rarity. Otherwise return the first value of the enum (COMMON)
            SkillCollection.SkillRarity newRarity = Enum.IsDefined(typeof(Enum), (SkillCollection.SkillRarity)skillRarityIndex++)
            ? (SkillCollection.SkillRarity)skillRarityIndex++
            : (SkillCollection.SkillRarity)0;

            //Recursive call with the new rarity
            GetAwardedSkill(newRarity);
        }

        int skillIndex = UnityEngine.Random.Range(0, skills.Count());

        //OrderedDictionary
        var awardedSkill = skills[skillIndex];

        return new Skill(awardedSkill["name"].ToString(), awardedSkill["description"].ToString(),
                awardedSkill["skillRarity"].ToString(), awardedSkill["category"].ToString(), awardedSkill["icon"].ToString());
    }

    private SkillCollection.SkillRarity GetRandomSkillRarityBasedOnChest()
    {
        Dictionary<SkillCollection.SkillRarity, float> skillRarityProbabilitiesForChest = Chest.battleChestSkillRewardProbability[chestRarityAwarded];

        float diceRoll = UnityEngine.Random.Range(0f, 100);

        foreach (KeyValuePair<SkillCollection.SkillRarity, float> skill in skillRarityProbabilitiesForChest)
        {
            if (skill.Value >= diceRoll)
                return skill.Key;

            diceRoll -= skill.Value;
        }

        Debug.LogError("Error");
        //Fallback
        return SkillCollection.SkillRarity.COMMON;
    }

    private void ShowSkillIcon(Skill skill)
    {
        //ShowFrame
        commonSkill.SetActive(SkillCollection.SkillRarity.COMMON == GeneralUtils.StringToSkillRarityEnum(skill.rarity));
        rareSkill.SetActive(SkillCollection.SkillRarity.RARE == GeneralUtils.StringToSkillRarityEnum(skill.rarity));
        epicSkill.SetActive(SkillCollection.SkillRarity.EPIC == GeneralUtils.StringToSkillRarityEnum(skill.rarity));
        legendarySkill.SetActive(SkillCollection.SkillRarity.LEGENDARY == GeneralUtils.StringToSkillRarityEnum(skill.rarity));

        //Show icon
        Sprite icon = Resources.Load<Sprite>("Icons/IconsSkills/" + skill.icon);
        skillIcon.GetComponent<Image>().sprite = icon;
    }
}
