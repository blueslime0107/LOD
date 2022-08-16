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
    public TextMeshProUGUI ability_message;

    void Awake() {
        rect = GetComponent<RectTransform>();
    }


    private bool mouse_over = false;
     void Update()
     {
        //  if (mouse_over)
        //  {
        //     rect.anchoredPosition = Vector2.up*15;
        //  }
            
     }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
        rect.anchoredPosition += Vector2.up*15;
        transform.SetAsLastSibling();
        mouse_over = true;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        rect.anchoredPosition += Vector2.down*15;
         mouse_over = false;
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
