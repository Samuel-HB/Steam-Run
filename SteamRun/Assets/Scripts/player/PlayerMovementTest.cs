using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public Rigidbody2D rb;
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
    public GameObject rightWallChecker;
    public GameObject leftWallChecker;

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
            rb.AddForceX(300);
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
            rb.AddForceX(-300);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            rb.gravityScale = 1.9f;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (moveInput != 0) 
            {
                moveInput = 0;
            }
            rb.linearVelocityY = -1;
            isWallSliding = true;
            CheckWallSlidDirection(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isWallSliding = false;
            CheckWallSlidDirection(collision);
            wallSlideDirectionLeft = false;
            wallSlideDirectionRight = false;
        }
    }
    
    private void CheckWallSlidDirection(Collision2D collision)
    {
        Vector2 wallDirection = transform.position - collision.transform.position;

        if (wallDirection.x < 0) 
        {
            wallSlideDirectionRight = true;
        }
        else
        {
            wallSlideDirectionLeft = true;
        }
    }
}
