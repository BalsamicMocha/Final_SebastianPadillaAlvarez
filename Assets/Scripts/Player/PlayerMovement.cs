using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    public float walkSpeed = 5;
    public float jumpForce;
    
    public Rigidbody rb;

    public GroundCheck GroundCheck;
    public bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GroundCheck = GetComponent<GroundCheck>();
    }

    
    void Update()
    {
        Movement();
        Jump();
        grounded = GroundCheck.IsTouchingGround();
    }
    
    private void Movement()
    {
        rb.velocity = transform.rotation * new Vector3(InputController.Instance.HorizontalMovement() * walkSpeed, rb.velocity.y, InputController.Instance.VerticalMovement() * walkSpeed);
    }

    private void Jump()
    {
        if (InputController.Instance.JumpInput() && GroundCheck.IsTouchingGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
