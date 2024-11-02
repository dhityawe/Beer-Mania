using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customer : MonoBehaviour
{
    [HideInInspector] public CustomerSpawnPoint CustomerSpawnPoint;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float walkDistance = 1f;
    [SerializeField] private int score = 100;
    private float walkTimer = 0f;
    private bool isDrinking = false;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        animator.SetBool("ISWALKING", true);
    }
    
    private void Update()
    {
        WalkToCounter();
        GotDrink();
    }

    private void WalkToCounter()
    {
        if (isDrinking) return;

        rb.velocity = Vector2.zero;

        if (walkTimer <= 0.5f / walkSpeed)
        {
            walkTimer += Time.deltaTime;
            return;
        }

        walkTimer = 0f;

        Vector2 direction = CustomerSpawnPoint.CustomerDeadlinePoint.transform.position - transform.position;
        transform.position += (Vector3)direction.normalized / (10f / walkDistance);

        if (Vector2.Distance(transform.position, CustomerSpawnPoint.CustomerDeadlinePoint.transform.position) < 0.1f)
        {
            EventManager.Broadcast(new OnCustomerArrivedAtCounter(this));
            Destroy(gameObject);
        }
    }

    public void GiveDrink(BeerQuality beerQuality)
    {
        Vector2 direction = CustomerSpawnPoint.transform.position- CustomerSpawnPoint.CustomerDeadlinePoint.transform.position;
        rb.velocity = direction.normalized * 5f;
        animator.SetBool("ISWALKING", false);
        isDrinking = true;

        int totalScore = 0;

        if (beerQuality == BeerQuality.Bad)
        {
            totalScore = score / 2;
        }
        else if (beerQuality == BeerQuality.Good)
        {
            totalScore = score;
        }
        else if (beerQuality == BeerQuality.Perfect)
        {
            totalScore = score * 2;
        }

        EventManager.Broadcast(new AddScore(totalScore));
    }

    private void GotDrink()
    {
        if (!isDrinking) return;

        if (Vector2.Distance(transform.position, CustomerSpawnPoint.transform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
