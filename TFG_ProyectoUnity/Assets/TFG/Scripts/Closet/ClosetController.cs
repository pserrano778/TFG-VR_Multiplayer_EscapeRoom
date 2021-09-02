using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetController : MonoBehaviour
{
    // Cerrojo para evitar que dos objetos comprueben la runa al mismo tiempo
    private static readonly object checkingRunes = new object();

    private bool[] runesActive = { true, true };
    private bool doorsOpened = false;

    public GameObject objectInside;

    private void Start()
    {

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
        // Bloquear el cerrojo
        lock (checkingRunes)
        {
            // Si no están abiertas las puertas
            if (!doorsOpened)
            {
                bool allRunesDisabled = true;

                // Comprobamos si se han desactivado todas las ruanas
                for (int i = 0; i < runesActive.Length && allRunesDisabled; i++)
                {
                    allRunesDisabled = !runesActive[i];
                }

                // Si se han desactivado todas, se abren las puertas
                if (allRunesDisabled)
                {
                    if (!GetComponent<Animator>().enabled)
                    {
                        GetComponent<Animator>().enabled = true;
                    }

                    doorsOpened = true;
                    objectInside.SetActive(true);

                    GetComponent<Animator>().Play("openDoors", 0, 0.0f);
                }
            }
        } // Desbloquear el cerrojo
    }

    public void  SetRune(int rune, bool state)
    {
        if (0 <= rune && rune <= 1)
        {
            runesActive[rune] = state;
        }
    }

    public bool GetRune(int rune)
    {
        bool state = true;

        if (0<=rune && rune <= 1)
        {
            state = runesActive[rune];
        }

        return state;
    }

    public void SetOpened(bool state)
    {
        doorsOpened = state;
    }

    public bool GetOpened()
    {
        return doorsOpened;
    }

    public int GetNumRunes()
    {
        return runesActive.Length;
    }
}
