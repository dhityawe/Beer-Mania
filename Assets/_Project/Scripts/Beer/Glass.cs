using UnityEngine;
using GabrielBigardi.SpriteAnimator;
using System.Collections;

public class Glass : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float descendSpeed = 2f; // Speed for smooth descent when breaking
    public BeerQuality quality;
    private int tableIndex;

    private SpriteAnimator spriteAnimator;
    private bool isBreaking = false;

    private void Start() 
    {
        spriteAnimator = GetComponent<SpriteAnimator>();
        spriteAnimator.PlayIfNotPlaying("Slide");
    }

    private void Update()
    {
        if (GameManager.IsGameStopped || isBreaking) return;

        // Move the glass normally
        transform.Translate(new Vector2(movementSpeed * Time.unscaledDeltaTime, 0));

        // Check if glass has reached off-screen threshold
        CustomerSpawnPoint currentSpawnPoint = CustomerManager.CustomerSpawnPoints.Find(sp => sp.Lane == tableIndex + 1);
        if (transform.position.x < currentSpawnPoint.transform.position.x - 0.7f && !isBreaking)
        {
            // Turn off the glass colider
            GetComponent<BoxCollider2D>().enabled = false;
            
            // Start breaking animation and trigger downward transition coroutine
            spriteAnimator.PlayIfNotPlaying("Break");
            isBreaking = true;
            EventManager.Broadcast(new OnLiveLost(1));
            StartCoroutine(SmoothlyMoveDownward());
        }
    }

    private IEnumerator SmoothlyMoveDownward()
    {
        float targetPositionY = transform.position.y - 1.5f;

        while (Mathf.Abs(transform.position.y - targetPositionY) > 0.01f)
        {
            // Move downward over time using unscaledDeltaTime
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x, targetPositionY),
                descendSpeed * Time.unscaledDeltaTime
            );
            yield return null; // Wait for the next frame
        }
    }

    private void ReturnToPool()
    {
        GlassPool pool = FindObjectOfType<GlassPool>();
        pool.ReturnGlass(gameObject);
        isBreaking = false; // Reset breaking status
    }

    public void SetQuality(BeerQuality quality)
    {
        this.quality = quality;
    }

    public void SetTableIndex(int index)
    {
        tableIndex = index;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Customer customer = collision.GetComponent<Customer>();
        if (customer != null)
        {
            customer.GiveDrink(quality);
            ReturnToPool();
        }
    }
}
