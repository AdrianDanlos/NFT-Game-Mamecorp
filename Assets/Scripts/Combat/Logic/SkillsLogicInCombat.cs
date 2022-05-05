using System.Collections;
using System;
using UnityEngine;

public class SkillsLogicInCombat : MonoBehaviour
{
    private Combat combatScript;
    private Movement movementScript;
    private Attack attackScript;
    //TODO: This should be encapsulated on a class whenever we have a class for each skill
    private const float PassiveSkillsModifier = 1.05f;
    private void Awake()
    {
        combatScript = this.GetComponent<Combat>();
        movementScript = this.GetComponent<Movement>();
        attackScript = this.GetComponent<Attack>();
    }
    public IEnumerator AttackWithoutSkills(Fighter attacker, Fighter defender)
    {
        yield return combatScript.MoveForwardHandler(attacker);

        //Counter attack
        if (attackScript.IsCounterAttack(defender)) yield return BasicAttackLogic(defender, attacker);

        // Attack
        yield return BasicAttackLogic(attacker, defender);

        //Reversal attack
        if (attackScript.IsReversalAttack(defender)) yield return BasicAttackLogic(defender, attacker);

        if (!Combat.isGameOver)
        {
            FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
            yield return combatScript.MoveBackHandler(attacker);
        }
    }
    private IEnumerator BasicAttackLogic(Fighter attacker, Fighter defender)
    {
        int attackCounter = 0;

        while (!Combat.isGameOver && (attackCounter == 0 || attackScript.IsBasicAttackRepeated(attacker)))
        {
            yield return StartCoroutine(attackScript.PerformAttack(attacker, defender));
            attackCounter++;
        };
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
    public IEnumerator ExplosiveBomb(Fighter attacker, Fighter defender)
    {
        yield return StartCoroutine(attackScript.PerformExplosiveBomb(attacker, defender));
        if (!Combat.isGameOver) FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
    }
    public IEnumerator ShadowTravel(Fighter attacker, Fighter defender)
    {
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.IDLE_BLINKING);
        //Wait for blinking animation to finish
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1.2f));
        SetOpacityOfFighterAndShadow(attacker, 0.15f);
        yield return combatScript.MoveForwardHandler(attacker);
        SetOpacityOfFighterAndShadow(attacker, 1);
        yield return StartCoroutine(attackScript.PerformAttack(attacker, defender));
        if (!Combat.isGameOver) FighterAnimations.ChangeAnimation(defender, FighterAnimations.AnimationNames.IDLE);
        yield return combatScript.MoveBackHandler(attacker);
    }
    public IEnumerator HealingPotion(Fighter attacker)
    {
        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.IDLE_BLINKING);
        yield return StartCoroutine(attackScript.PerformHealingPotion(attacker));
    }
    private void SetOpacityOfFighterAndShadow(Fighter attacker, float opacity)
    {
        attacker.GetComponent<Renderer>().material.color = GetFighterColorWithCustomOpacity(attacker, opacity);
        GetFighterShadow(attacker).GetComponent<SpriteRenderer>().color = GetFighterShadowColorWithCustomOpacity(attacker, opacity);
    }
    private Color GetFighterShadowColorWithCustomOpacity(Fighter fighter, float opacity)
    {
        GameObject shadow = GetFighterShadow(fighter);
        Color shadowColor = shadow.GetComponent<SpriteRenderer>().color;
        shadowColor.a = opacity;
        return shadowColor;
    }
    private GameObject GetFighterShadow(Fighter fighter)
    {
        return fighter == Combat.player ? GameObject.FindGameObjectWithTag("PlayerShadow") : GameObject.FindGameObjectWithTag("BotShadow");
    }
    private Color GetFighterColorWithCustomOpacity(Fighter fighter, float opacity)
    {
        Color fighterColor = fighter.GetComponent<Renderer>().material.color;
        fighterColor.a = opacity;
        return fighterColor;
    }

    public void BoostStatsBasedOnPassiveSkills(Fighter fighter)
    {
        if (fighter.HasSkill(SkillNames.DangerousStrength)) fighter.damage *= PassiveSkillsModifier;
        if (fighter.HasSkill(SkillNames.Heavyweight)) fighter.hp *= PassiveSkillsModifier;
        if (fighter.HasSkill(SkillNames.Lightning)) fighter.speed *= PassiveSkillsModifier;
        if (fighter.HasSkill(SkillNames.Persistant)) fighter.repeatAttackChance *= PassiveSkillsModifier;
        if (fighter.HasSkill(SkillNames.FelineAgility)) fighter.dodgeChance *= PassiveSkillsModifier;
        if (fighter.HasSkill(SkillNames.CriticalBleeding)) fighter.criticalChance *= PassiveSkillsModifier;
        if (fighter.HasSkill(SkillNames.Reversal)) fighter.reversalChance *= PassiveSkillsModifier;
        if (fighter.HasSkill(SkillNames.CounterAttack)) fighter.counterAttackChance *= PassiveSkillsModifier;
    }
}