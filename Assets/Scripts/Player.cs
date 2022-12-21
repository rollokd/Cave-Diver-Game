using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public new void Die()
    {
        Debug.Log("Character die");
    }

    public void increaseMaxHP(int amount)
    {
        maxHealth += amount;
        health += amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ooga");
        if (collision.gameObject.tag == "Enemy")
            Hit();
    }

}
