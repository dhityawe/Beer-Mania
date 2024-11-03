using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHourEffectUIGameplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
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
        canvasGroup.alpha = evt.IsRushHour ? 1 : 0;
    }
}
