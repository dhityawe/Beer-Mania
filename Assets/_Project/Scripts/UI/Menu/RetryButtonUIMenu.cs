using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButtonUIMenu : ButtonUIMenu
{
    public override bool Click()
    {
        StartCoroutine(ClickCoroutine());
        return true;
    }
    
    protected override IEnumerator ClickCoroutine()
    {
        yield return base.ClickCoroutine();
        EventManager.Broadcast(new OnRestartGame());
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
