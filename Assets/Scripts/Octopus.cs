using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : Character
{
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float maxHorizontal;
    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private float maxVertical;

    private float initialHorizontal;
    private float initialVertical;
    private float timer = 0;
    private float prevsin;

    void Start()
    {
        initialHorizontal = transform.position.x;
        initialVertical = transform.position.y;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float sin = Mathf.Sin(timer * horizontalSpeed);
        if ((sin > prevsin && !facingRight) || (sin < prevsin && facingRight))
            Flip();

        float horiz = sin * maxHorizontal + initialHorizontal;
        prevsin = sin;

        float vert = Mathf.Cos(timer * verticalSpeed) * maxVertical + initialVertical;

        transform.position = new Vector2(horiz, vert);
    }
}
