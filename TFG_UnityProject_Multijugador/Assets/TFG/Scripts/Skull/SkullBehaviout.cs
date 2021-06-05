using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SkullBehaviout : MonoBehaviour
{
    public Animator chestAnimator;
    public GameObject note;

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
            chestAnimator.Play("openChest", 0, 0.0f);
            note.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
            StartCoroutine(WaitForAnimation(chestAnimator.GetCurrentAnimatorStateInfo(0).length * chestAnimator.GetCurrentAnimatorStateInfo(0).speed));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bone")
        {
            chestAnimator.Play("closeChest", 0, 0.0f);
            StartCoroutine(WaitForAnimation(chestAnimator.GetCurrentAnimatorStateInfo(0).length * chestAnimator.GetCurrentAnimatorStateInfo(0).speed));
        }
    }

    IEnumerator WaitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
