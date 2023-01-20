using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Character
{
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float maxHorizontal;

    private float initialHorizontal;
    private float timer = 0;
    private float prevsin;

    // Start is called before the first frame update
    void Start()
    {
        initialHorizontal = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float sin = Mathf.Sin(timer * horizontalSpeed);
        if ((sin > prevsin && !facingRight) || (sin < prevsin && facingRight))
            Flip();

        float horiz = sin * maxHorizontal + initialHorizontal;
        prevsin = sin;

        transform.position = new Vector2(horiz, transform.position.y);
    }
}
