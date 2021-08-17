using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerMenu : Menu
{
    public GameObject previousMenu;

    private void Awake()
    {
        currentMenu.SetActive(false);
    }

    private void Update()
    {

    }

    public override void Return()
    {
        GoNextMenu(previousMenu);
    }

    public void SearchPlayer()
    {
        PlayersInsideHouse.Reset();
        LoadScene("Multijugador");
    }
}
