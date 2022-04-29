using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject RewardStats;
    public GameObject RewardItems;
    public GameObject buttonContinue;
    public GameObject buttonOpen;
    void Start()
    {
        RewardStats.SetActive(true);
        RewardItems.SetActive(false);
        buttonContinue.SetActive(true);
        buttonOpen.SetActive(false);
    }
}
