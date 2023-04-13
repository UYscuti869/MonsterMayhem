using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDamage : MonoBehaviour
{
    private HitPoint hitPoint;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Blue Enemy") || other.gameObject.CompareTag("Red Enemy") || other.gameObject.CompareTag("Black Enemy"))
        {
            hitPoint = other.GetComponent<HitPoint>();
            hitPoint.EnemyDamageInput(4);
        }
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Blue Boss") || other.gameObject.CompareTag("Red Boss") || other.gameObject.CompareTag("Final Boss"))
        {
            hitPoint = other.GetComponent<HitPoint>();
            hitPoint.BossDamageInput(4);
        }
    }
}
