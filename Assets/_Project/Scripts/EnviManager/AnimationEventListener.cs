// File: AnimationEventListener.cs
using UnityEngine;

public class AnimationEventListener : MonoBehaviour, IEnviEventListener
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EnviEventManager.Instance.RegisterListener("StartPouringAnimation", this);
    }

    private void OnDisable()
    {
        EnviEventManager.Instance.UnregisterListener("StartPouringAnimation", this);
    }

    public void OnEventRaised(string eventName)
    {
        if (eventName == "StartPouringAnimation")
        {
            animator.SetTrigger("Pour"); // Make sure "Pour" is defined as a trigger in your Animator
            animator.SetBool("IsPouring", true);
        }

        if (eventName == "StopPouringAnimation")
        {
            animator.SetBool("IsPouring", false);
        }
    }
}
