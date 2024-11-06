using UnityEngine;
using System;

public class PlayerMoveState : IPlayerState
{
    private PlayerStateManager player;
    private float leftBoundary = -6.6f;
    private float rightBoundary = 7.85f;

    public PlayerMoveState(PlayerStateManager player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        Debug.Log("PlayerMoveState: EnterState");
    }

    public void UpdateState()
    {
        if (GameManager.IsGameStopped)
        {
            return;
        }

        bool isMoving = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new PourState(player));
        }

        // Only allow left movement if player's x position is greater than the left boundary
        if (Input.GetKey(KeyCode.LeftArrow) && player.transform.position.x > leftBoundary)
        {
            player.MoveLeft();
            isMoving = true;
        }

        // Only allow right movement if player's x position is less than the right boundary
        if (Input.GetKey(KeyCode.RightArrow) && player.transform.position.x < rightBoundary)
        {
            player.MoveRight();
            isMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.SwitchTable(-1);
            isMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.SwitchTable(1);
            isMoving = true;
        }

        // If no movement keys are pressed, play the Idle animation
        if (!isMoving && player.spriteAnimator != null)
        {
            player.spriteAnimator.PlayIfNotPlaying("Idle");
        }
    }

    public void ExitState()
    {
        Debug.Log("PlayerMoveState: ExitState");
    }
}
