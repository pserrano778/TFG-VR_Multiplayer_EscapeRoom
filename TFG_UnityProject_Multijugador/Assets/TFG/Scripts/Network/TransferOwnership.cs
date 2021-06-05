using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TransferOwnership : MonoBehaviour
{
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine && (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere"))
        {
            photonView.RequestOwnership();
        }
    }
}
