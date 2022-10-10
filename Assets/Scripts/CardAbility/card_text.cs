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
    [SerializeField]GameObject card_light;
    [SerializeField]GameObject block_img;

    Vector2 target_pos;
    float target_spd;

    void Awake() {
        
        GameObject obj1 = gameObject.transform.GetChild(0).gameObject;
        GameObject obj2 = gameObject.transform.GetChild(1).gameObject;
        GameObject obj4_1 = gameObject.transform.GetChild(2).gameObject;
        GameObject obj4_2 = obj4_1.transform.GetChild(0).gameObject;
        GameObject obj5_1 = gameObject.transform.GetChild(3).gameObject;
        GameObject obj5_2 = obj5_1.transform.GetChild(0).gameObject;
        rect = GetComponent<RectTransform>();
        name = obj1.GetComponent<TextMeshProUGUI>();
        message = obj2.GetComponent<TextMeshProUGUI>();
        illust = GetComponent<Image>();
        ability_message = obj4_2.GetComponent<TextMeshProUGUI>();
        ability_message2 = obj5_2.GetComponent<TextMeshProUGUI>();




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
        
        
        
        }

    }

     public void OnPointerEnter(PointerEventData eventData)
     {
        if(isLeft) {ability_img.SetActive(true);}
        else {ability_img2.SetActive(true);}
        target_pos = new Vector2(rect.anchoredPosition.x,45);
        
        target_spd = 100;
        StartCoroutine("lerpMove");

        
        transform.SetAsLastSibling();
        
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
 
    public void OnPointerExit(PointerEventData eventData)
     {
        if(isLeft) {ability_img.SetActive(false);}
        else {ability_img2.SetActive(false);}
        transform.SetSiblingIndex(card_num);

        target_pos = new Vector2(rect.anchoredPosition.x,0);
        target_spd = 100;
        StartCoroutine("lerpMove");
     }

     public void OnPointerClick(PointerEventData eventData){
        if(battleManager.card_select_trigger){
            battleManager.SelectiedCard(card);
            CardUpdate();
            return;
        }
        if(isLeft){
            battleManager.cardViewChar_left.cards[card_num].ability.CardActivate(battleManager.cardViewChar_left.cards[card_num], battleManager);
            // if(battleManager.players[battleManager.cardViewChar_left].cards[card_num].card_activating){
                
            //     material.SetInt("_Active",1);
            // }
            // else{
            //     material.SetInt("_Active",0);
            // }
        }
        else{
            battleManager.cardViewChar_right.cards[card_num].ability.CardActivate(battleManager.cardViewChar_right.cards[card_num], battleManager);
            // if(battleManager.players[battleManager.cardViewChar_right].cards[card_num].card_activating){
            //     material.SetInt("_Active",1);

            // }
            // else{
            //     material.SetInt("_Active",0);
            // }
        
        
        
        }
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

}
