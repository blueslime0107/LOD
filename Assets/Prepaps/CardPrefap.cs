using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CardPrefap : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]Image image;
    [SerializeField]GameObject tain;
    [SerializeField]TextMeshProUGUI name_;
    [SerializeField]TextMeshProUGUI message;

    public void cardUpdate(CardAbility card){
        image.sprite = card.illust;
        name_.text = card.name;
        message.text = card.message;
        tain.SetActive(card.tained);
    }

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("Card_Touched");
    }

}
