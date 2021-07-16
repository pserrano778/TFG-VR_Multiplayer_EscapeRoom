using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class DigitPadControllerRPC : DigitPadController
{   
    public void NextDigitRPCButton()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("NextDigitRPC", RpcTarget.All);
    }

    [PunRPC]
    private void NextDigitRPC()
    {
        NextDigit();
    }
}
