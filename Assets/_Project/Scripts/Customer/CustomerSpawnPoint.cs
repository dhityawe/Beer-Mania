using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnPoint : MonoBehaviour
{
    [SerializeField] private CustomerDeadlinePoint customerDeadlinePoint;
    public CustomerDeadlinePoint CustomerDeadlinePoint { get => customerDeadlinePoint; }

    private void Start()
    {
        EventManager.Broadcast(new RegisterCustomerSpawnPoint(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);

        Gizmos.DrawLine(transform.position, customerDeadlinePoint.transform.position);

        if (customerDeadlinePoint == null)
        {
            Gizmos.color = Color.red;
        }
    }
}
