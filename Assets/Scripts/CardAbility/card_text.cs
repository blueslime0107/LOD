using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class card_text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{


    public int card_num;
    public bool isLeft;
    public BattleManager battleManager;
    RectTransform rect;
    Image illust;
    
    Material material;
    //MaterialPropertyBlock material_block;
    //Image card_image;
    public CardPack card;
    new TextMeshProUGUI name;
    TextMeshProUGUI message;
    public GameObject ability_img;
    public GameObject ability_img2;

    TextMeshProUGUI ability_message;
    TextMeshProUGUI ability_message2;
    Image card_overImg;
    [SerializeField]GameObject card_light;
    [SerializeField]GameObject block_img;
    [SerializeField]GameObject tain_img;
    //[SerializeField]GameObject card_overImg_obj;
    [SerializeField]GameObject obj1;
    [SerializeField]GameObject obj2;

    Vector2 target_pos;
    float target_spd;

    void Awake() {

        GameObject obj4_2 = ability_img.transform.GetChild(0).gameObject;
        GameObject obj5_2 = ability_img2.transform.GetChild(0).gameObject;
        rect = GetComponent<RectTransform>();
        name = obj1.GetComponent<TextMeshProUGUI>();
        message = obj2.GetComponent<TextMeshProUGUI>();
        illust = GetComponent<Image>();
        ability_message = obj4_2.GetComponent<TextMeshProUGUI>();
        ability_message2 = obj5_2.GetComponent<TextMeshProUGUI>();
        //card_overImg = card_overImg_obj.GetComponent<Image>();




        material = Instantiate(card_light.GetComponent<Image>().material);
        card_light.GetComponent<Image>().material = material;
        //card_light.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(material);
        //material_block = new MaterialPropertyBlock();
    }

    public void CardUpdate(){
        illust.sprite = card.illust;
        name.text = card.name;
        message.text = card.message;
        ability_message.text = card.ability_message;
        ability_message2.text = card.ability_message;
        if(card.ability.name.Equals("NULL")){
            block_img.SetActive(true);
        }
        else{
            block_img.SetActive(false);
        }
        if(isLeft){
            try
            {if(battleManager.cardViewChar_left.cards[card_num].card_activating){material.SetInt("_Active",1);}
            else{material.SetInt("_Active",0);}}
            catch
            {if(battleManager.render_cardViewChar_left.cards[card_num].card_activating){material.SetInt("_Active",1);}
            else{material.SetInt("_Active",0);}}
            if(card.ability.tained){
                tain_img.SetActive(true);
            }
            else{
                tain_img.SetActive(false);
            }
            // if(card.overCard != null){
            //     card_overImg.gameObject.SetActive(true);
            //     card_overImg.sprite = card.overCard;
            // }
            // else{
            //     card_overImg.gameObject.SetActive(false);
            // }
            
        }
        else{  
            try
            {
                if(battleManager.cardViewChar_right.cards[card_num].card_activating){material.SetInt("_Active",1);}
                else{material.SetInt("_Active",0);}
            }
            catch
            {
                if(battleManager.render_cardViewChar_right.cards[card_num].card_activating){material.SetInt("_Active",1);}
                else{material.SetInt("_Active",0);}
            }
            if(card.ability.tained){
                tain_img.SetActive(true);
            }
            else{
                tain_img.SetActive(false);
            }
            // if(card.overCard != null){
            //     card_overImg.gameObject.SetActive(true);
            //     card_overImg.sprite = card.overCard;
            // }
            // else{
            //     card_overImg.gameObject.SetActive(false);
            // }
        
        
        
        }

    }

     

     IEnumerator lerpMove(){
        while (true)
        {rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition,target_pos,target_spd*Time.deltaTime);
        if(Vector2.Distance(rect.anchoredPosition,target_pos) <= 0.5f){
            rect.anchoredPosition=target_pos;
            StopCoroutine("lerpMove");
        }
        yield return null;
        }
     }

     public void OnPointerEnter(PointerEventData eventData)
     {
        battleManager.cardTouching = true;
        if(battleManager.card_select_trigger) {StartCoroutine("ImSelectedCard");}
        if(isLeft) {ability_img.SetActive(true);}
        else {ability_img2.SetActive(true);}
        target_pos = new Vector2(rect.anchoredPosition.x,45);
        
        target_spd = 100;
        StartCoroutine("lerpMove");

        
        transform.SetAsLastSibling();
        CardUpdate();
        
     }
 
    public void OnPointerExit(PointerEventData eventData)
     {
        battleManager.cardTouching = false;
        StopCoroutine("ImSelectedCard");
        if(isLeft) {ability_img.SetActive(false);}
        else {ability_img2.SetActive(false);}
        transform.SetSiblingIndex(card_num);

        target_pos = new Vector2(rect.anchoredPosition.x,0);
        target_spd = 100;
        StartCoroutine("lerpMove");
        CardUpdate();
     }

     public void OnPointerDown(PointerEventData eventData){
        if(isLeft){
            battleManager.cardViewChar_left.cards[card_num].ability.CardActivate(battleManager.cardViewChar_left.cards[card_num], battleManager);
        }
        else{
            battleManager.cardViewChar_right.cards[card_num].ability.CardActivate(battleManager.cardViewChar_right.cards[card_num], battleManager);
        }
        CardUpdate();
     }

     public void OnPointerUp(PointerEventData eventData){
        if(!battleManager.cardTouching)
            battleManager.card_select_trigger = false;
        Debug.Log("de select");
        CardUpdate();
     }

     public IEnumerator CardActivated(){
        rect.anchoredPosition += Vector2.up*45;
        for(int i = 0;i<6;i++){
            card_light.SetActive(!card_light.activeSelf);
            yield return new WaitForSeconds(0.07f);
        }
        card_light.SetActive(true);
        card.card_active = false;
        rect.anchoredPosition -= Vector2.up*45;
        //battleManager.battleCaculate.card_activated = false;
     }

    public IEnumerator ImSelectedCard(){
        while(true)
        {if(Input.GetMouseButtonUp(0)){
            Debug.Log("GetMouseButtonUp");
            if(battleManager.card_select_trigger){
            battleManager.SelectiedCard(card);
            CardUpdate();
            StopCoroutine("ImSelectedCard");
            }
            battleManager.card_select_trigger = false;
            battleManager.cardlineRender.gameObject.SetActive(false);
        }
        
        yield return null;}
    }

}
