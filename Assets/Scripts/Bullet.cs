using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private bool isBoss = false;

    public float speed = 20f;
    private Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log("Bullet hit " + hitInfo.name);
        if (isBoss)
        {
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)
            {
                player.Hit();
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (hitInfo.gameObject.layer == 3) //3 is ground
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            return;
        }

        if (hitInfo.gameObject.tag == "BossInteraction")
        {
            BossInteractionFade bossInteraction = hitInfo.GetComponent<BossInteractionFade>();
            if (bossInteraction != null)
            {
                bossInteraction.Teleport();
                bossInteraction.Hit();
            }
        }

        Crab crab = hitInfo.GetComponent<Crab>();
        if(crab != null){
            crab.Hit();
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        Jumper jumper = hitInfo.GetComponent<Jumper>();
        if(jumper != null){
            jumper.Hit();
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        Octopus octopus = hitInfo.GetComponent<Octopus>();
        if (octopus != null)
        {
            octopus.Hit();
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        Boss boss = hitInfo.GetComponent<Boss>();
        if (boss != null)
        {
            boss.Hit();
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.layer == 3) //3 is ground
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
