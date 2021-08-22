using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private static bool completed = false;
    public SaveManager saveManager = null;
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
        if (saveManager != null)
        {
            // Se borran los datos
            saveManager.DeleteSaveData();
        }
        SetCompleted(true);
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
