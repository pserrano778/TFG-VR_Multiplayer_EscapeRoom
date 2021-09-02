using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpeakerSetup: MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public GameObject speaker;
    public GameObject Recorder;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("info: " + info);

       
    }
}
