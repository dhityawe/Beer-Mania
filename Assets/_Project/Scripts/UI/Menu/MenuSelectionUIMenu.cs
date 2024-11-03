using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectionUIMenu : MonoBehaviour
{
    [SerializeField] private List<ButtonUIMenu> buttons;

    private int currentButtonIndex = 0;

    private void Start()
    {
        buttons[currentButtonIndex].Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
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
            buttons[currentButtonIndex].Deselect();
            currentButtonIndex++;
            if (currentButtonIndex >= buttons.Count)
            {
                currentButtonIndex = 0;
            }
            buttons[currentButtonIndex].Select();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[currentButtonIndex].Click();
        }
    }
}
