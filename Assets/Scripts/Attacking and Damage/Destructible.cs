using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable
{
    public LayerMask interactableLayers;
    public int maxHealth = 50;
    private int health;

    public void Awake()
    {
        health = maxHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerAttackController attack = other.gameObject.GetComponentInParent<PlayerAttackController>();

        //Checks if the colliding object is allowed to damage this object
        int objLayerMask = (1 << other.gameObject.layer);
        if (((interactableLayers.value & objLayerMask) > 0)
            && attack != null)
        {
            TakeDamage(attack.primary.GetDamageAmount());
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Destroy(this.gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
}
