using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int lives = 3;
    public int Lives { get { return lives; } private set { lives = value; EventManager.Broadcast(new OnLiveChanged(lives)); } }
    private bool isGameStopped = false;
    public static bool IsGameStopped { get { return Instance.isGameStopped; } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable()
    {
        EventManager.AddListener<OnLiveLost>(OnLiveLost);
        EventManager.AddListener<OnRestartGame>(OnRestartGame);
        EventManager.AddListener<OnRushHour>(OnRushHour);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnLiveLost>(OnLiveLost);
        EventManager.RemoveListener<OnRestartGame>(OnRestartGame);
        EventManager.RemoveListener<OnRushHour>(OnRushHour);
    }

    private void OnRushHour(OnRushHour evt)
    {
        Time.timeScale = evt.IsRushHour ? 3 : 1f;
    }

    private void OnLiveLost(OnLiveLost evt)
    {
        Lives -= evt.Lives;
        isGameStopped = true;

        if (Lives <= 0)
        {
            StartCoroutine(GameOverRoutine());
        }

        else
        {
            StartCoroutine(LiveLostRoutine());
        }
    }

    private IEnumerator LiveLostRoutine()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestRifqi");
        Time.timeScale = 1f;
        isGameStopped = false;
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(3f);
        EventManager.Broadcast(new OnGameOver());
    }

    private void OnRestartGame(OnRestartGame evt)
    {
        Lives = 3;
        Time.timeScale = 1f;
        isGameStopped = false;
        ScoreManager.SaveHighScore();
        ScoreManager.ResetScore();
    }
}
