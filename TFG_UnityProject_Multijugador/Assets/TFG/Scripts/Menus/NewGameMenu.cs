using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewGameMenu : Menu
{ 
    private string savePath;
    private string saveName = "house";

    // Start is called before the first frame update
    void Start()
    {
        string dataPath = Application.persistentDataPath;
        savePath = dataPath + "/" + saveName + ".save";
    }

    public void NuevaPartida()
    {
        if (System.IO.File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        LoadScene("MenuNuevaPartida");
    }

    public override void Return()
    {
        LoadScene("MenuModoUnJugador");
    }
}
