using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreUIGameplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private Coroutine scoreUpAnimationCoroutine;
    private int realTargetScore;

    private void Update()
    {
        scoreText.text = ScoreManager.Score.ToString();
    }
}
