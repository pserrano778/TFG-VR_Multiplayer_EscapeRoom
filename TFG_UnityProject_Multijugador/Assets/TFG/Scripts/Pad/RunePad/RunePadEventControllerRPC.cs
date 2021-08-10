using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RunePadEventControllerRPC : PadEventController
{
    public RunePadControllerRPC[] codeElements;
    public Animator doorAnimator;
    public AudioClip audioOpen;

    public void CheckCodeRPCObject()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("CheckCodeRPC", RpcTarget.All);
    }

    [PunRPC]
    private void CheckCodeRPC()
    {
        CheckCode();
    }

    override
    public void CheckCode()
    {
        if (locked)
        {
            string currentStr = "";

            for (int i = 0; i < codeElements.Length; i++)
            {
                currentStr += (codeElements[i] as RunePadControllerRPC).GetRune().ToString();
            }

            if (code.ToString().Equals(currentStr))
            {
                PlayAnimation();
                doorAnimator.Play("doorOpen", 0, 0.0f);
                PlayAudioOpen();
                gameObject.SetActive(false);
            }            
        }
    }

    public void PlayAudioOpen()
    {
        if (audioOpen != null)
        {
            AudioSource.PlayClipAtPoint(audioOpen, transform.position, 0.025f);
        }
    }
}
