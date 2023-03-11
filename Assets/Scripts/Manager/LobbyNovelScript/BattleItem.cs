using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.Localization.Settings;
using TMPro;

// 전투들을 불러올때 항목들의 스크립트
public class BattleItem : MonoBehaviour
{
    public Lobby lobby;
    public Stage stage;
    public DiceIcon diceIcon;
    public TextMeshProUGUI battle_title;
    public GameObject cleared;
    public GameObject alert;

    // 랭크와 제목 로컬라이즈
    public void UpdateStat(){
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){ReadXML(stage.xmlFile_path[0]);}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){ReadXML(stage.xmlFile_path[1]);}

        diceIcon.SetRank(stage.rank);
        battle_title.text = stage.title;
        cleared.SetActive(stage.victoryed);
        alert.SetActive(!stage.discovered);

    }
    private void ReadXML(TextAsset textAsset){
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textAsset.text);


        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Stage");
        foreach(XmlNode node in thisCardXML){
            if(node.Attributes[0].Value.Equals(stage.id.ToString())){
                XmlNodeList cardXML = node.ChildNodes;
                stage.title = cardXML[0].InnerText;
                stage.sub_text = cardXML[1].InnerText;
                break;
            }
        }

    }

    // 눌렀을때 양팀의 전투카드 불러오기
    public void Clicked(){
        lobby.selectedbattleItem = this;
        lobby.stage = stage; 
        lobby.stageManager.play_stage = stage; // 적팀 스테이지
        lobby.stageManager.player_battleCard = lobby.player; // 플레이어 스테이지
        if(stage.playerStageLock != null){ // 정해진 플레이어 스테이지가 있을때 그 스테이지로 바꾸기
            lobby.stageManager.player_battleCard = stage.playerStageLock;
            if(lobby.stageManager.player_battleCard.title.Equals("")){ // 제목이 없으면 원래의 플레이어 스테이지의 텍스트 가져오기
                lobby.stageManager.player_battleCard.title = lobby.player.title;
                lobby.stageManager.player_battleCard.sub_text = lobby.player.sub_text;
            }
        }
        lobby.OpenBattleCard();
        
    }
}
