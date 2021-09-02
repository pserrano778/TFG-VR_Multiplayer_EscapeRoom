using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Permissions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Micrófono
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        // Lectura
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        // Escritura
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

}
