using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private readonly PlayerStateManager playerStateManager;

    public PlayerMoveState(PlayerStateManager playerStateManager)
    {
        this.playerStateManager = playerStateManager;
    }

    public void EnterState()
    {
        Debug.Log("PlayerMoveState: EnterState");
    }

    public void UpdateState()
    {
        Debug.Log("PlayerMoveState: UpdateState");
    }

    public void ExitState()
    {
        Debug.Log("PlayerMoveState: ExitState");
    }
}