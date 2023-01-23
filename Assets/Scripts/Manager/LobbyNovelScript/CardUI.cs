using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

// 캐릭터가 들고있는 카드들을 표시하는 프리펩 스크립트
public class CardUI : MonoBehaviour, IPointerDownHandler
{
    
    RectTransform rect;
    Image illust;
    
    Material material;
    //MaterialPropertyBlock material_block;
    //Image card_image;
    public CardAbility card;
    new TextMeshProUGUI name;
    TextMeshProUGUI message;

    [SerializeField]GameObject card_light;
    [SerializeField]GameObject block_img;
    [SerializeField]GameObject tain_img;
    [SerializeField]GameObject obj1;
    [SerializeField]GameObject obj2;

    Vector2 target_pos;
    float target_spd;

    public bool cardSelecting;
    public MenuCard menuCard;

    void Awake() {

        rect = GetComponent<RectTransform>();
        name = obj1.GetComponent<TextMeshProUGUI>();
        message = obj2.GetComponent<TextMeshProUGUI>();
        illust = GetComponent<Image>();


        material = Instantiate(card_light.GetComponent<Image>().material);
        card_light.GetComponent<Image>().material = material;
        //card_light.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(material);
        //material_block = new MaterialPropertyBlock();
        CardUpdate();
    }

    public void CardUpdate(){
        illust.sprite = card.illust;
        name.text = card.name;
        message.text = card.message;
        if(card.name.Equals("NULL")){
            block_img.SetActive(true);
        }
        else{
            block_img.SetActive(false);
        }

    }

    public void OnPointerDown(PointerEventData eventData){
        if(Input.GetMouseButtonDown(1)){return;}
        if(!cardSelecting){return;}
        bool triggered = false;
        menuCard.lobby.sdm.Play("Paper1");
        for(int i=0;i<menuCard.selectingChar.char_preCards.Length;i++){
            if(triggered){
                menuCard.selectingChar.char_preCards[i-1] = menuCard.selectingChar.char_preCards[i]; 
            }
            try
            {if(menuCard.selectingChar.char_preCards[i].Equals(card)){
                triggered = true;
                menuCard.selectingChar.char_preCards[i] = null;
            }}
            catch{
                triggered = true;
                menuCard.selectingChar.char_preCards[i] = null;
            }
            
            
            
        }
        menuCard.RenderSelectCard();
    }
}
