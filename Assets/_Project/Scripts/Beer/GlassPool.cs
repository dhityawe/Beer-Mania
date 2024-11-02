using UnityEngine;
using System.Collections.Generic;

public class GlassPool : MonoBehaviour
{
    public GameObject glassPrefab;
    public int poolSize = 10;

    private Queue<GameObject> glassPool = new Queue<GameObject>();

    private void Start()
    {
        // Initialize the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject glass = Instantiate(glassPrefab);
            glass.SetActive(false); // Deactivate the glass initially
            glassPool.Enqueue(glass);
        }
    }

    public GameObject GetGlass()
    {
        if (glassPool.Count > 0)
        {
            GameObject glass = glassPool.Dequeue();
            glass.SetActive(true); // Activate the glass when retrieved
            return glass;
        }
        else
        {
            // Optionally instantiate more glasses if needed
            GameObject glass = Instantiate(glassPrefab);
            return glass; // Return the new instance
        }
    }

    public void ReturnGlass(GameObject glass)
    {
        glass.SetActive(false); // Deactivate and return to the pool
        glassPool.Enqueue(glass);
    }
}
