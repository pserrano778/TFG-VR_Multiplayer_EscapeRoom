using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CheckPlayerOutside : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Se busca al jugador  
        if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere")
        {
            PhotonView photonView = PhotonView.Get(this);

            // Se modifica el estado este por medio de RPC
            photonView.RPC("setPlayerOutside", RpcTarget.All, NetworkManager.getPlayer());
        }
    }

    [PunRPC]
    private void setPlayerOutside(int player)
    {
        PlayersInsideHouse.setPlayerOutside(player);
    }
}
