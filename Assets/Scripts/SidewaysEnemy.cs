using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysEnemy : Character
{
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float maxHorizontal;
    [SerializeField]
    private float initialHorizontal;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float horiz = Mathf.Sin(timer * horizontalSpeed) * maxHorizontal;
        horiz += initialHorizontal;

        transform.position = new Vector2(horiz, transform.position.y);
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
