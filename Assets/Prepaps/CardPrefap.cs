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

}
