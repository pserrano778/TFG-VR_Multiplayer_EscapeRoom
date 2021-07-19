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
                controller.ChangeObjectState(true);
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
                // Objeto Activado
                controller.ChangeObjectState(false);
            }
        }
    }
}
