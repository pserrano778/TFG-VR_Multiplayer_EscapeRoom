using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CombineObjectsColliderTracker : MonoBehaviour
{
    public string typeOfTag;
    public CombineObjectColliderPuzzleController controller;

    private GameObject currentObject;

    private void OnTriggerEnter(Collider other)
    {
        // Si coincide la etiqueta
        if (other.tag.Contains(typeOfTag))
        {
            // Si soy el dueño del objeto
            if (other.GetComponent<PhotonView>().IsMine)
            {
                if (currentObject == null)
                {
                    // Asignamos el objeto actual
                    currentObject = other.gameObject;

                    // Objeto Activado
                    controller.SetNewObjectState(true, currentObject);
                }    
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si coincide la etiqueta
        if (other.tag.Contains(typeOfTag))
        {
            // Si soy el dueño del objeto
            if (other.GetComponent<PhotonView>().IsMine)
            {
                if (currentObject == other.gameObject)
                {
                    // Objeto Activado
                    controller.SetNewObjectState(false, currentObject);

                    // Eliminamos el objeto actual
                    currentObject = null;
                }
            }
        }
    }
}
