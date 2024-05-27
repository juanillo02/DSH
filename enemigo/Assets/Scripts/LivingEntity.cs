using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour, IDamageble
{
    protected bool dead;
    protected float health;
    public float SaludStart;
    public delegate void OnDeath();
    public static event OnDeath onDeathAnother;
    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }
    }
    
    public void Die()
    {
        dead = true;
        Destroy(gameObject);
        if(onDeathAnother != null)
        {
            onDeathAnother();
        }
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        health = SaludStart;
        dead = false;
    }
}