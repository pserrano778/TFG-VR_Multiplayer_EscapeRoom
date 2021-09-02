using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public Animation openAnimation;

    public GameObject postBoxFlag;
    public GameObject key;

    private bool closed = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Si el buzón está cerrado
        if (closed)
        {
            // Si la flag de buzón entra en el rango 260-300, se abre el buzón
            if (postBoxFlag.transform.eulerAngles.z > 260 && postBoxFlag.transform.eulerAngles.z < 300)
            {
                key.SetActive(true);
                closed = false;
                openAnimation.Play();
            }
        }
    }

    public bool GetClosed()
    {
        return closed;
    }

    public void SetClosed(bool doorClosed)
    {
        closed = doorClosed;
    }
}
