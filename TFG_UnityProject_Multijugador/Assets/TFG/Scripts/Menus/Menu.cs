using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Menu : MonoBehaviour
{
    public GameObject manager;
    public void LoadScene(string levelName)
    {
        Destroy(manager);
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
