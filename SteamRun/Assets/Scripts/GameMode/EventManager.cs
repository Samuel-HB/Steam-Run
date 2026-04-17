using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance => instance;

    public event Action RestartLevel;
    public event Action PlayerDeath;
    public event Action EndLevelReached;
    public event Action GoNextLevel;
    public event Action AbleToPlay;
    public event Action PauseGame;
    public event Action Interact;

    public event Action PlayerFlipRight;
    public event Action PlayerFlipLeft;
    public event Action PlayerMove;
    public event Action PlayerWallJump;

    private void Awake()
    {
        if (instance) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    public void GamePauseFunc()
    {
        PauseGame.Invoke();
    }
    public void InteractFunc()
    {
        Interact.Invoke();
    }
    public void StartLevelFunc()
    {
        RestartLevel.Invoke();
    }
    public void NextLevelFunc()
    {
        GoNextLevel.Invoke();
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

    public void PlayerFlipRightFunc()
    {
        PlayerFlipRight.Invoke();
    }

    public void PlayerFlipLeftFunc()
    {
        PlayerFlipLeft.Invoke();
    }

    public void PlayerMoveFunc()
    {
        PlayerMove.Invoke();
    }

    public void PlayerWallJumpFunc()
    {
        PlayerWallJump.Invoke();
    }
}
