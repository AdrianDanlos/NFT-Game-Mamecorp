using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public IEnumerator PerformAttack(Fighter attacker, Fighter defender)
    {
        if (Combat.movementScript.FighterShouldAdvanceToAttack(attacker)){
            yield return StartCoroutine(Combat.movementScript.MoveToMeleeRangeAgain(attacker, defender));
        }
        //FIXME: We need to use yield return to wait until anim is finished? In the last game we didnt yield return
        yield return StartCoroutine(FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.ATTACK));
        if (IsAttackDodged(defender))
        {
            Debug.Log("Dodged!");
            StartCoroutine(Combat.movementScript.DodgeMovement(defender));
            yield return StartCoroutine(FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.JUMP));
            yield return StartCoroutine(FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE));
            //This break exits the PerformAttack function and therefore prevents from calling the dealDamage function.
            yield break;
        }
        DealDamage(attacker, defender);
        Combat.isGameOver = defender.hp <= 0 ? true : false;
        if (Combat.isGameOver) StartCoroutine(FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.DEATH));
    }

    private void DealDamage(Fighter attacker, Fighter defender)
    {
        var attackerDamageForNextHit = IsAttackCritical(attacker) ? attacker.damage * 2 : attacker.damage;
        defender.hp -= attackerDamageForNextHit;
        StartCoroutine(ReceiveDamageAnimation(defender));
        Combat.fightersUIDataScript.ModifyHealthBar(defender, Combat.player == defender);
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
        return Probabilities.IsHappening(attacker.repeatAttackChance);
    }

    private bool IsAttackDodged(Fighter defender)
    {
        return Probabilities.IsHappening(defender.dodgeChance);
    }

    private bool IsAttackCritical(Fighter attacker)
    {
        return Probabilities.IsHappening(attacker.criticalChance);
    }
}