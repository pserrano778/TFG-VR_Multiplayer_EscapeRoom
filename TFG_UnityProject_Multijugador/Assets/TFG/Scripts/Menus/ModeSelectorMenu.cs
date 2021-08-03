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
        LoadScene("MenuModoUnJugador");
    }

    public void ModoMultijugador()
    {
        LoadScene("MenuModoMultijugador");
    }
}
