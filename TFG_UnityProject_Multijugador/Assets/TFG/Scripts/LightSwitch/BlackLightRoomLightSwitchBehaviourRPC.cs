using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Photon.Pun;

public class BlackLightRoomLightSwitchBehaviourRPC : MonoBehaviour
{
    public bool off = true;
    public Light light = null;
    public Animator animator = null;
    public GameObject trackerLinterna = null;
    public GameObject trackerJugadores = null;
    public Animator doorAnimator = null;
    public GameObject doorCode = null;
    private bool codeActive = false;
    public AudioClip audioClose = null;

    // Start is called before the first frame update
    void Start()
    {
        light.gameObject.SetActive(false);
        doorCode.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "[VRTK][AUTOGEN][BodyColliderContainer]" || other.gameObject.name == "Sphere")
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("turnLightSwitch", RpcTarget.All);            
        }
    }

    [PunRPC]
    private void turnLightSwitch()
    {
        // Si la luz está apagada
        if (off)
        {
            // Lanzar la animación On
            animator.Play("On", 0, 0.0f);
            // Activar la luz
            light.gameObject.SetActive(true);
            off = false;
        }
        else // Si está encendida
        {
            // Lanzar la animación Off
            animator.Play("Off", 0, 0.0f);
            // Desactivar la luz
            light.gameObject.SetActive(false);
            off = true;

            // Si el codigo no se ha activado
            if (!codeActive)
            {
                // Si hay un jugador en la sala, y el objeto no ha salido de la sala
                if (trackerJugadores.GetComponent<PlayersInsideCounter>().GetPlayersInside() == 1 && (trackerLinterna.GetComponent<RoomObjectTracker>().GetObjectInsideRoom()))
                {
                    closeDoor();
                }
            }
        }
    }

    private void closeDoor()
    {
        trackerJugadores.SetActive(false);
        trackerLinterna.SetActive(false);
        codeActive = true;
        doorAnimator.Play("doorClose", 0, 0.0f);
        PlayAudioClose();
        // Corrutina con la duracion de la animacion
        StartCoroutine(SetCodeAfterClose(doorAnimator.GetCurrentAnimatorStateInfo(0).length * doorAnimator.GetCurrentAnimatorStateInfo(0).speed));
    }

    IEnumerator SetCodeAfterClose(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        doorCode.SetActive(true);
    }

    public void PlayAudioClose()
    {
        if (audioClose != null)
        {
            AudioSource.PlayClipAtPoint(audioClose, doorAnimator.gameObject.transform.position, 0.3f);
        }
    }
}
