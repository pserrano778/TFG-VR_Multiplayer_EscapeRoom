using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ObjectColliderTracker : MonoBehaviour
{
    public string objectTag;
    public ObjectColliderPuzzleController controller;

    private void OnTriggerEnter(Collider other)
    {
        // Si coincide la etiqueta
        if (other.CompareTag(objectTag))
        {
            // Si soy el dueño del objeto
            if (other.GetComponent<PhotonView>().IsMine)
            {
                // Objeto Activado
                GetComponent<PhotonView>().RPC("SetObjectInside", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si coincide la etiqueta
        if (other.CompareTag(objectTag))
        {
            // Si soy el dueño del objeto
            if (other.GetComponent<PhotonView>().IsMine)
            {
                // Objeto Desactivado
                GetComponent<PhotonView>().RPC("SetObjectOutside", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void SetObjectInside()
    {
        controller.ChangeObjectState(true);
    }

    [PunRPC]
    private void SetObjectOutside()
    {
        controller.ChangeObjectState(false);
    }
}
