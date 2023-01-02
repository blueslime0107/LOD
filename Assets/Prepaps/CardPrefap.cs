using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class CardPrefap : MonoBehaviour
{
    CardAbility ability;
    [SerializeField]Image image;
    [SerializeField]GameObject tain;
    [SerializeField]TextMeshProUGUI name_;
    [SerializeField]TextMeshProUGUI message;
    public bool loaded = false;
    

    public void cardUpdate(CardAbility card){
        if(loaded){return;}
        ability = card;
        image.sprite = ability.illust;

        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){ReadXML(card.xmlFile_path[0]);}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){ReadXML(card.xmlFile_path[1]);}
        name_.text = ability.name;
        message.text = ability.message;
        tain.SetActive(ability.tained);
        loaded = true;
    }

    private void ReadXML(string filename){
        XmlDocument xmlDocument = new XmlDocument();
        TextAsset textAsset = (TextAsset) Resources.Load(filename.Split(".")[0]);  
        xmlDocument.LoadXml(textAsset.text);


        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Card");
        foreach(XmlNode node in thisCardXML){
            if(node.Attributes[0].Value.Equals(ability.card_id.ToString())){
                XmlNodeList cardXML = node.ChildNodes;
                ability.name = cardXML[0].InnerText;
                ability.message = cardXML[1].InnerText;
                ability.ability_message = cardXML[2].InnerText;
                ability.story_message = cardXML[3].InnerText;
                break;
            }
        }

    }

}
