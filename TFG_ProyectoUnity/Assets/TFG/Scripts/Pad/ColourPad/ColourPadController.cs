using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColourPadController : MonoBehaviour
{
    private char color = 'b';
 
    // Start is called before the first frame update
    void Start()
    {

    }

    public void NextColor()
    {
        if (color == 'b'){
            setColor('r');
        }
        else if (color == 'r')
        {
            setColor('g');
        }
        else
        {
            setColor('b');
        }
    }

    private void setNewColor(Color color)
    {
        var newColors = GetComponent<Button>().colors;

        newColors.normalColor = color;

        var transparentColor = color;
        transparentColor.a = 0.4f;

        newColors.highlightedColor = transparentColor;

        GetComponent<Button>().colors = newColors;
    }

    public char getColor()
    {
        return color;
    }

    public void setColor(char colorC)
    {
        if (colorC == 'b')
        {
            color = 'b';
            setNewColor(Color.blue);
        }
        else if (colorC == 'r')
        {
            color = 'r';
            setNewColor(Color.red);
        }
        else if (colorC == 'g')
        {
            color = 'g';
            setNewColor(Color.green);
        }
    }
}
