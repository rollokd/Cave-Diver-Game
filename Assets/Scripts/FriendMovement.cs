using UnityEngine;

public class FriendMovement : Character
{
    [Header("Friend")]
    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    private Rigidbody2D playerRb;
    [SerializeField]
    private Animator animator;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if((horizontal > 0 && facingRight) || (horizontal < 0 && !facingRight))
            Flip();

        gameObject.transform.position = new Vector2(player.transform.position.x, gameObject.transform.position.y);
        gameObject.transform.localScale = player.transform.localScale;

        animator.SetBool("Sideways", Mathf.Abs(playerRb.velocity.x) > 0.01f);
    }
}
