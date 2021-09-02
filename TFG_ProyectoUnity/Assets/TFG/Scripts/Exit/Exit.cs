using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private static bool completed = false;
    public SaveManager saveManager = null;
    public GameObject manager = null;

    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Se busca al jugador  
        if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere")
        {
            ExitGame();
        }
    }

    protected virtual void ExitGame()
    {
        SetCompleted(true);
        if (saveManager != null)
        {
            // Se borran los datos
            saveManager.DeleteSaveData();
        }
        if (manager != null)
        {
            Destroy(manager);
        }
        SceneManager.LoadScene("Menus");
    }

    public static bool GetCompleted()
    {
        return completed;
    }

    public static void SetCompleted(bool isCompleted)
    {
        completed = isCompleted;
    }
}
