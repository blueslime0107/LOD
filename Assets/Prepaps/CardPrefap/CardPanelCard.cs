using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPanelCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardAbility cardAbility;
    public CardPrefap cardPrefap;
    public CardExplain cardExplain;

    public void OnPointerEnter(PointerEventData eventData){
        cardExplain.gameObject.SetActive(true);
        cardExplain.updateCard(cardAbility);


    }

    public void OnPointerExit(PointerEventData eventData){
        cardExplain.gameObject.SetActive(false);
    }
}
