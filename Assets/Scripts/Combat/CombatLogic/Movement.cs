using System.Collections;
using UnityEngine;
public class Movement : MonoBehaviour
{
    private float runningDurationInSeconds = 1f;
    public double dodgeDurationInSeconds = 0.15;
    
    //FIXME: This value is not correct + Is it possible to get this value automatically from the canvas?
    float screenEdgeX = 22;

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
        //FIXME: instead of declaring isPlayerDodging use the utility function isPlayer 
        bool isPlayerDodging = Combat.player == defender;
        int xDistanceOnJump = isPlayerDodging ? -1 : 1;
        int xDistanceOnLand = isPlayerDodging ? -2 : 2;

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
}