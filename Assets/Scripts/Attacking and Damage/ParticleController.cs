using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float life = 1.5f;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(ResetParent());
    }

    private IEnumerator ResetParent()
    {
        yield return new WaitForSeconds(life);
        ps.transform.SetParent(null);
    }
    /*private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
        ParticlePhysicsExtensions.GetTriggerParticles(GetComponent<ParticleSystem>(), ParticleSystemTriggerEventType.Exit, particles);

        foreach(ParticleSystem.Particle part in particles)
        {
            part.game
        }
    }*/
}
