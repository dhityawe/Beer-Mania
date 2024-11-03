// File: AnimationEventListener.cs
using UnityEngine;

public class AnimationEventListener : MonoBehaviour, IEnviEventListener
{
    private Animator animator;
    [SerializeField] private int laneIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnBarrelPouring>(OnBarrelPouring);
        //EnviEventManager.Instance.RegisterListener("StartPouringAnimation", this);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnBarrelPouring>(OnBarrelPouring);
        //EnviEventManager.Instance.UnregisterListener("StartPouringAnimation", this);
    }

    public void OnBarrelPouring(OnBarrelPouring e)
    {
        if (laneIndex != e.laneIndex) return;

        if (e.isPouring)
        {
            animator.SetTrigger("Pour"); // Make sure "Pour" is defined as a trigger in your Animator
            animator.SetBool("IsPouring", true);
        }
        else
        {
            animator.SetBool("IsPouring", false);
        }
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
