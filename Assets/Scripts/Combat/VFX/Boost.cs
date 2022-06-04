using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Boost : MonoBehaviour
{
    float boostDuration = 5f;
    public void OnClickTriggerBoostEffects()
    {
        this.GetComponent<Button>().interactable = false;
        TriggerBoostEffects(Combat.player);
    }

    IEnumerator RemoveBoostEffects(Fighter fighter){
        yield return new WaitForSeconds(boostDuration);
        fighter.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        fighter.damage /= 1.5f;
    }

    private void StartParticlesAnimation(Fighter fighter)
    {
        fighter.transform.Find("VFX/Boost_VFX/Particles_VFX").GetComponent<ParticleSystem>().Play();
    }

    public void TriggerBoostEffects(Fighter fighter)
    {
        Animator lightningAnimator = fighter.transform.Find("VFX/Boost_VFX/Lightning_VFX").GetComponent<Animator>();
        lightningAnimator.Play("lightning_0", -1, 0f);
        StartParticlesAnimation(fighter);
        Renderer fighterRenderer = fighter.GetComponent<Renderer>();
        fighterRenderer.material.color = new Color(155 / 255f, 0 / 255f, 255 / 255f);
        fighter.damage *= 1.5f;
        StartCoroutine(RemoveBoostEffects(fighter));
    }
}