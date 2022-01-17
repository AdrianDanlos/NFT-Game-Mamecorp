using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public void DealDamage(Fighter attacker, Fighter defender)
    {
        defender.hp -= attacker.damage;
        Debug.Log(defender.fighterName);
        Debug.Log(defender.hp);
        StartCoroutine(ReceiveDamageAnimation(defender));
    }

    IEnumerator ReceiveDamageAnimation(Fighter defender)
    {
        Renderer defenderRenderer = defender.GetComponent<Renderer>();
        defenderRenderer.material.color = new Color(255, 1, 1);
        yield return new WaitForSeconds(.1f);
        defenderRenderer.material.color = new Color(1, 1, 1);
    }

    public bool IsAttackRepeated(Fighter attacker)
    {
        Probabilities probabilitiesScript = this.GetComponent<Probabilities>();

        return probabilitiesScript.IsHappening(attacker.repeatAttackChance);
    }
}