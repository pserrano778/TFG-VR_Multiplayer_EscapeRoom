using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    public SaveManager saveManager;
    public GameObject pauseMenuDisplay;

    private bool buttonPressed = false;
    private bool paused = false;

    private void Start()
    {
        pauseMenuDisplay.SetActive(false);
    }

    private void Update()
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

    public override void Return()
    {
        LoadScene("MenuModoUnJugador");
    }

    public void Save()
    {
        saveManager.Save();
    }

    private void Pause()
    {
        paused = true;
        pauseMenuDisplay.SetActive(true);
        //Time.timeScale = 0;
    }

    public void Continue()
    {
        paused = false;
        pauseMenuDisplay.SetActive(false);
        //Time.timeScale = 1;
    }
}
