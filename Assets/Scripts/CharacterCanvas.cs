using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private GameObject speech;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        speech.transform.position = camera.WorldToScreenPoint(transform.parent.position + offset);
    }
}
