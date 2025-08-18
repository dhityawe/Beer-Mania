using System.Collections;
using System.Collections.Generic;
using HYPLAY.Core.Runtime;
using TMPro;
using UnityEngine;

public class HighscoreUIWebGL : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private GameObject loading;


    private void Awake()
    {
        ShowHighscore(false);
    }
    
    private void Start()
    {
        if (HyplayBridge.IsLoggedIn)
        {
            ScoreManagerWebGL.InstanceWebGL.GetMyHighscore();
            ShowHighscore(true);
        }
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnOnlineMyHighscoreChanged>(OnOnlineMyHighscoreChanged);
        EventManager.AddListener<OnLoggedIn>(OnLoggedIn);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnOnlineMyHighscoreChanged>(OnOnlineMyHighscoreChanged);
        EventManager.RemoveListener<OnLoggedIn>(OnLoggedIn);
    }

    private void OnOnlineMyHighscoreChanged(OnOnlineMyHighscoreChanged evt)
    {
        if (evt.MyHighscore == null)
        {
            highscoreText.text = $"Highscore: 0";
            ShowHighscore(true);
            return;
        }

        highscoreText.text = $"Highscore: {evt.MyHighscore.score}";
        ShowHighscore(true);
    }

    private void OnLoggedIn(OnLoggedIn evt)
    {
        ScoreManagerWebGL.InstanceWebGL.GetMyHighscore();
    }

    private void ShowHighscore(bool show)
    {
        loading.SetActive(!show);
        highscoreText.gameObject.SetActive(show);
    }
}
