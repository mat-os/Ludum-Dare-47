using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool isUseSaveSystem = true;

    public SaveData SaveData;
    
    [SerializeField] private bool loadSpecificLevel = false;
    [SerializeField] private int specificLevelNow = 0;
    
    [ContextMenu("Save")]
    public void Save()
    {
        if (isUseSaveSystem)
        {
            var json = JsonUtility.ToJson(SaveData);

            File.WriteAllText(GetFilePath(), json);
        }
    }

    [ContextMenu("Load")]
    public SaveData Load()
    {
        if (isUseSaveSystem)
        {
            if (!File.Exists(GetFilePath())) Save();

            var json = File.ReadAllText(GetFilePath());

            SaveData = JsonUtility.FromJson<SaveData>(json);
        }

        return SaveData;
    }

    private string GetFilePath()
    {
        string folderPath = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer 
            ? Application.persistentDataPath : Application.dataPath) + "/Resources/Save/";
        string filePath = folderPath + "SaveData.txt";
        
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); 
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            
            var json = JsonUtility.ToJson(SaveData);
            File.WriteAllText(filePath, json);
        }

        return filePath;
        //return $"{Application.dataPath}/Resources/Save/SaveData.txt";
    }
    
    /*public string GetPath()
    {
        string folderPath = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer 
            ? Application.persistentDataPath : Application.dataPath) + "/Resources/Save/";
        string filePath = folderPath + "SaveData.txt";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); 
        }
        if (!File.Exists(filePath)) 
        {
            File.Create(filePath).Close();
            
            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(filePath, json);
        }
        return  filePath;
    }*/

    private void OnEnable()
    {
        if (SaveData.LevelNowForPlayer <= 0)
        {
            SaveData.LevelNowForPlayer = 1;
        }

        if (loadSpecificLevel)
        {
            SaveData.LevelNow = specificLevelNow;
        }
    }
    
}
[Serializable]
public class SaveData
{
    public int LevelNow = 0;
    
    public int LevelNowForPlayer = 1;
    
    public bool isLevelLoops = false;
    
   // private LevelConfig loopLevels = null;
    
    //private int countAppStartTimes;
}
