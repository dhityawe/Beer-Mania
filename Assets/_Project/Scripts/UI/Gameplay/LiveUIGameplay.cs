using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveUIGameplay : MonoBehaviour
{
    [SerializeField] private GameObject liveIconPrefab;

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnLiveChanged>(OnLiveChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnLiveChanged>(OnLiveChanged);
    }

    private void OnLiveChanged(OnLiveChanged evt)
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.Instance.Lives; i++)
        {
            RectTransform liveIcon = Instantiate(liveIconPrefab, transform).GetComponent<RectTransform>();
            liveIcon.SetParent(transform);
        }
    }
}