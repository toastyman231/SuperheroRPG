using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Attack primary;
    public Attack secondary;
    public Animator playerAnim;
    public GameObject particleSystemSpawnLocation;
    public Camera cam;

    private void Awake()
    {
        if (primary == null)
        {
            primary = new Attack("Punch", 10, playerAnim, "Primary");
            secondary = new Attack("Lightning", 10, playerAnim, "Secondary");
        }
    }

    public void PrimaryAttack()
    {
        primary.DoAttack();
    }

    public void SecondaryAttack()
    {
        if (secondary != null)
        {
            secondary.DoAttack();
        }
    }

    public void ResetAttacks()
    {
        primary.ResetAttack();

        if (secondary != null)
        {
            secondary.ResetAttack();
        }
    }

    public void CreateParticleSystem(ParticleSystem ps)
    {
        Quaternion rot = cam.transform.rotation;
        rot *= Quaternion.Euler(-90, 0, 0);
        Instantiate(ps, particleSystemSpawnLocation.transform.position, rot, GameObject.FindGameObjectWithTag("Player").transform);
        ps.Play();
    }
}
