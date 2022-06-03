using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Boost : MonoBehaviour
{
    float boostDuration = 5f;
    public void OnClickTriggerBoostEffects()
    {
        this.GetComponent<Button>().interactable = false;
        Animator lightningAnimator = Combat.player.transform.Find("VFX/Boost_VFX/Lightning_VFX").GetComponent<Animator>();
        lightningAnimator.Play("lightning_0", -1, 0f);
        StartParticlesAnimation();
        Renderer playerRenderer = Combat.player.GetComponent<Renderer>();
        playerRenderer.material.color = new Color(96 / 255f, 227 / 255f, 227 / 255f);
        Combat.player.damage *= 1.5f;
        StartCoroutine(RemoveBoostEffects(playerRenderer));
    }

    IEnumerator RemoveBoostEffects(Renderer playerRenderer){
        yield return new WaitForSeconds(boostDuration);
        playerRenderer.material.color = new Color(1, 1, 1);
        Combat.player.damage /= 1.5f;
    }

    private void StartParticlesAnimation()
    {
        Combat.player.transform.Find("VFX/Boost_VFX/Particles_VFX").GetComponent<ParticleSystem>().Play();
    }
}