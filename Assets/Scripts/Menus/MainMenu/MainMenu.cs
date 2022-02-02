using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    Fighter player;
    public GameObject playerNameGO;
    public GameObject playerLevelGO;
    public GameObject playerExpGO;
    void Start()
    {
        player = PlayerUtils.FindInactiveFighter();
        player.level = 10;
        player.experiencePoints = 10;
        SetPlayerName();
        SetPlayerLevel();
        SetPlayerExperiencePoints();
        
    }
    private void SetPlayerName()
    {
        playerNameGO.GetComponent<TextMeshProUGUI>().text = player.fighterName;
    }
    private void SetPlayerLevel()
    {
        playerLevelGO.GetComponent<TextMeshProUGUI>().text = player.level.ToString();
    }
    private void SetPlayerExperiencePoints()
    {
        int maxXp = Levels.MaxXpOfCurrentLevel(player.level);
        playerExpGO.GetComponent<TextMeshProUGUI>().text = $"{player.experiencePoints.ToString()}/{maxXp}";
    }
}
