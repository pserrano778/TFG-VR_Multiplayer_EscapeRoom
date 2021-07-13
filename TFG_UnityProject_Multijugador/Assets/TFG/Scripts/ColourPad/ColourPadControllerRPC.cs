using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ColourPadControllerRPC : ColourPadController
{
    public void NextColorRPCObject()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("NextColorRPC", RpcTarget.All);
    }

    [PunRPC]
    private void NextColorRPC()
    {
        NextColor();
    }

}
