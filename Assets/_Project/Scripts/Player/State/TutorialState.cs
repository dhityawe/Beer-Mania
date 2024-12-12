using UnityEngine;

public class TutorialState : IPlayerState
{
    private PlayerStateManager player;

    public TutorialState(PlayerStateManager player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        if (GameManager.Instance.Lives == 3)
        {
            ShowTutorialPanel();
        }
        else
        {
            player.SetState(new PlayerMoveState(player));
        }
    }

    public void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // set active false tutorialPanel
            HideTutorialPanel();
        }
    }

    public void ExitState()
    {

    }

    void ShowTutorialPanel()
    {
        Time.timeScale = 0;
        player.TutorialPanel.SetActive(true);
    }

    void HideTutorialPanel()
    {
        player.TutorialPanel.SetActive(false);
        Time.timeScale = 1;
        player.SetState(new PlayerMoveState(player));
    }
}
