using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum Transactions
{
    CHEST,
    ENERGY,
    GOLD
}

public class ShopUI : MonoBehaviour
{
    // UI
    GameObject buyConfirmation;
    GameObject chestRewards;
    GameObject inventoryButton;
    GameObject backToShopButton;
    GameObject noCurrencyButton;
    GameObject abortButton;
    GameObject confirmButton;
    TextMeshProUGUI messageText;
    GameObject chestsTab;
    GameObject energyTab;
    GameObject goldTab;
    GameObject chestsGroupdown;
    GameObject energyGroupdown;
    GameObject goldGroupdown;

    // chest on open
    GameObject chestPopUp;
    GameObject chestPopUpChest;
    GameObject nextButton;

    // energy on open
    GameObject energyPopUp;
    GameObject energyNextButton;

    // gold on open
    GameObject goldPopUp;
    GameObject goldNextButton;

    // shop flow
    string chestButtonPressed;
    string energyButtonPressed;
    string goldButtonPressed;
    int gemsValue = 0;
    int previousTransaction;

    // data
    Fighter fighterData;

    // chest colors mockup
    [SerializeField] private List<GameObject> frameColors = new List<GameObject>();


    private void Awake()
    {
        // transaction buttons and UI
        // Chest
        chestPopUp = GameObject.Find("Canvas_PopUp_Chest");
        chestPopUpChest = GameObject.Find("PopUp_Chest");
        nextButton = GameObject.Find("Button_Next");

        // Energy
        energyPopUp = GameObject.Find("Canvas_PopUp_Energy");
        energyNextButton = GameObject.Find("Button_Energy");

        // Gold
        goldPopUp = GameObject.Find("Canvas_PopUp_Gold");
        goldNextButton = GameObject.Find("Button_Next_Gold");

        chestRewards = GameObject.Find("Rewards");
        buyConfirmation = GameObject.Find("Canvas_Buy_Confirmation");
        inventoryButton = GameObject.Find("Button_Inventory");
        backToShopButton = GameObject.Find("Button_BackToShop");
        noCurrencyButton = GameObject.Find("Button_NoCurrency");
        abortButton = GameObject.Find("Button_Abort");
        confirmButton = GameObject.Find("Button_Confirm");
        messageText = GameObject.Find("Message_Text").GetComponent<TextMeshProUGUI>();
        fighterData = GameObject.Find("FighterWrapper").gameObject.transform.GetChild(0).GetComponent<Fighter>();

        // tabs
        chestsTab = GameObject.Find("Button_Chests_Tab");
        energyTab = GameObject.Find("Button_Energy_Tab");
        goldTab = GameObject.Find("Button_Gold_Tab");

        // scrollrects
        chestsGroupdown = GameObject.Find("Group_Down_Chests");
        energyGroupdown = GameObject.Find("Group_Down_Energy");
        goldGroupdown = GameObject.Find("Group_Down_Gold");

        // hide UI on shop enter
        chestPopUp.SetActive(false);
        buyConfirmation.SetActive(false);
        chestRewards.SetActive(false);
        chestPopUpChest.SetActive(false);
        nextButton.SetActive(false);
        chestRewards.SetActive(false);
        inventoryButton.SetActive(false);
        backToShopButton.SetActive(false);
        noCurrencyButton.SetActive(false);
        abortButton.SetActive(false);
        confirmButton.SetActive(false);
        energyGroupdown.SetActive(false);
        goldGroupdown.SetActive(false);

        energyPopUp.SetActive(false);
        energyNextButton.SetActive(false);

        goldPopUp.SetActive(false);
        goldNextButton.SetActive(false);
    }

    private void Start()
    {
        // button pressed from main menu
        ShowTab(ShopTab.GetTab());
    }

    public void ConfirmPurchase()
    {
        switch (previousTransaction)
        {
            case (int)Transactions.CHEST:
                HandleChestPopUp();
                break;
            case (int)Transactions.ENERGY:
                HandleEnergyPopUp();
                break;
            case (int)Transactions.GOLD:
                HandleGoldPopUp();
                break;
        }
    }

