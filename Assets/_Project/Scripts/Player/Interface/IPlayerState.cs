public interface IPlayerState
{
    void EnterState(PlayerStateManager player);
    void UpdateState(PlayerStateManager player);
    void ExitState(PlayerStateManager player);
}
