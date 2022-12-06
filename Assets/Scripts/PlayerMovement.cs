using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public float distanceToGround;

    private Rigidbody2D rigidbody;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rigidbody.velocity.y);

        isGrounded = rigidbody.velocity.y < 0.1f;
        Debug.Log(isGrounded);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.01f)
            rigidbody.velocity += Vector2.up * jumpSpeed;
    }
}
