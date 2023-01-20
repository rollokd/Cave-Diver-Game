using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private bool isBoss = false;
    [SerializeField]
    private float speed = 15f;
    [SerializeField]
    private GameObject rocketExplosion;
    [SerializeField]
    private AudioClip rocketSound;
    [SerializeField]
    private AudioClip impactSound;

    private Rigidbody2D rb;
    private AudioSource sounds;

    void Start()
    {
        sounds = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        sounds.PlayOneShot(rocketSound, 1F);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //If boss is shooting rocket
        if (isBoss)
        {
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)
            {
                player.Hit();
                player.Hit();
                Impact();
            }

            if (hitInfo.gameObject.layer == 3) //3 is ground
                Impact();

            return;
        }

        if (hitInfo.gameObject.tag == "BossInteraction")
        {
            BossInteractionFade bossInteraction = hitInfo.GetComponent<BossInteractionFade>();
            if (bossInteraction != null)
            {
                bossInteraction.Teleport();
                bossInteraction.Hit();
                bossInteraction.Hit();
                Impact();
            }
        }

        Character character = hitInfo.GetComponent<Character>();
        if (character != null)
        {
            character.Hit();
            character.Hit();
            Impact();
        }

        if(hitInfo.gameObject.tag == "RocketDoor")
        {
            Destroy(hitInfo.gameObject);
            Impact();
        }

        if (hitInfo.gameObject.layer == 3 || hitInfo.gameObject.tag == "Chest") // 3 is ground
            Impact();
    }

    private void Impact()
    {
        AudioSource.PlayClipAtPoint(impactSound, gameObject.transform.position, 0.5F);
        Instantiate(rocketExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
