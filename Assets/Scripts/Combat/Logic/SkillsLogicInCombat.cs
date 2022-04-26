using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsLogicInCombat : MonoBehaviour
{
    private Combat combatScript;
    private Movement movementScript;
    private Attack attackScript;
    private void Awake()
    {
        combatScript = this.GetComponent<Combat>();
        movementScript = this.GetComponent<Movement>();
        attackScript = this.GetComponent<Attack>();
    }
    public IEnumerator AttackWithoutSkills(Fighter attacker, Fighter defender)
    {
        yield return combatScript.MoveForwardHandler(attacker);

        // Attack
        int attackCounter = 0;

        while (!Combat.isGameOver && (attackCounter == 0 || attackScript.IsAttackRepeated(attacker)))
        {
            yield return StartCoroutine(attackScript.PerformAttack(attacker, defender));
            attackCounter++;
        };

        if (!Combat.isGameOver) FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);

        yield return combatScript.MoveBackHandler(attacker);
    }
    public IEnumerator LowBlow(Fighter attacker, Fighter defender)
    {
        combatScript.SetFightersDestinationPositions(0.8f);
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.RUN);
        yield return movementScript.MoveSlide(attacker);
        yield return StartCoroutine(attackScript.PerformLowBlow(attacker, defender));
        yield return combatScript.MoveBackHandler(attacker);
        combatScript.ResetFightersDestinationPosition();
    }

    public IEnumerator JumpStrike(Fighter attacker, Fighter defender)
    {
        combatScript.SetFightersDestinationPositions(1f);
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.RUN);

        yield return movementScript.MoveJumpStrike(attacker);

        float rotationDegrees = attacker == Combat.player ? -35f : 35f;
        movementScript.Rotate(attacker, rotationDegrees);

        int nStrikes = UnityEngine.Random.Range(4, 9); // 4-8 attacks

        for (int i = 0; i < nStrikes && !Combat.isGameOver; i++)
        {
            yield return StartCoroutine(attackScript.PerformJumpStrike(attacker, defender));
        }

        if (!Combat.isGameOver) FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);

        //Go back to the ground
        yield return StartCoroutine(movementScript.Move(attacker, attacker.transform.position, attacker.destinationPosition, 0.1f));
        movementScript.ResetRotation(attacker);

        yield return combatScript.MoveBackHandler(attacker);
        combatScript.ResetFightersDestinationPosition();
    }

    public IEnumerator ShurikenFury(Fighter attacker, Fighter defender)
    {
        int nShurikens = UnityEngine.Random.Range(4, 9); // 4-8 shurikens

        for (int i = 0; i < nShurikens && !Combat.isGameOver; i++)
        {
            yield return StartCoroutine(attackScript.PerformShurikenFury(attacker, defender));
        }

        if (!Combat.isGameOver) FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
    }

    public IEnumerator CosmicKicks(Fighter attacker, Fighter defender)
    {
        combatScript.SetFightersDestinationPositions(1.5f);
        yield return combatScript.MoveForwardHandler(attacker);

        int nKicks = UnityEngine.Random.Range(4, 9); // 4-8 kicks

        for (int i = 0; i < nKicks && !Combat.isGameOver; i++)
        {
            yield return StartCoroutine(attackScript.PerformCosmicKicks(attacker, defender));
        }

        if (!Combat.isGameOver) FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);

        yield return combatScript.MoveBackHandler(attacker);
        combatScript.ResetFightersDestinationPosition();
    }
}