using UnityEngine;
using TMPro;
using System.Collections;
using System;


public class FloatingHp : MonoBehaviour
{
    TextMeshPro floatingHp;
    Animator floatingHpTravelAnimator;

    private void Start()
    {
        floatingHp = this.gameObject.GetComponent<TextMeshPro>();
        floatingHpTravelAnimator = this.gameObject.GetComponent<Animator>();
    }
    public void StartAnimation(float hpChange, Color? color = null)
    {
        //this = FloatingHp script
        floatingHp.enabled = true;
        floatingHp.text = Math.Round(hpChange).ToString();
        floatingHp.color = color ?? Combat.noColor;
        floatingHpTravelAnimator.Play("floating_hp", -1, 0f);
        StartCoroutine(HideFloatingHp());
    }

    IEnumerator HideFloatingHp()
    {
        yield return new WaitForSeconds(.6f);
        floatingHp.enabled = false;

    }
}