using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


//카드덱을 편집할때 나오는 현재 캐릭터가 가진 카드들을 보여줌
public class CardPanelCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public bool cardSelecting;
    public MenuCard menuCard;

    public CardAbility cardAbility;
    public CardPrefap cardPrefap;
    public TextMeshProUGUI cardPriceText;
    public CardExplain cardExplain;

    public Image priceLoss;
    public Image equiped;
    public Image equiped_charIMG;
    public Character equiped_char;

    public GameObject givenCard;



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
        if(menuCard.NOTavalibleCard.Contains(cardAbility)){
        equiped_char.char_preCards.Remove(cardAbility);
        menuCard.NOTavalibleCard.Remove(cardAbility);
        menuCard.RenderSelectCard();
        return;
        }
        
        if((menuCard.selectingStage.GetPriceSum()+cardAbility.price > menuCard.avaliblePrice) && !menuCard.lobby.pc.debugBoolen ){cardExplain.sdm.Play("Pery");return;}
        
        
        if(menuCard.selectingChar.char_preCards.Count >= 7){cardExplain.sdm.Play("Pery");return;}
        foreach(CardAbility card in menuCard.selectingChar.char_preCards){
            if(card == null){break;}
            if(card == cardAbility){cardExplain.sdm.Play("Pery"); return;}
        }
        menuCard.lobby.sdm.Play("Paper1");
        menuCard.selectingChar.char_preCards.Add(cardAbility);
        menuCard.NOTavalibleCard.Add(cardAbility);
        menuCard.RenderSelectCard();
    }

}
