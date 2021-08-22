using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedMenu : Menu
{
    public GameObject nextMenu;

    private void Start()
    {
        if (!Exit.GetCompleted())
        {
            currentMenu.SetActive(false);
        }

        Exit.SetCompleted(false);
    }

    public override void Return()
    {
        GoNextMenu(nextMenu);
    }
}
