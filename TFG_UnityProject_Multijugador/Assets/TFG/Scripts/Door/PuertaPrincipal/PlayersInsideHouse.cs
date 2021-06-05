using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayersInsideHouse
{
    public static  bool[] playerInside = { false, false };

    public static void setPlayerInside(int player)
    {
        // Se modifica el estado del jugador
        if (player >= 0 && player <= 1)
        {
            playerInside[player] = true;
        }
        Debug.Log("debug1: " + playerInside[player]);
    }

    public static void setPlayerOutside(int player)
    {
        // Se modifica el estado del jugador
        if (player >= 0 && player <= 1)
        {
            playerInside[player] = false;
        }
        Debug.Log("debug2: " + playerInside[player]);
    }

    public static bool getPlayerState(int player)
    {
        bool inside = false;

        if (player >= 0 && player <= 1)
        {
            inside = playerInside[player];
        }
        return inside;
    }
}
