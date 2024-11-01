using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIGameplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        EventManager.AddListener<OnScoreChanged>(OnScoreChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnScoreChanged>(OnScoreChanged);
    }

    private void OnScoreChanged(OnScoreChanged evt)
    {
        scoreText.text = evt.Score.ToString();
    }
}
