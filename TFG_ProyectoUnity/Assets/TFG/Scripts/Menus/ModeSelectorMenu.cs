using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectorMenu : Menu
{
    public GameObject singlePlayerMenu;
    public GameObject multiplayerMenu;
    public GameObject previousMenu;

    private void Awake()
    {
        currentMenu.SetActive(false);
    }

    public override void Return()
    {
        GoNextMenu(previousMenu);
    }

    public void ModoUnJugador()
    {
        GoNextMenu(singlePlayerMenu);
    }

    public void ModoMultijugador()
    {
        GoNextMenu(multiplayerMenu);
    }
}
