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
    RectTransform rect;
    
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


    Vector2 target_pos;
    float target_spd;

    void Awake() {
        rect = GetComponent<RectTransform>();
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

     

     IEnumerator lerpMove(){
        while (true)
        {rect.pivot = Vector2.Lerp(rect.pivot,target_pos,target_spd*Time.deltaTime);
        if(Vector2.Distance(rect.pivot,target_pos) <= 0.1f){
            rect.pivot=target_pos;
            StopCoroutine("lerpMove");
        }
        yield return null;
        }
     }

     public void OnPointerEnter(PointerEventData eventData)
     {
        battleManager.sdm.Play("Paper2");
        battleManager.cardTouching = this;
        if(battleManager.card_select_trigger) {StartCoroutine("ImSelectedCard");}
        target_pos = new Vector2(0.5f,-0.5f);        
        target_spd = 100;
        StopCoroutine("lerpMove");
        StartCoroutine("lerpMove");  
        CardExplain obj = (isLeft) ? battleManager.ui.leftCardEx : battleManager.ui.rightCardEx; 
        obj.updateCard(card.ability);    
        obj.gameObject.SetActive(true);
        
     }
 
    public void OnPointerExit(PointerEventData eventData)
     {
        battleManager.cardTouching = null;
        StopCoroutine("ImSelectedCard");
        StopCoroutine("lerpMove");

        target_pos = new Vector2(0.5f,0.5f);  
        target_spd = 100;
        StartCoroutine("lerpMove");
        //battleManager.ui.CardUIUpdate((isLeft) ? "Left":"Right");
        CardExplain obj = (isLeft) ? battleManager.ui.leftCardEx : battleManager.ui.rightCardEx;   
        obj.gameObject.SetActive(false);
     }

     public void OnPointerDown(PointerEventData eventData){
        if(!battleManager.cardActiveAble){return;}
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

     public IEnumerator CardActivated(){
        rect.anchoredPosition += Vector2.up*45;
        for(int i = 0;i<6;i++){
            card_light.SetActive(!card_light.activeSelf);
            yield return new WaitForSeconds(0.07f);
        }
        card_light.SetActive(true);
        card.card_battleActive = false;
        rect.anchoredPosition -= Vector2.up*45;
     }

    public IEnumerator ImSelectedCard(){
        while(true)
        {if(Input.GetMouseButtonUp(0)){
            if(battleManager.card_select_trigger){
            battleManager.SelectiedCard(card);
            CardUpdate();
            StopCoroutine("ImSelectedCard");
            }
            battleManager.card_select_trigger = false;
            //battleManager.ui.CardUIUpdate((isLeft) ? "Left":"Right");
        }
        
        yield return null;}
    }

}
