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

    private void Awake()
    {
        if (instance) {
            Destroy(this);
        }
        else {
            instance = this;
        }
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
