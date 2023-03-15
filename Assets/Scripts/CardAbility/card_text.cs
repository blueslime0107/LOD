using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Card_text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public CardPrefap cardPrefap;

    public int card_num;
    public bool isLeft;
    public BattleManager battleManager;
    public RectTransform rect;
    public bool highLighted;
    
    Material material;
    //MaterialPropertyBlock material_block;
    //Image card_image;
    public CardPack card;
    Sprite card_originOverImg;

    [SerializeField]GameObject card_light;
    [SerializeField]GameObject block_obj;
    Image block_img;
    [SerializeField]GameObject countObj;
    [SerializeField]TextMeshProUGUI countText;



    void Awake() {
        material = Instantiate(card_light.GetComponent<Image>().material);
        card_light.GetComponent<Image>().material = material;
        block_img = block_obj.GetComponent<Image>();
    }

    public void CardUpdate(){
        countObj.SetActive(card.ability.usingCount);
        countText.text = card.count.ToString();
        //block_img.((card.ability.name.Equals("NULL")) ? true: false);
        if(card.overCard != card_originOverImg){
            Debug.Log("card.overCard");
            if(card.overCard == null){
                block_obj.SetActive(false); 
                block_img.sprite = null;
                card_originOverImg = null;
            }
            else{
                block_obj.SetActive(true); 
                block_img.sprite = card.overCard;
                card_originOverImg = block_img.sprite;

            }
            }
        if(isLeft){
            try
            {if(battleManager.cardViewChar_left.cards[card_num].active){material.SetInt("_Active",1);}
            else{material.SetInt("_Active",0);}}
            catch
            {if(battleManager.render_cardViewChar_left.cards[card_num].active){material.SetInt("_Active",1);}
            else{material.SetInt("_Active",0);}}
        }
        else{  
            try
            {
                if(battleManager.cardViewChar_right.cards[card_num].active){material.SetInt("_Active",1);}
                else{material.SetInt("_Active",0);}
            }
            catch
            {
                if(battleManager.render_cardViewChar_right.cards[card_num].active){material.SetInt("_Active",1);}
                else{material.SetInt("_Active",0);}
            }
        }

    }

     public void OnPointerEnter(PointerEventData eventData)
     {
        battleManager.sdm.Play("Paper2");
        battleManager.cardTouching = this;

        highLighted = true;

        CardExplain obj = (isLeft) ? battleManager.ui.leftCardEx : battleManager.ui.rightCardEx; 
        obj.updateCard(card.ability);    
        obj.gameObject.SetActive(true);
        
     }
 
    public void OnPointerExit(PointerEventData eventData)
     {
        battleManager.cardTouching = null;

        highLighted = false;

        CardExplain obj = (isLeft) ? battleManager.ui.leftCardEx : battleManager.ui.rightCardEx;   
        obj.gameObject.SetActive(false);
     }

     public void OnPointerDown(PointerEventData eventData){
        if(!battleManager.cardActiveAble){return;}
        battleManager.mouseTouchingCard = this;
        if(isLeft)
            battleManager.cardViewChar_left.cards[card_num].ability.CardActivate(battleManager.cardViewChar_left.cards[card_num], battleManager);
        else
            battleManager.cardViewChar_right.cards[card_num].ability.CardActivate(battleManager.cardViewChar_right.cards[card_num], battleManager);
        //battleManager.ui.CardUIUpdate((isLeft) ? "Left":"Right");
     }

     public void OnPointerUp(PointerEventData eventData){
        if(!battleManager.cardTouching || battleManager.cardTouching.Equals(this)){
            battleManager.card_select_trigger = false;
            battleManager.cardlineRender.gameObject.SetActive(false);
        }
       // battleManager.ui.CardUIUpdate((isLeft) ? "Left":"Right");
     }


}
