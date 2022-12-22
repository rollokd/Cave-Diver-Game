using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float maxFuel;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float fuelCost;
    [SerializeField]
    private float fuelRegen;
    [SerializeField]
    private int maxJumps = 1;
    [SerializeField]
    private bool jetPack = false;

    private float fuel;
    private bool isGrounded;
    private Animator animator;
    private int jumps;
    private bool canUseJetPack = true;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fuel = maxFuel;
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

        isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask);

        if (isGrounded)
            jumps = 0;

        if (fuel > 20)
            canUseJetPack = true;

        if (Input.GetButtonDown("Jump") && (isGrounded || jumps < maxJumps))
        {
            rb.velocity += Vector2.up * jumpSpeed;
            jumps++;
            animator.SetBool("Jump", true);
        }
        else if (Input.GetButton("Jump") && jetPack && (fuel - (fuelCost * Time.deltaTime) > 0) && rb.velocity.y < -0.01f && canUseJetPack)
        {
            Debug.Log("jetapck");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            fuel -= fuelCost * Time.deltaTime;
            animator.SetBool("Jump", false);

            if (fuel < 0.01f)
                canUseJetPack = false;
        }
        else
        {
            animator.SetBool("Jump", false);
            if (fuel + fuelRegen * Time.deltaTime < maxFuel)
                fuel += fuelRegen * Time.deltaTime;
        }
    }

    public void AddJump()
    {
        maxJumps++;
    }

    public void increaseMovementSpeed(int amount)
    {
        movementSpeed += amount;
    }


    // Drawing of GroundedBox
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    //}
}
