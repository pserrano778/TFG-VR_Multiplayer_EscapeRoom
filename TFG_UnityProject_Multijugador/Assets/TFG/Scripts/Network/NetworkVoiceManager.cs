using System;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;

public class NetworkVoiceManager : MonoBehaviour
{
    private VoiceConnection voiceConnection;

    void Awake()
    {
        voiceConnection = GetComponent<VoiceConnection>();
    }

    private void OnEnable()
    {
        voiceConnection.SpeakerLinked += this.OnSpeakerCreated;
    }

    private void OnDisable()
    {
        voiceConnection.SpeakerLinked -= this.OnSpeakerCreated;
    }

    private void OnSpeakerCreated(Speaker speaker)
    {
        foreach (var photonView in FindObjectsOfType(typeof(PhotonView)) as PhotonView[])
        {
            if (photonView.name == "Network Player(Clone)" && photonView.Owner.ActorNumber == speaker.Actor.ActorNumber)
            {
                speaker.transform.position = Vector3.zero;
                speaker.transform.SetParent(photonView.transform);
            }
        }
        speaker.OnRemoteVoiceRemoveAction += OnRemoteVoiceRemove;
    }

    private void OnRemoteVoiceRemove(Speaker speaker)
    {
        if (speaker != null)
        {
            Destroy(speaker.gameObject);
        }
    }
}