    public void ClosePurchase()
    {
        buyConfirmation.SetActive(false);
        noCurrencyButton.SetActive(false);
        chestPopUp.SetActive(false);
        energyPopUp.SetActive(false);
        energyNextButton.SetActive(false);
        goldPopUp.SetActive(false);
        goldNextButton.SetActive(false);
        inventoryButton.SetActive(false);
        backToShopButton.SetActive(false);
    }

    public void BuyChest()
    {
        chestButtonPressed = EventSystem.current.currentSelectedGameObject.name;
        GetChestValueFromType(chestButtonPressed);
        previousTransaction = (int)Transactions.CHEST;

        // handle which chest was opened to change icon after
        if (CurrencyHandler.instance.HasEnoughGems(gemsValue))
        {
            buyConfirmation.SetActive(true);
            abortButton.SetActive(true);
            confirmButton.SetActive(true);
            messageText.text = "Are you sure about buying this item ?";
        }
        else
        {
            buyConfirmation.SetActive(true);
            abortButton.SetActive(false);
            confirmButton.SetActive(false);
            noCurrencyButton.SetActive(true);
            messageText.text = "Not enough gems!";
        }
    }

    public void HandleChestPopUp()
    {
        CurrencyHandler.instance.SubstractGems(gemsValue);
        buyConfirmation.SetActive(false);
        abortButton.SetActive(false);
        confirmButton.SetActive(false);
        chestPopUp.SetActive(true);
        nextButton.SetActive(true);
        chestPopUpChest.SetActive(true);
    }

    public void BuyEnergy()
    {
        energyButtonPressed = EventSystem.current.currentSelectedGameObject.name;
        GetEnergyCostFromType(energyButtonPressed);
        previousTransaction = (int)Transactions.ENERGY;

        if (!CurrencyHandler.instance.HasEnergySurplus(GetEnergyValueFromType(energyButtonPressed)))
        {
            if (CurrencyHandler.instance.HasEnoughGems(gemsValue))
            {
                buyConfirmation.SetActive(true);
                abortButton.SetActive(true);
                confirmButton.SetActive(true);
                messageText.text = "Are you sure about buying this item ?";
            }
            else
            {
                buyConfirmation.SetActive(true);
                abortButton.SetActive(false);
                confirmButton.SetActive(false);
                noCurrencyButton.SetActive(true);
                messageText.text = "Not enough gems!";
            }
        }
        else
        {
            buyConfirmation.SetActive(true);
            abortButton.SetActive(false);
            confirmButton.SetActive(false);
            noCurrencyButton.SetActive(true);
            messageText.text = "Your energy can't be greater than " + PlayerUtils.maxEnergy + "!";
        }
    }

    public void HandleEnergyPopUp()
    {
        CurrencyHandler.instance.SubstractGems(gemsValue);
        CurrencyHandler.instance.AddEnergy(GetEnergyValueFromType(energyButtonPressed));
        buyConfirmation.SetActive(false);
        abortButton.SetActive(false);
        confirmButton.SetActive(false);
        energyPopUp.SetActive(true);
        energyNextButton.SetActive(true);
    }

    public void BuyGold()
    {
        goldButtonPressed = EventSystem.current.currentSelectedGameObject.name;
        GetGoldCostFromType(goldButtonPressed);
        previousTransaction = (int)Transactions.GOLD;

        if (CurrencyHandler.instance.HasEnoughGems(gemsValue))
        {
            buyConfirmation.SetActive(true);
            abortButton.SetActive(true);
            confirmButton.SetActive(true);
            messageText.text = "Are you sure about buying this item ?";
        }
        else
        {
            buyConfirmation.SetActive(true);
            abortButton.SetActive(false);
            confirmButton.SetActive(false);
            noCurrencyButton.SetActive(true);
            messageText.text = "Not enough gems!";
        }
    }

    public void HandleGoldPopUp()
    {
        CurrencyHandler.instance.SubstractGems(gemsValue);
        CurrencyHandler.instance.AddGold(GetGoldValueFromType(goldButtonPressed));
        buyConfirmation.SetActive(false);
        abortButton.SetActive(false);
        confirmButton.SetActive(false);
        goldPopUp.SetActive(true);
        goldNextButton.SetActive(true);
    }

