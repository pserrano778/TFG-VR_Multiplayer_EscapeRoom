using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyBehaviour : MonoBehaviour
{
    public Animation animation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float OpenDoor()
    {
        PhotonView photonView = gameObject.GetComponent<PhotonView>();
        if (photonView != null)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("startAnimationRPC", RpcTarget.All);
            }
        }
        else
        {
            startAnimation();
        }

        return animation.clip.length;
    }

    [PunRPC]
    private void startAnimationRPC()
    {
        startAnimation();
    }

    private void startAnimation()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<VRTK.VRTK_InteractableObject>().enabled = false;
        if (gameObject.GetComponent<PhotonTransformView>() != null)
        {
            gameObject.GetComponent<PhotonTransformView>().enabled = false;
        }
        animation.Play();
        float animationTime = animation.clip.length;

        PhotonView photonView = gameObject.GetComponent<PhotonView>();

        Destroy(gameObject, animationTime);
    }
}
