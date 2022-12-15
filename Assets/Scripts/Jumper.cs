using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Character
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpDelay;

    private float timer = 0;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > jumpDelay)
        {
            timer = 0;
            rb.AddForce(new Vector2(0, jumpForce));
            Debug.Log("Jump");
        }
    }

    public new void Die()
    {
        Debug.Log("Enemy die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            Hit();
    }
}
