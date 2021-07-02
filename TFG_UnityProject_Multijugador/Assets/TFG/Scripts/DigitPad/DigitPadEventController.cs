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
        objectInside.SetActive(false);
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
            print(number);
            print(numberStr);
            if (number.Equals(numberStr)){
               PlayAnimation();
            }

            locked = true;
        }
    }

    private void PlayAnimation()
    {
        objectInside.SetActive(true);
        animator.Play("open", 0, 0.0f);
    }
}
