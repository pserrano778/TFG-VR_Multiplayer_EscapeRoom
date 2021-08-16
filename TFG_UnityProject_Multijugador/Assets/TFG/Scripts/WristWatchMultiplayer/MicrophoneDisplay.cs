using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MicrophoneDisplay : MonoBehaviour
{
    private bool microphoneActive = true;
    private bool buttonPressed;

    public GameObject microOnIcon = null;
    public GameObject microOffIcon = null;

    private void Start()
    {
        microOffIcon.SetActive(false);
        microOnIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Track button input
        if (OVRInput.Get(OVRInput.Button.One))
        {
            // If button wasnt pressed
            if (!buttonPressed)
            {
                // Set presed
                buttonPressed = true;

                // If photonView os mine
                if (GetComponent<PhotonView>().IsMine)
                {
                    microphoneActive = !microphoneActive;

                    // Call the function
                    GetComponent<PhotonView>().RPC("ChangeMicrophoneState", RpcTarget.All, microphoneActive);
                }
            }
        }
        else
        {
            // Set not presed
            buttonPressed = false;
        }
    }

    [PunRPC]
    private void ChangeMicrophoneState(bool state)
    {
        microphoneActive = state;

        if (microphoneActive)
        {
            microOffIcon.SetActive(false);
            microOnIcon.SetActive(true);
        }
        else
        {
            microOnIcon.SetActive(false);
            microOffIcon.SetActive(true);  
        }
    }
}
