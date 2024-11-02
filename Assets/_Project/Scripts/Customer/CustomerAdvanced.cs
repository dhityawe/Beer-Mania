using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAdvanced : Customer
{
    [SerializeField] private int drinkTimes = 2;
    private bool isStillDrinking = false;
    private int currentDrinkTimes = 0;

    protected override void GotDrink()
    {
        if (!isGotDrink) return;
        if (isStillDrinking) return;

        if (Vector2.Distance(transform.position, CustomerSpawnPoint.transform.position) < 0.1f)
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
                Destroy(gameObject);
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
    }
}
