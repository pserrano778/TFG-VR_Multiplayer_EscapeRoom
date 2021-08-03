using System;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;

public class NetworkVoiceManager : MonoBehaviour
{
    private VoiceConnection voiceConnection = null;

    void Awake()
    {
        print("Asignado");
        voiceConnection = GetComponent<VoiceConnection>();
    }

    private void OnEnable()
    {
        print("Enable");
        // Por seguridad
        if (voiceConnection == null)
        {
            voiceConnection = GetComponent<VoiceConnection>();
        }
        voiceConnection.SpeakerLinked += this.OnSpeakerCreated;
    }

    private void OnDisable()
    {
        voiceConnection.SpeakerLinked -= this.OnSpeakerCreated;
    }
    
    private void OnSpeakerCreated(Speaker speaker)
    {
        print("Creado");
        foreach (var photonView in FindObjectsOfType(typeof(PhotonView)) as PhotonView[])
        {
            print(photonView.gameObject.name);
            if (photonView.name == "Network Player(Clone)" && photonView.Owner.ActorNumber == speaker.Actor.ActorNumber)
            {
                print("entro");
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
