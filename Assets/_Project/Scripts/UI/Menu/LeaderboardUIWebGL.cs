using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardUIWebGL : MonoBehaviour
{
    [SerializeField] private GameObject loading;
    [SerializeField] private List<TextMeshProUGUI> highscoreTexts;
    [SerializeField] private List<TextMeshProUGUI> usernameTexts;

    private void Awake()
    {
        ShowHighscore(false);
    }

    private void Start()
    {
        ScoreManagerWebGL.InstanceWebGL.GetOnlineHighscore();
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnOnlineHighscoreChanged>(OnOnlineHighscoreChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnOnlineHighscoreChanged>(OnOnlineHighscoreChanged);
    }

    private void OnOnlineHighscoreChanged(OnOnlineHighscoreChanged evt)
    {   
        for (int i = 0; i < highscoreTexts.Count; i++)
        {
            if (i < evt.UserHighscores.Count)
            {
                highscoreTexts[i].text = $"{evt.UserHighscores[i].score}";
                usernameTexts[i].text = evt.UserHighscores[i].username;
            }
            else
            {
                highscoreTexts[i].text = "";
                usernameTexts[i].text = "";
            }
        }

        ShowHighscore(true);
    }

    private void ShowHighscore(bool show)
    {
        loading.SetActive(!show);
        foreach (var highscoreText in highscoreTexts)
        {
            highscoreText.gameObject.SetActive(show);
        }
        foreach (var usernameText in usernameTexts)
        {
            usernameText.gameObject.SetActive(show);
        }
    }
}
