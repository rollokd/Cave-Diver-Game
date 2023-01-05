using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public new void Die()
    {
        Debug.Log("Character die");
    }

    public void IncreaseHP()
    {
        healthMultiplier = 1.2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player hit something");
        if (collision.gameObject.tag == "Enemy")
            Hit();

        if (collision.gameObject.tag == "Health")
        {
            Heal();
            Destroy(collision.gameObject);
        }
    }

}
