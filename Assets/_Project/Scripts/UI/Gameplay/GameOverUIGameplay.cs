using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIGameplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
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
    }

    public void RestartGame()
    {
        EventManager.Broadcast(new OnRestartGame());
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestRifqi");
    }
}
