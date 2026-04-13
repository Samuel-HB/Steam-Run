using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
//new
using System.Collections;


public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;
    private float moveSpeed;
    [SerializeField] float maxSpeed;
    public Rigidbody2D rb;
    public float Acceleration;
    public float deceleration;
    public float velPower;
    public float frictionAmount;

    [SerializeField]
    private Jump jumpRef;

    private float inputDirection;
    private float inputDirectionRef;

    //new
    [SerializeField] private Animator animator;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private Transform wheelTransform;
    private float spriteScale;
    private float invertedSpriteScale;
    private bool canFlip = true;

    private void Start()
    {
        moveSpeed = speed;

        //new
        spriteScale = spriteTransform.localScale.x;
        invertedSpriteScale = spriteTransform.localScale.x * -1;

        //new
        EventManager.Instance.PlayerWallJump += StartWaitingBeforeFlip;
    }
    private void FixedUpdate()
    {
        if(jumpRef.isLeftWallSliding ==true && inputDirection < -0.1f)
        {
            inputDirection = 0;
        }
        else if (jumpRef.isRightWallSliding ==true && inputDirection> 0.1f)
        {
            inputDirection = 0;
        }
        else
        {
            inputDirection = inputDirectionRef;
        }
            float targetSpeed = inputDirection * moveSpeed;
        float speedDif = targetSpeed - rb.linearVelocityX;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);


        //new
        if (inputDirection != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else {
            animator.SetBool("isRunning", false);
        }

        wheelTransform.Rotate(0, 0, Mathf.Abs(inputDirection * moveSpeed) * -25 * Time.deltaTime);

        if (rb.linearVelocity.x > 0.1f) {
            spriteTransform.localScale = new Vector3(spriteScale,
                                                     spriteTransform.localScale.y, spriteTransform.localScale.z);
            //new
            if (canFlip == true) {
                EventManager.Instance.PlayerFlipRightFunc();
            }
        }
        else if (rb.linearVelocity.x < -0.1f) {
            spriteTransform.localScale = new Vector3(invertedSpriteScale,
                                                     spriteTransform.localScale.y, spriteTransform.localScale.z);
            //new
            if (canFlip == true) {
                EventManager.Instance.PlayerFlipLeftFunc();
            }
        }
    }

    public void SetInputDirection(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            inputDirection = _context.ReadValue<float>();
            inputDirectionRef = _context.ReadValue<float>();

            ////new
            EventManager.Instance.PlayerMoveFunc();
        }
        else if (_context.canceled)
        {
            inputDirection = 0;
            inputDirectionRef = 0;
        }
    }

    public void Sprint(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            moveSpeed = sprintSpeed;
        }
        else if (_context.canceled)
        {
            moveSpeed = speed;
        }
    }

    //new
    public void StartWaitingBeforeFlip()
    {
        StopAllCoroutines();
        StartCoroutine(WaitBeforeFlip(1f));
    }

    //avoid camera flipping direction every time wall jump is made
    IEnumerator WaitBeforeFlip(float duration)
    {
        canFlip = false;

        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canFlip = true;
    }

    //new
    private void OnDestroy()
    {
        EventManager.Instance.PlayerWallJump -= StartWaitingBeforeFlip;
    }
}
