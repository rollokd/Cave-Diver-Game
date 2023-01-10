using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string sceneToChangeTo = "Menu";
    [SerializeField]
    private float time;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            if (sceneToChangeTo == "Menu")
                Destroy(FindObjectOfType<GameController>());
            
            SceneManager.LoadScene(sceneToChangeTo);
        }
    }
}
