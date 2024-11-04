using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;

    [Header("Ground Check")]
    private bool isGrounded;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] const float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    [Space]
    [Header("Player Components")]
    [SerializeField] Collider2D standingCollider;
    [SerializeField] Collider2D crouchingCollider;

    [Space]
    [Header("Player Values")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float jumpPower;

    private float horizontalVal;
    private int jumpCounter = 0;

    private bool crouch = false;
    private bool jump;
    private bool doubleJump;
    private bool coyoteJump;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        crouchingCollider.enabled = false;
    }

    void Update()
    {
        //Movement control
        horizontalVal = Input.GetAxisRaw("Horizontal");

        //Jump control
        if (Input.GetButtonDown("Jump"))
            Jump();

        //Crouch control
        if (Input.GetButtonDown("Crouch") && !crouch)
            crouch = true;
        else if (Input.GetButtonDown("Crouch") && crouch)
            crouch = false;
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalVal, crouch);
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        //FixedUpdate will always assume the character isn't grounded until proven otherwise
        isGrounded = false;

        //Here is the proof of otherwise
        //Creates an array of colliders with the layer "Ground" that come into contact with the groundChecker within a small radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        //If there is more than one collider in the array, then the player is proven to be grounded
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                jumpCounter = 0;
                doubleJump = false;
            }
        }
    }

    void Move(float dir, bool crouchFlag)
    {
        //***************************************************//
        //*                 PLAYER MOVEMENT                 *//
        //***************************************************//

        #region Player Movement
        if (!crouchFlag)
        {
            //Set horizontal speed value using horizontalVal and speed - multiplied by 100 to avoid huge speed value (2.5 instead of 250)
            float speedVal = dir * playerSpeed * 100 * Time.fixedDeltaTime;
            //Implement the complete player speed
            Vector2 targetVelocity = new Vector2(speedVal, playerRB.velocity.y);
            //Apply the velocity to the player
            playerRB.velocity = targetVelocity;
        }
        else if(crouchFlag)
        {
            //Use crouch speed instead of regular speed
            float speedVal = dir * crouchSpeed * 100 * Time.fixedDeltaTime;
            Vector2 targetVelocity = new Vector2(speedVal, playerRB.velocity.y);
            playerRB.velocity = targetVelocity;
        }
        #endregion

        //*************************************************//
        //*                 PLAYER CROUCH                 *//
        //*************************************************//

        #region Player Crouching
        //Make sure player is on the ground before toggling crouch
        if (isGrounded && crouchFlag)
        {
            standingCollider.enabled = false;
            crouchingCollider.enabled = true;
        }
        if (!crouchFlag)
        {
            standingCollider.enabled = true;
            crouchingCollider.enabled = false;  
        }
        #endregion
    }

    void Jump()
    {
        if (isGrounded && !crouch)
        {
            playerRB.velocity = Vector2.up * jumpPower;

            jumpCounter++;
            doubleJump = true;
        }
        else
        {
            if (doubleJump && jumpCounter < 2)
            {
                playerRB.velocity = Vector2.up * jumpPower / 1.2f;

                jumpCounter++;
            }
        }
    }
}
