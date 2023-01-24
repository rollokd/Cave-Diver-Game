using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public bool alive = true;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public bool facingRight = false;

    [SerializeField]
    protected Healthbar healthbar;
    [SerializeField]
    protected int maxHealth;

    public void Hit()
    {
        health--;
        healthbar.SetHealth(health, maxHealth);
        
        if (health <= 0)
            Die();
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    protected void Heal()
    {
        health++;
        if (health > maxHealth)
            health = maxHealth;
        healthbar.SetHealth(health, maxHealth);
    }

    protected virtual void Die()
    {
        Debug.Log("Die");
        alive = false;
        Destroy(gameObject);
    }
}
