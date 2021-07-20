using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SkullBehaviour : MonoBehaviour
{
    public Animator chestAnimator;
    public GameObject note;
    private bool objectInside = true;
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
        if (other.tag == "Bone")
        {
            chestAnimator.Play("open", 0, 0.0f);
            note.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
            StartCoroutine(WaitForAnimation(chestAnimator.GetCurrentAnimatorStateInfo(0).length * chestAnimator.GetCurrentAnimatorStateInfo(0).speed));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bone")
        {
            chestAnimator.Play("close", 0, 0.0f);
            StartCoroutine(WaitForAnimation(chestAnimator.GetCurrentAnimatorStateInfo(0).length * chestAnimator.GetCurrentAnimatorStateInfo(0).speed));
            if (objectInside && !note.GetComponent<Rigidbody>().isKinematic)
            {
                note.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
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
}
