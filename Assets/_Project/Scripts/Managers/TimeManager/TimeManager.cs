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
    private bool isRushHourReady = false;
    private bool isRushHour = false;
    
    private void Update()
    {
        if (GameManager.IsGameStopped) return;
        
        RunTime();
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnCustomerSterilized>(StartRushHour);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnCustomerSterilized>(StartRushHour);
    }

    private void StartRushHour(OnCustomerSterilized evt)
    {
        AudioManager.PlaySound("rush_hour");
        AudioManager.StopSound("bgm");
        AudioManager.PlaySound("rush_hour_bgm");
        isRushHour = true;
        EventManager.Broadcast(new OnRushHour(true));
        rushHourTimer = 0;
        isRushHourReady = false;
    }

    private void RunTime()
    {
        if (isRushHour)
        {
            rushHourTimer += Time.unscaledDeltaTime;
        }

        else
        {
            rushHourTimer += Time.deltaTime;
        }

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
                AudioManager.StopSound("rush_hour_bgm");
                AudioManager.PlaySound("bgm");
            }
        }

        else
        {
            if (rushHourTimer >= rushHourTime && !isRushHourReady)
            {
                isRushHourReady = true;
                EventManager.Broadcast(new OnRushHourReady());
            }
        }
    }
}
