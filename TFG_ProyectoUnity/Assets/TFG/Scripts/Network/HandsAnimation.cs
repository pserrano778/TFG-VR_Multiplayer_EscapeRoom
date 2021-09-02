﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandsAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            GetComponent<OVRTouchSample.Hand>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
