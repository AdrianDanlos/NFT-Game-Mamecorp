using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightersUIData : MonoBehaviour
{
    public GameObject playerNameGO;
    public GameObject playerEloGO;
    public GameObject playerHealthBarGO;
    public GameObject botEloGO;
    public GameObject botNameGO;
    public GameObject botHealthBarGO;
    private float playerMaxHealth;
    private float botMaxHealth;

    public void SetFightersUIInfo(Fighter player, Fighter bot, int botElo)
    {
        SetFightersElo(botElo);
        SetFightersName(player.fighterName, bot.fighterName);
        SetFightersMaxHealth(player.hp, bot.hp);
    }

    private void SetFightersElo(int botElo)
    {
        playerEloGO.GetComponent<TextMeshProUGUI>().text = User.Instance.elo.ToString();
        botEloGO.GetComponent<TextMeshProUGUI>().text = botElo.ToString();
    }

    private void SetFightersName(string playerName, string botName)
    {
        playerNameGO.GetComponent<TextMeshProUGUI>().text = playerName;
        botNameGO.GetComponent<TextMeshProUGUI>().text = botName;
    }

    public void SetFightersMaxHealth(float playerMaxHealth, float botMaxHealth)
    {
        this.playerMaxHealth = playerMaxHealth;
        this.botMaxHealth = botMaxHealth;
    }

    public void ModifyHealthBar(Fighter fighter, bool isPlayer)
    {
        if(isPlayer){
            SetHealthBarValue(playerHealthBarGO, fighter, playerMaxHealth);
            return;
        }

        SetHealthBarValue(botHealthBarGO, fighter, botMaxHealth);
    }

    private void SetHealthBarValue(GameObject healthBar, Fighter fighter, float maxHealth){
        healthBar.GetComponent<Slider>().value = fighter.hp / maxHealth;
    }
}
