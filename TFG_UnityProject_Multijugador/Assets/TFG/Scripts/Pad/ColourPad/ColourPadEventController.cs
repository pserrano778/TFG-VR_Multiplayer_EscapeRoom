using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPadEventController : MonoBehaviour
{
    public string str;
    public ColourPadController[] colours;
    public Animator animator;
    public GameObject objectInside;

    private bool locked = true;

    private void Start()
    {

    }

    public void CheckCode()
    {
        if (locked)
        {
            string currentStr = "";

            for (int i=0; i< colours.Length; i++)
            {
                currentStr += colours[i].getColor();
            }

            if (str.ToString().Equals(currentStr)){

                if (!animator.enabled)
                {
                    animator.enabled = true;
                }

                PlayAnimation();
                locked = false;
            } 
        }
    }

    private void PlayAnimation()
    {
        objectInside.SetActive(true);
        animator.Play("open", 0, 0.0f);
    }

    public bool GetLocked()
    {
        return locked;
    }

    public void SetLocked(bool isLocked)
    {
        locked = isLocked;
    }
}
