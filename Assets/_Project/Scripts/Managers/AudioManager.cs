using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioData
{
    public string name;
    public AudioSource audioSource;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioData[] audioData;

    public static void PlaySound(string name)
    {
        foreach (var data in Instance.audioData)
        {
            if (data.name == name)
            {
                data.audioSource.Play();
                return;
            }
        }

        Debug.LogError($"Sound with name {name} not found");
    }

    public static void StopSound(string name)
    {
        foreach (var data in Instance.audioData)
        {
            if (data.name == name)
            {
                data.audioSource.Stop();
                return;
            }
        }
    }
}