using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillfeedUIGameplay : MonoBehaviour
{
    public enum KillfeedType { Perfect, Good, Bad }

    public List<GameObject> killfeedPrefabs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerKillfeed(KillfeedType.Perfect);
            ClearKillfeed();
        }
    }

    public void TriggerKillfeed(KillfeedType type)
    {
        GameObject killfeedPrefab = killfeedPrefabs[(int)type];
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
