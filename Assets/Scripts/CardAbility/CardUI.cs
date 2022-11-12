using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
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
}
