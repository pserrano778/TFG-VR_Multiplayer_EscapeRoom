using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayersInsideCounter : MonoBehaviour
{
    private bool [] playerInside = { false, false };

    private void OnTriggerEnter(Collider other)
    {

        // Se busca al jugador  
        if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere")
        {
            PhotonView photonView = GetComponent<PhotonView>();

            // Se modifica el estado este por medio de RPC
            photonView.RPC("SetPlayerInside", RpcTarget.All, NetworkManager.getPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Se busca al jugador  
        if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere")
        {
            PhotonView photonView = GetComponent<PhotonView>();

            // Se modifica el estado este por medio de RPC
            photonView.RPC("SetPlayerOutside", RpcTarget.All, NetworkManager.getPlayer());
        }
    }

    public int GetPlayersInside()
    {
        int numPlayersInside = 0;

        for (int i=0; i<playerInside.Length; i++)
        {
            if (playerInside[i])
            {
                numPlayersInside++;
            }
        }
        return numPlayersInside;
    }

    [PunRPC]
    public void SetPlayerInside(int player)
    {
        // Se modifica el estado del jugador
        if (player >= 0 && player <= 1)
        {
            playerInside[player] = true;
        }
    }

    [PunRPC]
    public void SetPlayerOutside(int player)
    {
        // Se modifica el estado del jugador
        if (player >= 0 && player <= 1)
        {
            playerInside[player] = false;
        }
    }
}
