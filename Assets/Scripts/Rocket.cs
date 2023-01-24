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
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private AudioSource sounds;

    void Start()
    {
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

        if (hitInfo.gameObject.CompareTag("BossInteraction"))
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

        if(hitInfo.gameObject.CompareTag("RocketDoor"))
        {
            Destroy(hitInfo.gameObject);
            Impact();
        }

        if (hitInfo.gameObject.layer == 3 || hitInfo.gameObject.CompareTag("Chest")) // 3 is ground
            Impact();
    }

    private void Impact()
    {
        AudioSource.PlayClipAtPoint(impactSound, gameObject.transform.position, 0.5F);
        Instantiate(rocketExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
