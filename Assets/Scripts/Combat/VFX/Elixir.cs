using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Elixir : MonoBehaviour
{
    public void OnClickTriggerElixirEffects()
    {
        this.GetComponent<Button>().interactable = false;
        TriggerElixirEffects(Combat.player);
    }

    IEnumerator StopElixirAnimation(Transform elixir)
    {
        yield return new WaitForSeconds(1.5f);
        elixir.GetComponent<Animator>().enabled = false;
        elixir.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TriggerElixirEffects(Fighter fighter)
    {
        Transform elixir = fighter.transform.Find("VFX/Elixir_VFX");
        elixir.GetComponent<Animator>().Play("elixir_0", -1, 0f);

        float missingHp = fighter == Combat.player ? Combat.playerMaxHp - fighter.hp : Combat.botMaxHp - fighter.hp;
        //Heals for 50% of the missing hp
        fighter.hp += missingHp * 0.5f;
        Combat.fightersUIDataScript.ModifyHealthBar(fighter);
        StartCoroutine(StopElixirAnimation(elixir));
    }
}