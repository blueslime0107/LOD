using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{

    public string newFile;

    public GameDataBase db;
    public StageManager stageManager;
    public ProperContainer pc;

    public FloorScript fs;

    public string filePath;

    private void Awake()
    {
        stageManager = FindObjectOfType<StageManager>();
        pc = FindObjectOfType<ProperContainer>();
    }

    public void Save(){
        stageManager.SavetoDB();
        string jsonString = JsonUtility.ToJson(stageManager.stageManagerDB);
        string newFilePath = Application.dataPath + filePath;
        File.WriteAllText(newFilePath, jsonString);
    }

    public void Load(){
        string newFilePath = Application.dataPath + filePath;
        string jsonString = "";
        try{
        jsonString = File.ReadAllText(newFilePath);
        stageManager.stageManagerDB = JsonUtility.FromJson<StageManagerDB>(jsonString) ; 
        }
        catch{
            stageManager.LoadDataFromDB();
            Save();
            return;
        }
        
        stageManager.LoadDataFromDB();
        fs.RefreshStageCard();
    }

}

