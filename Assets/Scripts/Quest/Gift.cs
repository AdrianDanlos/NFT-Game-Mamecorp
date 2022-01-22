using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    GameObject rewardType;
    GameObject rewardQuantity;
    Button button;
    Image image;
    public Sprite[] imageList;

    void Start()
    {
        rewardType = this.transform.GetChild(0).gameObject;
        rewardQuantity = this.transform.GetChild(1).gameObject;
        button = this.GetComponent<Button>();
        image = this.GetComponent<Image>();
        image.sprite = imageList[0];

        button.onClick.AddListener(TaskOnClick);

        rewardType.GetComponent<Text>().text = "Coin";
        rewardQuantity.GetComponent<Text>().text = GenerateRandomReward("coin").ToString();
    }

    void TaskOnClick()
    {
        button.interactable = false;
        rewardType.GetComponent<Text>().text = null;
        rewardQuantity.GetComponent<Text>().text = null;
        image.sprite = imageList[1];
        Debug.Log("Yoink!");
    }

    int GenerateRandomReward(string currencyType)
    {
        int quantity;
        
        switch(currencyType)
        {
            case "coin":
                quantity = GenerateCoinReward();
                break;
            default:
                quantity = 0; // error?
                break;
        }

        return quantity;
    }

    int GenerateCoinReward()
    {
        int minRange = 2;
        int maxRange = 5;
        int increment = 10;
        return (Random.Range(minRange, maxRange) + 1) * increment;
    }

}
