using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Debug.Log(hitInfo.name);
        Crab crab = hitInfo.GetComponent<Crab>();
        if(crab != null){
            crab.Die();
        }

        Jumper jumper = hitInfo.GetComponent<Jumper>();
        if(jumper != null){
            jumper.Die();
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
