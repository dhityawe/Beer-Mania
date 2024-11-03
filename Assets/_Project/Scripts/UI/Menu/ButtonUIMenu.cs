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

    public virtual void Click()
    {
        Debug.Log("Button Clicked");
    }
}
