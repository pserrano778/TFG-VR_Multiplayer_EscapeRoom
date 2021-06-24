using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitPadEventController : MonoBehaviour
{
    public uint number;
    public DigitPadController[] digits;
    public Animator animator;

    private bool locked = true;  

    public void CheckNumber()
    {
        if (locked)
        {
            string numberStr = "";

            for (int i=0; i<digits.Length; i++)
            {
                numberStr = digits[i].ToString();
            }

            if (number.ToString().Equals(numberStr)){
               PlayAnimation();
            }

            locked = true;
        }
    }

    private void PlayAnimation()
    {
        animator.Play("open", 0, 0.0f);
    }
}
