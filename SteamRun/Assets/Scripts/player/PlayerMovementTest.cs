using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public GameObject sprite;
    public Rigidbody2D rb;
    public GameObject groundCollider;
    public GameObject rightWallCollider;
    public GameObject leftWallCollider;
    public float moveSpeed;
    public float Acceleration;
    public float deceleration;
    public float velPower;
    public float frictionAmount;

    public float jumpForce;
    private float jumpForceRef;

    public float jumpCutMultiplier;

    public float jumpCoyoteTime;
    private bool canCoyoteJump;
    private float lastGroundedTime;
    public float jumpBufferTime;
    private bool isGrounded = false;

    public float fallGravityMultiplier;
    private float gravityScale;

    private bool isJumping;

    public LayerMask groundLayer;
    bool isWalkingToTheRight = false;
    bool isWalkingToTheLeft = false;
    int moveInput = 0;

    private bool isWallSliding = false;
    private bool wallSlideDirectionRight =false;
    private bool wallSlideDirectionLeft =false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        SC_InputManager.instance.onRightArrowPressStarted += moveRight;
        SC_InputManager.instance.onRightArrowPressCanceled += stopMoveRight;
        SC_InputManager.instance.onLeftArrowPressStarted += moveLeft;
        SC_InputManager.instance.onLeftArrowPressCanceled += stopMoveLeft;
        SC_InputManager.instance.onKeySpacePressStarted += StartJump;
        SC_InputManager.instance.onKeySpacePressCanceled += StopJump;
        jumpForceRef = jumpForce;
    }
    private void FixedUpdate()
    {
        RaycastHit2D ground = Physics2D.Raycast(groundCollider.transform.position,Vector2.right,0.85f);
        if (ground == true)
        {
            isGrounded = true;
            rb.gravityScale = 1.9f;
            if (isWalkingToTheRight)
            {
                moveInput = 1;
            }
            if (isWalkingToTheLeft)
            {
                moveInput = -1;
            }
        }
        else if (ground == false)
        {
            isGrounded= false;
        }
            RaycastHit2D rightWall = Physics2D.Raycast(rightWallCollider.transform.position, Vector2.up, 0.85f);
        if (rightWall == true)
        {
            if (moveInput == 1)
            {
                moveInput = 0;
            }
            if(jumpForce <= 0 && rb.linearVelocityY <=-0.1)
            {
                rb.linearVelocityY = -1;
                isWallSliding = true;
                wallSlideDirectionRight = true;
                if (isGrounded == false)
                {
                    jumpForce = 1.5f;
                }
            }
                
                        
            
        }
        else if (rightWall == false)
        {
            wallSlideDirectionRight=false;
        }
            RaycastHit2D leftWall = Physics2D.Raycast(leftWallCollider.transform.position, Vector2.up, 0.85f);
        if (leftWall == true)
        {
            if (moveInput == -1)
            {
                moveInput = 0;
            }
            if (jumpForce <= 0 && rb.linearVelocityY <= -0.1)
            {
                rb.linearVelocityY = -1f;
                isWallSliding = true;
                wallSlideDirectionLeft = true;
                if (isGrounded == false)
                {
                    jumpForce = 1.5f;
                }
            }           
        }
        else if (leftWall == false)
        {
            wallSlideDirectionLeft=false;
        }
        if(wallSlideDirectionLeft == false && wallSlideDirectionRight == false)
        {
            isWallSliding=false;
        }
        if (isGrounded)
        {
            canCoyoteJump = true;
            lastGroundedTime = jumpCoyoteTime;
        }
        if (lastGroundedTime < 0)
        {
            canCoyoteJump = false;
        }
        if (isWallSliding == true && isGrounded == false)
        { 
            rb.AddForceY(-0.5f);
            if (rb.linearVelocityY <= 0.1 && isJumping ==false)
            {
                rb.gravityScale = 0;
            }           
        }
        float targetSpeed = moveInput * moveSpeed;
        float speedDif = targetSpeed - rb.linearVelocityX;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);

        if (isJumping == true ) 
        {
            Jump();
        }
        if (isWallSliding == false)
        {
            if (rb.linearVelocityY <= 0.1)
            {
                rb.gravityScale = 1 * fallGravityMultiplier;
            }
            else
            {
                rb.gravityScale = 1;
            }
        }
        
        lastGroundedTime -= Time.deltaTime;

  
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (jumpForce > 0)
        {
            jumpForce -= 0.3f;
        }
        if (wallSlideDirectionLeft && isGrounded ==false)
        {
            rb.AddForceX(600);
            if (isWalkingToTheLeft)
            {
                moveInput = -1;
                if (isJumping == true)
                {
                    StartJump();
                    jumpForce = 1.5f;
                }
            }
        }
        if (wallSlideDirectionRight && isGrounded == false)
        {
            rb.AddForceX(-20,ForceMode2D.Impulse);
            if (isWalkingToTheRight)
            {
                moveInput = 1;
                if (isJumping == true)
                {
                    StartJump();
                    jumpForce = 1.5f;
                }
            }
        }
        isJumping = true;
    }
    public void moveRight()
    {
        if (wallSlideDirectionRight == false)
        {
            isWalkingToTheRight = true;
            moveInput = 1;
        }

    }

    public void moveLeft() 
    {
        if (wallSlideDirectionLeft == false) 
        {
            isWalkingToTheLeft = true;
            moveInput = -1;
        }
    }

    public void stopMoveRight()
    {
        isWalkingToTheRight=false;
        if (isWalkingToTheLeft == true)
        {
            moveInput = -1;
        }
        else
        {
            moveInput = 0;
        }
    }
    public void stopMoveLeft()
    {
        isWalkingToTheLeft=false;
        if (isWalkingToTheRight == true)
        {
            moveInput = 1;
        }
        else
        {
            moveInput = 0;
        }
    }
    public void StartJump()
    {
        if (isGrounded == true || canCoyoteJump ==true || isWallSliding ==true)
        {
            canCoyoteJump = false;
            isGrounded = false;
            isJumping = true;
            jumpForce = jumpForceRef;
        }
    }
    public void StopJump()
    {
        isJumping=false;
        jumpForce = 0;
    }
}
