using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor{
    public Stage PlayerStage;
    [Space (15f), Header ("MainStage1")]
    public int rank1;
    public string title1;
    public List<Stage> Stage1 = new List<Stage>();
    [Space (15f), Header ("MainStage2")]
    public int rank2;
    public string title2;
    public List<Stage> Stage2 = new List<Stage>();
    [Space (15f), Header ("SubStage")]
    public List<Stage> SubStage = new List<Stage>();
    [HideInInspector]public bool addedStage1;
    [HideInInspector]public bool addedSubStage;
    [HideInInspector]public bool addedStage2;


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
    public Stage player_battleCard; // 플레이어 카드
    public List<CardAbility> player_cardDic;
    public List<CardAbility> collected_card;

    public List<AchieveMent> achiveItms= new List<AchieveMent>();

    public Floor FloorOfBattle;
    public Floor FloorOfResource;
    public Floor FloorOfSocial;

    [HideInInspector]

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
                case 1: floor_.Stage1.Add(stage.stage); 
                floor_.addedStage1 = true;break;     
                case 2: floor_.Stage2.Add(stage.stage); 
                floor_.addedStage2 = true;break;    
                case 3: floor_.SubStage.Add(stage.stage); 
                floor_.addedSubStage = true;break;    
            }
        }
    }

    public void AddPlayerCardChar(List<Character> chars){
        Floor floor_ = new Floor();
        switch(floor){
         case 1:floor_ = FloorOfBattle; break;
         case 2:floor_ = FloorOfResource; break;
         case 3:floor_ = FloorOfSocial; break;
        }
        List<Character> charlist = new List<Character>(floor_.PlayerStage.characters);
        Debug.Log(charlist.Count);
        charlist.RemoveAll(x => x == null);
        Debug.Log(charlist.Count);
        charlist.AddRange(chars);
        for(int i = 0; i < charlist.Count; i++){
            floor_.PlayerStage.characters[i] = charlist[i];
            
        }

    }

    public void AddCardDic(List<CardAbility> cards){
        player_cardDic.AddRange(cards);
    }

}
