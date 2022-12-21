using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private GameObject waterCollision;
    public new void Die()
    {
        Debug.Log("Character die");
    }

    public void IncreaseMaxHP(int amount)
    {
        maxHealth += amount;
        health += amount;
    }

    public void WalkOnWater()
    {
        waterCollision.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ooga");
        if (collision.gameObject.tag == "Enemy")
            Hit();
    }

}
