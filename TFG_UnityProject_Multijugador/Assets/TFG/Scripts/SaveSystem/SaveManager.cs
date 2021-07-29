using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData activeSave;

    public ObjectsToSave objectsToSave;

    public bool saveLoaded = false;

    private void Awake()
    {
        instance = this;
        Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteSaveData();
        }
    }

    public void Save()
    {
        UpdateSaveData();

        string dataPath = Application.persistentDataPath;
        string savePath = dataPath + "/" + activeSave.saveName + ".save";

        var serializer = new XmlSerializer(typeof(SaveData));     

        var stream = new FileStream(savePath, FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        string savePath = dataPath + "/" + activeSave.saveName + ".save";

        if (System.IO.File.Exists(savePath))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(savePath, FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            UpdateObjectsState();
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;
        string savePath = dataPath + "/" + activeSave.saveName + ".save";

        if (System.IO.File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }

    private void UpdateSaveData()
    {
        // Limpiamos los datos actuales
        activeSave.objectsPosition.Clear();
        activeSave.objectsRotation.Clear();

        activeSave.nonPermanentObjectsPosition.Clear();
        activeSave.nonPermanentObjectsRotation.Clear();
        activeSave.nonPermanentObjectsName.Clear();

        activeSave.deactivableObjects.Clear();

        activeSave.lightsOff.Clear();

        // Actualizamos los valores

        // Posiciones y rotaciones de los objetos
        for (int i = 0; i < objectsToSave.objectsToControlPosRot.Count; i++)
        {
            activeSave.objectsPosition.Add(objectsToSave.objectsToControlPosRot[i].transform.position);
            activeSave.objectsRotation.Add(objectsToSave.objectsToControlPosRot[i].transform.rotation);
        }

        // Objetos que pueden ser eliminados
        for (int i = 0; i < objectsToSave.nonPermanentObjectsToControlPosRot.Count; i++)
        {
            if(objectsToSave.nonPermanentObjectsToControlPosRot[i] != null)
            {
                activeSave.nonPermanentObjectsPosition.Add(objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.position);
                activeSave.nonPermanentObjectsRotation.Add(objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.rotation);
                activeSave.nonPermanentObjectsName.Add(objectsToSave.nonPermanentObjectsToControlPosRot[i].name);
            } 
        }

        // Objetos que pueden activarse y desactivarse
        for (int i = 0; i < objectsToSave.deactivableObjects.Count; i++)
        {
            activeSave.deactivableObjects.Add(objectsToSave.deactivableObjects[i].active);
        }

        // Luces
        for (int i = 0; i < objectsToSave.lights.Count; i++)
        {
            if (objectsToSave.lights[i].GetComponent<LightSwitchBehaviour>() != null)
            {
                activeSave.lightsOff.Add(objectsToSave.lights[i].GetComponent<LightSwitchBehaviour>().off);
            }
            else if (objectsToSave.lights[i].GetComponent<LampLightSwitchBehaviour>())
            {
                activeSave.lightsOff.Add(objectsToSave.lights[i].GetComponent<LampLightSwitchBehaviour>().off);
            }
        }
    }

    private void UpdateObjectsState()
    {
        // Posicion y rotacion de los objetos
        for (int i=0; i<activeSave.objectsPosition.Count; i++)
        {
            // Si hay un animador en el padre, se desactiva
            if (objectsToSave.objectsToControlPosRot[i].transform.parent.gameObject.GetComponent<Animator>() != null)
            {
                objectsToSave.objectsToControlPosRot[i].transform.parent.gameObject.GetComponent<Animator>().enabled = false;
            }
            objectsToSave.objectsToControlPosRot[i].transform.position = activeSave.objectsPosition[i];
            objectsToSave.objectsToControlPosRot[i].transform.rotation = activeSave.objectsRotation[i];
        }

        // Objetos que pueden ser eliminados
        // No hay objetos guardados
        if (activeSave.nonPermanentObjectsPosition.Count == 0)
        {
            // Se eliminan los objetos de la escena
            for (int i=0; i< objectsToSave.nonPermanentObjectsToControlPosRot.Count; i++)
            {
                Destroy(objectsToSave.nonPermanentObjectsToControlPosRot[i], 0);
                objectsToSave.nonPermanentObjectsToControlPosRot.RemoveAt(i);
            }
        }
        else
        {
            for (int i = 0; i < activeSave.nonPermanentObjectsPosition.Count; i++)
            {
                // Eliminamos los objetos que no están guardadoos
                while (objectsToSave.nonPermanentObjectsToControlPosRot[i].name != activeSave.nonPermanentObjectsName[i])
                {
                    Destroy(objectsToSave.nonPermanentObjectsToControlPosRot[i], 0);
                    objectsToSave.nonPermanentObjectsToControlPosRot.RemoveAt(i);

                }

                // Si el objeto está guardado, se actualiza
                if (objectsToSave.nonPermanentObjectsToControlPosRot[i].name == activeSave.nonPermanentObjectsName[i])
                {
                    objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.position = activeSave.nonPermanentObjectsPosition[i];
                    objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.rotation = activeSave.nonPermanentObjectsRotation[i];
                }
            }
        }

        // Objetos que pueden activarse y desactivarse
        for (int i = 0; i < activeSave.deactivableObjects.Count; i++)
        {
            objectsToSave.deactivableObjects[i].SetActive(activeSave.deactivableObjects[i]);
        }

        // Luces
        for (int i = 0; i < activeSave.lightsOff.Count; i++)
        {
            if (objectsToSave.lights[i].GetComponent<LightSwitchBehaviour>() != null)
            {
                objectsToSave.lights[i].GetComponent<LightSwitchBehaviour>().off = activeSave.lightsOff[i];
            }
            else if (objectsToSave.lights[i].GetComponent<LampLightSwitchBehaviour>() != null)
            {
                objectsToSave.lights[i].GetComponent<LampLightSwitchBehaviour>().off = activeSave.lightsOff[i];
            }
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

    public List<Vector3> objectsPosition;

    public List<Quaternion> objectsRotation;

    public List<Vector3> nonPermanentObjectsPosition;

    public List<Quaternion> nonPermanentObjectsRotation;

    public List<string> nonPermanentObjectsName;

    public List<bool> deactivableObjects;

    public List<bool> lightsOff;
}

[System.Serializable]
public class ObjectsToSave
{
    public List<GameObject> objectsToControlPosRot;

    public List<GameObject> nonPermanentObjectsToControlPosRot;

    public List<GameObject> deactivableObjects;

    public List<GameObject> lights;
}