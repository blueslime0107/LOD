using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using UnityEngine;
using TMPro;

public class CardExplain : MonoBehaviour
{
    public SoundManager sdm;
    public TextMeshProUGUI cardAbText;
    public Image cardImg; 

    public void updateCard(CardAbility card){
        cardAbText.text = card.ability_message;
        cardImg.sprite = card.illust;
    }

    private void ReadXML(int id, string filename){
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(Application.dataPath + "\\Resources\\" + filename);

        Debug.Log(xmlDocument);

        XmlNodeList thisCardXML = xmlDocument.GetElementsByTagName("Card");
        foreach(XmlNode node in thisCardXML){
            Debug.Log(node.Attributes[0].Value);
            if(node.Attributes[0].Value.Equals(id.ToString())){
                XmlNodeList cardXML = node.ChildNodes;
                cardAbText.text = cardXML[2].InnerText;
                break;
            }
        }

    }
}
