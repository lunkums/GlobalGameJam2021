using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    
    public float minJumpHeight = 2f;
    public float maxJumpHeight = 10f;
    public float jumpChargeRate = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private float jumpHeight = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        // Apply horizontal and vertical motion
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jump behavior
        if (isGrounded)
        {
            // Holding the jump button
            if (Input.GetButton("Jump"))
            {
                if (jumpHeight < maxJumpHeight)
                {
                    jumpHeight += jumpChargeRate * Time.deltaTime;
                }
                else
                {
                    jumpHeight = maxJumpHeight;
                }
            }
            // Jump button released
            else
            {
                // Jump
                if (jumpHeight > 0f)
                {
                    jumpHeight += minJumpHeight;
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    jumpHeight = 0;
                }
            }
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
