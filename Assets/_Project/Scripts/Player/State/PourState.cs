using System;
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
        EventManager.Broadcast(new OnBarrelPouring(player.CurrentTableIndex, true));
        EventManager.Broadcast(new OnKeranPouring(true));
        Debug.Log("Entered PlayerPourState");
    }

    public void UpdateState()
    {
        if (player.fillLevel >= 0.3f)
        {
            player.ParameterImage.SetActive(true);
        }
        else 
        {
            player.ParameterImage.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // Resume the animation if it was paused
            player.spriteAnimator.Resume();

            // Trigger environmental animation event
            EnviEventManager.Instance.RaiseEvent("StartPouringAnimation");

            // Update UI fill level and check if pouring is complete
            player.fillImage.fillAmount = player.fillLevel;

            if (player.PourBeer())
            {
                EventManager.Broadcast(new OnBarrelPouring(player.CurrentTableIndex, false));
                EventManager.Broadcast(new OnKeranPouring(false));
                player.SetState(new ThrowState(player));
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // Broadcast events to stop pouring
            EventManager.Broadcast(new OnKeranPouring(false));
            EventManager.Broadcast(new OnBarrelPouring(player.CurrentTableIndex, false));
            player.SetState(new ThrowState(player));
        }



        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            EventManager.Broadcast(new OnKeranPouring(false));
            EventManager.Broadcast(new OnBarrelPouring(player.CurrentTableIndex, false));
            player.SwitchTable(-1);
            player.CancelPouring();
            player.SetState(new PlayerMoveState(player));
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            EventManager.Broadcast(new OnKeranPouring(false));
            EventManager.Broadcast(new OnBarrelPouring(player.CurrentTableIndex, false));
            player.SwitchTable(1);
            player.CancelPouring();
            player.SetState(new PlayerMoveState(player));
        }
    }


    public void ExitState()
    {
        player.StopPouring();
        Debug.Log("Exited PlayerPourState");
    }
}