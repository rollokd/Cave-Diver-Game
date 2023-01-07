using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    private bool fade = true;
    [SerializeField]
    private int fadeSpeed;
    [SerializeField]
    private string newScene;

    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (!fade)
            {
                SceneManager.LoadScene(newScene);
                return;
            }

            gameController.Fade(true, fadeSpeed, newScene);
        }
    }
}
