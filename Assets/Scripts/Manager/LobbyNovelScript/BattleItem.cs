using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.Localization.Settings;
using TMPro;

public class BattleItem : MonoBehaviour
{
    public Lobby lobby;
    public Stage stage;
    public DiceIcon diceIcon;
    public TextMeshProUGUI battle_title;
    public GameObject cleared;

    public void UpdateStat(){
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){ReadXML(stage.xmlFile_path[0]);}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){ReadXML(stage.xmlFile_path[1]);}

        diceIcon.SetRank(stage.rank);
        battle_title.text = stage.title;
        cleared.SetActive(stage.victoryed);
    }
    private void ReadXML(string filename){
        XmlDocument xmlDocument = new XmlDocument();
        TextAsset textAsset = (TextAsset) Resources.Load(filename.Split(".")[0]);  
        xmlDocument.LoadXml(textAsset.text);


        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Stage");
        foreach(XmlNode node in thisCardXML){
            if(node.Attributes[0].Value.Equals(stage.id.ToString())){
                XmlNodeList cardXML = node.ChildNodes;
                stage.title = cardXML[0].InnerText;
                stage.sub_text = cardXML[1].InnerText;
                stage.values = cardXML[2].InnerText;
                break;
            }
        }

    }

    public void Clicked(){
        lobby.stage = stage;
        lobby.stageManager.play_stage = stage;
        lobby.stageManager.player_battleCard = lobby.player;
        if(stage.playerStageLock != null){
            lobby.stageManager.player_battleCard = stage.playerStageLock;
            if(lobby.stageManager.player_battleCard.title.Equals("")){
                lobby.stageManager.player_battleCard.title = lobby.player.title;
                lobby.stageManager.player_battleCard.sub_text = lobby.player.sub_text;
                lobby.stageManager.player_battleCard.values = lobby.player.values;
            }
        }
        lobby.OpenBattleCard();
        
    }
}
