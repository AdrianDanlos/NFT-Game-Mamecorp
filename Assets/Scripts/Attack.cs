using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public void DealDamage(Fighter attacker, Fighter defender)
    {
        defender.hp -= attacker.damage;
        Debug.Log(defender.hp);
    }
}