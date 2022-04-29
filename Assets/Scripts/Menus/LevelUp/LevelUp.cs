using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public GameObject RewardStats;
    public GameObject RewardItems;
    public GameObject buttonShowChest;
    public GameObject buttonOpenChest;
    public GameObject buttonGoToMainMenu;
    public GameObject chestRewardPopUp;
    void Start()
    {
        AddListenerToButtons();
        RewardStats.SetActive(true);
        RewardItems.SetActive(false);
        buttonShowChest.SetActive(true);
        buttonOpenChest.SetActive(false);
        chestRewardPopUp.SetActive(false);
    }

    private void AddListenerToButtons() {
        buttonShowChest.GetComponent<Button>().onClick.AddListener(() => OnClickShowChest());
        buttonOpenChest.GetComponent<Button>().onClick.AddListener(() => OnClickOpenChest());
        buttonGoToMainMenu.GetComponent<Button>().onClick.AddListener(() => OnClickGoToMainMenu());
    }

    private void OnClickShowChest(){
        RewardStats.SetActive(false);
        RewardItems.SetActive(true);
        buttonOpenChest.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnClickOpenChest(){
        chestRewardPopUp.SetActive(true);
    }
    private void OnClickGoToMainMenu(){
        chestRewardPopUp.SetActive(false);
    }
}
