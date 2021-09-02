using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectColliderPuzzleController : MonoBehaviour
{
    // Cerrojo para evitar que dos objetos comprueben el valor de objetos activos al mismo tiempo
    protected static readonly object checkingActiveObjects = new object();

    // Cerrojo para evitar que dos entidades modifiquen el valor de objetos al mismo tiempo
    protected static readonly object modifyingNumberOfObjects = new object();

    private bool opened;

    private int objectsActive;

    public int objectsActiveToOpen;

    public GameObject[] colliders;
    public string nameOfAnimation;

    // Start is called before the first frame update
    void Start()
    {
        opened = false;
        objectsActive = 0;
    }

    public void ChangeObjectState(bool active)
    {
        if (active)
        {
            // Comprobación de seguridad para no superar el número de objetos activos
            if (objectsActive < objectsActiveToOpen)
            {
                ActivateObject();
            }
        }
        else
        {
            // Comprobación de seguridad para no establecer un número de objetos activos negativo
            if (objectsActive > 0)
            {
                DeactivateObject();
            }
        }
    }

    protected void ActivateObject()
    {
        lock (modifyingNumberOfObjects)
        {
            objectsActive++;

            // Cuando se activa un objeto se comprueba si se han activado todos
            CheckNumberOfObjectsActive();
        }
    }

    protected void DeactivateObject()
    {
        lock (modifyingNumberOfObjects)
        {
            objectsActive--;
        }
    }

    private void CheckNumberOfObjectsActive()
    {
        // Bloquear el cerrojo
        lock (checkingActiveObjects)
        {
            // Si no están abiertas las puertas
            if (!opened)
            {
                // Si se han activado los objetos necesarios, se abren las puertas
                if (objectsActive == objectsActiveToOpen)
                {
                    Open();
                }
            }
        } // Desbloquear el cerrojo
    }

    protected virtual void Open()
    {
        opened = true;

        // Se desactivan los colliders asociados
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].SetActive(false);
        }

        GetComponent<Animator>().Play(nameOfAnimation, 0, 0.0f);
    }
}
