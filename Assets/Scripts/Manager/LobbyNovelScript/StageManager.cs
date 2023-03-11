using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Xml;

[System.Serializable]
public class Floor{
    public Stage PlayerStage;
    public Character[] player_Characters;
    [Space (15f), Header ("MainStage")]
    public List<Stage> Mainstage = new List<Stage>();
    [Space (15f), Header ("SubStage")]
    public List<Stage> SubStage = new List<Stage>();

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
public class StageManagerDB{

    public List<StageProperSave> stageProperSaves;

    public List<CardAbility> player_cardDic;

    public List<AchieveMent> achiveItms = new List<AchieveMent>();

    public List<Floor> FloorOfBattle;

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

    public List<Floor> Floors = new List<Floor>();

    [Space (15f), Header ("Debug")]
    public bool nogiveStage;
    public bool nogiveChar;
    public bool noGotoNewbie;

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
        
    
        if(noGotoNewbie){return;}
        saveManager.Load();
    }

    // public void LoadDataFromDB(){
    //     player_cardDic = stageManagerDB.player_cardDic;
    //     FloorOfBattle = stageManagerDB.FloorOfBattle;
    //     FloorOfResource = stageManagerDB.FloorOfResource;
    //     FloorOfSocial = stageManagerDB.FloorOfSocial;
        
    //     foreach(StageProperSave sps in stageManagerDB.stageProperSaves){
    //         sps.stage.discovered = sps.discovered;
    //         sps.stage.victoryed = sps.victoryed;
    //         sps.stage.noPrice = sps.noPrice;
    //     }
    //     if(stageManagerDB.FloorOfBattle.player_Characters[0] == null){return;}
    //     FloorOfBattle.PlayerStage.characters = stageManagerDB.FloorOfBattle.player_Characters;
    //     FloorOfResource.PlayerStage.characters = stageManagerDB.FloorOfResource.player_Characters;
    //     try{
    //     FloorOfSocial.PlayerStage.characters = stageManagerDB.FloorOfSocial.player_Characters;
    //     }catch{}

    //     LoadStageTitle();

    // }

    // public void SavetoDB(){

    //     stageManagerDB.player_cardDic = player_cardDic;
    //     stageManagerDB.FloorOfBattle = FloorOfBattle;
    //     stageManagerDB.FloorOfBattle.player_Characters = FloorOfBattle.PlayerStage.characters;

    //     stageManagerDB.FloorOfResource = FloorOfResource;
    //     stageManagerDB.FloorOfResource.player_Characters = FloorOfResource.PlayerStage.characters;
    //     try{
    //     stageManagerDB.FloorOfSocial = FloorOfSocial;
    //     stageManagerDB.FloorOfSocial.player_Characters = FloorOfSocial.PlayerStage.characters;}
    //     catch{
    //         Debug.Log("nah");
    //     }

    //     stageManagerDB.stageProperSaves = new List<StageProperSave>();
        
    //     foreach(Stage stage in stageManagerDB.FloorOfBattle.allStages()){
    //         StageProperSave sps = new StageProperSave();
    //         sps.stage = stage;
    //         sps.discovered = stage.discovered;
    //         sps.noPrice = stage.noPrice;
    //         sps.victoryed = stage.victoryed;
    //         stageManagerDB.stageProperSaves.Add(sps);
    //     }
    //     foreach(Stage stage in stageManagerDB.FloorOfResource.allStages()){
    //         StageProperSave sps = new StageProperSave();
    //         sps.stage = stage;
    //         sps.discovered = stage.discovered;
    //         sps.noPrice = stage.noPrice;
    //         sps.victoryed = stage.victoryed;
    //         stageManagerDB.stageProperSaves.Add(sps);
    //     }
    //     foreach(Stage stage in stageManagerDB.FloorOfSocial.allStages()){
    //         StageProperSave sps = new StageProperSave();
    //         sps.stage = stage;
    //         sps.discovered = stage.discovered;
    //         sps.noPrice = stage.noPrice;
    //         sps.victoryed = stage.victoryed;
    //         stageManagerDB.stageProperSaves.Add(sps);
    //     }
    // }

    public void AddStageFun(List<AddStage> stages){
        if(nogiveStage){return;}
        foreach(AddStage stage in stages){
            if(stage.sub){Floors[stage.floor].SubStage.Add(stage.stage); return;}
            else{Floors[stage.floor].Mainstage.Add(stage.stage);}
        }
    }

    public void AddPlayerCardChar(List<Character> chars){
        if(nogiveChar){return;}
        List<Character> charlist = new List<Character>(floor.PlayerStage.characters);
        charlist.RemoveAll(x => x == null);
        charlist.AddRange(chars);
        try{
        for(int i = 0; i < charlist.Count; i++){
            floor.PlayerStage.characters[i] = charlist[i];
            
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
