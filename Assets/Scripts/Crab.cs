using UnityEngine;

public class Crab : Character
{
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float maxHorizontal;

    private float initialHorizontal;
    private float timer;
    private float prevsin;

    void Start()
    {
        initialHorizontal = transform.position.x;
    }

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
