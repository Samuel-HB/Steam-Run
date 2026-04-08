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
    private bool isSprinting =false;

    public float jumpForce;
    private float jumpForceRef;

    public float jumpCutMultiplier;

    public float jumpCoyoteTime;
    private bool canCoyoteJump;
    private float lastGroundedTime;
    public float jumpBufferTime;
    private bool isGrounded = false;
    private bool hasDoneJump;

    public float fallGravityMultiplier;
    private float gravityScale;

    private bool isJumping;

    public LayerMask groundLayer;
    bool isWalkingToTheRight = false;
    bool isWalkingToTheLeft = false;
    float moveInput = 0;

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
        SC_InputManager.instance.onShiftPressStarted += StartSprint;
        SC_InputManager.instance.onShiftPressCanceled += StopSprint;
        jumpForceRef = jumpForce;
    }

    //new
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.75f, 0f, 0.75f);

        Gizmos.DrawRay(groundCollider.transform.position, Vector2.right * 0.85f);
        Gizmos.DrawRay(leftWallCollider.transform.position, Vector2.up * 0.85f);
        Gizmos.DrawRay(rightWallCollider.transform.position, Vector2.up * 0.85f);
    }

    private void FixedUpdate()
    {
        RaycastHit2D ground = Physics2D.Raycast(groundCollider.transform.position,Vector2.right,0.85f);
        if (ground == true)
        {
            isGrounded = true;
            rb.gravityScale = 1.9f;
            if (isWalkingToTheRight && isSprinting ==true)
            {
                moveInput = 1.5f;
            }
            else if (isWalkingToTheRight && isSprinting == false)
            {
                moveInput = 1;
            }
                if (isWalkingToTheLeft && isSprinting == true)
            {
                moveInput = -1.5f;
            }
            else if (isWalkingToTheLeft && isSprinting == false)
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
            if (moveInput >= 1)
            {
                moveInput = 0;
            }
            if(jumpForce <= 0 && rb.linearVelocityY <=-0.1)
            {
                rb.linearVelocityY = -0.5f;
                isWallSliding = true;
                wallSlideDirectionRight = true;
            }
                
                        
            
        }
        else if (rightWall == false)
        {
            wallSlideDirectionRight=false;
        }
            RaycastHit2D leftWall = Physics2D.Raycast(leftWallCollider.transform.position, Vector2.up, 0.85f);
        if (leftWall == true)
        {
            if (moveInput <= -1)
            {
                moveInput = 0;
            }
            if (jumpForce <= 0 && rb.linearVelocityY <= -0.1)
            {
                rb.linearVelocityY = -0.5f;
                isWallSliding = true;
                wallSlideDirectionLeft = true;
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

        if (isJumping == true  && hasDoneJump ==false) 
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
            if (isWalkingToTheLeft && isSprinting == true)
            {
                moveInput = -1.5f;
            }
            else if (isWalkingToTheLeft && isSprinting == false)
            {
                moveInput = -1;
            }
        }
        if (wallSlideDirectionRight && isGrounded == false)
        {
            rb.AddForceX(-20,ForceMode2D.Impulse);
            if (isWalkingToTheRight && isSprinting == true)
            {
                moveInput = 1.5f;
            }
            else if (isWalkingToTheRight && isSprinting == false)
            {
                moveInput = 1;
            }
        }
        isJumping = true;
        if (jumpForce <=0)
        {
            hasDoneJump = true;
        }
        if (isWalkingToTheRight && isSprinting == true)
        {
            moveInput = 1.5f;
        }
        else if (isWalkingToTheRight && isSprinting == false)
        {
            moveInput = 1;
        }
        if (isWalkingToTheLeft && isSprinting == true)
        {
            moveInput = -1.5f;
        }
        else if (isWalkingToTheLeft && isSprinting == false)
        {
            moveInput = -1;
        }
    }
    public void moveRight()
    {
        if (wallSlideDirectionRight == false && isSprinting == false)
        {
            isWalkingToTheRight = true;
            moveInput = 1.5f;
        }
        if (wallSlideDirectionRight == false && isSprinting == true)
        {
            isWalkingToTheRight = true;
            moveInput = 1;
        }


    }

    public void moveLeft() 
    {
        if (wallSlideDirectionLeft == false && isSprinting == true) 
        {
            isWalkingToTheLeft = true;
            moveInput = -1.5f;
        }
        if (wallSlideDirectionLeft == false && isSprinting == false)
        {
            isWalkingToTheLeft = true;
            moveInput = -1;
        }
    }

    public void stopMoveRight()
    {
        isWalkingToTheRight=false;
        if (isWalkingToTheLeft == true && isSprinting == true)
        {
            moveInput = -1.5f;
        }
        else if (isWalkingToTheLeft == true && isSprinting == false)
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
        if (isWalkingToTheRight == true && isSprinting == true)
        {
            moveInput = 1.5f;
        }
        else if (isWalkingToTheRight == true && isSprinting == false)
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
            hasDoneJump = false;
        }
        
    }
    public void StopJump()
    {
        isJumping=false;
        jumpForce = 0;
        hasDoneJump = true;
    }

    public void StartSprint()
    {
        isSprinting = true; 
    }

    public void StopSprint()
    {
        isSprinting=false;
    }
}
