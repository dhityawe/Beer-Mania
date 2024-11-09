using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreUIGameplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

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
        scoreText.text = ScoreManager.Score.ToString();
    }
}
