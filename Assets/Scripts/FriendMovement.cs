using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMovement : MonoBehaviour
{

    public PlayerMovement player;

    private bool facingRight = true;
    private Animator animator;
    private Animator playerAnimator;
    private Vector3 prevPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
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

        Vector3 pos = transform.position;

        Debug.Log(pos);
        Debug.Log(pos != prevPos);
        animator.SetBool("Sideways", playerAnimator.GetBool("Sideways"));

        prevPos = pos;
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f,180f,0f);
    }
}
