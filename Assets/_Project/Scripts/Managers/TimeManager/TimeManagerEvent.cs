public class OnRushHour : GameEvent
{
    public bool IsRushHour { get; private set; }

    public OnRushHour(bool isRushHour)
    {
        IsRushHour = isRushHour;
    }
}