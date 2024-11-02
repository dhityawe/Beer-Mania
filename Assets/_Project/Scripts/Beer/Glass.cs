using UnityEngine;

public class Glass : MonoBehaviour
{
    public float movementSpeed = 5f;
    public BeerQuality quality; // Change to BeerQuality enum

    private void Update()
    {
        // Move the glass
        transform.Translate(new Vector2(movementSpeed * Time.deltaTime, 0));

        // Return to pool if off-screen // !!! Should be when triggerEnter on Customer Spawn Point then return to pool
        if (transform.position.x < -10f)
        {
            // start break glass animation
            
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

    // private void OnTriggerEnter2D(Collider2D collision) // !!! Uncomment this when adding Customer script
    // {
    //     Customer customer = collision.GetComponent<Customer>();
    //     if (customer != null)
    //     {
    //         customer.CheckQuality(quality);
    //         ReturnToPool(); // Return the glass after checking quality
    //     }
    // }
}
