using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class CardPrefap : MonoBehaviour
{
    public CardAbility ability;
    [SerializeField]Image image;
    [SerializeField]GameObject tain;
    [SerializeField]TextMeshProUGUI name_;
    [SerializeField]TextMeshProUGUI message;
    [SerializeField]TextMeshProUGUI price;
    public bool loaded = false;

    public void cardUpdate(CardAbility card){
        if(loaded){return;}
        ability = card;
        image.sprite = ability.illust;
        name_.text = ability.name;
        message.text = ability.message;
        if(price){price.text = ability.price.ToString();}
        tain.SetActive(ability.tained);
        loaded = true;
    }

    // private void ReadXML(string filename){
    //     XmlDocument xmlDocument = new XmlDocument();
    //     TextAsset textAsset = (TextAsset) Resources.Load(filename);  
    //     Debug.Log(filename);
    //     Debug.Log(textAsset);
    //     xmlDocument.LoadXml(textAsset.text);

    //     XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Card");
    //     foreach(XmlNode node in thisCardXML){
    //         if(node.Attributes[0].Value.Equals(ability.card_id.ToString())){
    //             XmlNodeList cardXML = node.ChildNodes;
    //             ability.name = cardXML[0].InnerText;
    //             ability.message = cardXML[1].InnerText;
    //             ability.ability_message = cardXML[2].InnerText;
    //             break;
    //         }
    //     }

    // }

}
