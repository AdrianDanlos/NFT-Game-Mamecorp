using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Fighter player;
    int maxXp;
    public GameObject playerNameGO;
    public GameObject playerLevelGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;
    public GameObject gold;
    public GameObject energy;
    void Start()
    {
        player = PlayerUtils.FindInactiveFighter();
        maxXp = Levels.MaxXpOfCurrentLevel(player.level);
        
        SetName();
        SetLevel();
        SetExperiencePoints();
        SetSlider();
        SetGold();
        SetEnergy();

    }
    private void SetName()
    {
        playerNameGO.GetComponent<TextMeshProUGUI>().text = player.fighterName;
    }
    private void SetLevel()
    {
        playerLevelGO.GetComponent<TextMeshProUGUI>().text = player.level.ToString();
    }
    private void SetExperiencePoints()
    {
        playerExpGO.GetComponent<TextMeshProUGUI>().text = $"{player.experiencePoints.ToString()}/{maxXp}";
    }
    private void SetSlider()
    {
        playerLevelSlider.GetComponent<Slider>().value = (float)player.experiencePoints / (float)maxXp;
    }

    private void SetGold()
    {
        gold.GetComponent<TextMeshProUGUI>().text = User.Instance.gold.ToString();
    }

    private void SetEnergy()
    {
        //FIXME: Set max energy here
        energy.GetComponent<TextMeshProUGUI>().text = $"{User.Instance.energy.ToString()}/{10}";
    }
}
