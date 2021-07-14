using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetController : MonoBehaviour
{
    // Cerrojo para evitar que dos objetos comprueben la runa al mismo tiempo
    private static readonly object comprobandoRunas = new object();

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
        // Bloquear el cerrojo
        lock (comprobandoRunas)
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
                    doorsOpened = true;
                    objectInside.SetActive(true);

                    GetComponent<Animator>().Play("openDoors", 0, 0.0f);
                }
            }
        } // Desbloquear el cerrojo
    }
}
