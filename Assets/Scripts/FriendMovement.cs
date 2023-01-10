using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMovement : MonoBehaviour
{

    public PlayerMovement player;

    private bool facingRight = true;
    private Animator animator;
    private Rigidbody2D playerRb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)){
            Flip();
        }

        gameObject.transform.position = new Vector2(player.transform.position.x, gameObject.transform.position.y);
        gameObject.transform.localScale = player.transform.localScale;

        animator.SetBool("Sideways", Mathf.Abs(playerRb.velocity.x) > 0.01f);
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f,180f,0f);
    }
}
