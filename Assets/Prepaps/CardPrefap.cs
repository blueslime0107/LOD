using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardPrefap : MonoBehaviour
{
    [SerializeField]Image image;
    [SerializeField]GameObject tain;
    [SerializeField]TextMeshProUGUI name;
    [SerializeField]TextMeshProUGUI message;

    public void cardUpdate(CardAbility card){
        image.sprite = card.illust;
        name.text = card.name;
        message.text = card.message;
        tain.SetActive(card.tained);
    }

}
