using UnityEngine;

public class ThrowState : IPlayerState
{
    private PlayerStateManager player;

    public ThrowState(PlayerStateManager player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        Debug.Log("Entered ThrowState");
        player.ThrowGlass();
        player.SetState(new PlayerMoveState(player));
    }

    public void UpdateState() { }

    public void ExitState()
    {
        Debug.Log("Exited ThrowState");
    }
}