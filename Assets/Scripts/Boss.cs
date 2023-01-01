using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool doubleJump;
    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float movementSpeed = 3;

    private Rigidbody2D rb;
    private float horizontal;
    private float jumpSpeed = 9;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(StartFight());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }

    public IEnumerator StartFight()
    {
        for (int i = 0; i < 100; i++)
        {
            if (doubleJump)
            {
                //First block
                Flip();
                Move(-1, 3.6f);
                yield return new WaitForSeconds(2.2f * 3 / movementSpeed);
                Jump();
                yield return new WaitForSeconds(2 * 3 / movementSpeed);

                //Second block
                Flip();
                Move(1, 3.4f);
                yield return new WaitForSeconds(0.2f * 3 / movementSpeed);
                Jump();
                yield return new WaitForSeconds(1 * 3 / movementSpeed);
                Jump();
                yield return new WaitForSeconds(3 * 3 / movementSpeed);

                //Back down
                Flip();
                Move(-1, 3.4f);
                yield return new WaitForSeconds(2.2f * 3 / movementSpeed);
                Jump();
                yield return new WaitForSeconds(4 * 3 / movementSpeed);

                //Jump up left
                Jump();
                yield return new WaitForSeconds(1 * 3 / movementSpeed);
                Jump();
                yield return new WaitForSeconds(0.5f * 3 / movementSpeed);
                Move(-1, 1f);
                yield return new WaitForSeconds(2 * 3 / movementSpeed);

                //Back right
                Flip();
                Move(1, 4.6f);
                yield return new WaitForSeconds(5 * 3 / movementSpeed);
            }
        }
    }
    public IEnumerator StartRandomFight()
    {
        for (int i = 0; i < 100; i++)
        {
            Move(-1, Random.Range(0, 5));
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
        animator.SetBool("Jump", true);
        rb.velocity = new Vector2(rb.velocity.x, 0) + Vector2.up * jumpSpeed;
    }

    private void Move(float horizontalDirection, float time)
    {
        animator.SetBool("Jump", false);
        animator.SetBool("Sideways", true);
        horizontal = horizontalDirection;
        StartCoroutine(StopMoving(time*3/movementSpeed));
    }

    private IEnumerator StopMoving(float time)
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
