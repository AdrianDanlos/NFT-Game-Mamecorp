using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Transactions
{
    CHEST,
    ENERGY
}

public class ShopUI : MonoBehaviour
{
    // UI
    int previousTransaction;
    GameObject chestPopUp;
    GameObject buyConfirmation;
    GameObject chestRewards;
    GameObject chestPopUpChest;
    GameObject nextButton;
    GameObject inventoryButton;
    GameObject backToShopButton;

    // shop flow
    string chestButtonPressed;
    ChestLogic chestLogic;

    // test
    [SerializeField] private List<GameObject> frameColors = new List<GameObject>();


    private void Awake()
    {
        chestPopUp = GameObject.Find("Canvas_PopUp_Chest");
        buyConfirmation = GameObject.Find("Canvas_Buy_Confirmation");
        chestRewards = GameObject.Find("Rewards");
        chestPopUpChest = GameObject.Find("PopUp_Chest");
        nextButton = GameObject.Find("Button_Next");
        inventoryButton = GameObject.Find("Button_Inventory");
        backToShopButton = GameObject.Find("Button_BackToShop");
        chestLogic = GameObject.Find("ChestManager").GetComponent<ChestLogic>();

        // hide UI
        chestPopUp.SetActive(false);
        buyConfirmation.SetActive(false);
        chestRewards.SetActive(false);
        chestPopUpChest.SetActive(false);
        nextButton.SetActive(false);
        chestRewards.SetActive(false);
        inventoryButton.SetActive(false);
        backToShopButton.SetActive(false);
    }

    public void ClosePurchase()
    {
        buyConfirmation.SetActive(false);
        chestPopUp.SetActive(false);
        inventoryButton.SetActive(false);
        backToShopButton.SetActive(false);
    }

    public void ConfirmPurchase()
    {
        switch (previousTransaction)
        {
            case (int) Transactions.CHEST:
                HandleChestPopUp();
                break;
            case (int)Transactions.ENERGY:
                HandleEnergyPopUp();
                break;
            default:
                break;
        }
    }

    public void BuyChest()
    {
        // handle which chest was opened to change icon after
        chestButtonPressed = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(chestButtonPressed);
        buyConfirmation.SetActive(true);
    }

    public void HandleChestPopUp()
    {
        buyConfirmation.SetActive(false);

        // handle gems - test 
        // TODO: update userdatabars in real time
        Debug.Log(User.Instance.gems);
        User.Instance.gems -= 30;
        Debug.Log(User.Instance.gems);

        previousTransaction = (int) Transactions.CHEST; 
        chestPopUp.SetActive(true);
        nextButton.SetActive(true);
        chestPopUpChest.SetActive(true);
    }

    public void HandleEnergyPopUp()
    {
        // TODO: energy 
    }

    public void OpenChest()
    {
        nextButton.SetActive(false);
        chestPopUpChest.SetActive(false);
        chestRewards.SetActive(true);
        inventoryButton.SetActive(true);
        backToShopButton.SetActive(true);

        int x = 0;
        int y = 0;
        int z = 0;

        // dice roll test
        for (int i = 0; i < 10000; i++)
        {
            switch (chestLogic.OpenChest(chestButtonPressed))
            {
                case "RARE":
                    x++;
                    break;
                case "EPIC":
                    y++;
                    break;
                case "LEGENDARY":
                    z++;
                    break;
            }
        }

        Debug.Log("rare: " + x + " | epic: " + y + " | legendary: " + z);

        // reward mockup
        switch (chestLogic.OpenChest(chestButtonPressed))
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
}
