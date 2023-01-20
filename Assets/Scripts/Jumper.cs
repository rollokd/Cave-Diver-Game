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
}
