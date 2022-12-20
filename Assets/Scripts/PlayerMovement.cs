using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public Rigidbody2D rb;
    
    private bool isGrounded;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-5f, 5f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(5f, 5f, 1f);
        }

        animator.SetBool("Sideways", Mathf.Abs(rb.velocity.x) > 0);

        isGrounded = rb.velocity.y < 0.1f;
        Debug.Log(isGrounded);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity += Vector2.up * jumpSpeed;
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }
}
