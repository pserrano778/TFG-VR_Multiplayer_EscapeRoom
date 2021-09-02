using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Menu : MonoBehaviour
{
    public GameObject manager;

    public GameObject currentMenu;

    public void LoadScene(string levelName)
    {
        if (manager != null)
        {
            Destroy(manager);
        }
        SceneManager.LoadScene(levelName);
    }

    public void PressQuitButton()
    {
        Application.Quit();
    }

    public virtual void Return()
    {

    }

    public void GoNextMenu(GameObject menu)
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }
        
        if (menu != null)
        {
            menu.SetActive(true);
        }
    }
}
