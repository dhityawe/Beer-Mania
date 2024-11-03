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
        player.StartPouring();
        Debug.Log("Entered PlayerPourState");
    }

    public void UpdateState()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Trigger environmental animation event
            EnviEventManager.Instance.RaiseEvent("StartPouringAnimation");

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
        player.StopPouring();
        Debug.Log("Exited PlayerPourState");
    }
}
