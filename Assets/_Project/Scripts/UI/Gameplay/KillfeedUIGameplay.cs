using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillfeedUIGameplay : MonoBehaviour
{
    public enum KillfeedType { Perfect, Good, Bad }

    public List<GameObject> killfeedPrefabs;
    private float timer;

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= 5)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        EventManager.AddListener<TriggerKillFeed>(TriggerKillfeed);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<TriggerKillFeed>(TriggerKillfeed);
    }

    public void TriggerKillfeed(TriggerKillFeed evt)
    {
        timer = 0;
        
        GameObject killfeedPrefab = killfeedPrefabs[(int)evt.KillfeedType];
        GameObject killfeed = Instantiate(killfeedPrefab, transform.position, Quaternion.identity);

        // Set the parent of the killfeed to this object
        killfeed.transform.SetParent(transform);

        // Reset local scale to maintain the original size
        killfeed.transform.localScale = Vector3.one;

        // Set the index of the killfeed as the last child
        killfeed.transform.SetSiblingIndex(transform.childCount - 1);
        
        ClearKillfeed();
    }

    public void ClearKillfeed()
    {
        if (transform.childCount >= 4)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
