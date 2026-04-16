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
    //new
    public event Action PlayerFlipRight;
    public event Action PlayerFlipLeft;
    public event Action PlayerMove;

    //new
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

    //new
    public void PlayerFlipRightFunc()
    {
        PlayerFlipRight.Invoke();
    }

    //new
    public void PlayerFlipLeftFunc()
    {
        PlayerFlipLeft.Invoke();
    }

    //new
    public void PlayerMoveFunc()
    {
        PlayerMove.Invoke();
    }

    //new
    public void PlayerWallJumpFunc()
    {
        PlayerWallJump.Invoke();
    }
}
