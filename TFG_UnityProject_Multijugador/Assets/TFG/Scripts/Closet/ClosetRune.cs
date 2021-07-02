using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetRune : MonoBehaviour
{
    public ClosetController closetController;
    public int rune;
    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Si coincide la etiqueta del objeto con el de objeto actual
        if (other.CompareTag(tag))
        {
            float animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();
            StartCoroutine(NotifyAfterAnim(animationTime));
        }
    }

    IEnumerator NotifyAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        Destroy(gameObject, 0);
        closetController.DisebleRune(rune);
    }
}
