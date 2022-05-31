using UnityEngine;

public class Lightning : MonoBehaviour
{
    public static void StartAnimation(Fighter fighter)
    {
        Animator lightningAnimator = fighter.transform.Find("VFX/Lightning_VFX").GetComponent<Animator>();
        lightningAnimator.Play("lightning_0", -1, 0f);
    }
}