using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            Health wizardHealth = other.GetComponent<Health>();
            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }
        }
    }
}