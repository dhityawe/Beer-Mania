using UnityEngine;

public class GlassEmpty : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private int tableIndex;
    private void Update()
    {
        if (GameManager.IsGameStopped) return;
        
        // Move the glass
        transform.Translate(new Vector2(movementSpeed * Time.unscaledDeltaTime, 0));

        // Return to pool if off-screen // !!! Should be when triggerEnter on Customer Spawn Point then return to pool
        CustomerSpawnPoint currentSpawnPoint = CustomerManager.CustomerSpawnPoints.Find(sp => sp.Lane == tableIndex + 1);
        if (transform.position.x > currentSpawnPoint.CustomerDeadlinePoint.transform.position.x + 0.7f)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Destroy");
            EventManager.Broadcast(new OnLiveLost(1));
        }
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
