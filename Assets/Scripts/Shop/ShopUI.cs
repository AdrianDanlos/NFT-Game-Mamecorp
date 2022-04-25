using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        chestPopUp = GameObject.Find("Canvas_PopUp_Chest");
        buyConfirmation = GameObject.Find("Canvas_Buy_Confirmation");

        // hide UI
        chestPopUp.SetActive(false);
        buyConfirmation.SetActive(false);
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
        buyConfirmation.SetActive(true);
    }

    public void HandleChestPopUp()
    {
        previousTransaction = (int) Transactions.CHEST;
        chestPopUp.SetActive(true);
    }

    public void HandleEnergyPopUp()
    {

    }
}
