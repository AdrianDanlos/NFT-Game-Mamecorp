using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    // objects & components
    GameObject rewardType;
    GameObject rewardQuantity;
    GameObject giftTimer;
    GameObject giftTimerValue;
    Button button;
    Image image;
    public Sprite[] imageList;

    // economy
    string rewardTypeValue;
    Text rewardTypeText;
    Text rewardQuantityText;
    int rewardQuantityValue;

    // timer variables
    float DEFAULT_GIFT_READY_TIMER = 3f;
    float secondsUntilGiftReady;
    bool isTimerActive = false;

    void Awake()
    {
        rewardType = this.transform.Find("GiftRewardType").gameObject;
        rewardQuantity = this.transform.Find("GiftRewardQuantity").gameObject;
        giftTimer = this.transform.Find("GiftTimer").gameObject;
        giftTimerValue = giftTimer.transform.GetChild(1).gameObject; 
        button = this.GetComponent<Button>();
        image = this.GetComponent<Image>();
        image.sprite = imageList[0];
        secondsUntilGiftReady = DEFAULT_GIFT_READY_TIMER;

        rewardTypeText = rewardType.GetComponent<Text>();
        rewardQuantityText = rewardQuantity.GetComponent<Text>();

        button.onClick.AddListener(TaskOnClick);
        button.interactable = false;
        giftTimer.SetActive(false);

        // this would depend on server time on future - timer too
        GenerateCoinReward();
    }

    void Update()
    {
        if (isTimerActive)
        {
            secondsUntilGiftReady -= Time.deltaTime;
            giftTimerValue.GetComponent<Text>().text = ((int)secondsUntilGiftReady).ToString();
        } 
        else
        {
            giftTimer.SetActive(false);
        }

        if (secondsUntilGiftReady <= 0f)
                timerEnded();
    }

    void timerEnded()
    {
        GenerateCoinReward();
        EnableGiftButton();
        button.interactable = true;

        isTimerActive = false;
        secondsUntilGiftReady = DEFAULT_GIFT_READY_TIMER;
    }

    void TaskOnClick()
    {
        Debug.Log(rewardQuantityValue + " " + rewardTypeValue + " collected");
        DisableGiftButton();
        isTimerActive = true;
        giftTimer.SetActive(true);
    }

    void DisableGiftButton()
    {
        giftTimer.SetActive(false);
        button.interactable = false;
        rewardTypeText.text = null;
        rewardQuantityText.text = null;
        rewardTypeValue = null;
        rewardQuantityValue = 0;

        image.sprite = imageList[1];
    }

    void EnableGiftButton()
    {
        image.sprite = imageList[0];
    }

    void GenerateCoinReward()
    {
        button.interactable = true;

        rewardTypeValue = "coin";
        rewardTypeText.text = rewardTypeValue;

        rewardQuantityValue = GenerateRandomReward("coin");
        rewardQuantityText.text = rewardQuantityValue.ToString();
    }

    int GenerateRandomReward(string currencyType)
    {
        int quantity;
        
        switch(currencyType)
        {
            case "coin":
                quantity = GenerateCoinRewardQuantity();
                break;
            default:
                Debug.LogWarning("Currency not found!");
                quantity = 0; // error?
                break;
        }

        return quantity;
    }

    int GenerateCoinRewardQuantity()
    {
        int minRange = 2;
        int maxRange = 5;
        int increment = 10;
        return (Random.Range(minRange, maxRange) + 1) * increment;
    }

}
