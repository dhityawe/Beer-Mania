using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHourUIGameplay : MonoBehaviour
{
 [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnRushHour>(OnRushHour);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnRushHour>(OnRushHour);
    }

    private void OnRushHour(OnRushHour evt)
    {
        if (evt.IsRushHour)
        {
            animator.SetTrigger("RUSH");
        }
    }
}
