using System.Collections.Generic;

public class AddScore : GameEvent
{
    public int Score { get; }

    public AddScore(int score)
    {
        Score = score;
    }
}

public class OnScoreChanged : GameEvent
{
    public int Score { get; }

    public OnScoreChanged(int score)
    {
        Score = score;
    }
}

public class OnOnlineHighscoreChanged : GameEvent
{
    public List<LeaderboardScore> UserHighscores { get; }

    public OnOnlineHighscoreChanged(List<LeaderboardScore> userHighscores)
    {
        UserHighscores = userHighscores;
    }
}

public class OnOnlineMyHighscoreChanged : GameEvent
{
    public LeaderboardScore MyHighscore { get; }

    public OnOnlineMyHighscoreChanged(LeaderboardScore myHighscore)
    {
        MyHighscore = myHighscore;
    }
}