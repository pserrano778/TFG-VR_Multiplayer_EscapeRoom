using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWithKey : MonoBehaviour
{
    private string tagOpen = "Key";
    private bool closed = true;

    public Animator animator = null;

    public GameObject objectInside;

    private void Start()
    {
        objectInside.SetActive(false);
    }
    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Si la puerta está cerrada
        if (closed)
        {
                // Si coincide la etiqueta del objeto con la de apertura
                if (other.CompareTag(tagOpen))
                {
                    float animationTime = 0;
                    animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();

                    closed = false;
                    objectInside.SetActive(true);
                    StartCoroutine(OpenAfterAnim(animationTime));
                }
            }
        }

    IEnumerator OpenAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        animator.Play("open", 0, 0.0f);
    }
}
