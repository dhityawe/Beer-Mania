public class OnLiveLost : GameEvent
{
    public int Lives { get; private set; }

    public OnLiveLost(int lives)
    {
        Lives = lives;
    }
}

public class OnLiveChanged : GameEvent
{
    public int Lives { get; private set; }

    public OnLiveChanged(int lives)
    {
        Lives = lives;
    }
}

public class OnGameOver : GameEvent
{
    public OnGameOver()
    {
    }
}

public class OnRestartGame : GameEvent
{
    public OnRestartGame()
    {
    }
}