    public void ShowTab(string buttonPressed)
    {
        switch (buttonPressed)
        {
            case "Button_Chests":
                ShowChestsTab();
                HideEnergyTab();
                HideGoldTab();
                break;
            case "Button_Energy":
                ShowEnergyTab();
                HideChestsTab();
                HideGoldTab();
                break;
            case "Button_Gold":
                ShowGoldTab();
                HideChestsTab();
                HideEnergyTab();
                break;
        }
    }

    public void ShowChestsTab()
    {
        chestsTab.transform.Find("Focus").GetComponent<Image>().enabled = true;
        chestsGroupdown.SetActive(true);
    }

    public void ShowEnergyTab()
    {
        energyTab.transform.Find("Focus").GetComponent<Image>().enabled = true;
        energyGroupdown.SetActive(true);
    }

    public void ShowGoldTab()
    {
        goldTab.transform.Find("Focus").GetComponent<Image>().enabled = true;
        goldGroupdown.SetActive(true);
    }

    public void HideChestsTab()
    {
        chestsTab.transform.Find("Focus").GetComponent<Image>().enabled = false;
        chestsGroupdown.SetActive(false);
    }

    public void HideEnergyTab()
    {
        energyTab.transform.Find("Focus").GetComponent<Image>().enabled = false;
        energyGroupdown.SetActive(false);
    }

    public void HideGoldTab()
    {
        goldTab.transform.Find("Focus").GetComponent<Image>().enabled = false;
        goldGroupdown.SetActive(false);
    }

    public void OpenChest()
    {
        nextButton.SetActive(false);
        chestPopUpChest.SetActive(false);
        chestRewards.SetActive(true);
        inventoryButton.SetActive(true);
        backToShopButton.SetActive(true);

        // reward mockup
        switch (ChestManager.OpenChest(chestButtonPressed))
        {
            case "RARE":
                chestRewards.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = frameColors[1].GetComponent<SpriteRenderer>().sprite;
                break;
            case "EPIC":
                chestRewards.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = frameColors[2].GetComponent<SpriteRenderer>().sprite;
                break;
            case "LEGENDARY":
                chestRewards.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = frameColors[3].GetComponent<SpriteRenderer>().sprite;
                break;
        }
    }

    public void GoToInventory()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Inventory.ToString());
    }

    // fighter call from skillcollection
    public void GetFighterSkills()
    {
        Inventory.GetFighterSkillsData(fighterData.skills);
    }

    public void GetChestValueFromType(string chestType)
    {
        gemsValue = Chest.shopChestsValue[(Chest.ShopChestTypes)System.Enum.Parse
            (typeof(Chest.ShopChestTypes), chestType.ToUpper())]["gems"];
    }

    public void GetEnergyCostFromType(string energyBundle)
    {
        gemsValue = Energy.shopEnergyBundlesCost[(Energy.ShopEnergyBundles)System.Enum.Parse
            (typeof(Energy.ShopEnergyBundles), energyBundle.ToUpper())]["gems"];
    }

    public int GetEnergyValueFromType(string energyBundle)
    {
        return Energy.shopEnergyBundlesValue[(Energy.ShopEnergyBundles)System.Enum.Parse
            (typeof(Energy.ShopEnergyBundles), energyBundle.ToUpper())]["energy"];
    }

    public void GetGoldCostFromType(string goldBundle)
    {
        gemsValue = Gold.shopGoldBundlesCost[(Gold.ShopGoldBundles)System.Enum.Parse
            (typeof(Gold.ShopGoldBundles), goldBundle.ToUpper())]["gems"];
    }

    public int GetGoldValueFromType(string goldBundle)
    {
        return Gold.shopGoldBundlesValue[(Gold.ShopGoldBundles)System.Enum.Parse
            (typeof(Gold.ShopGoldBundles), goldBundle.ToUpper())]["gold"];
    }
}
