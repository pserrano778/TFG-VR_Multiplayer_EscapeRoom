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
        objectInside.SetActive(false);
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
               PlayAnimation();
            }

            locked = true;
        }
    }

    private void PlayAnimation()
    {
        print("anim");
        objectInside.SetActive(true);
        animator.Play("open", 0, 0.0f);
    }
}
