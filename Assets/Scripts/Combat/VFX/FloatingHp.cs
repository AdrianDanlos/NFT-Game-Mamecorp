using UnityEngine;
using TMPro;
using System.Collections;
using System;


public class FloatingHp : MonoBehaviour
{
    TextMeshPro floatingHp;
    Animator floatingHpTravelAnimator;

    private void Awake()
    {
        //this = FloatingHp script
        floatingHp = this.gameObject.GetComponent<TextMeshPro>();
        floatingHpTravelAnimator = this.gameObject.GetComponent<Animator>();

        //Make each floating text appear in front of the previous one
        int otherPrefabsCount = GameObject.FindGameObjectsWithTag("FloatingHp").Length;
        floatingHp.sortingOrder = Combat.floatingHpInstancesCount;

        Combat.floatingHpInstancesCount++;
    }
    public void StartAnimation(float hpChange, Color? color = null)
    {
        floatingHp.text = Math.Round(hpChange).ToString();
        floatingHp.color = color ?? GlobalConstants.noColor;
        floatingHpTravelAnimator.Play("floating_hp", -1, 0f);
        StartCoroutine(DestroyFloatingText());
    }

    IEnumerator DestroyFloatingText()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(this.gameObject);
    }
}