using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerMenu : Menu
{
    private string saveName = "house";

    private string savePath;

    public Button newGameButton;
    public Button continueGameButton;
    private bool saveActive = false;

    public GameObject previousMenu;
    public GameObject nextMenu;

    private void Awake()
    {
        currentMenu.SetActive(false);
    }

    private void Start()
    {
        string dataPath = Application.persistentDataPath;
        savePath = dataPath + "/" + saveName + ".save";

        if (!System.IO.File.Exists(savePath))
        {
            saveActive = false;
            continueGameButton.gameObject.SetActive(false);
            newGameButton.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            saveActive = true;
        }
    }

    public override void Return()
    {
        GoNextMenu(previousMenu);
    }

    public void ContinuarPartida()
    {
        LoadScene("Individual");
    }

    public void NuevaPartida()
    {
        if (saveActive)
        {
            GoNextMenu(nextMenu);
        }
        else
        {
            ContinuarPartida();
        }
    }
}
