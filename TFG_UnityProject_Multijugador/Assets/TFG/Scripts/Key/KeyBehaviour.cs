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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<VRTK.VRTK_InteractableObject>().enabled = false;
        if (gameObject.GetComponent<PhotonTransformView>() != null)
        {
            gameObject.GetComponent<PhotonTransformView>().enabled = false;
            gameObject.GetComponent<TransferOwnership>().enabled = false;
        }
        animation.Play();
        float animationTime = animation.clip.length;
        Destroy(gameObject, animationTime);

        return animationTime;
    }
}
