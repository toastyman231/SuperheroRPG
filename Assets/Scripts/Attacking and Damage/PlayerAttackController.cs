using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Attack primary;
    public Attack secondary;
    public List<Attack> attacks;
    private Attack recentAttack;
    public Animator playerAnim;
    public GameObject particleSystemSpawnLocation;
    public Camera cam;

    private void Awake()
    {
        attacks = new List<Attack>();

        if (primary == null)
        {
            attacks.Add(new Attack(playerAnim, Attack.Range.MELEE, "Punch", 10, "Primary"));
            attacks.Add(new Attack(playerAnim, Attack.Range.SHORT, "Lightning", 2, "Secondary"));
            primary = attacks[0];
            secondary = attacks[1];
        }
    }

    public void PrimaryAttack()
    {
        primary.DoAttack();
        recentAttack = primary;
    }

    public void SecondaryAttack()
    {
        if (secondary != null)
        {
            secondary.DoAttack();
            recentAttack = secondary;
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

    public Attack GetRecentAttack()
    {
        return recentAttack;
    }
}
