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

        AudioManager.PlaySound("killfeed");
        
        GameObject killfeedPrefab = killfeedPrefabs[(int)evt.KillfeedType];
        GameObject killfeed = Instantiate(killfeedPrefab, transform.position, Quaternion.identity);

        // Set the parent of the killfeed to this object
        killfeed.transform.SetParent(transform);

        // Reset local scale to maintain the original size
        killfeed.transform.localScale = Vector3.one;

        // Set the index of the killfeed as the last child
        killfeed.transform.SetSiblingIndex(transform.childCount - 1);

        // Destroy the killfeed after 3 seconds
        Destroy(killfeed, 3f);
    }
    
    // Coroutine method to remove the killfeed after a delay
    private IEnumerator RemoveKillfeedAfterDelay(GameObject killfeed, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(killfeed);
    }

    public void ClearKillfeed()
    {
        // Optional: If you still want to clear the first child manually when reaching a certain count
        if (transform.childCount >= 4)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
