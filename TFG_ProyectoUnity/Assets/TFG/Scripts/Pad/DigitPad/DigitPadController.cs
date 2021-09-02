using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DigitPadController : MonoBehaviour
{
    private int digit = 0;
    public TextMeshProUGUI digitText;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    public void NextDigit()
    {
        SetDigit((digit + 1) % 10);
    }

    protected void UpdateUIText()
    {
        digitText.text = digit.ToString();
    }

    public int GetDigit()
    {
        return digit;
    }

    public void SetDigit(int number)
    {
        if (number >= 0 && number <= 9)
        {
            digit = number;
            UpdateUIText();
        }
    }
}
