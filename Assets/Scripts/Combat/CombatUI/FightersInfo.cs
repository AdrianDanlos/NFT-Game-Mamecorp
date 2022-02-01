using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightersInfo : MonoBehaviour
{
    public GameObject playerEloGo;
    public GameObject botEloGo;
    public GameObject playerName;
    public GameObject botName;

    public void SetFightersInfo(Fighter bot, int botElo)
    {
        SetFightersElo(botElo);
        SetFightersName(bot);
    }

    private void SetFightersElo(int botElo)
    {
        playerEloGo.GetComponent<TextMeshProUGUI>().text = User.Instance.elo.ToString();
        botEloGo.GetComponent<TextMeshProUGUI>().text = botElo.ToString();
    }

    private void SetFightersName(Fighter bot)
    {
        playerName.GetComponent<TextMeshProUGUI>().text = User.Instance.userName.ToString();
        botName.GetComponent<TextMeshProUGUI>().text = bot.fighterName.ToString();
    }
}
