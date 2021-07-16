using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PadEventController : MonoBehaviour
{
    public string code;
    public Animator animator;
    public GameObject objectInside;
    protected bool locked = true;

    private void Start()
    {
        objectInside.SetActive(false);
    }
    public abstract void CheckCode();

    protected void PlayAnimation()
    {
        objectInside.SetActive(true);
        animator.Play("open", 0, 0.0f);
        locked = false;
    }
}
