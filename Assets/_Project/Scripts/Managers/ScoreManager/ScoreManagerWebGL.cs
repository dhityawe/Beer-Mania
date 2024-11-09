using System;
using System.Collections;
using System.Collections.Generic;
using HYPLAY.Core.Runtime;
using HYPLAY.Leaderboards.Runtime;
using UnityEngine;

public class ScoreManagerWebGL : ScoreManager
{
    [SerializeField] private HyplayLeaderboard Leaderboard;
    public static ScoreManagerWebGL InstanceWebGL;
    List<LeaderboardScore> UserHighscores;

    protected override void Awake()
    {
        base.Awake();
        InstanceWebGL = this;
    }

    public async void GetOnlineHighscore()
    {
        if (Leaderboard == null) return;

        var scores = await Leaderboard.GetScores(HyplayLeaderboard.OrderBy.descending, 0, 10);
        if (!scores.Success)
        {
            Debug.Log($"Getting scores failed: {scores.Error}");
            return;
        }

        UserHighscores = new List<LeaderboardScore>();

        foreach (var score in scores.Data.scores)
        {
            UserHighscores.Add(score);
        }

        EventManager.Broadcast(new OnOnlineHighscoreChanged(UserHighscores));
    }

    public async void GetMyHighscore()
    {
        if (Leaderboard == null) return;

        HyplayResponse<LeaderboardScore> myScore = await Leaderboard.GetCurrentUserScore();
        if (!myScore.Success)
        {
            Debug.Log($"Getting score failed: {myScore.Error}");
            EventManager.Broadcast(new OnOnlineMyHighscoreChanged(null));
            return;
        }

        highScore = (int)myScore.Data.score;

        EventManager.Broadcast(new OnOnlineMyHighscoreChanged(myScore.Data));
    }

    protected override async void _saveHighScore()
    {
        if (Score > HighScore)
        {
            InstanceWebGL.highScore = Score;
            await Leaderboard.PostScore(Score);
        }
    }

    protected override void _resetHighScore()
    {}
}