using UnityEngine;

public class PopUP : MonoBehaviour
{
    [SerializeField]
    private GameObject popUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            popUp.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            popUp.SetActive(false);
    }
}
