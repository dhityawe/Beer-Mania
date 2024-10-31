using UnityEngine; // Ensure this is included if PlayerMoveState is in the same namespace

public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState currentState;

    [SerializeField] private float moveSpeed = 5f;


    private void Start()
    {
        currentState = new PlayerMoveState(this);
        currentState.EnterState();
    }

    private void Update()
    {
        currentState.UpdateState(); // Ensure no arguments are passed
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

}