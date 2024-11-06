using UnityEngine;
using GabrielBigardi.SpriteAnimator;
using System.Collections;

public class GlassEmpty : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float descendSpeed = 2f; // Speed for smooth descent when breaking
    private int tableIndex;
    private bool isBreaking = false;
    private float targetPositionY;

    private SpriteAnimator spriteAnimator;

    private void Start() 
    {
        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    private void Update()
    {
        if (GameManager.IsGameStopped || isBreaking) return;

        // Move the glass normally
        transform.Translate(new Vector2(movementSpeed * Time.unscaledDeltaTime, 0));

        // Check if glass has reached the deadline point
        CustomerSpawnPoint currentSpawnPoint = CustomerManager.CustomerSpawnPoints.Find(sp => sp.Lane == tableIndex + 1);
        if (transform.position.x > currentSpawnPoint.CustomerDeadlinePoint.transform.position.x + 0.7f)
        {
            // Turn off the glass colider
            GetComponent<BoxCollider2D>().enabled = false;
            
            // Trigger breaking animation and start downward movement
            spriteAnimator.PlayIfNotPlaying("Break");
            EventManager.Broadcast(new OnLiveLost(1));

            // Set up downward movement
            targetPositionY = transform.position.y - 1.5f;
            isBreaking = true;
            StartCoroutine(SmoothlyMoveDownward());
        }
    }

    private IEnumerator SmoothlyMoveDownward()
    {
        // Move downward until reaching the target position
        while (Mathf.Abs(transform.position.y - targetPositionY) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x, targetPositionY),
                descendSpeed * Time.unscaledDeltaTime
            );
            yield return null; // Wait for the next frame
        }

        // Once downward movement is complete, destroy the object
        Destroy(gameObject);
    }

    public void SetTableIndex(int index)
    {
        tableIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerStateManager>())
        {
            Destroy(gameObject);
        }
    }
}
