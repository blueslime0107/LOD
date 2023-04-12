using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{

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
        Debug.Log("Save");
        stageManager.SavetoDB();
        string jsonString = JsonUtility.ToJson(stageManager.stageManagerDB);
        string newFilePath = Application.persistentDataPath + filePath;
        File.WriteAllText(newFilePath, jsonString);
    }

    public void Load(){
        string newFilePath = Application.persistentDataPath + filePath;
        string jsonString = "";




        try{
            Debug.Log("파일있음");
        jsonString = File.ReadAllText(newFilePath);
        stageManager.stageManagerDB = JsonUtility.FromJson<StageManagerDB>(jsonString) ; 
        }
        catch{
            Debug.Log("파일없음");

            stageManager.LoadDataFromDB();
            fs.curFloor = stageManager.Floors[stageManager.preFloor];
            fs.RefreshStageCard();
            Save();
            return;
        }
        
        stageManager.LoadDataFromDB();
        fs.curFloor = stageManager.Floors[stageManager.preFloor];
        fs.RefreshStageCard();
    }

}

