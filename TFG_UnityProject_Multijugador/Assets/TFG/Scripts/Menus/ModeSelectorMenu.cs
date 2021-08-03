using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectorMenu : Menu
{
    public override void Return()
    {
        LoadScene("MenuPrincipal");
    }

    public void ModoUnJugador()
    {
        LoadScene("MenuUnJugador");
    }

    public void ModoMultijugador()
    {
        LoadScene("MenuModoMultijugador");
    }
}
