using UnityEngine;

public class Aura : MonoBehaviour
{
    public static void StartAnimation(Fighter fighter)
    {
        Animator auraAnimator = fighter.transform.Find("VFX/Aura_VFX").GetComponent<Animator>();
        auraAnimator.Play("aura_0", -1, 0f);
    }
}