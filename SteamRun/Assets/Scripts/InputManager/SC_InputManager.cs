using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_InputManager : MonoBehaviour
{
    public static SC_InputManager instance { get; private set; }

    public event Action onLeftArrowPressStarted;
    public event Action onLeftArrowPressCanceled;

    public event Action onRightArrowPressStarted;
    public event Action onRightArrowPressCanceled;

    public event Action onShiftPressStarted;
    public event Action onShiftPressCanceled;

    public event Action onKeySpacePressStarted;
    public event Action onKeySpacePressCanceled;

    public event Action onEscapeButtonPressStarted;

    public event Action onInteractButtonPress;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OnEscapeButtonPressed(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            onEscapeButtonPressStarted?.Invoke();
        }
    }

    public void OnRightArrowPressStarted(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            onRightArrowPressStarted?.Invoke();
        }
        else if (_context.canceled)
        {
            onRightArrowPressCanceled?.Invoke();
        }
    }

    public void OnLeftArrowPressStarted(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            onLeftArrowPressStarted?.Invoke();
        }
        else if (_context.canceled)
        {
            onLeftArrowPressCanceled?.Invoke();
        }
    }

    public void OnShiftPressedStarted(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            onShiftPressStarted?.Invoke();
        }
        else if (_context.canceled)
        {
            onShiftPressCanceled?.Invoke();
        }
    }

    public void OnSpacePressStarted(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            onKeySpacePressStarted?.Invoke();
        }
        else if (_context.canceled)
        {
            onKeySpacePressCanceled?.Invoke();
        }
    }

    public void OnInteractPress(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            onShiftPressStarted?.Invoke();
        }
    }
}
