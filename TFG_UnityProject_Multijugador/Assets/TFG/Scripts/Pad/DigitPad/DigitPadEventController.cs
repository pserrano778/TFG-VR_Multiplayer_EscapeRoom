using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitPadEventController : MonoBehaviour
{
    public string number;
    public DigitPadController[] digits;
    public Animator animator;
    public GameObject objectInside;

    private bool locked = true;

    private void Start()
    {

    }

    public void CheckNumber()
    {
        if (locked)
        {
            string numberStr = "";

            for (int i=0; i<digits.Length; i++)
            {
                numberStr += digits[i].digitText.text;
            }

            if (number.Equals(numberStr)){

                if (!animator.enabled)
                {
                    animator.enabled = true;
                }

                PlayAnimation();
                locked = false;
            }
        }
    }

    protected void PlayAnimation()
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
