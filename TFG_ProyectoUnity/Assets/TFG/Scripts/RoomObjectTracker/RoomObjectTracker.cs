using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomObjectTracker : MonoBehaviour
{
    public string objectTag;
    private bool objectInsideRoom;
    // Start is called before the first frame update
    void Start()
    {
        objectInsideRoom = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto está fuera
        if (!objectInsideRoom && other.CompareTag(objectTag))
        {
            // Si soy el dueño del objeto
            if (other.gameObject.GetComponent<PhotonView>() != null && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponent<PhotonView>().RPC("SetObjectInside", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el objeto está dentro
        if (objectInsideRoom && other.CompareTag(objectTag))
        {
            // Si soy el dueño del objeto
            if (other.gameObject.GetComponent<PhotonView>() != null && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponent<PhotonView>().RPC("SetObjectOutside", RpcTarget.All);
            }  
        }
    }

    [PunRPC]
    private void SetObjectInside()
    {
        objectInsideRoom = true;
    }

    [PunRPC]
    private void SetObjectOutside()
    {
        objectInsideRoom = false;
    }

    public bool GetObjectInsideRoom()
    {
        return objectInsideRoom;
    }
}
