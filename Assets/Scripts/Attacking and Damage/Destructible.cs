using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable
{
    public LayerMask interactableLayers;
    public int maxHealth = 50;
    public int health;

    private PlayerAttackController attackController;

    public void Start()
    {
        health = maxHealth;
        attackController = GameObject.FindGameObjectWithTag("Arms").GetComponent<PlayerAttackController>();
    }

    public void OnParticleCollision(GameObject other)
    {
        //Checks if the colliding object is allowed to damage this object
        Debug.Log("Particle collided!");
        if (interactableLayers == (interactableLayers | 1 << other.gameObject.layer) 
            && attackController != null)
        {
            Debug.Log("Damage taken");
            TakeDamage(attackController.GetRecentAttack().GetDamageAmount());
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
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }

    public void OnCollisionEnter(Collision other)
    {
        Rigidbody rb;

        if(other.collider.transform.parent.gameObject.TryGetComponent(out rb))
        {
            Debug.Log(rb.velocity.magnitude);
            if(rb.velocity.magnitude >= 1)
            {
                TakeDamage(1);
            }
        }
    }
}
