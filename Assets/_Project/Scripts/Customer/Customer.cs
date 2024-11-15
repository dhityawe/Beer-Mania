using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customer : MonoBehaviour
{
    [HideInInspector] public CustomerSpawnPoint CustomerSpawnPoint;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Collider2D customerCollider;
    [SerializeField] protected float walkSpeed = 1f;
    [SerializeField] private float walkDistance = 1f;
    [SerializeField] private int score = 100;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float walkTimer = 0f;
    protected bool isGotDrink = false;
    private Vector2 lastDirection;
    protected int lane;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (customerCollider == null)
        {
            customerCollider = GetComponent<Collider2D>();
        }
        
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        animator.SetBool("ISWALKING", true);
    }
    
    private void Update()
    {
        if (GameManager.IsGameStopped) return;

        WalkToCounter();
        GotDrink();
    }

    public void SetLane(int lane)
    {
        this.lane = lane;
        if (spriteRenderer == null) return;

        spriteRenderer.sortingLayerName = "Lane " + lane;
    }

    protected virtual void WalkToCounter()
    {
        if (isGotDrink) return;

        rb.velocity = Vector2.zero;

        if (walkTimer <= 0.5f / walkSpeed)
        {
            walkTimer += Time.deltaTime;
            return;
        }

        walkTimer = 0f;

        Vector2 direction = CustomerSpawnPoint.CustomerDeadlinePoint.transform.position - transform.position;
        transform.position += (Vector3)direction.normalized / (10f / walkDistance);

        if (lastDirection != null && Vector2.Dot(direction, lastDirection) < 0f)
        {
            Destroy(gameObject);
            EventManager.Broadcast(new OnLiveLost(1));
        }

        if (Vector2.Distance(transform.position, CustomerSpawnPoint.CustomerDeadlinePoint.transform.position) < 0.1f)
        {
            Destroy(gameObject);
            EventManager.Broadcast(new OnLiveLost(1));
        }

        lastDirection = direction;
    }

    public void GiveDrink(BeerQuality beerQuality)
    {
        AudioManager.PlaySound("pop");
        
        Vector2 direction = CustomerSpawnPoint.transform.position- CustomerSpawnPoint.CustomerDeadlinePoint.transform.position;
        rb.velocity = direction.normalized * 5f;
        animator.SetBool("ISWALKING", false);
        isGotDrink = true;
        customerCollider.enabled = false;

        int totalScore = 0;

        if (beerQuality == BeerQuality.Bad)
        {
            totalScore = score / 2;
            EventManager.Broadcast(new TriggerKillFeed(KillfeedUIGameplay.KillfeedType.Bad));
        }
        else if (beerQuality == BeerQuality.Good)
        {
            totalScore = score;
            EventManager.Broadcast(new TriggerKillFeed(KillfeedUIGameplay.KillfeedType.Good));
        }
        else if (beerQuality == BeerQuality.Perfect)
        {
            totalScore = score * 2;
            EventManager.Broadcast(new TriggerKillFeed(KillfeedUIGameplay.KillfeedType.Perfect));
        }

        EventManager.Broadcast(new AddScore(totalScore));
    }

    protected virtual void GotDrink()
    {
        if (!isGotDrink) return;

        Vector2 spawnPointDirection = (CustomerSpawnPoint.transform.position - CustomerSpawnPoint.CustomerDeadlinePoint.transform.position).normalized;
        bool isFacingRight = spawnPointDirection.x < 0;

        if (isFacingRight && transform.position.x < CustomerSpawnPoint.transform.position.x)
        {
            OnCustomerLeft();
        }

        else if (!isFacingRight && transform.position.x > CustomerSpawnPoint.transform.position.x)
        {
            OnCustomerLeft();
        }
    }

    protected void OnCustomerLeft()
    {
        EventManager.Broadcast(new OnCustomerLeft(this));
        Destroy(gameObject);
    }
}
