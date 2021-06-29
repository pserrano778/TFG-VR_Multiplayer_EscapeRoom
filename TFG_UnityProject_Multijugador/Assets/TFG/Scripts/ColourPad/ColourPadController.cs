using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColourPadController : MonoBehaviour
{
    private char color;
 
    // Start is called before the first frame update
    void Start()
    {
        color = 'b';
        setNewColor(Color.blue);
    }

    public void NextColor()
    {
        if (color == 'b'){
            color = 'r';
            setNewColor(Color.red);
        }
        else if (color == 'r')
        {
            color = 'g';
            setNewColor(Color.green);
        }
        else
        {
            color = 'b';
            setNewColor(Color.blue);
        }
    }

    private void setNewColor(Color color)
    {
        var newColors = GetComponent<Button>().colors;

        newColors.normalColor = color;

        var transparentColor = color;
        transparentColor.a = 0.6f;

        newColors.highlightedColor = transparentColor;

        GetComponent<Button>().colors = newColors;
    }

    public char getColor()
    {
        return color;
    }
}
