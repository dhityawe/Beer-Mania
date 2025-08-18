using System.Collections;
using System.Collections.Generic;
using HYPLAY.Core.Runtime;
using UnityEngine;

public class MenuSelectionUIMenuWebGL : MonoBehaviour
{
    [SerializeField] private List<ButtonUIMenu> buttons;

    private int currentButtonIndex = 0;
    private bool isButtonSelected = false;

    private void Start()
    {
        if (HyplayBridge.IsLoggedIn)
        {
            buttons[currentButtonIndex].Select();
        }
    }

    private void OnEnable()
    {
        EventManager.AddListener<OnLoggedIn>(OnLoggedIn);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnLoggedIn>(OnLoggedIn);
    }

    private void OnLoggedIn(OnLoggedIn evt)
    {
        buttons[currentButtonIndex].Select();
    }

    private void Update()
    {
        if (!HyplayBridge.IsLoggedIn)
        {
            return;
        }

        if (isButtonSelected)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AudioManager.PlaySound("ChangeMenu");
            buttons[currentButtonIndex].Deselect();
            currentButtonIndex--;
            if (currentButtonIndex < 0)
            {
                currentButtonIndex = buttons.Count - 1;
            }
            buttons[currentButtonIndex].Select();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioManager.PlaySound("ChangeMenu");
            buttons[currentButtonIndex].Deselect();
            currentButtonIndex++;
            if (currentButtonIndex >= buttons.Count)
            {
                currentButtonIndex = 0;
            }
            buttons[currentButtonIndex].Select();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.PlaySound("ClickUi");
           isButtonSelected = buttons[currentButtonIndex].Click();
        }
    }
}
