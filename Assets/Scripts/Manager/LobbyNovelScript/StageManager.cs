using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor{
    public Stage PlayerStage;
    public List<Stage> Stage1 = new List<Stage>();
    public List<Stage> Stage2 = new List<Stage>();
    public List<Stage> SubStage = new List<Stage>();
}
[System.Serializable]
public class AddStage{
    public int floor;
    public int panel;
    public Stage stage;
}

public class StageManager : MonoBehaviour
{
    public int floor;
    public int panel;

    public Stage play_stage; // 플레이 할 스테이지
    public Stage player_card; // 플레이어 카드

    public Floor FloorOfBattle;
    public Floor FloorOfResource;
    public Floor FloorOfSocial;

    private void Awake() {
        var obj = FindObjectsOfType<StageManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    public void AddStageFun(List<AddStage> stages){
        foreach(AddStage stage in stages){
            Floor floor_ = FloorOfBattle;
            switch(stage.floor){
                case 1:floor_ = FloorOfBattle; break;
                case 2:floor_ = FloorOfResource; break;
                case 3:floor_ = FloorOfSocial; break;
            }
            switch(stage.panel){
                case 1: floor_.Stage1.Add(stage.stage); break;     
                case 2: floor_.Stage2.Add(stage.stage); break;    
                case 3: floor_.SubStage.Add(stage.stage); break;    
            }
        }
    }

}
