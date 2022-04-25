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

    // shop flow
    string chestButtonPressed;


    private void Awake()
    {
        chestPopUp = GameObject.Find("Canvas_PopUp_Chest");
        buyConfirmation = GameObject.Find("Canvas_Buy_Confirmation");
        chestRewards = GameObject.Find("Rewards");
        chestPopUpChest = GameObject.Find("PopUp_Chest");
        nextButton = GameObject.Find("Button_Next");

        // hide UI
        chestPopUp.SetActive(false);
        buyConfirmation.SetActive(false);
        chestRewards.SetActive(false);
    }

    public void AbortPurchase()
    {
        buyConfirmation.SetActive(false);
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
        // handle economy
        previousTransaction = (int) Transactions.CHEST; 
        chestPopUp.SetActive(true);
    }

    public void HandleEnergyPopUp()
    {
        // TODO: energy 
    }

    public void OpenChest()
    {
        chestPopUpChest.SetActive(false);
        nextButton.SetActive(false);
    }
}
