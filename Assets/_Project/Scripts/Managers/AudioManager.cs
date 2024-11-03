using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
use this Audio Manager script in every scene if you want play the audio
this script is reusable

*/


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;
    [Space]
    [Header("music")]
    public List<AudioClip> musicClips = new();
    [Space]
    [Header("sfx")]
    public List<AudioClip> sfxClips = new();

    public static AudioManager instance; //* UPDATE !!! - static instance
    

    public void SetMusic(AudioClip clip) {
        musicSource.clip = clip;
        musicSource.Play();
    }

    void Awake() {
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        SetMusic(musicSource.clip);
    }

    public void SetSFX(AudioClip clip) {
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
    }

    public static void StopMusic() {
        instance.musicSource.Stop();
    }

    public static void StopSfx()
    {
        instance.sfxSource.Stop();
    }

}