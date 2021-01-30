using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float slopeForce = 25f;
    public float slopeForceRayLength = 2.6f;

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
    private bool isOnSlope;
    private float jumpHeight = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isOnSlope = OnSlope();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || isOnSlope;

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
                    jumpHeight = 0f;
                }
            }
            // Player must be jumping if jumpHeight or y velocity is greater than 0
            isJumping = jumpHeight > 0f || velocity.y > 0;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Apply horizontal and vertical motion
        Vector3 move = Vector3.zero;
        if (!isJumping)
        {
            move += transform.right * x + transform.forward * z;
        }
        else if (!isGrounded)
        {
            move += transform.forward;
        }
        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        // Apply additional downward force if on slope
        if ((x != 0 || z != 0) && isOnSlope)
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        }
    }

    // Uses raycasting to determine if the player is on a slope
    private bool OnSlope()
    {
        bool isOnSlope = false;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2.0f * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                isOnSlope = true;
            }
        }
        return isOnSlope;
    }
}
