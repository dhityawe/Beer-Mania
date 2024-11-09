using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int score = 0;
    private int highScore = 0;
    public static int HighScore {get => Instance.highScore;}
    public static int Score {get => Instance.score; private set { Instance.score = value; EventManager.Broadcast(new OnScoreChanged(Instance.score)); }}

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    private void OnEnable()
    {
        EventManager.AddListener<AddScore>(OnAddScore);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<AddScore>(OnAddScore);
    }

    private void OnAddScore(AddScore evt)
    {
        Score += evt.Score;
    }

    public static void SaveHighScore()
    {
        if (Score > HighScore)
        {
            Instance.highScore = Score;
            PlayerPrefs.SetInt("HighScore", Score);
            PlayerPrefs.Save();
        }
    }

    public static void ResetScore()
    {
        Score = 0;
    }

    public static void ResetHighScore()
    {
        Instance.highScore = 0;
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
    }
}