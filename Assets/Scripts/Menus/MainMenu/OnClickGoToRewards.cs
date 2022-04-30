using UnityEngine;

public class OnClickGoToRewards : MonoBehaviour
{
    public GameObject dailyRewards;
    DailyGift dailyGift;

    private void Awake()
    {
        dailyGift = GameObject.Find("DailyRewardsCanvas").GetComponent<DailyGift>();
    }

    public void ShowDailyRewards()
    {
        dailyRewards.SetActive(true);
        dailyGift.SaveFirstTime(1);
    }
}
