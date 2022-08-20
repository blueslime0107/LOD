using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class card_text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int card_num;
    public bool isLeft;
    public BattleManager battleManager;
    RectTransform rect;

    public new TextMeshProUGUI name;
    public TextMeshProUGUI message;
    public GameObject ability_img;
    public TextMeshProUGUI ability_message;

    void Awake() {
        rect = GetComponent<RectTransform>();
    }

    //  void Update()
    //  {
    //     //  if (mouse_over)
    //     //  {
    //     //     rect.anchoredPosition = Vector2.up*15;
    //     //  }
            
    //  }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
        ability_img.SetActive(true);
        if(isLeft){
            ability_message.text = battleManager.players[battleManager.cardViewChar_left].cards[card_num].ability_message;
        }
        else{
            ability_message.text = battleManager.players[battleManager.cardViewChar_right].cards[card_num].ability_message;
        }
        
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

    

}
