using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeverClosetController : MonoBehaviour
{
    // Cerrojo para evitar que dos objetos comprueben la runa al mismo tiempo
    private static readonly object checkingLevers = new object();

    // Cerrojo para evitar que dos entidades modifiquen el valor de palancas al mismo tiempo
    private static readonly object modifyingNumberOfLevers = new object();

    private bool doorsOpened;

    private int leversActive;

    public int leversActiveToOpen;

    public GameObject[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        doorsOpened = false;
        leversActive = 0;
    }

    public void ChangeLeverState(bool active)
    {
        if (active)
        {
            // Comprobación de seguridad para no superar el número de palancas activas
            if (leversActive < leversActiveToOpen)
            {
                GetComponent<PhotonView>().RPC("ActivateLever", RpcTarget.All);
            }
        }
        else
        {
            // Comprobación de seguridad para no establecer un número de palancas activas negativo
            if (leversActive > 0)
            {
                GetComponent<PhotonView>().RPC("DeactivateLever", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void ActivateLever()
    {
        lock (modifyingNumberOfLevers)
        {
            leversActive++;

            // Cuando se activa una palanca se comprueba si se han activado todas
            CheckNumberOfLeversActive();
        }
    }

    [PunRPC]
    private void DeactivateLever()
    {
        lock (modifyingNumberOfLevers)
        {
            leversActive--;
        }
    }

    private void CheckNumberOfLeversActive()
    {
        // Bloquear el cerrojo
        lock (checkingLevers)
        {
            // Si no están abiertas las puertas
            if (!doorsOpened)
            {
                // Si se han activado todas las palancas necesarioas, se abren las puertas
                if (leversActive == leversActiveToOpen)
                {
                    doorsOpened = true;

                    // Se desactivan los colliders asociados
                    for (int i=0; i<colliders.Length; i++)
                    {
                        colliders[i].SetActive(false);
                    }

                    GetComponent<Animator>().Play("openDoors", 0, 0.0f);
                }
            }
        } // Desbloquear el cerrojo
    }
}
