using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance => instance;

    public event Action RestartLevel;
    public event Action PlayerDeath;
    public event Action EndLevelReached;
    public event Action AbleToPlay;
    public event Action PauseGame;

    private void Awake()
    {
        if (instance) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    public void GamePauseFunct()
    {
        PauseGame.Invoke();
    }
    public void StartLevelFunc()
    {
        RestartLevel.Invoke();
    }

    public void PlayerDeathFunc()
    {
        PlayerDeath.Invoke();
    }

    public void EndLevelFunc()
    {
        EndLevelReached.Invoke();
    }

    public void AbleToPlayFunc()
    {
        AbleToPlay.Invoke();
    }
}
