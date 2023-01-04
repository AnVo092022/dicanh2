using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpSpeed = 5f;
    Vector2 inputMovement;
    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        myBody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
        myBody.velocity += new Vector2(0f, jumpSpeed);
    }
}
