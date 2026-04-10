using System.Collections;
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
    private bool isRightWallJumping;
    private bool isLeftWallJumping;
    public bool isLeftWallSliding;
    public bool isRightWallSliding;
    public float wallFriction;
    public float wallJumpStrength;
    private float wallJumpStrengthRef;
    private bool canCoyoteJump;
    public float jumpCoyoteTime;
    private float lastGroundedTime;

    private LayerMask surface;

    [SerializeField] private Animator animator;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.75f, 0f, 0.75f);

        Gizmos.DrawRay(groundCollider.transform.position, Vector2.right * 0.5f);
        Gizmos.DrawRay(leftWallCollider.transform.position, Vector2.up * 1.25f);
        Gizmos.DrawRay(rightWallCollider.transform.position, Vector2.up * 1.25f);
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
            if(isJumping == false)
            {
                canCoyoteJump = true;
            }         
        }
        else if (ground == false)
        {
            isGrounded = false;           
        }

        RaycastHit2D rightWall = Physics2D.Raycast(rightWallCollider.transform.position, Vector2.up, 1.25f ,surface);
        if (rightWall == true)
        {
            //new
            animator.SetBool("isGripping", true);

            isWallSliding = true;
            if (ground == false && playerRb.linearVelocityY < 0.1)
            {
                playerRb.linearVelocityY = 0f;
                isRightWallSliding = true;
            }
        }
        else 
        {
            isRightWallSliding = false;
        }

        RaycastHit2D leftWall = Physics2D.Raycast(leftWallCollider.transform.position, Vector2.up, 1.25f ,surface);
        if (leftWall == true)
        {
            //new
            animator.SetBool("isGripping", true);

            isWallSliding = true;
            if (ground == false && playerRb.linearVelocityY <0.1)
            {
                playerRb.linearVelocityY = 0f;
                isLeftWallSliding = true;
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
        if (lastGroundedTime < 0)
        {
            canCoyoteJump = false;
        }
        if (isJumping|| isRightWallJumping||isLeftWallJumping)
        {
            if (jumpForce > 0)
            {
                playerRb.AddForceY(jumpForce * Time.deltaTime * Screen.width);
                jumpForce -= 0.1f * Time.deltaTime * Screen.width;
            }
            if (isRightWallJumping)
            {
                if (wallJumpStrength > 0)
                {
                    playerRb.AddForce(wallJumpStrength * Vector2.left * Time.deltaTime * Screen.width);
                    wallJumpStrength -= 0.1f * Time.deltaTime * Screen.width;
                    print(wallJumpStrength);
                }
            }
            if (isLeftWallJumping)
            {
                if (wallJumpStrength > 0)
                {
                    playerRb.AddForce(wallJumpStrength * Vector2.right * Time.deltaTime * Screen.width);
                    wallJumpStrength -= 0.1f * Time.deltaTime * Screen.width;
                    print(wallJumpStrength);
                }
            }
        }
        lastGroundedTime -= Time.deltaTime;
        print(lastGroundedTime);
    }

    public void StartJump(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            if (isGrounded || canCoyoteJump ==true)
            {
                jumpForce = jumpForceRef;
                isJumping = true;
                canCoyoteJump = false;
            }
            if (isGrounded == false && isWallSliding)
            {
                jumpForce = jumpForceRef;
                wallJumpStrength = wallJumpStrengthRef;
                if (isLeftWallSliding)
                {
                    isLeftWallJumping = true;
                }
                if (isRightWallSliding)
                {
                    isRightWallJumping = true;
                }
            }
        }
        else if (_context.canceled)
        {
            isJumping = false;
            isLeftWallJumping = false;
            isRightWallJumping = false;
        }
    }
}
