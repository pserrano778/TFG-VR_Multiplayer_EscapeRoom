using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator = null;

    public bool openTrigger = false;
    public bool closeTrigger = false;

    public bool closed = false;

    public string tagOpen = "";
    public string tagClose = "";

    public GameObject mark = null;

    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Si la puerta está cerrada
        if (closed) 
        {
            // Si tiene activo el disparador para abrirla
            if (openTrigger)
            {
                // Si coincide la etiqueta del objeto con la de apertura
                if (other.CompareTag(tagOpen))
                {
                    float animationTime = 0;
                    if (other.CompareTag("Key"))
                    {
                        animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();
                    }
                    else
                    {
                        Destroy(other.gameObject, 1);
                    }
                    closed = false;
                    StartCoroutine(OpenAfterAnim(animationTime));       
                }
            }
        }
        else // Puerta abierta
        {
            // Si tiene activo el disparador para cerrarla
            if (closeTrigger)
            {
                // Si coincide la etiqueta del objeto con la de cierre
                if (other.CompareTag(tagClose))
                {
                    animator.Play("doorClose", 0, 0.0f);
                    other.gameObject.SetActive(false);
                    closed = true;
                }
                else // Se busca al jugador
                {
                    if (tagClose == "Player" && other.gameObject.ToString().Equals("[VRTK][AUTOGEN][BodyColliderContainer] (UnityEngine.GameObject)"))
                    {
                        if (mark != null)
                        {
                            mark.SetActive(true);
                        }
                        animator.Play("doorClose", 0, 0.0f);
                        gameObject.SetActive(false);
                        closed = true;
                    }
                }
            }
        }
    }

    IEnumerator OpenAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        animator.Play("doorOpen", 0, 0.0f);
    }
}
