using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Menu : MonoBehaviour
{
    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PressQuitButton()
    {
        Application.Quit();
    }

    public virtual void Return()
    {

    }
}
