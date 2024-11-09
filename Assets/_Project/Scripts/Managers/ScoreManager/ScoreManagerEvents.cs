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