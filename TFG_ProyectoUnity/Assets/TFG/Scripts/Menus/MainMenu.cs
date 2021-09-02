using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
    public GameObject nextMenu;
    public GameObject creditsMenu;
    private void Start()
    {
        if (Exit.GetCompleted())
        {
            currentMenu.SetActive(false);
        }
    }

    public void PressStartButton()
    {
        GoNextMenu(nextMenu);
    }

    public void GoToCredits()
    {
        GoNextMenu(creditsMenu);
    }
}
