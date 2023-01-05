using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private void Start()
    {
        healthbar.slider.maxValue = maxHealth;
        healthbar.slider.value = health;
    }

    public override void Die()
    {
        Debug.Log("Character die");
        SceneManager.LoadScene("Death Screen");
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
