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
        SavePositionAndRotation();

        SavePositionAndRotationOfNonPermanentObjects();

        SaveDeactivableObjectsStatus();

        SaveLightsStatus();

        SaveClosedThingsState();

        SaveClosetDoor();

        SavePadsState();
    }

    private void UpdateObjectsState()
    {
        UpdatePositionAndRotation();

        UpdatePositionAndRotationOfNonPermanentObjects();

        UpdateDeactivableObjects();

        UpdateLights();

        UpdateClosedThingsState();

        UpdateClosetDoorState();

        UpdatePadsState();
    }

    private void SavePositionAndRotation()
    {
        // Limpiamos los datos actuales
        activeSave.objectsPosition.Clear();
        activeSave.objectsRotation.Clear();

        // Posiciones y rotaciones de los objetos
        for (int i = 0; i < objectsToSave.objectsToControlPosRot.Count; i++)
        {
            activeSave.objectsPosition.Add(objectsToSave.objectsToControlPosRot[i].transform.position);
            activeSave.objectsRotation.Add(objectsToSave.objectsToControlPosRot[i].transform.rotation);
        }
    }

    private void UpdatePositionAndRotation()
    {
        // Posicion y rotacion de los objetos
        for (int i = 0; i < activeSave.objectsPosition.Count; i++)
        {
            // Si hay un animador, se desactiva
            if (objectsToSave.objectsToControlPosRot[i].transform.parent.gameObject.GetComponent<Animator>() != null)
            {
                objectsToSave.objectsToControlPosRot[i].transform.parent.gameObject.GetComponent<Animator>().enabled = false;
            }
            else if (objectsToSave.objectsToControlPosRot[i].GetComponent<Animator>() != null)
            {
                objectsToSave.objectsToControlPosRot[i].GetComponent<Animator>().enabled = false;
            }
            objectsToSave.objectsToControlPosRot[i].transform.position = activeSave.objectsPosition[i];
            objectsToSave.objectsToControlPosRot[i].transform.rotation = activeSave.objectsRotation[i];
        }
    }

    private void SavePositionAndRotationOfNonPermanentObjects()
    {
        // Limpiamos los valores acutales
        activeSave.nonPermanentObjectsPosition.Clear();
        activeSave.nonPermanentObjectsRotation.Clear();
        activeSave.nonPermanentObjectsIsActive.Clear();
        activeSave.nonPermanentObjectsName.Clear();
        
        // Objetos que pueden ser eliminados
        for (int i = 0; i < objectsToSave.nonPermanentObjectsToControlPosRot.Count; i++)
        {
            if (objectsToSave.nonPermanentObjectsToControlPosRot[i] != null)
            {
                activeSave.nonPermanentObjectsPosition.Add(objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.position);
                activeSave.nonPermanentObjectsRotation.Add(objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.rotation);
                activeSave.nonPermanentObjectsIsActive.Add(objectsToSave.nonPermanentObjectsToControlPosRot[i].active);
                activeSave.nonPermanentObjectsName.Add(objectsToSave.nonPermanentObjectsToControlPosRot[i].name);
            }
        }
    }

    private void UpdatePositionAndRotationOfNonPermanentObjects()
    {
        // Objetos que pueden ser eliminados
        // No hay objetos guardados
        if (activeSave.nonPermanentObjectsPosition.Count == 0)
        {
            // Se eliminan los objetos de la escena
            for (int i = 0; i < objectsToSave.nonPermanentObjectsToControlPosRot.Count; i++)
            {
                objectsToSave.nonPermanentObjectsToControlPosRot[i].SetActive(false);
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
                    objectsToSave.nonPermanentObjectsToControlPosRot[i].SetActive(false);
                    Destroy(objectsToSave.nonPermanentObjectsToControlPosRot[i], 0);
                    objectsToSave.nonPermanentObjectsToControlPosRot.RemoveAt(i);

                }

                // Si el objeto está guardado, se actualiza
                if (objectsToSave.nonPermanentObjectsToControlPosRot[i].name == activeSave.nonPermanentObjectsName[i])
                {
                    objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.position = activeSave.nonPermanentObjectsPosition[i];
                    objectsToSave.nonPermanentObjectsToControlPosRot[i].transform.rotation = activeSave.nonPermanentObjectsRotation[i];
                    objectsToSave.nonPermanentObjectsToControlPosRot[i].SetActive(activeSave.nonPermanentObjectsIsActive[i]);
                }
            }

            // Si han quedado objetos por eliminar, se elimina por el final hasta que el tamaño de la lista coincida
            while(objectsToSave.nonPermanentObjectsToControlPosRot.Count != activeSave.nonPermanentObjectsPosition.Count)
            {
                objectsToSave.nonPermanentObjectsToControlPosRot[activeSave.nonPermanentObjectsPosition.Count].SetActive(false);
                Destroy(objectsToSave.nonPermanentObjectsToControlPosRot[activeSave.nonPermanentObjectsPosition.Count], 0);
                objectsToSave.nonPermanentObjectsToControlPosRot.RemoveAt(activeSave.nonPermanentObjectsPosition.Count);
            }
        }
    }

    private void SaveDeactivableObjectsStatus()
    {
        // Limpiamos los valores actuales
        activeSave.deactivableObjects.Clear();

        // Objetos que pueden activarse y desactivarse
        for (int i = 0; i < objectsToSave.deactivableObjects.Count; i++)
        {
            activeSave.deactivableObjects.Add(objectsToSave.deactivableObjects[i].active);
        }
    }

    private void UpdateDeactivableObjects()
    {
        // Objetos que pueden activarse y desactivarse
        for (int i = 0; i < activeSave.deactivableObjects.Count; i++)
        {
            objectsToSave.deactivableObjects[i].SetActive(activeSave.deactivableObjects[i]);
        }
    }

    private void SaveLightsStatus()
    {
        // Limpiamos los valores actuales
        activeSave.lightsOff.Clear();

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

    private void UpdateLights()
    {
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

    private void SaveClosedThingsState()
    {
        // Limpiamos los valores actuales
        activeSave.closedThings.Clear();

        // Puertas
        for (int i = 0; i < objectsToSave.closedThings.Count; i++)
        {
            if (objectsToSave.closedThings[i].GetComponent<DoorController>() != null)
            {
                activeSave.closedThings.Add(objectsToSave.closedThings[i].GetComponent<DoorController>().closed);
            }
            else if (objectsToSave.closedThings[i].GetComponent<Open>() != null)
            {
                activeSave.closedThings.Add(objectsToSave.closedThings[i].GetComponent<Open>().GetClosed());
            }
            else if (objectsToSave.closedThings[i].GetComponent<OpenWithKey>() != null)
            {
                activeSave.closedThings.Add(objectsToSave.closedThings[i].GetComponent<OpenWithKey>().GetClosed());
            }
        }
    }

    private void UpdateClosedThingsState()
    {
        // Puertas
        for (int i = 0; i < activeSave.closedThings.Count; i++)
        {
            if (objectsToSave.closedThings[i].GetComponent<DoorController>() != null)
            {
                objectsToSave.closedThings[i].GetComponent<DoorController>().closed = activeSave.closedThings[i];
            }
            else if(objectsToSave.closedThings[i].GetComponent<Open>() != null)
            {
                objectsToSave.closedThings[i].GetComponent<Open>().SetClosed(activeSave.closedThings[i]);
            }
            else if (objectsToSave.closedThings[i].GetComponent<OpenWithKey>() != null)
            {
                objectsToSave.closedThings[i].GetComponent<OpenWithKey>().SetClosed(activeSave.closedThings[i]);
            }
        }
    }

    private void SaveClosetDoor()
    {
        // Limpiamos los valores actuales
        activeSave.roomClosetRunes.Clear();
        activeSave.roomClosetOpened.Clear();

        for(int i = 0; i < objectsToSave.roomCloset.Count; i++)
        {
            activeSave.roomClosetOpened.Add(objectsToSave.roomCloset[i].GetComponent<ClosetController>().GetOpened());

            for (int j = 0; j < objectsToSave.roomCloset[i].GetComponent<ClosetController>().GetNumRunes(); j++)
            {
                activeSave.roomClosetRunes.Add(objectsToSave.roomCloset[i].GetComponent<ClosetController>().GetRune(j));
            }
        }
    }

    private void UpdateClosetDoorState()
    {
        for (int i = 0; i < activeSave.roomClosetOpened.Count; i++)
        {
            objectsToSave.roomCloset[i].GetComponent<ClosetController>().SetOpened(activeSave.roomClosetOpened[i]);

            for (int j = 0; j < activeSave.roomClosetRunes.Count; j++)
            {
                objectsToSave.roomCloset[i].GetComponent<ClosetController>().SetRune(j, activeSave.roomClosetRunes[j]);
            }
        }
    }

    private void SavePadsState()
    {
        // Limpiamos los valores actuales
        activeSave.padLocked.Clear();
        activeSave.digitsOfButtons.Clear();

        for (int i = 0; i < objectsToSave.pads.Count; i++)
        {
            if (objectsToSave.pads[i].GetComponent<DigitPadEventController>() != null)
            {
                activeSave.padLocked.Add(objectsToSave.pads[i].GetComponent<DigitPadEventController>().GetLocked());
            }
            else if (objectsToSave.pads[i].GetComponent<ColourPadEventController>() != null)
            {
                activeSave.padLocked.Add(objectsToSave.pads[i].GetComponent<ColourPadEventController>().GetLocked());
            }
        }

        for (int i = 0; i < objectsToSave.buttonsOfPads.Count; i++)
        {
            if (objectsToSave.buttonsOfPads[i].GetComponent<DigitPadController>() != null)
            {
                activeSave.digitsOfButtons.Add(objectsToSave.buttonsOfPads[i].GetComponent<DigitPadController>().GetDigit());
            }
            else if (objectsToSave.buttonsOfPads[i].GetComponent<ColourPadController>() != null)
            {
                activeSave.digitsOfButtons.Add(objectsToSave.buttonsOfPads[i].GetComponent<ColourPadController>().getColor());
            }
        }
    }

    private void UpdatePadsState()
    {
        for (int i = 0; i < activeSave.padLocked.Count; i++)
        {
            if (objectsToSave.pads[i].GetComponent<DigitPadEventController>() != null)
            {
                objectsToSave.pads[i].GetComponent<DigitPadEventController>().SetLocked(activeSave.padLocked[i]);
            }
            else if (objectsToSave.pads[i].GetComponent<ColourPadEventController>() != null)
            {
                objectsToSave.pads[i].GetComponent<ColourPadEventController>().SetLocked(activeSave.padLocked[i]);
            }
        }

        for (int i = 0; i < activeSave.digitsOfButtons.Count; i++)
        {
            if (objectsToSave.buttonsOfPads[i].GetComponent<DigitPadController>() != null)
            {
                objectsToSave.buttonsOfPads[i].GetComponent<DigitPadController>().SetDigit(activeSave.digitsOfButtons[i]);
            }
            else if (objectsToSave.buttonsOfPads[i].GetComponent<ColourPadController>() != null)
            {
                objectsToSave.buttonsOfPads[i].GetComponent<ColourPadController>().setColor((char)activeSave.digitsOfButtons[i]);
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
    public List<bool> nonPermanentObjectsIsActive;
    public List<string> nonPermanentObjectsName;

    public List<bool> deactivableObjects;

    public List<bool> lightsOff;

    public List<bool> closedThings;

    public List<bool> roomClosetRunes;
    public List<bool> roomClosetOpened;

    public List<bool> padLocked;

    public List<int> digitsOfButtons;   
}

[System.Serializable]
public class ObjectsToSave
{
    public List<GameObject> objectsToControlPosRot;

    public List<GameObject> nonPermanentObjectsToControlPosRot;

    public List<GameObject> deactivableObjects;

    public List<GameObject> lights;

    public List<GameObject> closedThings;

    public List<GameObject> roomCloset;

    public List<GameObject> pads;

    public List<GameObject> buttonsOfPads;
}