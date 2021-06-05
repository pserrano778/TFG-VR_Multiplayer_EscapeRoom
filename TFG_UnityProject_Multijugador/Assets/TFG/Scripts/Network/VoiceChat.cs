using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice;
using VRTK.GrabAttachMechanics;
using OVR;
public class VoiceChat : MonoBehaviour
{
    private Photon.Voice.Unity.Recorder voice;
    private bool buttonPressed;

    // Start is called before the first frame update
    void Start()
    {
        voice = GetComponent<Photon.Voice.Unity.Recorder>();
        buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (!buttonPressed)
            {
                voice.TransmitEnabled = !voice.TransmitEnabled;
                buttonPressed = true;
            }
        }
        else
        {
            buttonPressed = false;
        }
    }
}
