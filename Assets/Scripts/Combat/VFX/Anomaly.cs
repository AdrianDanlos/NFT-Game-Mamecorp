using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public static void StartAnimation(Fighter fighter)
    {
        Animator anomalyAnimator = fighter.transform.Find("VFX/Anomaly_VFX").GetComponent<Animator>();
        anomalyAnimator.Play("anomaly_0", -1, 0f);
    }
}