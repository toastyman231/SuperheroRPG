using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damageAmount);
    public void OnCollisionEnter(Collision other); //For general purpose collisions
    public void OnParticleCollision(GameObject other); //For attacks that use particles
    public void OnDeath();
}
