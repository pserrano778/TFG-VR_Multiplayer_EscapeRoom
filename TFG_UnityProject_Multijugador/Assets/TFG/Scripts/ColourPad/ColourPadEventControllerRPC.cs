using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColourPadEventControllerRPC : ColourPadEventController
{
    public void CheckCodeRPCObject()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("CheckCodeRPC", RpcTarget.All);
    }

    [PunRPC]
    private void CheckCodeRPC()
    {
        CheckCode();
    }
}
