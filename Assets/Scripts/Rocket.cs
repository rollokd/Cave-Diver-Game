using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private bool isBoss = false;

    public float speed = 15f;
    private Rigidbody2D rb;
    public GameObject rocketExplosion;
    private AudioSource sounds;
    public AudioClip rocketSound;
    public AudioClip impactSound;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        sounds.PlayOneShot(rocketSound, 1F);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        // Destroy(gameObject);
        Debug.Log("Bullet hit " + hitInfo.name);
        if (isBoss)
        {
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)
            {
                player.Hit();
                player.Hit();
                // Instantiate(rocketExplosion, transform.position, transform.rotation);
                // Destroy(gameObject);
                Impact();
            }

            if (hitInfo.gameObject.layer == 3) //3 is ground
            {
                // Instantiate(rocketExplosion, transform.position, transform.rotation);
                // Destroy(gameObject);
                Impact();
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
                bossInteraction.Hit();
                Impact();
            }
        }

        Character character = hitInfo.GetComponent<Character>();
        if (character != null)
        {
            character.Hit();
            character.Hit();
            // Instantiate(rocketExplosion, transform.position, transform.rotation);
            // Destroy(gameObject);
            Impact();
        }

        if(hitInfo.gameObject.tag == "RocketDoor"){
            // Instantiate(rocketExplosion, transform.position, transform.rotation);
            Destroy(hitInfo.gameObject);
            // Destroy(gameObject);
            Impact();
        }

        if (hitInfo.gameObject.layer == 3 || hitInfo.gameObject.tag == "Chest") //3 is ground
        {
            // Instantiate(rocketExplosion, transform.position, transform.rotation);
            // Destroy(gameObject);
            Impact();
        }
    }

    void Impact(){
        AudioSource.PlayClipAtPoint(impactSound, gameObject.transform.position, 0.5F);
        Instantiate(rocketExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
