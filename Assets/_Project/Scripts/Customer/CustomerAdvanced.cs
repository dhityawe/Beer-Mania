using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAdvanced : Customer
{
    [SerializeField] private int drinkTimes = 2;
    [SerializeField] private GameObject emptyGlass;
    private bool isStillDrinking = false;
    private int currentDrinkTimes = 0;

    protected override void GotDrink()
    {
        if (!isGotDrink) return;
        if (isStillDrinking) return;

        Vector2 spawnPointDirection = (CustomerSpawnPoint.transform.position - CustomerSpawnPoint.CustomerDeadlinePoint.transform.position).normalized;
        bool isFacingRight = spawnPointDirection.x < 0;

        if (isFacingRight && transform.position.x < CustomerSpawnPoint.transform.position.x)
        {
            if (currentDrinkTimes <= drinkTimes)
            {
                rb.velocity = Vector2.zero;
                isStillDrinking = true;
                currentDrinkTimes++;
                StartCoroutine(DrinkAnimation());
            }
            else
            {
                OnCustomerLeft();
            }
        }

        else if (!isFacingRight && transform.position.x > CustomerSpawnPoint.transform.position.x)
        {
            if (currentDrinkTimes <= drinkTimes)
            {
                rb.velocity = Vector2.zero;
                isStillDrinking = true;
                currentDrinkTimes++;
                StartCoroutine(DrinkAnimation());
            }
            else
            {
                OnCustomerLeft();
            }
        }
    }

    private IEnumerator DrinkAnimation()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("ISWALKING", true);
        customerCollider.enabled = true;
        isStillDrinking = false;
        isGotDrink = false;
        GlassEmpty glass = Instantiate(emptyGlass, transform.position, Quaternion.identity).GetComponent<GlassEmpty>();
        glass.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        glass.SetTableIndex(lane-1);
    }
}
