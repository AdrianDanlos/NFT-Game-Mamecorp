using System.Collections.Generic;
using UnityEngine;

public class DailyGift : MonoBehaviour
{
    // UI
    GameObject itemsContainer;
    List<GameObject> giftItems = new List<GameObject>();

    private void Awake()
    {
        itemsContainer = GameObject.Find("Group_Reward");

        GetDailyItems();
    }

    private void GetDailyItems()
    {
        for(int i = 1; i <= 7; i++)
        {
            giftItems.Add(GameObject.Find("Day" + i));
        }
    }

    private void CalculateDailyItem()
    {
        // Todo keep track of time
    }

    public void CollectGift(string day)
    {
        if (true)
        {
            // switch(DailyGiftDB.gifts[(DailyGiftDB.days)System.Enum.Parse(typeof(DailyGiftDB.days), day.ToUpper())])
        }


        // TODO if item is collectable collect and move focus to next one and date
    }

    public void ResetWeek()
    {
        // TODO reset gifts and enable first
    }
}
