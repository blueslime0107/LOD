using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Localization.Settings;

[System.Serializable]
public class Floor{
    public string name;
    [HideInInspector]public int playerSlot = 0;
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
public class StageAddress{
    public int floor;
    public bool sub;
}

[System.Serializable]
public class StageProperSave{
    public int stage;
    public bool victoryed;
    [HideInInspector]public bool noPrice;
    [HideInInspector]public bool discovered;
}

[System.Serializable]
public class StagePlayerSave{
    public int player_Characters_id;
    public List<int> player_cards = new List<int>();
}

[System.Serializable]
public class StageManagerDB{

    public List<StagePlayerSave> stagePlayerSaves;

    public List<int> player_cardDic;

    public List<StageProperSave> stageID = new List<StageProperSave>();

    public int curFloor;

} 



public class DataBase : MonoBehaviour
{
    public List<Stage> stages = new List<Stage>();
    public List<CardAbility> cards = new List<CardAbility>();
    public List<CharPack> charpacks = new List<CharPack>();

    public Stage playerStages;
    public List<Character> unlockable_chars = new List<Character>();

    public Stage IAmError;
    public CardAbility IAmCard;

    public void UpdatePlayerCard(List<StagePlayerSave> playerList){
        playerStages.characters = new Character[5];
        foreach(StagePlayerSave stagePlayer in playerList){
            if(stagePlayer == null){break;}
            playerStages.AddCharacter(unlockable_chars[stagePlayer.player_Characters_id]);
            unlockable_chars[stagePlayer.player_Characters_id].char_preCards.Clear();
            foreach(int id in stagePlayer.player_cards){
            unlockable_chars[stagePlayer.player_Characters_id].char_preCards.Add(cards.Find(x => x.card_id == id));

            }
        }
    }

    public Stage LoadFromINTStage(int id){
        Stage stage = stages.Find(x => x.id == id);
        if(stage == null){
            stage = IAmError;
        }
        return stage;
    }

    public CardAbility LoadFromINTCard(int id){
        CardAbility cardAbility = cards.Find(x => x.card_id == id);
        if(cardAbility == null){
            cardAbility = IAmCard;
        }
        return cardAbility;
    }

    public void Localize(){
        string localpath = "Localize";

        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){localpath += "/en";}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){localpath += "/ko";}
        ReadCardXML(localpath + "/Card");
        ReadStageXML(localpath + "/Floor");
        ReadCharPackXML(localpath + "/Name");
    }

    public void ReadCardXML(string filename){
        XmlDocument xmlDocument = new XmlDocument();
        TextAsset textAsset = (TextAsset) Resources.Load(filename);  
        xmlDocument.LoadXml(textAsset.text);

        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Card");
            
        foreach(CardAbility card in cards){
            foreach(XmlNode node in thisCardXML){
                if(node.Attributes[0].Value.Equals(card.card_id.ToString())){
                    XmlNodeList cardXML = node.ChildNodes;
                    card.name = cardXML[0].InnerText;
                    card.message = cardXML[1].InnerText;
                    card.ability_message = cardXML[2].InnerText;
                    break;
                }
            }
        }

        

    }

    public void ReadStageXML(string filename){
        XmlDocument xmlDocument = new XmlDocument();
        TextAsset textAsset = (TextAsset) Resources.Load(filename);  
        xmlDocument.LoadXml(textAsset.text);

        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Stage");
        foreach(Stage stage in stages){

        foreach(XmlNode node in thisCardXML){
            if(node.Attributes[0].Value.Equals(stage.id.ToString())){
                
                XmlNodeList cardXML = node.ChildNodes;
                stage.title = cardXML[0].InnerText;
                break;
            }
        }
        }
    }

    public void ReadCharPackXML(string filename){
        XmlDocument xmlDocument = new XmlDocument();
        TextAsset textAsset = (TextAsset) Resources.Load(filename);  
        xmlDocument.LoadXml(textAsset.text);
        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Name");
        foreach(CharPack charPack in charpacks){
        foreach(XmlNode node in thisCardXML){
            if(node.Attributes[0].Value.Equals(charPack.id.ToString())){
                XmlNodeList cardXML = node.ChildNodes;
                charPack.name_ = cardXML[0].InnerText;
                break;
            }
        }
        }
    }
}
