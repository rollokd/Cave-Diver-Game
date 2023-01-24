using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string sceneToChangeTo = "Menu";
    [SerializeField]
    private float time;

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            if (sceneToChangeTo == "Menu" || sceneToChangeTo == "Tutorial")
                Destroy(FindObjectOfType<GameController>());
            
            SceneManager.LoadScene(sceneToChangeTo);
        }
    }
}
