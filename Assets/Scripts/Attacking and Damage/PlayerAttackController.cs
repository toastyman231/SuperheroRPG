using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Attack primary;
    public Attack secondary;

    private void Awake()
    {
        if (primary == null)
        {
            primary = new Attack("Punch", 10, gameObject.GetComponent<Animator>(), "Punch");
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
}
