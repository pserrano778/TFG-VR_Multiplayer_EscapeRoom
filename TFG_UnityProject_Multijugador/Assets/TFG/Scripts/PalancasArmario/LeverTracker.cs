using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTracker : MonoBehaviour
{
    private string objectTag = "Palanca";
    public LeverClosetController controller;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectTag)){
            // Palanca activada
            controller.ChangeLeverState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(objectTag))
        {
            // Palanca desactivada
            controller.ChangeLeverState(false);
        }
    }
}
