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

    // Start is called before the first frame update
    void Start()
    {
        initialHorizontal = transform.position.x;
        initialVertical = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float horiz = Mathf.Sin(timer * horizontalSpeed) * maxHorizontal + initialHorizontal;

        float vert = Mathf.Cos(timer * verticalSpeed) * maxVertical + initialVertical;

        transform.position = new Vector2(horiz, vert);
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
