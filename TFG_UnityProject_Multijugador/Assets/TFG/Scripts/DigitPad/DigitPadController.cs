using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DigitPadController : MonoBehaviour
{
    private int digit;
    public TextMeshProUGUI digitText;
 
    // Start is called before the first frame update
    void Start()
    {
        digit = 0;
    }

    public void NextDigit()
    {
        digit = (digit + 1) % 10;
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        digitText.text = digit.ToString();
    }
}
