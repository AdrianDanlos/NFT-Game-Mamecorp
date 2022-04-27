using System.Collections;
using UnityEngine;
using System;
public class Attack : MonoBehaviour
{
    public GameObject shuriken;
    public GameObject bomb;
    public IEnumerator PerformAttack(Fighter attacker, Fighter defender)
    {
        //FIXME: Is this the solution to the bug?: https://trello.com/c/Hi2aaaoD/284-bug-dodge-shuriken-then-slide
        if (Combat.movementScript.FighterShouldAdvanceToAttack(attacker)) yield return StartCoroutine(Combat.movementScript.MoveToMeleeRangeAgain(attacker, defender));

        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.ATTACK);

        if (IsAttackShielded())
        {
            yield return StartCoroutine(ShieldAttack(defender));
            yield break;
        }

        if (IsAttackDodged(defender))
        {
            yield return DefenderDodgesAttack(defender);
            yield break;
        }

        yield return DefenderReceivesAttack(attacker, defender, attacker.damage, 0.25f, 0.05f);
    }

    IEnumerator ShieldAttack(Fighter defender, float secondsToWaitForAttackAnim = 0.35f)
    {
        FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.JUMP);
        Transform shield = defender.transform.Find("Shield");
        SpriteRenderer shieldSprite = shield.GetComponent<SpriteRenderer>();
        float xShieldDisplacement = Combat.player == defender ? 0.8f : -0.8f;
        Vector3 shieldDisplacement = new Vector3(xShieldDisplacement, -0.7f, 0);

        shield.transform.position = defender.transform.position;
        shield.transform.position += shieldDisplacement;
        shieldSprite.enabled = true;
        yield return new WaitForSeconds(secondsToWaitForAttackAnim);
        shieldSprite.enabled = false;
        FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
    }

    public IEnumerator PerformCosmicKicks(Fighter attacker, Fighter defender)
    {
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.KICK);
        yield return DefenderReceivesAttack(attacker, defender, attacker.damage, 0.1f, 0.05f);
    }
    public IEnumerator PerformLowBlow(Fighter attacker, Fighter defender)
    {
        if (IsAttackShielded())
        {
            yield return StartCoroutine(ShieldAttack(defender, 0.22f));
            yield break;
        }

        if (IsAttackDodged(defender))
        {
            yield return DefenderDodgesAttack(defender);
            yield break;
        }

        yield return DefenderReceivesAttack(attacker, defender, attacker.damage, 0, 0);
    }
    public IEnumerator PerformJumpStrike(Fighter attacker, Fighter defender)
    {
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.AIR_ATTACK);

        if (IsAttackShielded())
        {
            yield return StartCoroutine(ShieldAttack(defender));
            yield break;
        }

        yield return DefenderReceivesAttack(attacker, defender, attacker.damage, 0.15f, 0.05f);
        LifeSteal(attacker, 3);
        Combat.fightersUIDataScript.ModifyHealthBar(attacker, Combat.player == attacker);
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
            StartCoroutine(Combat.movementScript.RotateObjectOverTime(shurikenInstance, new Vector3(0, 0, 3000), 0.5f));
            StartCoroutine(Combat.movementScript.MoveShuriken(shurikenInstance, shurikenStartPos, shurikenEndPos, 0.5f)); //We dont yield here so we can jump mid animation
            yield return new WaitForSeconds(.2f); //Wait for the shuriken to approach before jumping
            yield return DefenderDodgesAttack(defender);
            yield return new WaitForSeconds(.2f); //Wait for the shuriken to be in its final position before destroying it (This could be avoided with colliders)
            Destroy(shurikenInstance);
            yield break;
        }

        StartCoroutine(Combat.movementScript.RotateObjectOverTime(shurikenInstance, new Vector3(0, 0, 2000), 0.35f));
        yield return StartCoroutine(Combat.movementScript.MoveShuriken(shurikenInstance, shurikenStartPos, shurikenEndPos, 0.35f));
        Destroy(shurikenInstance);

        if (IsAttackShielded())
        {
            yield return StartCoroutine(ShieldAttack(defender));
            yield break;
        }

        yield return DefenderReceivesAttack(attacker, defender, attacker.damage, 0.25f, 0);
    }

    public IEnumerator PerformExplosiveBomb(Fighter attacker, Fighter defender)
    {
        Vector3 bombStartPos = attacker.transform.position;

        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.THROW);
        yield return new WaitForSeconds(.1f); //Throw the bomb when the fighter arm is already up

        GameObject bombInstance = Instantiate(bomb, bombStartPos, Quaternion.identity);
        bombInstance.AddComponent(Type.GetType("BombAnimation"));
        bombInstance.GetComponent<BombAnimation>().targetPos = defender.initialPosition;

        if (IsAttackShielded())
        {
            //Cast shield when bomb is mid air
            yield return new WaitForSeconds(.4f);
            yield return StartCoroutine(ShieldAttack(defender));
            yield return new WaitForSeconds(.2f);
            yield break;
        }

        //Wait bomb travel time
        yield return new WaitForSeconds(.6f);

        yield return DefenderReceivesAttack(attacker, defender, attacker.damage, 0.25f, 0);
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

    IEnumerator DefenderReceivesAttack(Fighter attacker, Fighter defender, float damagePerHit, float secondsToWaitForHurtAnim, float secondsUntilHitMarker)
    {
        //TODO: Add VFX to show that the attack missed
        if (defender.HasSkill(SkillNames.BalletShoes))
        {
            defender.removeUsedSkill(SkillNames.BalletShoes);
            //Wait for attack animation to finish
            yield return new WaitForSeconds(.3f);
            yield break;
        }

        DealDamage(attacker, defender, damagePerHit);

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
        }
    }

    private void DealDamage(Fighter attacker, Fighter defender, float damagePerHit)
    {
        if (IsAttackCritical(attacker)) damagePerHit = damagePerHit * 1.5f;

        defender.hp -= damagePerHit;

        if (defender.hp <= 0 && defender.HasSkill(SkillNames.Survival))
        {
            defender.hp = 1;
            defender.removeUsedSkill(SkillNames.Survival);
        }

        Combat.fightersUIDataScript.ModifyHealthBar(defender, Combat.player == defender);
    }

    //Restores x % of missing health
    private void LifeSteal(Fighter attacker, int percentage)
    {
        bool isPlayerAttacking = Combat.player == attacker;
        float maxHp = isPlayerAttacking ? Combat.playerMaxHp : Combat.botMaxHp;
        float hpToRestore = percentage * maxHp / 100;
        float hpAfterLifesteal = attacker.hp + hpToRestore;
        attacker.hp = hpAfterLifesteal > maxHp ? maxHp : hpAfterLifesteal;
    }

    IEnumerator ReceiveDamageAnimation(Fighter defender, float secondsUntilHitMarker)
    {
        yield return new WaitForSeconds(secondsUntilHitMarker);
        Renderer defenderRenderer = defender.GetComponent<Renderer>();
        defenderRenderer.material.color = new Color(255, 1, 1);
        yield return new WaitForSeconds(.08f);
        defenderRenderer.material.color = new Color(1, 1, 1);
    }

    public bool IsAttackShielded()
    {
        int probabilityOfShielding = 10;
        return Probabilities.IsHappening(probabilityOfShielding);
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

    private bool IsReversalAttack(Fighter defender)
    {
        return Probabilities.IsHappening(defender.reversalChance);
    }

    private bool IsCounterAttack(Fighter defender)
    {
        return Probabilities.IsHappening(defender.counterAttackChance);
    }
}