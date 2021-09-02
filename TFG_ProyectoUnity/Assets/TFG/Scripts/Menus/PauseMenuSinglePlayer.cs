using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSinglePlayer : PauseMenu
{
    public SaveManager saveManager;

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
        LoadScene("Menus");
    }

    public void Save()
    {
        if (saveManager != null)
        {
            saveManager.Save();
        }
    }
}
