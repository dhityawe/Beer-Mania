using System;
using System.Collections;
using System.Collections.Generic;
using HYPLAY.Core.Runtime;
using UnityEngine;

public class LoginManager : Singleton<LoginManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (HyplayBridge.IsLoggedIn)
        {
            EventManager.Broadcast(new OnLoggedIn(HyplayBridge.CurrentUser));
        }

        else
        {
            HyplayBridge.GuestLoginAndReturnUser((onComplete) =>
            {
                if (onComplete.Success)
                {
                    HyplayUser user = onComplete.Data;
                    OnGuestLogin(user);
                }
            });
        }
    }

    private void OnGuestLogin(HyplayUser user)
    {
        EventManager.Broadcast(new OnLoggedIn(user));
    }
}