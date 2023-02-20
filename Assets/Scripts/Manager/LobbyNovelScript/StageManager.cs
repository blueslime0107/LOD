using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Xml;

[System.Serializable]
public class Floor{
    public Stage PlayerStage;
    public Character[] player_Characters;
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

    public List<Stage> allStages(){
        List<Stage> stages = new List<Stage>();
        stages.AddRange(Stage1);
        stages.AddRange(Stage2);
        stages.AddRange(SubStage);
        return stages;
    }

}

[System.Serializable]
public class AddStage{
    public int floor;
    public int panel;
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

    public List<AchieveMent> achiveItms= new List<AchieveMent>();

    public Floor FloorOfBattle;
    public Floor FloorOfResource;
    public Floor FloorOfSocial;

} 

public class StageManager : MonoBehaviour
{
    public SaveManager saveManager;
    public StageManagerDB stageManagerDB;
    public TextAsset[] textAssets;

    [Space(30f)]

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

    public void LoadDataFromDB(){
        player_cardDic = stageManagerDB.player_cardDic;
        FloorOfBattle = stageManagerDB.FloorOfBattle;
        FloorOfResource = stageManagerDB.FloorOfResource;
        FloorOfSocial = stageManagerDB.FloorOfSocial;
        
        foreach(StageProperSave sps in stageManagerDB.stageProperSaves){
            sps.stage.discovered = sps.discovered;
            sps.stage.victoryed = sps.victoryed;
            sps.stage.noPrice = sps.noPrice;
        }
        if(stageManagerDB.FloorOfBattle.player_Characters[0] == null){return;}
        FloorOfBattle.PlayerStage.characters = stageManagerDB.FloorOfBattle.player_Characters;
        FloorOfResource.PlayerStage.characters = stageManagerDB.FloorOfResource.player_Characters;
        try{
        FloorOfSocial.PlayerStage.characters = stageManagerDB.FloorOfSocial.player_Characters;
        }catch{}

        LoadStageTitle();

    }

    public void SavetoDB(){

        stageManagerDB.player_cardDic = player_cardDic;
        stageManagerDB.FloorOfBattle = FloorOfBattle;
        stageManagerDB.FloorOfBattle.player_Characters = FloorOfBattle.PlayerStage.characters;

        stageManagerDB.FloorOfResource = FloorOfResource;
        stageManagerDB.FloorOfResource.player_Characters = FloorOfResource.PlayerStage.characters;
        try{
        stageManagerDB.FloorOfSocial = FloorOfSocial;
        stageManagerDB.FloorOfSocial.player_Characters = FloorOfSocial.PlayerStage.characters;}
        catch{
            Debug.Log("nah");
        }

        stageManagerDB.stageProperSaves = new List<StageProperSave>();
        
        foreach(Stage stage in stageManagerDB.FloorOfBattle.allStages()){
            StageProperSave sps = new StageProperSave();
            sps.stage = stage;
            sps.discovered = stage.discovered;
            sps.noPrice = stage.noPrice;
            sps.victoryed = stage.victoryed;
            stageManagerDB.stageProperSaves.Add(sps);
        }
        foreach(Stage stage in stageManagerDB.FloorOfResource.allStages()){
            StageProperSave sps = new StageProperSave();
            sps.stage = stage;
            sps.discovered = stage.discovered;
            sps.noPrice = stage.noPrice;
            sps.victoryed = stage.victoryed;
            stageManagerDB.stageProperSaves.Add(sps);
        }
        foreach(Stage stage in stageManagerDB.FloorOfSocial.allStages()){
            StageProperSave sps = new StageProperSave();
            sps.stage = stage;
            sps.discovered = stage.discovered;
            sps.noPrice = stage.noPrice;
            sps.victoryed = stage.victoryed;
            stageManagerDB.stageProperSaves.Add(sps);
        }
    }

    public void AddStageFun(List<AddStage> stages){
        if(nogiveStage){return;}
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

    public void AddPlayerCardChar(List<Character> chars){
        if(nogiveChar){return;}
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
        try{
        for(int i = 0; i < charlist.Count; i++){
            floor_.PlayerStage.characters[i] = charlist[i];
            
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
            switch(node.Attributes[0].Value){
                
                case "1": FloorOfBattle.rank1 = 1; FloorOfBattle.title1 = cardXML[0].InnerText; break;
                case "2": FloorOfResource.rank1 = 2; FloorOfResource.title1 = cardXML[0].InnerText; break;
                case "3": FloorOfBattle.rank2 = 3; FloorOfBattle.title2 = cardXML[0].InnerText; break;
                case "4": FloorOfResource.rank2 = 4; FloorOfResource.title2 = cardXML[0].InnerText; break;
                case "5": FloorOfSocial.rank1 = 5; FloorOfSocial.title1 = cardXML[0].InnerText; break;
                case "6": FloorOfSocial.rank2 = 6; FloorOfSocial.title2 = cardXML[0].InnerText; break;
            }
        }

    }


}
