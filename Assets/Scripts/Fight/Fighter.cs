using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public float health = 10;
    public float maxHealth = 10;
    public float recoveryPerSecond = 0;
    public float armor = 0;
    //damage public 
    public float attackDamage = 1;
    public float attackSpeed = 1;
    public float attackRange = 10;

    public float PushRecoverySpeed = 0.01f;

    //immunity -so that we can spams hits
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    //all fighters can receive damage/die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            health -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            if (health <= 0)
            {
                health = 0;
                Death();
            }
        }
    }

    protected virtual void Death() { }
}
