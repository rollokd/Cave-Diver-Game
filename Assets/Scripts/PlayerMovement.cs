using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool jetPack = false;
    public int maxJumps = 0;
    
    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float fuelCost;
    [SerializeField]
    private float fuelRegen;

    private float maxFuel = 10;
    private float fuel;
    private bool isGrounded;
    private Animator animator;
    private int jumps;
    private bool canUseJetPack = true;
    private Rigidbody2D rb;
    private AudioSource footsteps;
    
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        footsteps = GetComponent<AudioSource>();
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(footsteps.isPlaying);

        if (gameController.paused)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)){
            Flip();
        }

        animator.SetBool("Sideways", Mathf.Abs(rb.velocity.x) > 0 && !animator.GetBool("Jump"));
        
        isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask);

        if (isGrounded)
        {
            animator.SetBool("Jump", false);
            jumps = 0;
        }

        if (Mathf.Abs(horizontal) > 0.01f && isGrounded)
        {
            if (!footsteps.isPlaying)
                footsteps.Play();
        }
        else
        {
            if (footsteps.isPlaying)
                footsteps.Pause();
        }

        if (fuel > 9)
            canUseJetPack = true;

        if (Input.GetButtonDown("Jump") && (isGrounded || jumps < maxJumps))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0) + Vector2.up * jumpSpeed;
            jumps++;
            animator.SetBool("Jump", true);
        }
        else if (Input.GetButton("Jump") && jetPack && (fuel - (fuelCost * Time.deltaTime) > 0) && rb.velocity.y < -0.01f && canUseJetPack)
        {
            Debug.Log("Jetpack");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            fuel -= fuelCost * Time.deltaTime;
            animator.SetBool("Jump", false);

            if (fuel < 0.01f)
                canUseJetPack = false;
        }
        else
        {
            if (fuel + fuelRegen * Time.deltaTime < maxFuel)
                fuel += fuelRegen * Time.deltaTime;
        }
    }

    public void AddJump()
    {
        maxJumps++;
    }

    public void IncreaseMovementSpeed(int amount)
    {
        movementSpeed += amount;
    }


    // Drawing of GroundedBox
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    //}

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f,180f,0f);
    }
}
