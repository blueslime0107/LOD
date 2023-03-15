using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Xml;

[System.Serializable]
public class Floor{
    public string name;
    public int playerSlot;
    [Space (15f)]
    public List<Stage> Mainstage = new List<Stage>();
    [Space (15f)]
    public List<Stage> SubStage = new List<Stage>();

    public string battleBGM;

    public List<Stage> allStages(){
        List<Stage> stages = new List<Stage>();
        stages.AddRange(Mainstage);
        stages.AddRange(SubStage);
        return stages;
    }

}

[System.Serializable]
public class AddStage{
    public int floor;
    public bool sub;
    public Stage stage;
}

[System.Serializable]
public class StageProperSave{
    public Stage stage;
    public bool victoryed;
    public bool noPrice;
    public bool discovered;
}

[System.Serializable]
public class StagePlayerSave{
    public Stage PlayerStage;
    public Character[] player_Characters;
}

[System.Serializable]
public class StageManagerDB{

    public List<StageProperSave> stageProperSaves;
    public List<StagePlayerSave> stagePlayerSaves;

    public List<CardAbility> player_cardDic;

    public List<AchieveMent> achiveItms = new List<AchieveMent>();

    public List<Floor> Floors = new List<Floor>();

} 

public class StageManager : MonoBehaviour
{
    public SaveManager saveManager;
    public StageManagerDB stageManagerDB;
    public TextAsset[] textAssets;

    [Space(30f)]

    public Floor floor;

    [HideInInspector]public Stage play_stage; // 플레이 할 스테이지
    [HideInInspector]public Stage player_battleCard; // 플레이어 카드
    public List<CardAbility> player_cardDic;
    public List<CardAbility> collected_card;

    public List<AchieveMent> achiveItms= new List<AchieveMent>();

    public List<StagePlayerSave> PlayerStages = new List<StagePlayerSave>();
    public List<Floor> Floors = new List<Floor>();

    public int preFloor;

    [Space (15f), Header ("Debug")]
    public bool nogiveStage;
    public bool nogiveChar;
    public bool noGotoNewbie;

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
        
    
        if(noGotoNewbie){return;}
        saveManager.Load();
    }

    public void LoadDataFromDB(){
        player_cardDic = stageManagerDB.player_cardDic;
        Floors = stageManagerDB.Floors;
        
        foreach(StageProperSave sps in stageManagerDB.stageProperSaves){
            sps.stage.discovered = sps.discovered;
            sps.stage.victoryed = sps.victoryed;
            sps.stage.noPrice = sps.noPrice;
        }

        PlayerStages = stageManagerDB.stagePlayerSaves;

        foreach(StagePlayerSave stagePlayerSave in PlayerStages){
            stagePlayerSave.PlayerStage.characters = stagePlayerSave.player_Characters;
        }

        LoadStageTitle();

    }

    public void SavetoDB(){

        stageManagerDB.player_cardDic = player_cardDic;
        stageManagerDB.Floors = Floors;
        stageManagerDB.stagePlayerSaves = PlayerStages;
        stageManagerDB.stageProperSaves = new List<StageProperSave>();

        foreach(Floor floor in stageManagerDB.Floors){
            foreach(Stage stage in floor.allStages()){
            StageProperSave sps = new StageProperSave();
            sps.stage = stage;
            sps.discovered = stage.discovered;
            sps.noPrice = stage.noPrice;
            sps.victoryed = stage.victoryed;
            stageManagerDB.stageProperSaves.Add(sps);
        }
        }

        }
    

    public void AddStageFun(List<AddStage> stages){
        if(nogiveStage){return;}
        foreach(AddStage stage in stages){
            if(stage.sub){Floors[stage.floor-1].SubStage.Add(stage.stage); return;}
            else{Floors[stage.floor-1].Mainstage.Add(stage.stage);}
        }
    }

    public void AddPlayerCardChar(List<Character> chars){
        if(nogiveChar){return;}
        List<Character> charlist = new List<Character>(PlayerStages[floor.playerSlot].player_Characters);
        charlist.RemoveAll(x => x == null);
        charlist.AddRange(chars);
        try{
        for(int i = 0; i < charlist.Count; i++){
            PlayerStages[floor.playerSlot].player_Characters[i] = charlist[i];
            
        }}
        catch{Debug.LogWarning("CharFlow!");}

    }

    public void AddCardDic(List<CardAbility> cards){
        player_cardDic.AddRange(cards);
        saveManager.Save();
    }

    public void LoadStageTitle(){
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){ReadXML(textAssets[0]);}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){ReadXML(textAssets[1]);}
    }
    private void ReadXML(TextAsset filename){
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(filename.text);


        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Stage");
        foreach(XmlNode node in thisCardXML){
            XmlNodeList cardXML = node.ChildNodes;
        }

    }


}
