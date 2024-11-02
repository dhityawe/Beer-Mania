using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public void EnterState(PlayerStateManager player)
    {
        Debug.Log("PlayerMoveState: EnterState");
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.SwitchTable(-1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.SwitchTable(1);
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        Debug.Log("PlayerMoveState: ExitState");
    }
}
