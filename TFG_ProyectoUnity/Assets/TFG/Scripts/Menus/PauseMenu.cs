using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenu : Menu
{
    public GameObject pauseMenuDisplay;
    public GameObject menu;
    public GameObject confirmation;

    private bool buttonPressed = false;
    private bool paused = false;

    protected void CheckButton()
    {
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            if (!buttonPressed)
            {
                buttonPressed = true;
                if (!paused)
                {
                    Pause();
                }
                else
                {
                    Continue();
                }   
            }
        }
        else
        {
            buttonPressed = false;
        }
           
    }

    private void Pause()
    {
        paused = true;
        confirmation.SetActive(false);
        menu.SetActive(true);
        pauseMenuDisplay.SetActive(true);
        //Time.timeScale = 0;
    }

    public void Continue()
    {
        paused = false;
        pauseMenuDisplay.SetActive(false);
        confirmation.SetActive(false);
        menu.SetActive(true);
        //Time.timeScale = 1;
    }

    public void ExitConfirm()
    {
        menu.SetActive(false);
        confirmation.SetActive(true);    
    }

    public void CancelExit()
    {
        confirmation.SetActive(false);
        menu.SetActive(true);
    }
}
