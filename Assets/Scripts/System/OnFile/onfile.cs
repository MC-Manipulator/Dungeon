using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    private bool _showSaveDataButtons = false;
    private bool _showLoadDataButtons = false;
    void OnGUI()
    {

        Rect buttonSaveDataOne = new Rect(50, 150, 100, 30);
        Rect buttonSaveDataTwo = new Rect(50, 200, 100, 30);
        Rect buttonSaveDataThree = new Rect(50, 250, 100, 30);

        Rect buttonLoadDataOne = new Rect(50, 150, 100, 30);
        Rect buttonLoadDataTwo = new Rect(50, 200, 100, 30);
        Rect buttonLoadDataThree = new Rect(50, 250, 100, 30);

        Rect buttonSave = new Rect(50, 50, 100, 30);
        Rect buttonLoad = new Rect(50, 100, 100, 30);

        if (GUI.Button(buttonSave, "Save"))
        {
            Debug.Log("ButtonSave was clicked!");
            _showSaveDataButtons = !_showSaveDataButtons;
            if (_showLoadDataButtons == true)
                _showLoadDataButtons = false;
        }
        if (GUI.Button(buttonLoad, "Load"))
        {
            Debug.Log("ButtonLoad was clicked!");
            _showLoadDataButtons = !_showLoadDataButtons;
            if (_showSaveDataButtons == true)
                _showSaveDataButtons = false;
        }

        if (_showSaveDataButtons)
        {
            if (GUI.Button(buttonSaveDataOne, "SaveDataOne"))
            {
                Debug.Log("ButtonSaveDataOne was clicked!");
                Save("PlayerDataONE.sav");
                _showSaveDataButtons = false;
            }
            if (GUI.Button(buttonSaveDataTwo, "SaveDataTwo"))
            {
                Debug.Log("ButtonSaveDataTwo was clicked!");
                Save("PlayerDataTWO.sav");
                _showSaveDataButtons = false;
            }
            if (GUI.Button(buttonSaveDataThree, "SaveDataThree"))
            {
                Debug.Log("ButtonSaveDataThree was clicked!");
                Save("PlayerDataTHREE.sav");
                _showSaveDataButtons = false;
            }
        }
        if (_showLoadDataButtons)
        {
            if (GUI.Button(buttonLoadDataOne, "LoadDataOne"))
            {
                Debug.Log("ButtonLoadDataOne was clicked!");
                Load("PlayerDataONE.sav");
                _showLoadDataButtons = false;
            }
            if (GUI.Button(buttonLoadDataTwo, "LoadDataTwo"))
            {
                Debug.Log("ButtonLoadDataTwo was clicked!");
                Load("PlayerDataTWO.sav");
                _showLoadDataButtons = false;
            }
            if (GUI.Button(buttonLoadDataThree, "LoadDataThree"))
            {
                Debug.Log("ButtonLoadDataThree was clicked!");
                Load("PlayerDataTHREE.sav");
                _showLoadDataButtons = false;
            }
        }

    }

    void moveText()
    {
        float speed = 5;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }

    void Save(string saveFileName)
    {
        SaveByJson(saveFileName);
    }
    void Load(string saveFileName)
    {
        LoadFromJson(saveFileName);
    }

    void SaveByJson(string saveFileName)
    {
        SaveSystem.SaveByJson(saveFileName, SavingData());
    }
    void LoadFromJson(string saveFileName)
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(saveFileName);
        LoadData(saveData);
    }

    SaveData SavingData()
    {
        var saveData = new SaveData();

        saveData.x = transform.position.x;
        saveData.y = transform.position.y;

        return saveData;
    }
    void LoadData(SaveData saveData)
    {
        transform.position = new Vector3(saveData.x, saveData.y, 0);
    }
    void Start()
    {
        Debug.Log(gameObject.name);

    }
    void Update()
    {
        moveText();
    }
}

public class SaveSystem
{
    public static void SaveByJson(string saveFileName, object data)
    {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine("D:\\", saveFileName);

        File.WriteAllText(path, json);
        Debug.Log("Susscessfully save");
    }
    public static T LoadFromJson<T>(string saveFileName)
    {
        var path = Path.Combine("D:\\", saveFileName);
        var json = File.ReadAllText(path);

        var data = JsonUtility.FromJson<T>(json);
        Debug.Log("Susscessfully load");
        return data;
    }
    public static void DeleteSaveFile(string saveFileName)
    {
        var path = Path.Combine("D:\\", saveFileName);
        File.Delete(path);
        Debug.Log("Susscessfully delete");
    }
}
public class SaveData
{
    public float x;
    public float y;
}
