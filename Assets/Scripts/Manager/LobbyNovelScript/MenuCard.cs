using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

// 카드 도감 스크립트
public class MenuCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]bool debug;
    StageManager sm;
    [SerializeField]bool panel;

    [SerializeField]public Lobby lobby;
    [SerializeField]GameObject cardPrefap;
    public List<CardAbility> cards = new List<CardAbility>();
    [SerializeField]List<CardPanelCard> objList = new List<CardPanelCard>();
    [SerializeField]CardExplain cardExplain;


    public bool cardSelecting;
    public Character selectingChar;
    public Stage selectingStage;

    [SerializeField]public GameObject florrPanel;
    [SerializeField]public GameObject playerCardPanel;
    [SerializeField]CardUI[] player_cards = new CardUI[7];

    public Slider priceGague;
    public TextMeshProUGUI selfprice_text;
    public TextMeshProUGUI price_text;

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
        lobby.OpenCardMenu();
        RenderCard();
    }

    // 들어올때 카드를 로딩함
    public void RenderCard(){
        RenderSelectCard();
        if(curLanguage.Equals(LocalizationSettings.SelectedLocale.name)){return;}
        curLanguage = LocalizationSettings.SelectedLocale.name;
        cards = sm.player_cardDic;
        sm.floor = lobby.curFloor;
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

    //
    public void RenderSelectCard(){
        
        if(cardSelecting){
            priceGague.maxValue = selectingStage.avaliblePrice;
            int getPrice = selectingStage.GetPriceSum();
            priceGague.value = getPrice;
            price_text.text = getPrice.ToString() + "/" + priceGague.maxValue.ToString();
            selfprice_text.text = selectingStage.GetSelfPriceSum(selectingChar).ToString();
            foreach(CardPanelCard item in objList){
            item.cardSelecting = true;
            }
            playerCardPanel.SetActive(true);
            florrPanel.SetActive(false);
            for(int i=0;i<selectingChar.char_preCards.Length;i++){
                if(selectingChar.char_preCards[i] != null){
                    player_cards[i].card = selectingChar.char_preCards[i];
                    player_cards[i].CardUpdate();
                    player_cards[i].gameObject.SetActive(true);
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
            florrPanel.SetActive(true);
            playerCardPanel.SetActive(false);
        }
    }

    public void ReloadCards(){
        for(int i=0;i<cards.Count;i++){
            objList[i].cardPrefap.loaded = false;
            objList[i].cardPrefap.cardUpdate(objList[i].cardAbility);
        }
    }
}
