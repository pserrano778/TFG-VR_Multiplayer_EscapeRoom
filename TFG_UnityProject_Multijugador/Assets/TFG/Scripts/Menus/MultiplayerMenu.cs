using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerMenu : Menu
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SearchPlayer();
        }
    }

    public override void Return()
    {
        LoadScene("MenuSeleccionModo");
    }

    public void SearchPlayer()
    {
        LoadScene("Multijugador");
    }
}
