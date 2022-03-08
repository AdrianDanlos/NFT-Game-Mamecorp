using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public IEnumerator PerformAttack(Fighter attacker, Fighter defender)
    {
        if (Combat.movementScript.FighterShouldAdvanceToAttack(attacker)) yield return StartCoroutine(Combat.movementScript.MoveToMeleeRangeAgain(attacker, defender));

        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.ATTACK);

        if (IsAttackDodged(defender))
        {
            StartCoroutine(Combat.movementScript.DodgeMovement(defender));
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.JUMP);
            yield return new WaitForSeconds(.3f);
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
            //This break exits the PerformAttack function and therefore prevents from calling the dealDamage function.
            yield break;
        }

        DealDamage(attacker, defender);

        Combat.isGameOver = defender.hp <= 0 ? true : false;

        if (Combat.isGameOver)
        {
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.DEATH);
            yield return StartCoroutine(ReceiveDamageAnimation(defender));
        }
        else
        {
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.HURT);
            yield return StartCoroutine(ReceiveDamageAnimation(defender));
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
        }
    }

    private void DealDamage(Fighter attacker, Fighter defender)
    {
        var attackerDamageForNextHit = IsAttackCritical(attacker) ? attacker.damage * 2 : attacker.damage;
        defender.hp -= attackerDamageForNextHit;
        Combat.fightersUIDataScript.ModifyHealthBar(defender, Combat.player == defender);
    }

    IEnumerator ReceiveDamageAnimation(Fighter defender)
    {
        yield return new WaitForSeconds(.1f);
        Renderer defenderRenderer = defender.GetComponent<Renderer>();
        defenderRenderer.material.color = new Color(255, 1, 1);
        yield return new WaitForSeconds(.1f);
        defenderRenderer.material.color = new Color(1, 1, 1);
        //Wait for hurt animation to finish
        yield return new WaitForSeconds(.2f);
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