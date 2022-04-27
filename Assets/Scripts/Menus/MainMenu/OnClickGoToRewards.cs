using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickGoToRewards : MonoBehaviour
{
    public GameObject dailyRewards;

    public void ShowDailyRewards()
    {
        dailyRewards.SetActive(true);
    }
}
