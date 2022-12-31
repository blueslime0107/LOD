using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

public class MenuCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]bool debug;
    StageManager sm;
    [SerializeField]int panel;

    [SerializeField]Lobby lobby;
    [SerializeField]GameObject cardPrefap;
    public List<CardAbility> cards = new List<CardAbility>();
    [SerializeField]List<CardPanelCard> objList = new List<CardPanelCard>();
    [SerializeField]CardExplain cardExplain;


    public bool cardSelecting;
    public Character selectingChar;
    [SerializeField]GameObject playerCardPanel;
    [SerializeField]CardUI[] player_cards = new CardUI[7];

    

    string curLanguage;

    private void Awake() {
        sm = FindObjectOfType<StageManager>();
        curLanguage = "";
    }

    void Start(){
        RenderCard();
    }

    public void OnPointerDown(PointerEventData eventData){        
        if(Input.GetMouseButtonDown(1)){return;}
        if(panel.Equals(3)){
            lobby.OpenCardMenu();
        }
        else{
            lobby.OpenMainStageMenu();
        }
        RenderCard();
    }

    public void RenderCard(){
        RenderSelectCard();
        if(curLanguage.Equals(LocalizationSettings.SelectedLocale.name)){return;}
        curLanguage = LocalizationSettings.SelectedLocale.name;
        cards = sm.player_cardDic;
        sm.floor = lobby.floorNum;
        sm.panel = panel;
        if(cards.Count <= 0){return;}
        foreach(CardPanelCard item in objList){
            item.gameObject.SetActive(false);
        }
        for(int i=0;i<cards.Count;i++){
            if(objList.Count <= i){
                CardPanelCard obj = Instantiate(cardPrefap).GetComponent<CardPanelCard>();
                obj.transform.SetParent(lobby.card_board.transform,false);    
                obj.cardExplain = cardExplain;    
                obj.menuCard = this;
                objList.Add(obj);
            }
            objList[i].gameObject.SetActive(true);
            objList[i].cardAbility = cards[i];
            objList[i].cardPrefap.cardUpdate(objList[i].cardAbility);

        }
    }

    public void RenderSelectCard(){
        Debug.Log("RenderSelectCard");
        if(cardSelecting){
            foreach(CardPanelCard item in objList){
            item.cardSelecting = true;
            }
            playerCardPanel.SetActive(true);
            for(int i=0;i<selectingChar.char_preCards.Length;i++){
                if(selectingChar.char_preCards[i] != null){
                    player_cards[i].gameObject.SetActive(true);
                    player_cards[i].card = selectingChar.char_preCards[i];
                    player_cards[i].CardUpdate();
                }
                else{
                    player_cards[i].gameObject.SetActive(false);
                }
            }
        }
        else{
            foreach(CardPanelCard item in objList){
            item.cardSelecting = false;
            }
            playerCardPanel.SetActive(false);
        }
    }
}
