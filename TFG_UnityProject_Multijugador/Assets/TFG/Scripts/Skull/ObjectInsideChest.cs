using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectInsideChest : MonoBehaviour
{
    public SkullBehaviour boneTracker;
    public string tagToCompare;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            if (other.gameObject.GetComponent<PhotonView>() != null && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponent<PhotonView>().RPC("setObjectInside", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PhotonView>() != null && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            GetComponent<PhotonView>().RPC("setObjectOutside", RpcTarget.All);
        }
    }

    [PunRPC]
    private void setObjectInside()
    {
        boneTracker.ChangeObjectInside(true);
    }

    [PunRPC]
    private void setObjectOutside() {
        boneTracker.ChangeObjectInside(false);
    }
}