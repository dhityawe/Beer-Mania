using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIGameplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private Coroutine scoreUpAnimationCoroutine;
    private int realScore;

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
            StopCoroutine(scoreUpAnimationCoroutine);
            scoreText.text = realScore.ToString();
        }

        realScore = evt.Score;
        
        scoreUpAnimationCoroutine = StartCoroutine(ScoreUpAnimation());
    }

    private IEnumerator ScoreUpAnimation()
    {
        AudioManager.PlaySound("ScoreUp");

        int currentScore = int.Parse(scoreText.text);
        int targetScore = realScore;
        float duration = 0.85f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            int newScore = (int)Mathf.Lerp(currentScore, targetScore, t);
            scoreText.text = newScore.ToString();
            yield return null;
        }

        scoreText.text = realScore.ToString();
        AudioManager.StopSound("ScoreUp");
        scoreUpAnimationCoroutine = null;
    }
}
