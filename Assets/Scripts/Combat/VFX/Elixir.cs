using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Elixir : MonoBehaviour
{
    public void OnClickTriggerElixirEffects()
    {
        Transform elixir = Combat.player.transform.Find("VFX/Elixir_VFX");
        this.GetComponent<Button>().interactable = false;
        elixir.GetComponent<Animator>().Play("elixir_0", -1, 0f);
        float missingHp = Combat.playerMaxHp - Combat.player.hp;
        //Heals for 50% of the missing hp
        Combat.player.hp += missingHp * 0.5f;
        Combat.fightersUIDataScript.ModifyHealthBar(Combat.player, true);
        StartCoroutine(StopElixirAnimation(elixir));
    }

    IEnumerator StopElixirAnimation(Transform elixir)
    {
        yield return new WaitForSeconds(1.5f);
        elixir.GetComponent<Animator>().enabled = false;
        elixir.GetComponent<SpriteRenderer>().enabled = false;
    }
}