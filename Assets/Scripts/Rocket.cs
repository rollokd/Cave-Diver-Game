using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private bool isBoss = false;

    public float speed = 15f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Destroy(gameObject);
        Debug.Log("Bullet hit " + hitInfo.name);
        // if (isBoss)
        // {
        //     Player player = hitInfo.GetComponent<Player>();
        //     if (player != null)
        //     {
        //         player.Hit();
        //         // Instantiate(impactEffect, transform.position, transform.rotation);
        //         Destroy(gameObject);
        //     }

        //     if (hitInfo.gameObject.layer == 3) //3 is ground
        //     {
        //         // Instantiate(impactEffect, transform.position, transform.rotation);
        //         Destroy(gameObject);
        //     }

        //     return;
        // }

        // if (hitInfo.gameObject.tag == "BossInteraction")
        // {
        //     BossInteractionFade bossInteraction = hitInfo.GetComponent<BossInteractionFade>();
        //     if (bossInteraction != null)
        //     {
        //         bossInteraction.Teleport();
        //         bossInteraction.Hit();
        //     }
        // }

        // Character character = hitInfo.GetComponent<Character>();
        // if (character != null)
        // {
        //     character.Hit();
        //     // Instantiate(impactEffect, transform.position, transform.rotation);
        //     Destroy(gameObject);
        // }

        // if (hitInfo.gameObject.layer == 3 || hitInfo.gameObject.tag == "Chest") //3 is ground
        // {
        //     // Instantiate(impactEffect, transform.position, transform.rotation);
        //     Destroy(gameObject);
        // }
    }
}
