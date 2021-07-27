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
        activeSave.objectsPosition.Clear();
        activeSave.objectsRotation.Clear();
        //activeSave.objectsPosition.Add(objectsToSave.character.transform.position);
        //activeSave.objectsRotation.Add(objectsToSave.character.transform.rotation);
        //activeSave.characterRespawnPosition = objectsToSave.character.transform.position;
        //activeSave.characterRespawnRotation = objectsToSave.character.transform.rotation;
        for (int i = 0; i < objectsToSave.objectsToControlPosRot.Count; i++)
        {
            activeSave.objectsPosition.Add(objectsToSave.objectsToControlPosRot[i].transform.position);
            activeSave.objectsRotation.Add(objectsToSave.objectsToControlPosRot[i].transform.rotation);
        }
    }

    private void UpdateObjectsState()
    {
        for (int i=0; i<activeSave.objectsPosition.Count; i++)
        {
            objectsToSave.objectsToControlPosRot[i].transform.position = activeSave.objectsPosition[i];
            objectsToSave.objectsToControlPosRot[i].transform.rotation = activeSave.objectsRotation[i];
        }
        //objectsToSave.character.transform.position = activeSave.objectsPosition[1];//activeSave.characterRespawnPosition;
        //objectsToSave.character.transform.rotation = activeSave.objectsRotation[1];//activeSave.characterRespawnRotation;
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

    public List<Vector3> objectsPosition;

    public List<Quaternion> objectsRotation;
    // public Vector3 characterRespawnPosition;

    // public Quaternion characterRespawnRotation;

}

[System.Serializable]
public class ObjectsToSave
{
    public List<GameObject> objectsToControlPosRot;
    
}