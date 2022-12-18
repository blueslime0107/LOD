using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]bool debug;
    StageManager sm;
    [SerializeField]int panel;

    [SerializeField]Lobby lobby;
    [SerializeField]GameObject cardPrefap;
    public List<CardAbility> cards = new List<CardAbility>();
    [SerializeField]List<CardPanelCard> objList = new List<CardPanelCard>();

    private void Awake() {
        sm = FindObjectOfType<StageManager>();
    }

    public void OnPointerDown(PointerEventData eventData){
        cards = sm.player_cardDic;
        if(Input.GetMouseButtonDown(1)){return;}
        if(panel.Equals(3)){
            lobby.OpenCardMenu();
        }
        else{
            lobby.OpenMainStageMenu();
        }
        sm.floor = lobby.floorNum;
        sm.panel = panel;
        RenderCard();
    }

    public void RenderCard(){
        if(cards.Count <= 0){return;}
        foreach(CardPanelCard item in objList){
            item.gameObject.SetActive(false);
        }
        for(int i=0;i<cards.Count;i++){
            if(objList.Count <= i){
                CardPanelCard obj = Instantiate(cardPrefap).GetComponent<CardPanelCard>();
                obj.transform.SetParent(lobby.card_board.transform,false);        
                objList.Add(obj);
            }
            objList[i].gameObject.SetActive(true);
            objList[i].cardPrefap.cardUpdate(cards[i]);

        }
    }
}
