public class RegisterCustomerSpawnPoint : GameEvent
{
    public CustomerSpawnPoint CustomerSpawnPoint { get; }

    public RegisterCustomerSpawnPoint(CustomerSpawnPoint customerSpawnPoint)
    {
        CustomerSpawnPoint = customerSpawnPoint;
    }
}

public class OnCustomerArrivedAtCounter : GameEvent
{
    public Customer Customer { get; }

    public OnCustomerArrivedAtCounter(Customer customer)
    {
        Customer = customer;
    }
}

public class OnCustomerSterilized : GameEvent
{}

public class OnCustomerLeft : GameEvent
{
    public Customer Customer { get; }

    public OnCustomerLeft(Customer customer)
    {
        Customer = customer;
    }
}

public class TriggerKillFeed : GameEvent
{
    public KillfeedUIGameplay.KillfeedType KillfeedType { get; }

    public TriggerKillFeed(KillfeedUIGameplay.KillfeedType killfeedType)
    {
        KillfeedType = killfeedType;
    }
}