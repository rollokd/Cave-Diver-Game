using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMovement : MonoBehaviour
{

    public PlayerMovement player;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(player.transform.position.x, gameObject.transform.position.y);
        gameObject.transform.localScale = player.transform.localScale;
    }
}
