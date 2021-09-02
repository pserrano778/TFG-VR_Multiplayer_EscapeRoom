using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : Menu
{
    public GameObject nextMenu;

    public override void Return()
    {
        GoNextMenu(nextMenu);
    }
}
