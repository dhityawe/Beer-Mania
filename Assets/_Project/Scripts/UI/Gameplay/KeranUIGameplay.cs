using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeranUIGameplay : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        EventManager.AddListener<OnKeranPouring>(OnKeranPouring);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnKeranPouring>(OnKeranPouring);
    }

    private void OnKeranPouring(OnKeranPouring e)
    {
        if (e.isPouring)
        {
            animator.SetTrigger("StartKeran");
            animator.SetBool("isKeran", true);
        }
        
        else
        {
            animator.SetBool("isKeran", false);
        }
    }
}
