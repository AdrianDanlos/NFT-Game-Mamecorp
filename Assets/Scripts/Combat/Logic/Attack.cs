using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public GameObject shuriken;
    public IEnumerator PerformAttack(Fighter attacker, Fighter defender)
    {
        if (Combat.movementScript.FighterShouldAdvanceToAttack(attacker)) yield return StartCoroutine(Combat.movementScript.MoveToMeleeRangeAgain(attacker, defender));

        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.ATTACK);

        if (IsAttackDodged(defender))
        {
            yield return DefenderDodgesAttack(defender);
            yield break;
        }

        yield return DefenderReceivesAttack(attacker, defender, 0.25f, 0.05f);
    }

    public IEnumerator PerformCosmicKicks(Fighter attacker, Fighter defender)
    {
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.KICK);
        yield return DefenderReceivesAttack(attacker, defender, 0.1f, 0.05f);
    }
    public IEnumerator PerformLowBlow(Fighter attacker, Fighter defender)
    {
        if (IsAttackDodged(defender))
        {
            yield return DefenderDodgesAttack(defender);
            yield break;
        }

        yield return DefenderReceivesAttack(attacker, defender, 0.3f, 0);
    }
    public IEnumerator PerformShurikenFury(Fighter attacker, Fighter defender)
    {
        bool dodged = IsAttackDodged(defender);

        Vector3 shurikenStartPos = attacker.transform.position;
        Vector3 shurikenEndPos = defender.transform.position;
        shurikenStartPos.y -= 0.7f;
        shurikenEndPos.y -= 0.7f;
        shurikenEndPos.x = GetShurikenEndPositionX(dodged, attacker, shurikenEndPos);

        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.THROW);
        yield return new WaitForSeconds(.1f); //Throw the shuriken when the fighter arm is already up

        GameObject shurikenInstance = Instantiate(shuriken, shurikenStartPos, Quaternion.identity);

        if (dodged)
        {
            StartCoroutine(Combat.movementScript.RotateObject(shurikenInstance, new Vector3(0, 0, 3000), 0.5f));
            StartCoroutine(Combat.movementScript.MoveShuriken(shurikenInstance, shurikenStartPos, shurikenEndPos, 0.5f)); //We dont yield here so we can jump mid animation
            yield return new WaitForSeconds(.2f); //Wait for the shuriken to approach before jumping
            yield return DefenderDodgesAttack(defender);
            yield return new WaitForSeconds(.2f); //Wait for the shuriken to be in its final position before destroying it (This could be avoided with colliders)
            Destroy(shurikenInstance);
            yield break;
        }

        StartCoroutine(Combat.movementScript.RotateObject(shurikenInstance, new Vector3(0, 0, 2000), 0.35f));
        yield return StartCoroutine(Combat.movementScript.MoveShuriken(shurikenInstance, shurikenStartPos, shurikenEndPos, 0.35f));
        Destroy(shurikenInstance);
        yield return DefenderReceivesAttack(attacker, defender, 0.25f, 0);
    }

    private float GetShurikenEndPositionX(bool dodged, Fighter attacker, Vector3 shurikenEndPos)
    {
        if (dodged) return Combat.player == attacker ? shurikenEndPos.x + 10 : shurikenEndPos.x - 10;
        return Combat.player == attacker ? shurikenEndPos.x - 1f : shurikenEndPos.x + 1f; //To move the hitbox a bit upfront
    }


    IEnumerator DefenderDodgesAttack(Fighter defender)
    {
        StartCoroutine(Combat.movementScript.DodgeMovement(defender));
        FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.JUMP);
        yield return new WaitForSeconds(.3f);
        FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
    }

    IEnumerator DefenderReceivesAttack(Fighter attacker, Fighter defender, float secondsToWaitForHurtAnim, float secondsUntilHitMarker)
    {
        DealDamage(attacker, defender);

        Combat.isGameOver = defender.hp <= 0 ? true : false;

        if (Combat.isGameOver)
        {
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.DEATH);
            yield return StartCoroutine(ReceiveDamageAnimation(defender, secondsUntilHitMarker));
            yield return new WaitForSeconds(.15f); //Wait for attack animation to finish
        }
        else
        {
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.HURT);
            yield return StartCoroutine(ReceiveDamageAnimation(defender, secondsUntilHitMarker));
            yield return new WaitForSeconds(secondsToWaitForHurtAnim);
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
        }
    }

    private void DealDamage(Fighter attacker, Fighter defender)
    {
        var attackerDamageForNextHit = IsAttackCritical(attacker) ? attacker.damage * 2 : attacker.damage;
        defender.hp -= attackerDamageForNextHit;
        Combat.fightersUIDataScript.ModifyHealthBar(defender, Combat.player == defender);
    }

    IEnumerator ReceiveDamageAnimation(Fighter defender, float secondsUntilHitMarker)
    {
        yield return new WaitForSeconds(secondsUntilHitMarker);
        Renderer defenderRenderer = defender.GetComponent<Renderer>();
        defenderRenderer.material.color = new Color(255, 1, 1);
        yield return new WaitForSeconds(.08f);
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