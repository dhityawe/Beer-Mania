using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private List<CustomerSpawnPoint> customerSpawnPoints = new List<CustomerSpawnPoint>();
    [SerializeField] private List<GameObject> customerPrefabs;
    private CustomerSpawnPoint lastCustomerSpawnPoint;
    private int customerSpawnPointStreak = 0;
    [SerializeField] [Range(0, 1)] private float spawnChance = 0.5f;
    [SerializeField] private float spawnRate = 1f;
    private float spawnTimer = 0f;

    private void OnEnable()
    {
        EventManager.AddListener<RegisterCustomerSpawnPoint>(OnRegisterCustomerSpawnPoint);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<RegisterCustomerSpawnPoint>(OnRegisterCustomerSpawnPoint);
    }

    private void Update()
    {
        TrySpawnCustomer();
    }

    private void OnRegisterCustomerSpawnPoint(RegisterCustomerSpawnPoint evt)
    {
        customerSpawnPoints.Add(evt.CustomerSpawnPoint);
    }

    private void TrySpawnCustomer()
    {
        if (spawnTimer <= spawnRate)
        {
            spawnTimer += Time.deltaTime;
            return;
        }

        if (Random.value < spawnChance)
        {
            RandomSpawnCustomer();
        }

        spawnTimer = 0f;
    }

    private void RandomSpawnCustomer()
    {
        CustomerSpawnPoint customerSpawnPoint = customerSpawnPoints[Random.Range(0, customerSpawnPoints.Count)];

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
            customerSpawnPoint = customerSpawnPointsCopy[Random.Range(0, customerSpawnPointsCopy.Count)];
            customerSpawnPointStreak = 0;
        }

        GameObject customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Count)];
        Customer customer = SpawnCustomer(customerSpawnPoint, customerPrefab);
        customer.CustomerSpawnPoint = customerSpawnPoint;
    }

    private Customer SpawnCustomer(CustomerSpawnPoint customerSpawnPoint, GameObject customerPrefab)
    {
        Customer customer = Instantiate(customerPrefab, customerSpawnPoint.transform.position, Quaternion.identity).GetComponent<Customer>();
        return customer;
    }
}
