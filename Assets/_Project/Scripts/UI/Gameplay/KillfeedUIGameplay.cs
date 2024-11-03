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
    }

    public void ClearKillfeed()
    {
        if (transform.childCount >= 4)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
