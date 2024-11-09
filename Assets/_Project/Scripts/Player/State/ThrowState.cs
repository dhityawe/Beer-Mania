using UnityEngine;
using System.Collections; // To use coroutines

public class ThrowState : IPlayerState
{
    private PlayerStateManager player;

    public ThrowState(PlayerStateManager player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.spriteAnimator.PlayIfNotPlaying("Throw");

        player.fillImage.fillAmount = 0f;
        Debug.Log("Entered ThrowState");
        player.ThrowGlass();
        player.StartCoroutine(WaitForThrowAnimationToFinish()); // Start the coroutine to wait for animation
    }

    public void UpdateState() { }

    public void ExitState()
    {
        Debug.Log("Exited ThrowState");
    }

    private IEnumerator WaitForThrowAnimationToFinish()
    {
        // Wait 0.3 seconds for the animation to finish
        yield return new WaitForSeconds(0.15f);

        // After the animation is finished, move to PlayerMoveState
        player.SetState(new PlayerMoveState(player));
    }
}
