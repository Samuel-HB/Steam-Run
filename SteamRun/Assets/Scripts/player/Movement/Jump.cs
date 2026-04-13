using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    bool isGrounded = false;
    bool isWallSliding;
    public GameObject groundCollider;
    public GameObject rightWallCollider;
    public GameObject leftWallCollider;
    public Rigidbody2D playerRb;

    [Header("Jump")]
    public float jumpForce;
    private float jumpForceRef;
    private bool isJumping;

    //now public
    public bool isRightWallJumping;
    public bool isLeftWallJumping;

    public bool isLeftWallSliding;
    public bool isRightWallSliding;
    public float wallFriction;
    public float wallJumpStrength;
    private float wallJumpStrengthRef;

    private bool canCoyoteJump;
    public float jumpCoyoteTime;
    private float lastGroundedTime;
    public float BufferTime;
    private float timeSinceSpacePress;

    private LayerMask surface;

    [SerializeField] private Animator animator;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.75f, 0f, 0.75f);

        Gizmos.DrawRay(groundCollider.transform.position, Vector2.right * 0.5f);
        Gizmos.DrawRay(leftWallCollider.transform.position, Vector2.up * 1.35f);
        Gizmos.DrawRay(rightWallCollider.transform.position, Vector2.up * 1.35f);
    }

    void Start()
    {
        jumpForceRef = jumpForce;
        wallJumpStrengthRef = wallJumpStrength;
        surface = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        RaycastHit2D ground = Physics2D.Raycast(groundCollider.transform.position, Vector2.right, 0.5f ,surface);
        if (ground == true)
        {
            isGrounded = true;
            lastGroundedTime = jumpCoyoteTime;
            if (timeSinceSpacePress > 0)
            {
                playerRb.linearVelocityY = 0f;
                jumpForce = jumpForceRef;
                isJumping = true;
                canCoyoteJump = false;
                timeSinceSpacePress = -1;
            }
            if (isJumping == false)
            {
                canCoyoteJump = true;
            }
        }
        else if (ground == false)
        {
            isGrounded = false;           
        }

        RaycastHit2D rightWall = Physics2D.Raycast(rightWallCollider.transform.position, Vector2.up, 1.35f ,surface);
        if (rightWall == true)
        {
            //new
            animator.SetBool("isGripping", true);

            isWallSliding = true;
            if (ground == false && playerRb.linearVelocityY < 0.1)
            {
                playerRb.linearVelocityY = 0f;
                isRightWallSliding = true;
                if (timeSinceSpacePress > 0)
                {
                    playerRb.linearVelocityY = 0f;
                    jumpForce = jumpForceRef;
                    isJumping = true;
                    wallJumpStrength = wallJumpStrengthRef;
                    isRightWallJumping = true;
                    //new
                    EventManager.Instance.PlayerWallJumpFunc();

                    timeSinceSpacePress = -1;
                }
            }
        }
        else 
        {
            isRightWallSliding = false;
        }

        RaycastHit2D leftWall = Physics2D.Raycast(leftWallCollider.transform.position, Vector2.up, 1.35f ,surface);
        if (leftWall == true)
        {
            //new
            animator.SetBool("isGripping", true);

            isWallSliding = true;
            if (ground == false && playerRb.linearVelocityY <0.1)
            {
                playerRb.linearVelocityY = 0f;
                isLeftWallSliding = true;
                if (timeSinceSpacePress > 0)
                {
                    playerRb.linearVelocityY = 0f;
                    jumpForce = jumpForceRef;
                    isJumping = true;
                    isLeftWallJumping = true;
                    //new
                    EventManager.Instance.PlayerWallJumpFunc();

                    wallJumpStrength = wallJumpStrengthRef;
                    timeSinceSpacePress = -1;
                }
            }           
        }
        else
        {
            isLeftWallSliding = false;
        }
        if(leftWall == false && rightWall == false)
        {
            isWallSliding = false;
            //new
            animator.SetBool("isGripping", false);
        }
        if (isWallSliding && isGrounded==false && playerRb.linearVelocityY <0.1)
        {
            playerRb.AddForceY(wallFriction);
        }
        lastGroundedTime -= Time.deltaTime;
        timeSinceSpacePress -= Time.deltaTime;
        if (lastGroundedTime < 0) 
        {
            canCoyoteJump = false;
        }
        if (isJumping|| isRightWallJumping||isLeftWallJumping)
        {
            if (jumpForce > 0)
            {
                playerRb.AddForceY(jumpForce * Time.deltaTime * 912);
                jumpForce -= 0.1f * Time.deltaTime * 912;
            }
            if (isRightWallJumping)
            {
                if (wallJumpStrength > 0)
                {
                    playerRb.AddForce(wallJumpStrength * Vector2.left * Time.deltaTime * 912);
                    wallJumpStrength -= 0.1f * Time.deltaTime * 912;
                }
            }
            if (isLeftWallJumping)
            {
                if (wallJumpStrength > 0)
                {
                    playerRb.AddForce(wallJumpStrength * Vector2.right * Time.deltaTime * 912);
                    wallJumpStrength -= 0.1f * Time.deltaTime * 912;
                }
            }
        }
    }

    public void StartJump(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            if (isGrounded || canCoyoteJump == true)
            {
                jumpForce = jumpForceRef;
                isJumping = true;
                canCoyoteJump = false;
            }
            else if (isGrounded == false && isWallSliding)
            {
                jumpForce = jumpForceRef;
                wallJumpStrength = wallJumpStrengthRef;
                if (isLeftWallSliding)
                {
                    isLeftWallJumping = true;
                    //new
                    EventManager.Instance.PlayerWallJumpFunc();
                }
                if (isRightWallSliding)
                {
                    isRightWallJumping = true;
                    //new
                    EventManager.Instance.PlayerWallJumpFunc();
                }
            }
            else if (isGrounded == false && isWallSliding == false)
            {
                timeSinceSpacePress = BufferTime;
            }
        }
        else if (_context.canceled)
        {
            isJumping = false;
            isLeftWallJumping = false;
            isRightWallJumping = false;
            timeSinceSpacePress = 0; 
        }
    }
}
