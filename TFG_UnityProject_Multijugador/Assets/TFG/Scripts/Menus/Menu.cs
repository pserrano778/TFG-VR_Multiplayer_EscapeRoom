using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Menu : MonoBehaviour
{
    public GameObject manager;
    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Destroy(manager);
    }

    public void PressQuitButton()
    {
        Application.Quit();
    }

    public virtual void Return()
    {

    }
}
