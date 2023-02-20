using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

// 캐릭터가 들고있는 카드들을 표시하는 프리펩 스크립트
public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CardExplain cardExplain;
    
    public CardAbility card;
    public CardPrefap cardPrefap;

    Vector2 target_pos;
    float target_spd;

    public bool cardSelecting;
    public MenuCard menuCard;

    public void CardUpdate(){
        cardPrefap.loaded = false;
        cardPrefap.cardUpdate(card);

    }

    public void OnPointerDown(PointerEventData eventData){
        if(Input.GetMouseButtonDown(1)){return;}
        if(!cardSelecting){return;}
        bool triggered = false;
        menuCard.lobby.sdm.Play("Paper1");
        for(int i=0;i<menuCard.selectingChar.char_preCards.Length;i++){
            if(triggered){
                menuCard.selectingChar.char_preCards[i-1] = menuCard.selectingChar.char_preCards[i]; 
            }
            try
            {if(menuCard.selectingChar.char_preCards[i].Equals(card)){
                triggered = true;
                menuCard.selectingChar.char_preCards[i] = null;
            }}
            catch{
                triggered = true;
                menuCard.selectingChar.char_preCards[i] = null;
            }
            
            
            
        }
        menuCard.RenderSelectCard();
    }

    public void OnPointerEnter(PointerEventData eventData){
        cardExplain.updateCard(card);
        cardExplain.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData){
        cardExplain.gameObject.SetActive(false);
    }
}
