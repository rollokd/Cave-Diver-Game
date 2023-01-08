using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool alive = true;

    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected Healthbar healthbar;

    public void Hit()
    {
        Debug.Log("Hit");
        health--;
        healthbar.SetHealth(health, maxHealth);
        
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        health++;
        if (health > maxHealth)
            health = maxHealth;
        healthbar.SetHealth(health, maxHealth);
    }

    public virtual void Die()
    {
        Debug.Log("Die");
        alive = false;
        Destroy(gameObject);
    }

    public void SetHealth(int h)
    {
        maxHealth = h;
        health = h;
    }
}
