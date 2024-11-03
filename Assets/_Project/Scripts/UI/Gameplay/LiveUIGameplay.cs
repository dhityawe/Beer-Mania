using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveUIGameplay : MonoBehaviour
{
    [SerializeField] private GameObject liveIconPrefab;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

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
        int currentLives = transform.childCount;
        Initialize();

        if (currentLives > evt.Lives)
        {
            print(currentLives + " " + evt.Lives);
            StartCoroutine(LiveLostRoutine());
        }
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

    private IEnumerator LiveLostRoutine()
    {
        for (int i = 0; i < 6; i++)
        {
            canvasGroup.alpha = 0f;
            yield return new WaitForSeconds(0.25f);
            canvasGroup.alpha = 1f;
            yield return new WaitForSeconds(0.25f);
        }
    }
}