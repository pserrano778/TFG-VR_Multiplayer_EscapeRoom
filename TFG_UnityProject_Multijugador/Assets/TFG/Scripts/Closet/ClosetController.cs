using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetController : MonoBehaviour
{
    private bool[] runesActive = { true, true };
    private bool doorsOpened = false;

    public GameObject objectInside;

    private void Start()
    {
        objectInside.SetActive(false);
    }
    public void DisebleRune(int rune)
    {
        if (rune >= 0 && rune < runesActive.Length)
        {
            runesActive[rune] = false;
        }

        CheckRunes();
    }

    private void CheckRunes()
    {
        if (!doorsOpened)
        {
            bool allRunesDisabled = true;

            for (int i = 0; i < runesActive.Length && allRunesDisabled; i++)
            {
                allRunesDisabled = !runesActive[i];
            }

            if (allRunesDisabled)
            {
                objectInside.SetActive(true);

                GetComponent<Animator>().Play("openDoors", 0, 0.0f);
                
                doorsOpened = true;
            }
        }
        
    }
}
