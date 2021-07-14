using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
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
            float animationTime = 0;
            PhotonView photonView = other.GetComponent<PhotonView>();

            // Si es un objeto instanciado de Photon View
            if (photonView != null)
            {
                // Si es nuestro objeto
                if (photonView.IsMine)
                {
                    animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();
                    GetComponent<PhotonView>().RPC("NotifyAfterAnimRPC", RpcTarget.All, animationTime);
                }
            }
            else
            {
                animationTime = other.GetComponent<KeyBehaviour>().OpenDoor();
                StartCoroutine(NotifyAfterAnim(animationTime));
            }
        }
    }

    [PunRPC]
    private void NotifyAfterAnimRPC(float animationTime)
    {
        StartCoroutine(NotifyAfterAnim(animationTime));
    }

    IEnumerator NotifyAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        Destroy(gameObject, 0);
        closetController.DisebleRune(rune);
    }
}
