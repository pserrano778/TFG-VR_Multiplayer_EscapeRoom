using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Photon.Pun;
public class ObjectToOpenFurnitureBehaviour : MonoBehaviour
{
    public Animator furnitureAnimator;
    public GameObject note;
    private bool objectInside = true;
    public string tagToCompare;
    public string nameOfOpenAnimation;
    public string nameOfCloseAnimation;
    // Start is called before the first frame update
    void Start()
    {
        note.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            if (other.gameObject.GetComponent<PhotonView>() != null && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponent<PhotonView>().RPC("Open", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            if (other.gameObject.GetComponent<PhotonView>() != null && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponent<PhotonView>().RPC("Close", RpcTarget.All);
            }
        }
    }

    IEnumerator WaitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void ChangeObjectInside(bool isObjectInside)
    {
        objectInside = isObjectInside;
    }

    [PunRPC]
    private void Open()
    {
        furnitureAnimator.Play(nameOfOpenAnimation, 0, 0.0f);
        note.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
        StartCoroutine(WaitForAnimation(furnitureAnimator.GetCurrentAnimatorStateInfo(0).length * furnitureAnimator.GetCurrentAnimatorStateInfo(0).speed));
    }

    [PunRPC]
    private void Close()
    {
        furnitureAnimator.Play(nameOfCloseAnimation, 0, 0.0f);
        StartCoroutine(WaitForAnimation(furnitureAnimator.GetCurrentAnimatorStateInfo(0).length * furnitureAnimator.GetCurrentAnimatorStateInfo(0).speed));
        if (objectInside && !note.GetComponent<Rigidbody>().isKinematic)
        {
            note.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
        }
    }
}
