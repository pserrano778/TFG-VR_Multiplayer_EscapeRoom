using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice;
using VRTK.GrabAttachMechanics;
using OVR;
using TMPro;
public class VoiceChat : MonoBehaviour
{
    private Photon.Voice.Unity.Recorder voice;
    private bool buttonPressed;
    public TextMeshProUGUI microphoneText;

    // Start is called before the first frame update
    void Start()
    {
        voice = GetComponent<Photon.Voice.Unity.Recorder>();
        buttonPressed = false;
        ChangeText(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (!buttonPressed)
            {
                voice.TransmitEnabled = !voice.TransmitEnabled;

                ChangeText(voice.TransmitEnabled);

                buttonPressed = true;
            }
        }
        else
        {
            buttonPressed = false;
        }
    }

    private void ChangeText(bool state)
    {
        if (state)
        {
            microphoneText.text = "Micrófono: Activo";
        }
        else
        {
            microphoneText.text = "Micrófono: Silenciado";
        }
    }
}
