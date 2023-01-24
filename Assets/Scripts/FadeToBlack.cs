using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    private bool fade = true;
    [SerializeField]
    private int fadeSpeed;
    [SerializeField]
    private string newScene;
    [SerializeField]
    private bool isBossBattle;

    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!fade)
            {
                if (isBossBattle)
                {
                    gameController.StartBossFight();
                    return;
                }
                SceneManager.LoadScene(newScene);
                return;
            }

            gameController.Fade(true, fadeSpeed, newScene);
        }
    }
}
