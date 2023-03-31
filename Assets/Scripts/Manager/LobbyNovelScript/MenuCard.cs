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

    public int avaliblePrice;
    public List<CardAbility> NOTavalibleCard; 

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
        if(cardSelecting){return;}
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
            objList[i].cardPriceText.text = objList[i].cardAbility.price.ToString();

        }
    }

    //
    public void RenderSelectCard(){
        if(cardSelecting){
            priceGague.maxValue = avaliblePrice;
            int getPrice = selectingStage.GetPriceSum();
            priceGague.value = getPrice;
            price_text.text = getPrice.ToString() + "/" + priceGague.maxValue.ToString();
            selfprice_text.text = selectingStage.GetSelfPriceSum(selectingChar).ToString();
            foreach(CardPanelCard item in objList){
            item.cardSelecting = true;
            item.priceLoss.gameObject.SetActive(selectingStage.GetPriceSum()+item.cardAbility.price > avaliblePrice);
            item.equiped.gameObject.SetActive(NOTavalibleCard.Contains(item.cardAbility));
            }


            foreach(Character character in selectingStage.characters){
                if(character == null){break;}
                foreach(CardAbility card in character.char_preCards){
                    if(card == null){break;}
                    CardPanelCard newCard =  objList.Find(x => x.cardAbility == card);
                    newCard.equiped_char = character;
                    newCard.equiped_charIMG.sprite = character.char_sprites.poses[0];

                }
            }



            playerCardPanel.SetActive(true);
            florrPanel.SetActive(false);
            foreach(CardUI cardUI in player_cards){
                cardUI.gameObject.SetActive(false);
            }
            for(int i=0;i<selectingChar.char_preCards.Count;i++){
                player_cards[i].card = selectingChar.char_preCards[i];
                    player_cards[i].CardUpdate();
                    player_cards[i].gameObject.SetActive(true);
            }
            
        }
        else{
            foreach(CardPanelCard item in objList){
            item.cardSelecting = false;
            item.equiped.gameObject.SetActive(false);
            item.priceLoss.gameObject.SetActive(false);
            }
            florrPanel.SetActive(true);
            playerCardPanel.SetActive(false);
        }
    }

    public void ReloadCards(){
        for(int i=0;i<cards.Count;i++){
            objList[i].cardPrefap.loaded = false;
            objList[i].cardPrefap.cardUpdate(objList[i].cardAbility);
            objList[i].cardPriceText.text = objList[i].cardAbility.price.ToString();
        }
    }
}
