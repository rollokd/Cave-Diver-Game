using System.Collections;
using UnityEngine;

public class Boss : Character
{
    [Header("Boss")]
    public bool doubleJump;
    public bool jetPack;

    [SerializeField]
    private float movementSpeed = 3;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;

    private const float jumpSpeed = 9;

    private float horizontal;
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(StartFight());
    }

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
                if (jetPack)
                {
                    yield return new WaitForSeconds(1 * 3 / movementSpeed);
                    StartCoroutine(JetPack(1 * 3 / movementSpeed));
                    yield return new WaitForSeconds(1 * 3 / movementSpeed);
                }
                else
                {
                    yield return new WaitForSeconds(2 * 3 / movementSpeed);
                }

                //Second block
                Flip();
                Move(1, 3.4f);
                yield return new WaitForSeconds(0.2f * 3 / movementSpeed);
                Jump();
                yield return new WaitForSeconds(1 * 3 / movementSpeed);
                Jump();
                if (jetPack)
                {
                    yield return new WaitForSeconds(1 * 3 / movementSpeed);
                    StartCoroutine(JetPack(1 * 3 / movementSpeed));
                    yield return new WaitForSeconds(2 * 3 / movementSpeed);
                }
                else
                {
                    yield return new WaitForSeconds(3 * 3 / movementSpeed);
                }

                //Back down
                Flip();
                Move(-1, 3.4f);
                yield return new WaitForSeconds(2.2f * 3 / movementSpeed);
                Jump();
                if (jetPack)
                {
                    yield return new WaitForSeconds(1 * 3 / movementSpeed);
                    StartCoroutine(JetPack(1 * 3 / movementSpeed));
                    yield return new WaitForSeconds(3 * 3 / movementSpeed);
                }
                else
                {
                    yield return new WaitForSeconds(4 * 3 / movementSpeed);
                }

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
            else
            {
                //First block
                Flip();
                Move(-1, 3.6f);
                yield return new WaitForSeconds(2.2f * 3 / movementSpeed);
                Jump();
                if (jetPack)
                {
                    yield return new WaitForSeconds(1 * 3 / movementSpeed);
                    StartCoroutine(JetPack(1 * 3 / movementSpeed));
                    yield return new WaitForSeconds(2 * 3 / movementSpeed);
                }
                else
                {
                    yield return new WaitForSeconds(3 * 3 / movementSpeed);
                }

                //Back
                Flip();
                Move(1, 5.8f);
                yield return new WaitForSeconds(3.2f * 3 / movementSpeed);
                Jump();
                if (jetPack)
                {
                    yield return new WaitForSeconds(1 * 3 / movementSpeed);
                    StartCoroutine(JetPack(1 * 3 / movementSpeed));
                    yield return new WaitForSeconds(2 * 3 / movementSpeed);
                }
                else
                {
                    yield return new WaitForSeconds(3 * 3 / movementSpeed);
                }

                //Back left
                Flip();
                Move(-1, 2.2f);
                yield return new WaitForSeconds(3 * 3 / movementSpeed);
                Flip();
            }
        }
    }

    public void IncreaseMovementSpeed(int amount)
    {
        movementSpeed += amount;
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

    private IEnumerator JetPack(float time)
    {
        float timePassed = 0;
        while(timePassed < time)
        {
            timePassed += Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            yield return null;
        }
    }

    protected override void Die()
    {
        Debug.Log("Character die");
        gameController.KillBoss();
    }

    // Drawing of GroundedBox
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    //}

    // Randomized fight
    //public IEnumerator StartRandomFight()
    //{
    //    for (int i = 0; i < 100; i++)
    //    {
    //        Move(-1, Random.Range(0, 5));
    //        yield return new WaitForSeconds(Random.Range(0, 5));
    //        Jump();
    //        yield return new WaitForSeconds(Random.Range(0, 5));
    //        Move(1, Random.Range(1, 5));
    //        yield return new WaitForSeconds(Random.Range(0, 5));
    //        Jump();
    //        yield return new WaitForSeconds(Random.Range(0, 5));
    //    }
    //}
}
