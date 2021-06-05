using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using VRTK;
using Photon.Voice.Unity;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

    public Transform vrTargetHead;
    public Transform vrTargetLeftArm;
    public Transform vrTargetRightArm;

    public GameObject leftController;
    public GameObject rightController;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        spawnedPlayerPrefab.GetComponent<VRRigOnline>().head.vrTarget = vrTargetHead;
        spawnedPlayerPrefab.GetComponent<VRRigOnline>().leftArm.vrTarget = vrTargetLeftArm;
        spawnedPlayerPrefab.GetComponent<VRRigOnline>().rightArm.vrTarget = vrTargetRightArm;

        leftController.GetComponent<VRTK_InteractGrab>().controllerAttachPoint = spawnedPlayerPrefab.GetComponent<AttachPoints>().attachPointL;
        rightController.GetComponent<VRTK_InteractGrab>().controllerAttachPoint = spawnedPlayerPrefab.GetComponent<AttachPoints>().attachPointR;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

    
}
