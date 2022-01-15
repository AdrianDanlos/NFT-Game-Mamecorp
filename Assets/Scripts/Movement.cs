using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    private float movementDurationInSeconds = 2f;

    public IEnumerator MoveForward(GameObject fighter, Vector3 target)
    {
        Vector3 startingPos = fighter.transform.position;

        float elapsedTime = 0;

        while (elapsedTime < movementDurationInSeconds)
        {
            fighter.transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / movementDurationInSeconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}