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

    protected override void Die()
    {
        Debug.Log("Character die");

        if (gameController == null || !gameController.bossFight)
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

        if (collision.gameObject.CompareTag("Enemy"))
            Hit();

        if (collision.gameObject.CompareTag("Chest"))
            gameController.Chest();

        if (collision.gameObject.CompareTag("Health"))
        {
            Heal();
            Destroy(collision.gameObject);
        }
    }

}
