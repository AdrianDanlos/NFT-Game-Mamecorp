using UnityEngine;

public class OnClickContinueLevelUp : MonoBehaviour
{
    public GameObject RewardStats;
    public GameObject RewardItems;
    public GameObject buttonOpen;

    public void OnClickContinueHandler()
    {
        RewardStats.SetActive(false);
        RewardItems.SetActive(true);
        buttonOpen.SetActive(true);
        gameObject.SetActive(false);
    }
}
