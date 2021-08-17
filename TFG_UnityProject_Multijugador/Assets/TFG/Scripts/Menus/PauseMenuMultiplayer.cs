using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PauseMenuMultiplayer : PauseMenu
{ 
    public GameObject voiceManager;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckButton();
    }

    public override void Return()
    {
        PlayersInsideHouse.Reset();
        PhotonNetwork.Disconnect();
        Destroy(voiceManager);
        LoadScene("Menus");
    }
}
