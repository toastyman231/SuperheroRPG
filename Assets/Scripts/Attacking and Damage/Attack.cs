using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Attack
{
    public enum Range { MELEE, SHORT, LONG };

    private string attackName { get; set; }
    private int baseDamage = 10;
    private int attackDelay = 500; //milliseconds
    private Animator playerAnim { get; set; }
    private string attackTriggerName = "Primary";
    private Range attackRange { get; set; }
    private GameObject player;

    public Attack(Animator anim, Range range = Range.MELEE, 
        string name = "Attack", int damage = 10, string trigger = "Punch", int delay = 500)
    {
        attackName = name;
        baseDamage = damage;
        playerAnim = anim;
        attackTriggerName = trigger;
        attackRange = range;
        attackDelay = delay;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Override with special attack behaviors
    public void DoAttack()
    {
        playerAnim.SetTrigger(attackTriggerName);
        
        if(attackRange == Range.MELEE)
        {
            RegisterAttack();
        }
    }

    public async void RegisterAttack()
    {
        await Task.Delay(attackDelay);

        Collider[] hits = Physics.OverlapSphere(player.transform.position, 1);

        foreach (Collider hit in hits)
        {
            Vector3 vectorToCollider = (hit.transform.position - player.transform.position);
            vectorToCollider.Normalize();
            //Debug.Log(Vector3.Dot(vectorToCollider, player.transform.forward));
            // 180 degree arc, change 0 to 0.5 for a 90 degree "pie"
            if (Vector3.Dot(vectorToCollider, Camera.main.transform.forward) >= 0.5f)
            {
                //Damage the enemy
                //Debug.Log("Within range");
                IDamageable damageable = hit.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(baseDamage);
                }
            }
        }
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
