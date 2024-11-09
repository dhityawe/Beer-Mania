using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreText;

    private void Start()
    {
        highscoreText.text = ScoreManager.HighScore.ToString();
    }
}
