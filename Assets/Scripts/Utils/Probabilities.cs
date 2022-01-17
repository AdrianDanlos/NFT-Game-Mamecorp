using UnityEngine;

public class Probabilities : MonoBehaviour
{
    public bool IsHappening(int probabilityInPercentage)
    {
        int randomNumber = Random.Range(0, 100) + 1;
        return randomNumber <= probabilityInPercentage ? true : false;
    }
}

