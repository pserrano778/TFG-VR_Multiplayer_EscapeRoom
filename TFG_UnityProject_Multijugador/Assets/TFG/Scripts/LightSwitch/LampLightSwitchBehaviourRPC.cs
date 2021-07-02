using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Photon.Pun;

public class LampLightSwitchBehaviourRPC : MonoBehaviour
{
    public bool off = true;
    public Light[] lights;
    public Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<lights.Length; i++)
        {
            lights[i].gameObject.SetActive(false);
        }
        
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
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].gameObject.SetActive(true);
            }
            off = false;
        }
        else // Si está encendida
        {
            // Lanzar la animación Off
            animator.Play("Off", 0, 0.0f);
            // Desactivar la luz
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].gameObject.SetActive(false);
            }
            off = true;
        }
    }
}
