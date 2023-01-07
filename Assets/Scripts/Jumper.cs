using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Character
{
    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private float maxVertical;

    private float initialVertical;
    private float timer = 0;

    private void Start()
    {
        initialVertical = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        float cos = Mathf.Cos(timer * verticalSpeed);

        if (cos >= 0)
        {
            float vert = cos * maxVertical + initialVertical;
            transform.position = new Vector2(transform.position.x, vert);
        }
    }

    public new void Die()
    {
        Debug.Log("Enemy die");
        Destroy(gameObject);
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Bullet")
    //         Hit();
    // }
}
