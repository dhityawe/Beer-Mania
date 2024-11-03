using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIGameplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private MenuSelectionUIMenu menuSelection;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        menuSelection.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnGameOver>(OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnGameOver>(OnGameOver);
    }

    private void OnGameOver(OnGameOver evt)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        menuSelection.enabled = true;
    }

    public void RestartGame()
    {
        EventManager.Broadcast(new OnRestartGame());
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestRifqi");
    }
}
