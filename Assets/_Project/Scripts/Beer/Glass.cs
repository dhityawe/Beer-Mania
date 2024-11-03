using UnityEngine;

public class Glass : MonoBehaviour
{
    public float movementSpeed = 5f;
    public BeerQuality quality; // Change to BeerQuality enum
    private int tableIndex;

    private void Update()
    {
        if (GameManager.IsGameStopped) return;
        
        // Move the glass
        transform.Translate(new Vector2(movementSpeed * Time.unscaledDeltaTime, 0));

        // Return to pool if off-screen // !!! Should be when triggerEnter on Customer Spawn Point then return to pool
        CustomerSpawnPoint currentSpawnPoint = CustomerManager.CustomerSpawnPoints.Find(sp => sp.Lane == tableIndex + 1);
        if (transform.position.x < currentSpawnPoint.transform.position.x - 0.7f)
        {
            EventManager.Broadcast(new OnLiveLost(1));
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        GlassPool pool = FindObjectOfType<GlassPool>();
        pool.ReturnGlass(gameObject);
    }

    public void SetQuality(BeerQuality quality) // Update to accept BeerQuality
    {
        this.quality = quality;
    }

    public void SetTableIndex(int index)
    {
        tableIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D collision) // !!! Uncomment this when adding Customer script
    {
        Customer customer = collision.GetComponent<Customer>();
        if (customer != null)
        {
            customer.GiveDrink(quality);
            ReturnToPool(); // Return the glass after checking quality
        }
    }
}
