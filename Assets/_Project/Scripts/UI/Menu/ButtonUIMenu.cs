using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIMenu : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private GameObject cursor;

    public void Select()
    {
        image.sprite = selectedSprite;
        cursor.SetActive(true);
    }

    public void Deselect()
    {
        image.sprite = normalSprite;
        cursor.SetActive(false);
    }

    public virtual bool Click()
    {
        return false;
    }

    protected virtual IEnumerator ClickCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            image.sprite = normalSprite;
            yield return new WaitForSeconds(0.2f);
            image.sprite = selectedSprite;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
