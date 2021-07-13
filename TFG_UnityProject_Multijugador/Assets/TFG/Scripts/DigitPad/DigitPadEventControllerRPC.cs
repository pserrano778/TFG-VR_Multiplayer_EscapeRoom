using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DigitPadEventControllerRPC : DigitPadEventController
{
    public void CheckNumberRPCObject()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("CheckNumberRPC", RpcTarget.All);
    }

    [PunRPC]
    private void CheckNumberRPC()
    {
        CheckNumber();
    }
}
