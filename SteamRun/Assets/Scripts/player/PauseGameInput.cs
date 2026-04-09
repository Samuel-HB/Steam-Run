using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGameInput : MonoBehaviour
{
    public void PauseGame(InputAction.CallbackContext _context)
    {
        EventManager.Instance.GamePauseFunc();
    }
}
