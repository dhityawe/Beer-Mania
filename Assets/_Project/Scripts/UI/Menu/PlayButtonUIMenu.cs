using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonUIMenu : ButtonUIMenu
{
    public override bool Click()
    {
        AudioManager.PlaySound("ClickUi");
        StartCoroutine(ClickCoroutine());
        return true;
    }
    
    protected override IEnumerator ClickCoroutine()
    {
        yield return base.ClickCoroutine();
        yield return new WaitForSeconds(1.3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestRifqi");
    }
}
