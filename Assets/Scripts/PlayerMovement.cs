using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [Header("Jumping")]
    public int maxJumps = 0;
    [SerializeField]
    private float jumpSpeed;
    
    [Header("Jetpack")]
    public bool jetPack = false;
    [SerializeField]
    private float fuelCost;
    [SerializeField]
    private float fuelRegen;

    [Header("Ground detection cast")]
    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;

    [Header("Components")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private AudioSource footsteps;
    [SerializeField]
    private Player player;

    private readonly float maxFuel = 10;

    private float fuel;
    private int jumps;
    private bool isGrounded;
    private bool canUseJetPack = true;
    private GameController gameController;
    

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        fuel = maxFuel;
    }

    void Update()
    {
        if (gameController != null && gameController.paused)
            return;

        //Horizontal Movement
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        if((horizontal > 0 && player.facingRight) || (horizontal < 0 && !player.facingRight))
            player.Flip();

        //Grounded
        isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask) && Mathf.Abs(rb.velocity.y) < 0.01f;

        if (isGrounded)
        {
            animator.SetBool("Jump", false);
            jumps = 0;
        }
        
        animator.SetBool("Sideways", Mathf.Abs(rb.velocity.x) > 0 && !animator.GetBool("Jump"));

        //Footsteps audio
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

        //Fuel and jumping
        if (fuel > 9)
            canUseJetPack = true;

        if (Input.GetButtonDown("Jump") && (isGrounded || jumps <= maxJumps))
        {
            jumps++;
            rb.velocity = new Vector2(rb.velocity.x, 0) + Vector2.up * jumpSpeed;
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
}
