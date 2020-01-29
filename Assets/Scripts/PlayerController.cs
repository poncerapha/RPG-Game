﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D playerRB;
    public Animator playerMoveAnim;
    public static PlayerController instance;
    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

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

        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(this.transform.position.y, bottomLeftLimit.y, topRightLimit.y), this.transform.position.z);

    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(0.5f,1f,0f);
        topRightLimit = topRight + new Vector3(-0.5f,-1f,0f);
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
