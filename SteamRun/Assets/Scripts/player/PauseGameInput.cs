using UnityEngine;

public class PauseGameInput : MonoBehaviour
{
    public void PauseGame()
    {
        EventManager.Instance.GamePauseFunct();
    }
}
