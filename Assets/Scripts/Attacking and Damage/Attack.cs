using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private string attackName { get; set; }
    private int baseDamage = 10;
    private Animator playerAnim { get; set; }
    private string attackTriggerName = "Primary";

    public Attack(string name, int damage, Animator anim, string trigger)
    {
        attackName = name;
        baseDamage = damage;
        playerAnim = anim;
        attackTriggerName = trigger;
    }

    //Override with special attack behaviors
    public void DoAttack()
    {
        playerAnim.SetTrigger(attackTriggerName);
    }

    //All attacks should have an animation event that calls this
    public void ResetAttack()
    {
        playerAnim.ResetTrigger(attackTriggerName);
    }

    //Damage amouts will be more complicated for later, for now this just returns base damage
    public int GetDamageAmount()
    {
        return baseDamage;
    }
}
