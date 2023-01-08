using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        healthbar.slider.maxValue = maxHealth;
        healthbar.slider.value = health;
    }

    public override void Die()
    {
        Debug.Log("Character die");
        if (!gameController.bossFight)
        {
            SceneManager.LoadScene("Death Screen");
        }
        else
        {
            gameController.DieInBoss();
        }
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

        if (collision.gameObject.tag == "Chest")
        {
            gameController.Chest();
        }
    }

}
