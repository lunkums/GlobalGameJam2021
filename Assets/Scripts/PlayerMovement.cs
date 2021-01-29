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
    /*public float minJumpDistance = 2f;
    public float maxJumpDistance = 10f;*/
    public float jumpDistance = 10f;
    public float jumpChargeRate = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;
    private float jumpHeight = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Jump behavior
        if (isGrounded)
        {
            if (velocity.y < 0f)
            {
                velocity.y = -2f;
                isJumping = false;
            }

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
                    velocity.x = jumpDistance * transform.forward.x;
                    velocity.z = jumpDistance * transform.forward.z;
                    jumpHeight = 0f;
                }
                else
                {
                    velocity.x = 0;
                    velocity.z = 0;
                }
            }
            // Player must be jumping if jumpHeight or y velocity is greater than 0
            isJumping = jumpHeight > 0f || velocity.y > 0;
        }

        // Apply horizontal and vertical motion
        if (!isJumping)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
