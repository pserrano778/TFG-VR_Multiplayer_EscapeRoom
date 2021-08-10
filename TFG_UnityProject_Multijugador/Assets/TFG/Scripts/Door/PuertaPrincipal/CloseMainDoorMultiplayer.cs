using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CloseMainDoorMultiplayer : MonoBehaviour
{
    private bool closed = false;

    public Animator animator = null;
    public string tagClose = "";

    private PhotonView photonView;

    public GameObject mark = null;

    public AudioClip audioClose;

    private void Start()
    {
        photonView = PhotonView.Get(this);
    }

    // Sobreescribimos el disparador de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Si la puerta está abierta
        if (!closed)
        {   
            // Se busca al jugador  
            if (tagClose == "Player" && (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere"))
            {
                // Si el jugador no había entrado
                if (!PlayersInsideHouse.getPlayerState(NetworkManager.getPlayer()))
                {
                    // Se modifica el estado este por medio de RPC
                    photonView.RPC("setPlayerInside", RpcTarget.All, NetworkManager.getPlayer());
                }

                // Si era el último jugador en entrar, se llama a la función que cierra la puerta utilizando RPC
                if (PlayersInsideHouse.getPlayerState(0) && PlayersInsideHouse.getPlayerState(1))
                {
                    photonView.RPC("closeDoor", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    private void setPlayerInside(int player)
    {
        PlayersInsideHouse.setPlayerInside(player);
    }

    [PunRPC]
    private void closeDoor()
    {
        animator.Play("doorClose", 0, 0.0f);
        PlayAudioClose();
        if (mark != null)
        {
            StartCoroutine(SetMarkAfterClose(animator.GetCurrentAnimatorStateInfo(0).length * animator.GetCurrentAnimatorStateInfo(0).speed));
        }
        else
        {
            closed = true;
            gameObject.SetActive(false);
        }
    }

    IEnumerator OpenAfterAnim(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        animator.Play("doorOpen", 0, 0.0f);
    }

    IEnumerator SetMarkAfterClose(float animationTime)
    {
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
}
