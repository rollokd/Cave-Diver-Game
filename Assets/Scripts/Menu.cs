using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        DestroyAudio();
    }

    private void DestroyAudio()
    {
        var sources = FindObjectsOfType<AudioSource>();
        if (sources.Length > 1)
            Destroy(sources[0]);
    }

    public void Play()
    {
        Destroy(FindObjectOfType<GameController>());
        SceneManager.LoadScene("Tutorial");
    }
}

