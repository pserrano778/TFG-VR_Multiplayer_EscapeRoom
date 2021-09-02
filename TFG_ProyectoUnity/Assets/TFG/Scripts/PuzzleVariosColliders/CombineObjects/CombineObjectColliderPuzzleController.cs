using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
public class CombineObjectColliderPuzzleController : ObjectColliderPuzzleController
{
    public string tagCombination;
    private List<GameObject> activeObjects = new List<GameObject>();
    private List<int> idsAnimation = new List<int>();

    public SpriteRenderer[] sprites;
    public AudioClip audioOpen;

    public GameObject exitObject = null;

    public void SetNewObjectState(bool active, GameObject newObject, int id)
    {
        // Bloqueamos el cerrojo para no escribir en el array de forma simultánea
        lock (modifyingNumberOfObjects)
        {
            if (active)
            {
                // Guardamos el objeto y el id de animación
                activeObjects.Add(newObject);
                idsAnimation.Add(id);
            }
            else
            {
                // Eliminamos el objeto y el id de animación
                activeObjects.Remove(newObject);
                idsAnimation.Remove(id);
            }
        }
        
        ChangeObjectState(active);
    }

    protected override void Open()
    {
        string tagOfObjects = "";
        List<GameObject> orderedList = activeObjects.OrderBy(activeObject => activeObject.tag).ToList();
        orderedList.ForEach(activeObject => tagOfObjects += activeObject.tag);

        
        if (tagCombination.Equals(tagOfObjects))
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = new Color(0.3f, 0, 0);
            }

            for (int i=0; i< activeObjects.Count; i++)
            {
                // Lo hacemos Kinematic para ignorar colisones
                activeObjects[i].GetComponent<Rigidbody>().isKinematic = true;

                // Desactivamos los componentes necesarios
                activeObjects[i].GetComponent<VRTK.VRTK_InteractableObject>().enabled = false;
                if (activeObjects[i].GetComponent<PhotonTransformView>() != null)
                {
                    activeObjects[i].GetComponent<PhotonTransformView>().enabled = false;
                }

                // Reproducimos la animación
                activeObjects[i].GetComponent<Animator>().enabled = true;

                activeObjects[i].GetComponent<Animator>().Play("animateTo" + idsAnimation[i], 0, 0.0f);
            }
            exitObject.SetActive(true);
            base.Open();
            PlayAudioOpen();
        }
    }

    public void PlayAudioOpen()
    {
        if (audioOpen != null)
        {
            AudioSource.PlayClipAtPoint(audioOpen, GetComponent<Animator>().gameObject.transform.position, 0.3f);
        }
    }
}
