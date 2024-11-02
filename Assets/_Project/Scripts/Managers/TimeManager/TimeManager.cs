using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private float gameTime = 0;
    public float GameTime { get { return gameTime; } }
    [SerializeField] private float rushHourTime = 60;
    [SerializeField] private float rushHourDuration = 20;
    private float rushHourTimer = 0;
    private bool isRushHour = false;
    private bool isGameOver = false;
    
    private void Update()
    {
        if (isGameOver) return;
        
        RunTime();
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
        isGameOver = true;
    }

    private void RunTime()
    {
        gameTime += Time.deltaTime;
        rushHourTimer += Time.deltaTime;

        CheckRushHour();
    }

    private void CheckRushHour()
    {
        if (isRushHour)
        {
            if (rushHourTimer >= rushHourDuration)
            {
                isRushHour = false;
                EventManager.Broadcast(new OnRushHour(false));
                rushHourTimer = 0;
            }
        }

        else
        {
            if (rushHourTimer >= rushHourTime)
            {
                isRushHour = true;
                EventManager.Broadcast(new OnRushHour(true));
                rushHourTimer = 0;
            }
        }
    }
}
