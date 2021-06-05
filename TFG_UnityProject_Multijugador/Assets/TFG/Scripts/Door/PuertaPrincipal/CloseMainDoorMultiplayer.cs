using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CloseMainDoorMultiplayer : MonoBehaviour
{
    private bool closed = false;

    public Animator animator = null;
    public string tagClose = "";

    private PhotonView photonView;

    private void Start()
    {
        photonView = PhotonView.Get(this);
    }

    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Si la puerta está abierta
        if (!closed)
        {   
            // Se busca al jugador  
            if (tagClose == "Player" && (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere"))
            {
                // Si el jugador no había entrado
                if (!PlayersInsideHouse.getPlayerState(NetworkManager.getPlayer()))
                {
                    // Se modifica el estado este por medio de RPC
                    photonView.RPC("setPlayerInside", RpcTarget.All, NetworkManager.getPlayer());
                }

                // Si era el último jugador en entrar, se llama a la función que cierra la puerta utilizando RPC
                if (PlayersInsideHouse.getPlayerState(0) && PlayersInsideHouse.getPlayerState(1))
                {
                    photonView.RPC("closeDoor", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    private void setPlayerInside(int player)
    {
        PlayersInsideHouse.setPlayerInside(player);
    }

    [PunRPC]
    private void closeDoor()
    {
        animator.Play("doorClose", 0, 0.0f);
        gameObject.SetActive(false);
        closed = true;
    }
}
