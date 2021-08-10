using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DoorController : MonoBehaviour
{
    public Animator animator = null;

    public bool openTrigger = false;
    public bool closeTrigger = false;

    public bool closed = false;

    public string tagOpen = "";
    public string tagClose = "";

    public GameObject mark = null;

    public AudioClip audioOpen;

    public AudioClip audioClose;

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
                    // Si el animador está desactivado, se activa
                    if (!animator.enabled)
                    {
                        animator.enabled = true;
                    }

                    float animationTime = 0;

                    if (other.CompareTag("Key"))
                    {
                        PhotonView photonView = other.GetComponent<PhotonView>();

                        // Si es un objeto instanciado de Photon View
                        if (photonView != null)
                        {
                            // Si es nuestro objeto
                            if (photonView.IsMine)
                            {
                                animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();
                                GetComponent<PhotonView>().RPC("OpenDoorRPC", RpcTarget.All, animationTime);
                            }
                        }
                        else
                        {
                            animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();
                            StartCoroutine(OpenAfterAnim(animationTime));
                        }
                    }
                    else
                    {
                        Destroy(other.gameObject, 1);
                        StartCoroutine(OpenAfterAnim(animationTime));
                    }
                    closed = false;
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
                    // Si el animador está desactivado, se activa
                    if (!animator.enabled)
                    {
                        animator.enabled = true;
                    }

                    animator.Play("doorClose", 0, 0.0f);
                    PlayAudioClose();
                    other.gameObject.SetActive(false);
                    closed = true;
                }
                else // Se busca al jugador
                {
                    if (tagClose == "Player" && (other.gameObject.ToString().Equals("[VRTK][AUTOGEN][BodyColliderContainer] (UnityEngine.GameObject)") || other.gameObject.name == "Sphere"))
                    {
                        // Si el animador está desactivado, se activa
                        if (!animator.enabled)
                        {
                            animator.enabled = true;
                        }

                        animator.Play("doorClose", 0, 0.0f);
                        PlayAudioClose();
                        if (mark != null)
                        {
                            StartCoroutine(SetMarkAfterClose(animator.GetCurrentAnimatorStateInfo(0).length * animator.GetCurrentAnimatorStateInfo(0).speed));
                        }
                    }
                }
            }
        }
    }

    [PunRPC]
    private void OpenDoorRPC(float animationTime)
    {
        StartCoroutine(OpenAfterAnim(animationTime));
    }

    IEnumerator OpenAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        animator.Play("doorOpen", 0, 0.0f);
        PlayAudioOpen();
    }

    IEnumerator SetMarkAfterClose(float animationTime)
    {
        closed = true;

        yield return new WaitForSeconds(animationTime);

        mark.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PlayAudioClose()
    {
        if (audioClose != null)
        {
            AudioSource.PlayClipAtPoint(audioClose, animator.gameObject.transform.position, 0.03f);
        }
    }

    public void PlayAudioOpen()
    {
        if (audioOpen != null)
        {
            AudioSource.PlayClipAtPoint(audioOpen, animator.gameObject.transform.position, 0.025f);
        }
    }
}
