using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DailyGift : MonoBehaviour
{
    // UI
    GameObject itemsContainer;
    List<GameObject> giftItems = new List<GameObject>();


    // Manager
    MainMenu mainMenu;

    private void Awake()
    {
        itemsContainer = GameObject.Find("Group_Reward");
        mainMenu = GameObject.Find("MainMenuManager").GetComponent<MainMenu>(); // notifications system

        GetDailyItems();

        GiveReward(GetRewardType("day7"));
    }

    /* Day items structure
     * 
     * - 1-6 rewards GO container
     *      - DayX
     *          - VFX
     *          - UI
     *          - UI
     *          - Reward value
     *          - Reward collected UI
     *          - Focus UI (item is collectable)
     *              - GO container
     *              - Text
     * - 7 reward
     */



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

        }


        // TODO if item is collectable collect and move focus to next one and date

        mainMenu.DisableDailyGiftNotification();
    }

    public void ResetWeek()
    {
        // TODO reset gifts and enable first
    }

    public void BackToDailyGifts()
    {
        // reward collected popup
    }

    private Dictionary<string, string> GetRewardType(string day)
    {
        day = day.ToUpper();

        return new Dictionary<string, string>
        {
            { 
              DailyGiftDB.gifts[(DailyGiftDB.Days)System.Enum.Parse(typeof(DailyGiftDB.Days), day)]["reward"],
              DailyGiftDB.gifts[(DailyGiftDB.Days)System.Enum.Parse(typeof(DailyGiftDB.Days), day)]["value"]
            }
        };
    }

    private void GiveReward(Dictionary<string, string> reward)
    {
        if (reward.ContainsKey("gold"))
        {
            Debug.Log("gold");
        }
        if (reward.ContainsKey("gems"))
        {
            Debug.Log("gems");
        }
        if (reward.ContainsKey("chest"))
        {
            Debug.Log("chest");
        }
    }

    public void DisableDailyGiftNotification()
    {
        mainMenu.DisableDailyGiftNotification();
    }

    public void EnableDailyGiftNotification()
    {
        mainMenu.EnableDailyGiftNotification();
    }
}
