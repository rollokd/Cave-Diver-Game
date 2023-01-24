using UnityEngine;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private GameObject speech;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        speech.transform.position = mainCamera.WorldToScreenPoint(transform.parent.position + offset);
    }
}
