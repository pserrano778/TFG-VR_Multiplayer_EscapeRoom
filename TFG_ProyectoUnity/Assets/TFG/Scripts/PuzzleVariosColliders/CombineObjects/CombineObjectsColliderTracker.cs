using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CombineObjectsColliderTracker : MonoBehaviour
{
    public string typeOfTag;
    public CombineObjectColliderPuzzleController controller;

    private GameObject currentObject;

    public SpriteRenderer sprite;

    public int id;

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
                    GetComponent<PhotonView>().RPC("SetNewObject", RpcTarget.All, other.GetComponent<PhotonView>().ViewID);
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
                    GetComponent<PhotonView>().RPC("RemoveObject", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    private void SetNewObject(int photonId)
    {
        // Asignamos el objeto actual
        currentObject = PhotonView.Find(photonId).gameObject;

        sprite.color = new Color(0.7f, 0.6f, 0);

        // Objeto Activado
        controller.SetNewObjectState(true, currentObject, id);
    }

    [PunRPC]
    private void RemoveObject()
    {
        // Objeto Activado
        controller.SetNewObjectState(false, currentObject, id);

        sprite.color = new Color(0, 0, 0.3f);

        // Eliminamos el objeto actual
        currentObject = null;
    }
}
