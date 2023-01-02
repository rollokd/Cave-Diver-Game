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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        Debug.Log("Hit");
        health--;
        
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        alive = false;
    }

    public void SetHealth(int n)
    {
        maxHealth = n;
        health = n;
    }
}
