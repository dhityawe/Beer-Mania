using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIGameplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private Coroutine scoreUpAnimationCoroutine;
    private int realTargetScore;

    private void Start()
    {
        scoreText.text = ScoreManager.Score.ToString();
    }

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
        if (scoreUpAnimationCoroutine != null)
        {
            scoreText.text = realTargetScore.ToString();
            StopCoroutine(scoreUpAnimationCoroutine);
        }
        
        scoreUpAnimationCoroutine = StartCoroutine(ScoreUpAnimation(evt.Score));
    }

    private IEnumerator ScoreUpAnimation(int score)
    {
        realTargetScore = ScoreManager.Score + score;

        int currentScore = int.Parse(scoreText.text);
        int targetScore = currentScore + score;
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            int newScore = (int)Mathf.Lerp(currentScore, targetScore, t);
            scoreText.text = newScore.ToString();
            yield return null;
        }

        scoreText.text = targetScore.ToString();
        scoreUpAnimationCoroutine = null;
    }
}
