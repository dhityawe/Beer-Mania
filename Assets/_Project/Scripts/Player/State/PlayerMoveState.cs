using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private PlayerStateManager player;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new PourState(player));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.StartMovingLeft();
            player.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player.StartMovingRight();
            player.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.SwitchTable(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.SwitchTable(1);
        }
        else
        {
            player.StopMoving();
        }
    }

    public void ExitState()
    {
        Debug.Log("PlayerMoveState: ExitState");
    }
}
