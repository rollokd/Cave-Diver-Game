using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public bool doubleJump;
    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;

    private Rigidbody2D rb;
    private float horizontal;
    private float movementSpeed = 3;
    private float jumpSpeed = 9;
    private bool canJumpAgain;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartFight());
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask);

        if (isGrounded)
            canJumpAgain = true;

        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        if (doubleJump && canJumpAgain && !isGrounded && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            Jump();
            canJumpAgain = false;
        }
    }

    public IEnumerator StartFight()
    {
        for (int i = 0; i < 100; i++)
        {
            Move(-1, Random.Range(0,5));
            yield return new WaitForSeconds(Random.Range(0, 5)); 
            Jump();
            yield return new WaitForSeconds(Random.Range(0, 5)); 
            Move(1, Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0, 5));
            Jump();
            yield return new WaitForSeconds(Random.Range(0, 5));
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0) + Vector2.up * jumpSpeed;
    }

    private void Move(float horizontalDirection, int time)
    {
        horizontal = horizontalDirection;

        StartCoroutine(StopMoving(time));
    }

    private IEnumerator StopMoving(int time)
    {
        yield return new WaitForSeconds(time);
        horizontal = 0;
    }

    // Drawing of GroundedBox
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    //}
}
