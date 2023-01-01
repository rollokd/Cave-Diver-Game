using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUP : MonoBehaviour
{
    public GameObject popUp;

    private void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag == "Player"){
            popUp.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other){
        
        if(other.gameObject.tag == "Player"){
            popUp.SetActive(false);
        }
    }
}
