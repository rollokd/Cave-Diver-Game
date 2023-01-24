using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera mainFollowCam;

    [SerializeField]
    private CinemachineVirtualCamera newCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            newCam.Priority = mainFollowCam.Priority + 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            newCam.Priority = mainFollowCam.Priority - 1;
    }
}
