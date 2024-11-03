using UnityEngine;

public class ExampleAnimationTrigger : MonoBehaviour
{
    public void TriggerAnimation()
    {
        EnviEventManager.Instance.RaiseEvent("PlayAnimation");
    }

    void Update()
    {
        // Example: Press space to trigger the animation event
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerAnimation();
        }
    }
}
