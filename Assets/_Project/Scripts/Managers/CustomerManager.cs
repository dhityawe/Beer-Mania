using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomerSpawnData
{
    public GameObject CustomerPrefab;
    public int SpawnWeight;
    [HideInInspector] public float SpawnChance;
}

public class CustomerManager : Singleton<CustomerManager>
{
    private List<CustomerSpawnPoint> customerSpawnPoints = new List<CustomerSpawnPoint>();
    public static List<CustomerSpawnPoint> CustomerSpawnPoints { get => Instance.customerSpawnPoints; }
    [SerializeField] private List<CustomerSpawnData> customerSpawnData = new List<CustomerSpawnData>();
    private List<Customer> activeCustomers = new List<Customer>();
    private CustomerSpawnPoint lastCustomerSpawnPoint;
    private int customerSpawnPointStreak = 0;
    [SerializeField] [Range(0, 1)] private float spawnChance = 0.5f;
    [SerializeField] private float spawnRate = 1f;
    private float spawnTimer = 0f;
    private bool isRushHourReady = false;

    protected override void Awake()
    {
        base.Awake();
        CalculateSpawnWeights();
    }

    private void OnEnable()
    {
        EventManager.AddListener<RegisterCustomerSpawnPoint>(OnRegisterCustomerSpawnPoint);
        EventManager.AddListener<OnRushHourReady>(OnRushHourReady);
        EventManager.AddListener<OnRushHour>(OnRushHour);
        EventManager.AddListener<OnCustomerLeft>(OnCustomerLeft);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<RegisterCustomerSpawnPoint>(OnRegisterCustomerSpawnPoint);
        EventManager.RemoveListener<OnRushHourReady>(OnRushHourReady);
        EventManager.RemoveListener<OnRushHour>(OnRushHour);
        EventManager.RemoveListener<OnCustomerLeft>(OnCustomerLeft);
    }

    private void Update()
    {
        if (GameManager.IsGameStopped) return;
        if (isRushHourReady) return;

        TrySpawnCustomer();
    }

    private void OnRushHour(OnRushHour evt)
    {
        StartCoroutine(OnRushHour());
    }

    private void OnRegisterCustomerSpawnPoint(RegisterCustomerSpawnPoint evt)
    {
        customerSpawnPoints.Add(evt.CustomerSpawnPoint);
    }

    private void OnRushHourReady(OnRushHourReady evt)
    {
        isRushHourReady = true;

        if (activeCustomers.Count == 0)
        {
            EventManager.Broadcast(new OnCustomerSterilized());
        }
    }

    private IEnumerator OnRushHour()
    {
        isRushHourReady = false;
        yield return null;
    }

    private void OnCustomerLeft(OnCustomerLeft evt)
    {
        activeCustomers.Remove(evt.Customer);

        if (isRushHourReady && activeCustomers.Count == 0)
        {
            EventManager.Broadcast(new OnCustomerSterilized());
        }
    }

    private void CalculateSpawnWeights()
    {
        int totalWeight = 0;
        foreach (var customer in customerSpawnData)
        {
            totalWeight += customer.SpawnWeight;
        }

        foreach (var customer in customerSpawnData)
        {
            customer.SpawnChance = (float)customer.SpawnWeight / totalWeight * 100f;
            customer.SpawnChance /= 100f;
            customer.SpawnChance = (float)Math.Round(customer.SpawnChance, 2);
        }
        
        customerSpawnData.Sort((a, b) => a.SpawnChance.CompareTo(b.SpawnChance));
    }

    private void TrySpawnCustomer()
    {
        if (spawnTimer <= spawnRate)
        {
            spawnTimer += Time.deltaTime;
            return;
        }

        if (UnityEngine.Random.value < spawnChance)
        {
            RandomSpawnCustomer();
        }

        spawnTimer = 0f;
    }

    private void RandomSpawnCustomer()
    {
        CustomerSpawnPoint customerSpawnPoint = customerSpawnPoints[UnityEngine.Random.Range(0, customerSpawnPoints.Count)];

        if (customerSpawnPoint == lastCustomerSpawnPoint)
        {
            customerSpawnPointStreak++;
        }

        else
        {
            lastCustomerSpawnPoint = customerSpawnPoint;
            customerSpawnPointStreak = 0;
        }

        if (customerSpawnPointStreak >= 3)
        {
            List<CustomerSpawnPoint> customerSpawnPointsCopy = new List<CustomerSpawnPoint>(customerSpawnPoints);
            customerSpawnPointsCopy.Remove(lastCustomerSpawnPoint);
            customerSpawnPoint = customerSpawnPointsCopy[UnityEngine.Random.Range(0, customerSpawnPointsCopy.Count)];
            customerSpawnPointStreak = 0;
        }
        
        GameObject customerPrefab = customerSpawnData[0].CustomerPrefab;

        foreach (var customerSpawnData in customerSpawnData)
        {
            float randomValue = UnityEngine.Random.Range(0f, 1f);
            randomValue = (float)Math.Round(randomValue, 2);
            if (randomValue < customerSpawnData.SpawnChance)
            {
                customerPrefab = customerSpawnData.CustomerPrefab;
                break;
            }
        }
        
        Customer customer = SpawnCustomer(customerSpawnPoint, customerPrefab);
        customer.CustomerSpawnPoint = customerSpawnPoint;
        customer.SetLane(customerSpawnPoint.Lane);
        activeCustomers.Add(customer);
    }

    private Customer SpawnCustomer(CustomerSpawnPoint customerSpawnPoint, GameObject customerPrefab)
    {
        Customer customer = Instantiate(customerPrefab, customerSpawnPoint.transform.position, Quaternion.identity).GetComponent<Customer>();
        return customer;
    }
}
