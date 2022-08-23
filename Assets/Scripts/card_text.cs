using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class card_text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int card_num;
    public bool isLeft;
    public BattleManager battleManager;
    RectTransform rect;
    Image illust;


    public CardAbility card;
    new TextMeshProUGUI name;
    TextMeshProUGUI message;
    public GameObject ability_img;
    TextMeshProUGUI ability_message;

    public Color color_on;
    public Color color_off;

    void Awake() {
        Debug.Log("Awake");
        GameObject obj1 = gameObject.transform.GetChild(0).gameObject;
        GameObject obj2 = gameObject.transform.GetChild(1).gameObject;
        GameObject obj4_1 = gameObject.transform.GetChild(3).gameObject;
        GameObject obj4_2 = obj4_1.transform.GetChild(0).gameObject;
        rect = GetComponent<RectTransform>();
        name = obj1.GetComponent<TextMeshProUGUI>();
        message = obj2.GetComponent<TextMeshProUGUI>();
        illust = GetComponent<Image>();
        ability_message = obj4_2.GetComponent<TextMeshProUGUI>();
    }

    public void CardUpdate(){
        Debug.Log(name.text);
        illust.sprite = card.illust;
        name.text = card.name;
        message.text = card.message;
        ability_message.text = card.ability_message;

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

     public void OnClick(){
        if(isLeft){
            battleManager.players[battleManager.cardViewChar_left].cards[card_num].CardActivate(battleManager);
        }
        else{
            battleManager.players[battleManager.cardViewChar_right].cards[card_num].CardActivate(battleManager);
        }
        
     }

     public IEnumerator CardActivated(){
        bool colored = false;
        for(int i = 0;i<5;i++){
            if(colored){
                illust.color = color_off;
            }
            else{
                illust.color = color_on;
            }
            colored = !colored;
            yield return new WaitForSeconds(0.1f);
        }
        illust.color = color_on;
        battleManager.battleCaculate.card_activated = false;
     }

}
