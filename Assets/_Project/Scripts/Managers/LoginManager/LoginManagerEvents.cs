using HYPLAY.Core.Runtime;

public class OnLoggedIn : GameEvent
{
    public HyplayUser User { get; set; }
    public OnLoggedIn(HyplayUser user)
    {
        User = user;
    }
}