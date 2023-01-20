using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private bool isBoss = false;
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private GameObject impactEffect;
    [SerializeField]
    private AudioClip bulletSound;
    [SerializeField]
    private AudioClip impactSound;

    private Rigidbody2D rb;
    private AudioSource sounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        sounds = GetComponent<AudioSource>();
        sounds.PlayOneShot(bulletSound, 1F);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //If boss is shooting
        if (isBoss)
        {
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)
            {
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
            }
        }

        Character character = hitInfo.GetComponent<Character>();
        if (character != null)
        {
            character.Hit();
            Impact();
        }

        if (hitInfo.gameObject.layer == 3 || hitInfo.gameObject.tag == "Chest") //3 is ground
            Impact();
    }

    private void Impact()
    {
        AudioSource.PlayClipAtPoint(impactSound, gameObject.transform.position, 1F);
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
