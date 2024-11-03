using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonUIMenu : ButtonUIMenu
{
    public override bool Click()
    {
        StartCoroutine(ClickCoroutine());
        return true;
    }
    
    protected override IEnumerator ClickCoroutine()
    {
        yield return base.ClickCoroutine();
        yield return new WaitForSeconds(0.2f);
        EventManager.Broadcast(new OnRestartGame());
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
    }
}
