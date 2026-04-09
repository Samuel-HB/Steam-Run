using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    public void Interaction(InputAction.CallbackContext _context)
    {
        EventManager.Instance.InteractFunc();
    }
}
