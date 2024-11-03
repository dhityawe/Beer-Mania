using UnityEngine;
using UnityEngine.UI;

public class PourState : IPlayerState
{
    private PlayerStateManager player;

    public PourState(PlayerStateManager player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        
        Debug.Log("Entered PlayerPourState");
        player.StartPouring();
    }

    public void UpdateState()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //* Should be playing cross-in glass animation here
            //* Should be playing a start pouring animation here

            player.fillImage.fillAmount = player.fillLevel;

            if (player.PourBeer())
            {
                player.SetState(new ThrowState(player));
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            player.SetState(new ThrowState(player));
        }
    }

    public void ExitState()
    {
        Debug.Log("Exited PlayerPourState");
    }
}