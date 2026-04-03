using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;

    private Vector2 inputDirection;

    // Update is called once per frame

    private void Start()
    {
        //SC_InputManager.instance.OnMovement += SetInputDirection();
    }
    private void OnDestroy()
    {
        
    }
    private void FixedUpdate()
    {
        
    }

    private void SetInputDirection(InputAction.CallbackContext _context)
    {

    }
}
