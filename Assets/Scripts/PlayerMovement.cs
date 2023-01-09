using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpSpeed = 5f;
    public float climbSpeed = 5f;

    Vector2 inputMovement;
    Rigidbody2D myBody;
    CapsuleCollider2D myCollider;
    Animator myAnim;

    float savedGravityScale;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myAnim = GetComponent<Animator>();
        savedGravityScale = myBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FlipSprite();
        Ladder();
    }

    private void Ladder()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            return;
        }

        bool isPlayerHasVerticalMovement = Mathf.Abs(myBody.velocity.y) > Mathf.Epsilon;
        myAnim.SetBool("IsLadder", isPlayerHasVerticalMovement);

        myBody.velocity = new Vector2(myBody.velocity.x, inputMovement.y * climbSpeed);
        myBody.gravityScale = 0;
    }

    private void FlipSprite()
    {
        //Mathf.Abs = chuyển số dương thành số âm
        bool isPlayerHasHorizontalMovement = Mathf.Abs(myBody.velocity.x) > Mathf.Epsilon;
        if (isPlayerHasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(myBody.velocity.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
        //SpriteRenderer sp = GetComponent<SpriteRenderer>();
        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{ sp.flipX = true; }
        //else
        //{ sp.flipX = false; }

    }
    
    void Movement()
    {
        myBody.velocity = new Vector2(inputMovement.x * moveSpeed, myBody.velocity.y);

        myAnim.SetBool("IsLadder", false);
        myBody.gravityScale = savedGravityScale;
    }

    void OnJump(InputValue value)
    {
        //inputJump = value.Get<Vector2>();


        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        myBody.velocity += new Vector2(0f, jumpSpeed);
        
    }
}
