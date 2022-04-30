using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    public GameObject RewardStats;
    public GameObject RewardItems;
    public GameObject buttonShowChest;
    public GameObject buttonOpenChest;
    public GameObject buttonGoToMainMenu;
    public GameObject chestRewardPopUp;
    public GameObject attackNumber;
    public GameObject healthNumber;
    public GameObject speedNumber;
    public GameObject commonChest;
    public GameObject rareChest;
    public GameObject epicChest;
    public GameObject legendaryChest;
    void Start()
    {
        AddListenerToButtons();
        SetDefaultVisibilityOfUIElements();
        MenuUtils.DisplayLevelIcon(Combat.player.level, GameObject.Find("Levels"));
        SetStatRewardValue(attackNumber, "damage");
        SetStatRewardValue(healthNumber, "hp");
        SetStatRewardValue(speedNumber, "speed");
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
        chestRewardPopUp.SetActive(false);
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
        buttonGoToMainMenu.GetComponent<Button>().onClick.AddListener(() => OnClickGoToMainMenu());
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
        switch (ChestManager.GetRandomBattleChest())
        {
            case Chest.BattleChestRarities.COMMON:
                commonChest.SetActive(true);
                break;
            case Chest.BattleChestRarities.RARE:
                rareChest.SetActive(true);
                break;
            case Chest.BattleChestRarities.EPIC:
                epicChest.SetActive(true);
                break;
            case Chest.BattleChestRarities.LEGENDARY:
                legendaryChest.SetActive(true);
                break;
        }
    }
    private void OnClickOpenChest()
    {
        chestRewardPopUp.SetActive(true);
    }
    private void OnClickGoToMainMenu()
    {
        chestRewardPopUp.SetActive(false);
    }
}
