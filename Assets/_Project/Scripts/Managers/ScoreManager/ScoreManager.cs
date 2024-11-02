using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int score = 0;
    public int Score {get => score; private set { score = value; EventManager.Broadcast(new OnScoreChanged(score)); }}

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        EventManager.Broadcast(new OnScoreChanged(score));
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
}