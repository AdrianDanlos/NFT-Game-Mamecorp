using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    GameObject rewardType;
    GameObject rewardQuantity;
    string rewardTypeValue;
    int rewardQuantityValue;
    bool isTimerActive = false;
    Button button;
    Image image;
    public Sprite[] imageList;

    float DEFAULT_GIFT_READY_TIMER = 3f;
    float secondsUntilGiftReady;

    void Awake()
    {
        rewardType = this.transform.GetChild(0).gameObject;
        rewardQuantity = this.transform.GetChild(1).gameObject;
        button = this.GetComponent<Button>();
        image = this.GetComponent<Image>();
        image.sprite = imageList[0];
        secondsUntilGiftReady = DEFAULT_GIFT_READY_TIMER;

        button.onClick.AddListener(TaskOnClick);
        button.interactable = false;

        // this would depend on server time on future
        GenerateCoinReward();
    }

        void Update()
        {
            //need to add code here similar to energy system
            //- create clock class?
            //- runtime based timer atm -> server based timer in the future

            // test 
            Debug.Log(secondsUntilGiftReady);

            if(isTimerActive)
                secondsUntilGiftReady -= Time.deltaTime;

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
    }

    void DisableGiftButton()
    {
        button.interactable = false;
        rewardType.GetComponent<Text>().text = null;
        rewardQuantity.GetComponent<Text>().text = null;
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
        rewardType.GetComponent<Text>().text = rewardTypeValue;

        rewardQuantityValue = GenerateRandomReward("coin");
        rewardQuantity.GetComponent<Text>().text = rewardQuantityValue.ToString();
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
