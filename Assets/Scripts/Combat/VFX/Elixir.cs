using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Elixir : MonoBehaviour
{
    const int BOOST_ELIXIR_HP_VALUE = 8;

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

        // Heal = [BOOST_ELIXIR_HP_VALUE * Level] 
        float hpToRestore = BOOST_ELIXIR_HP_VALUE * fighter.level;
        Debug.Log(hpToRestore + " lv: " + fighter.level);
        fighter.hp += hpToRestore;
        Combat.ShowLifeChangesOnUI(fighter.hp);
        Combat.fightersUIDataScript.ModifyHealthBar(fighter);
        StartCoroutine(StopElixirAnimation(elixir));
    }
}