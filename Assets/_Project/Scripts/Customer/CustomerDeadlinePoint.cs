using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDeadlinePoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
