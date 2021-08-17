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
    }

    public static void setPlayerOutside(int player)
    {
        // Se modifica el estado del jugador
        if (player >= 0 && player <= 1)
        {
            playerInside[player] = false;
        }
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

    public static void Reset()
    {
        for (int i=0; i<playerInside.Length; i++)
        {
            playerInside [i] = false;
        }
    }
}
