using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D playerRB;
    public Animator playerMoveAnim;
    public static PlayerController instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        playerRB.velocity = new Vector2(horizontal, vertical) * moveSpeed;

        playerMoveAnim.SetFloat("moveX", playerRB.velocity.x);
        playerMoveAnim.SetFloat("moveY", playerRB.velocity.y);
        handleAnimation(horizontal, vertical);
    }

    private void handleAnimation(float horizontal, float vertical)
    {
        if (horizontal == 1 || horizontal == -1 || vertical == 1 || vertical == -1)
        {
            playerMoveAnim.SetFloat("lastMoveX", horizontal);
            playerMoveAnim.SetFloat("lastMoveY", vertical);
        }
    }
}
