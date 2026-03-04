using UnityEngine;
using UnityEngine.Events;

public class GameMode : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // quand joueur mort ou niveau stoppé
        {
            EventManager.Instance.PlayerDeathFunc();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            EventManager.Instance.StartLevelFunc();
        }
    }
}
