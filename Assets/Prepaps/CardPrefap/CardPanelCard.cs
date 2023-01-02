using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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


    }

    public void OnPointerExit(PointerEventData eventData){
        cardExplain.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData){
        if(Input.GetMouseButtonDown(1)){return;}
        if(!cardSelecting){return;}
        for(int i=0;i<menuCard.selectingChar.char_preCards.Length;i++){
            Debug.Log(i);
            try{
                Debug.Log(menuCard.selectingChar.char_preCards[i].Equals(null));
            }
            catch{
                menuCard.selectingChar.char_preCards[i] = cardAbility;
                menuCard.RenderSelectCard();
                break;
            }
            // Debug.Log(menuCard.selectingChar.char_preCards[i].Equals(null));
            // if(menuCard.selectingChar.char_preCards[i].Equals(null)){
            //     menuCard.selectingChar.char_preCards[i] = cardAbility;
            //     menuCard.RenderSelectCard();
            //     break;
            // }
        }
    }

}
