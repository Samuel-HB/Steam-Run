using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Start()
    {
        moveSpeed = speed;
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
    }

    public void SetInputDirection(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            inputDirection = _context.ReadValue<float>();
            inputDirectionRef = _context.ReadValue<float>();
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
}
