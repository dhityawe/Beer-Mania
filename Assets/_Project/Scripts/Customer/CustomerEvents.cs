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