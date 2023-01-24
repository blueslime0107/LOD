using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//카드덱을 편집할때 나오는 현재 캐릭터가 가진 카드들을 보여줌
public class CardPanelCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public bool cardSelecting;
    public MenuCard menuCard;

    public CardAbility cardAbility;
    public CardPrefap cardPrefap;
    public CardExplain cardExplain;

    public void OnPointerEnter(PointerEventData eventData){
        cardExplain.gameObject.SetActive(true);
        cardExplain.updateCard(cardAbility);
        cardExplain.sdm.Play("Paper2");

    }

    public void OnPointerExit(PointerEventData eventData){
        cardExplain.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData){ 
        if(Input.GetMouseButtonDown(1)){return;}
        if(!cardSelecting){return;}
        // 해당카드를 더한 총 코스트가 현재 코스트 값보다 크면 넣지 못함 
        if(menuCard.selectingStage.GetPriceSum()+cardAbility.price > menuCard.selectingStage.avaliblePrice){cardExplain.sdm.Play("Pery");return;}
        
        for(int i=0;i<menuCard.selectingChar.char_preCards.Length;i++){
            if(menuCard.selectingChar.char_preCards[i] != null){continue;}
            cardExplain.sdm.Play("DiceGrab");
            menuCard.selectingChar.char_preCards[i] = cardAbility;
            menuCard.RenderSelectCard();
            break;
            
        }
    }

}
