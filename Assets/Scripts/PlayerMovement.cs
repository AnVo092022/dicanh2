using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpSpeed = 5f;
    Vector2 inputMovement;
    Rigidbody2D myBody;
    CapsuleCollider2D myCollider;
    SpriteRenderer mFlip;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FlipSprite();
    }

    private void FlipSprite()
    {
        bool isPlayerHasHorizontalMovement = Mathf.Abs(myBody.velocity.x) > Mathf.Epsilon;
        if (isPlayerHasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(myBody.velocity.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
      
    }
    
    void Movement()
    {
        myBody.velocity = new Vector2(inputMovement.x * moveSpeed, myBody.velocity.y);
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
