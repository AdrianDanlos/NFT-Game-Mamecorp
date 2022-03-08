using System.Collections;
using UnityEngine;
using System;
public class Movement : MonoBehaviour
{
    private float runningDurationInSeconds = 0.6f;
    public double dodgeDurationInSeconds = 0.15;

    //FIXME: This value is not correct + Is it possible to get this value automatically from the canvas?
    float screenEdgeX = 7;

    public IEnumerator MoveForward(Fighter fighter, Vector3 target)
    {
        yield return StartCoroutine(Move(fighter, fighter.transform.position, target, runningDurationInSeconds));
    }

    public IEnumerator MoveBack(Fighter fighter, Vector3 target)
    {
        yield return StartCoroutine(Move(fighter, fighter.transform.position, target, runningDurationInSeconds));
    }

    public IEnumerator Move(Fighter fighter, Vector3 startingPosition, Vector3 targetPosition, double duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            fighter.transform.position = Vector3.Lerp(startingPosition, targetPosition, (float)(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator DodgeMovement(Fighter defender)
    {
        //This initial position might be at the back if we are defending or at the front if we are attacking and the fighter got hit by a counter or reversal attack
        Vector2 initialPosition = defender.transform.position;

        Vector2 defenderInitialPosition = initialPosition;
        Vector2 defenderMaxHeightInAirPosition = initialPosition;
        Vector2 defenderLandingPosition = initialPosition;

        const int JumpHeight = 1;

        bool isPlayerDodging = Combat.player == defender;
        int xDistanceOnJump = isPlayerDodging ? -1 : 1;
        int xDistanceOnLand = GetBackwardMovement(isPlayerDodging);

        if (!IsFighterInTheEdgeOfScreen(isPlayerDodging, defender.transform.position.x))
        {
            defenderMaxHeightInAirPosition.x += xDistanceOnJump;
            defenderLandingPosition.x += xDistanceOnLand;
        }
        defenderMaxHeightInAirPosition.y += JumpHeight;

        //Animation to max height
        yield return StartCoroutine(Move(defender, defenderInitialPosition, defenderMaxHeightInAirPosition, dodgeDurationInSeconds));
        //Animation from max height to landing
        yield return StartCoroutine(Move(defender, defenderMaxHeightInAirPosition, defenderLandingPosition, dodgeDurationInSeconds));
    }

    private bool IsFighterInTheEdgeOfScreen(bool isPlayerDodging, float defenderXPosition)
    {
        return isPlayerDodging && defenderXPosition <= -screenEdgeX || !isPlayerDodging && defenderXPosition >= screenEdgeX;
    }

    private bool IsAtMeleeRange()
    {        
        double currentDistanceAwayFromEachOther = ToSingleDecimal(Combat.player.transform.position.x - Combat.bot.transform.position.x);
        return System.Math.Abs(currentDistanceAwayFromEachOther) <= Combat.distanceAwayFromEachotherOnAttack;
    }

    private bool HasSpaceToKeepPushing(bool isPlayerAttacking, float attackerXPosition)
    {
        return isPlayerAttacking && attackerXPosition <= screenEdgeX - Combat.distanceAwayFromEachotherOnAttack || !isPlayerAttacking && attackerXPosition >= -screenEdgeX + Combat.distanceAwayFromEachotherOnAttack;
    }

    public bool FighterShouldAdvanceToAttack(Fighter attacker)
    {
        return !IsAtMeleeRange() && HasSpaceToKeepPushing(Combat.player == attacker, attacker.transform.position.x);
    }

    private int GetBackwardMovement(bool isPlayerDodging)
    {
        return isPlayerDodging ? -2 : 2;
    }

    public IEnumerator MoveToMeleeRangeAgain(Fighter attacker, Fighter defender)
    {
        Vector2 newDestinationPosition = attacker.transform.position;
        newDestinationPosition.x += GetBackwardMovement(Combat.player == defender);

        FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.RUN);
        yield return StartCoroutine(Move(attacker, attacker.transform.position, newDestinationPosition, runningDurationInSeconds * 0.2f));
    }

    private double ToSingleDecimal(double number){
		string numberAsString = number.ToString();
		int startingPositionToTrim = 3;
		
		string trimmedString = numberAsString.Remove(startingPositionToTrim, numberAsString.Length - startingPositionToTrim);
		return Convert.ToDouble(trimmedString);
    }
}