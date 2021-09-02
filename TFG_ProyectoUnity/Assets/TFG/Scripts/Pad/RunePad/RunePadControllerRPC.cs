using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class RunePadControllerRPC  : MonoBehaviour, PadController
{
    public enum Rune
    {
        Eye,
        Fire,
        Moon,
        Sun,
        Tree,
        Water,
        Wind,
    }

    private Rune rune;

    private void Start()
    {
        rune = Rune.Eye;
        UpdateImage();
    }

    public void NextRuneRPCObject()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("NextRuneRPC", RpcTarget.All);
    }

    [PunRPC]
    private void NextRuneRPC()
    {
        Next();
    }

    public void Next()
    {
        if(rune == Rune.Eye)
        {
            rune = Rune.Fire;
        }
        else if(rune == Rune.Fire)
        {
            rune = Rune.Moon;
        }
        else if (rune == Rune.Moon)
        {
            rune = Rune.Sun;
        }
        else if (rune == Rune.Sun)
        {
            rune = Rune.Tree;
        }
        else if (rune == Rune.Tree)
        {
            rune = Rune.Water;
        }
        else if (rune == Rune.Water)
        {
            rune = Rune.Wind;
        }
        else if (rune == Rune.Wind)
        {
            rune = Rune.Eye;
        }

        UpdateImage();
    }

    public Rune GetRune()
    {
        return rune;
    }

    private void UpdateImage()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("FantasyMagicRunes/" + rune);
    }
}
