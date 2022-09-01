using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class card_text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int card_num;
    public bool isLeft;
    public BattleManager battleManager;
    RectTransform rect;
    Image illust;


    public CardPack card;
    new TextMeshProUGUI name;
    TextMeshProUGUI message;
    public GameObject ability_img;
    TextMeshProUGUI ability_message;

    void Awake() {
        GameObject obj1 = gameObject.transform.GetChild(0).gameObject;
        GameObject obj2 = gameObject.transform.GetChild(1).gameObject;
        GameObject obj4_1 = gameObject.transform.GetChild(2).gameObject;
        GameObject obj4_2 = obj4_1.transform.GetChild(0).gameObject;
        rect = GetComponent<RectTransform>();
        name = obj1.GetComponent<TextMeshProUGUI>();
        message = obj2.GetComponent<TextMeshProUGUI>();
        illust = GetComponent<Image>();
        ability_message = obj4_2.GetComponent<TextMeshProUGUI>();
    }

    public void CardUpdate(){
        illust.sprite = card.ability.illust;
        name.text = card.ability.name;
        message.text = card.ability.message;
        ability_message.text = card.ability.ability_message;

    }

     public void OnPointerEnter(PointerEventData eventData)
     {
        ability_img.SetActive(true);

        rect.anchoredPosition += Vector2.up*45;
        transform.SetAsLastSibling();
        
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        ability_img.SetActive(false);
        transform.SetSiblingIndex(card_num);
        rect.anchoredPosition += Vector2.down*45;
     }

     public void OnPointerClick(PointerEventData eventData){
        if(battleManager.card_select_trigger){
            battleManager.SelectiedCard(card);
            return;
        }
        if(isLeft){
            battleManager.players[battleManager.cardViewChar_left].cards[card_num].ability.CardActivate(battleManager.players[battleManager.cardViewChar_left].cards[card_num], battleManager);
        }
        else{
            battleManager.players[battleManager.cardViewChar_right].cards[card_num].ability.CardActivate(battleManager.players[battleManager.cardViewChar_right].cards[card_num], battleManager);
        }
        
     }

     public IEnumerator CardActivated(){
        bool colored = true;
        for(int i = 0;i<6;i++){
            if(colored){
                rect.anchoredPosition += Vector2.up*45;
            }
            else{
                rect.anchoredPosition += Vector2.down*45;
            }
            colored = !colored;
            yield return new WaitForSeconds(0.07f);
        }

        battleManager.battleCaculate.card_activated = false;
     }

}