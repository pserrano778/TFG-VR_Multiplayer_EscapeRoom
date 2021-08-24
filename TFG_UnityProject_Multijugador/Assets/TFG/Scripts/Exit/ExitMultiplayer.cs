using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ExitMultiplayer : Exit
{
    public GameObject managerToDelete = null;

    private void OnTriggerEnter(Collider other)
    {
        // Se busca al jugador  
        if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere")
        {
            ExitGame();
        }
    }

    protected override void ExitGame()
    {
        PlayersInsideHouse.Reset();
        PhotonNetwork.Disconnect();
        if (managerToDelete != null)
        {
            Destroy(managerToDelete);
        }
        base.ExitGame();
    }
}
