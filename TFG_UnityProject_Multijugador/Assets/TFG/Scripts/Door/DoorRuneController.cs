using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRuneController : MonoBehaviour
{
    public Animator animator = null;


    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Si coincide la etiqueta del objeto con el de objeto actual
        if (other.CompareTag(tag))
        {
            float animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();
            StartCoroutine(OpenDoorAfterAnim(animationTime));       
        }
    }

    IEnumerator OpenDoorAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        Destroy(gameObject, 0);
        animator.Play("doorOpen", 0, 0.0f);  
    }
}
