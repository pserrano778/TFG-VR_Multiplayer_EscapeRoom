using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DoorRuneController : MonoBehaviour
{
    public Animator animator = null;

    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Si coincide la etiqueta del objeto con el de objeto actual
        if (other.CompareTag(tag))
        {
            float animationTime = 0;
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
                StartCoroutine(OpenDoorAfterAnim(animationTime));
            }   
        }
    }

    [PunRPC]
    private void OpenDoorRPC(float animationTime)
    {
        StartCoroutine(OpenDoorAfterAnim(animationTime));

    }

    IEnumerator OpenDoorAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        Destroy(gameObject, 0);
        animator.Play("doorOpen", 0, 0.0f);  
    }
}
