using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [HideInInspector]public Button battleStartButton;
    public GameObject cardMessage;
    public TextMeshProUGUI cardAbility;
    public TextMeshProUGUI cardStroy;

    [HideInInspector]public bool showleftCard;
    [HideInInspector]public int showleftCardamount;

    public List<GameObject> leftCardIndi = new List<GameObject>();
    public List<card_text> leftCardIndi_compo = new List<card_text>();
    List<RectTransform> leftCard_pos = new List<RectTransform>();
    public List<CardAbility> leftCard_card = new List<CardAbility>();
    // List<Image> leftCard_img = new List<Image>();
    // List<TextMeshProUGUI> leftCard_title = new List<TextMeshProUGUI>();
    // List<TextMeshProUGUI> leftCard_message = new List<TextMeshProUGUI>();

    public Slider left_gague;
    public Slider right_gague;

    [HideInInspector]
    public bool showrightCard;
    [HideInInspector]public int showrightCardamount;

    public List<GameObject> rightCardIndi = new List<GameObject>();
    List<RectTransform> rightCard_pos = new List<RectTransform>();
    public List<card_text> rightCardIndi_compo = new List<card_text>();
    public List<CardAbility> rightCard_card = new List<CardAbility>();
    // List<TextMeshProUGUI> rightCard_title = new List<TextMeshProUGUI>();
    // List<TextMeshProUGUI> rightCard_message = new List<TextMeshProUGUI>();
    public float cartSort_scale;

    void Awake(){
        
        for(int i = 0; i < leftCardIndi.Count; i++){
            leftCard_pos.Add(leftCardIndi[i].GetComponent<RectTransform>());        
            }
        for(int i = 0; i < leftCardIndi.Count; i++){
            leftCardIndi_compo.Add(leftCardIndi[i].GetComponent<card_text>());        
            }
        for(int i = 0; i < rightCardIndi.Count; i++){
            rightCard_pos.Add(rightCardIndi[i].GetComponent<RectTransform>());        
            }
        for(int i = 0; i < rightCardIndi.Count; i++){
            rightCardIndi_compo.Add(rightCardIndi[i].GetComponent<card_text>());        
            }
        // for(int i = 0; i < leftCardIndi.Count; i++){
        //     card_text txt = leftCardIndi[i].GetComponent<card_text>();    
        //     leftCard_title.Add(txt.name);                    
        //     }
        // for(int i = 0; i < leftCardIndi.Count; i++){
        //     card_text txt = leftCardIndi[i].GetComponent<card_text>();    
        //     leftCard_message.Add(txt.message);                    
        //     }
        // for(int i = 0; i < rightCardIndi.Count; i++){
        //     rightCard_pos.Add(rightCardIndi[i].GetComponent<RectTransform>());        
        //     }
        // for(int i = 0; i < rightCardIndi.Count; i++){
        //     rightCard_img.Add(rightCardIndi[i].GetComponent<Image>());        
        //     }
        // for(int i = 0; i < rightCardIndi.Count; i++){
        //     card_text txt = rightCardIndi[i].GetComponent<card_text>();    
        //     rightCard_title.Add(txt.name);                    
        //     }
        // for(int i = 0; i < rightCardIndi.Count; i++){
        //     card_text txt = rightCardIndi[i].GetComponent<card_text>();    
        //     rightCard_message.Add(txt.message);                    
        //     }
        
    }

    void Update(){
        if(showleftCard){
            // for(int i = 0; i < leftCardIndi.Count; i++){ // 카드수 만큼 카드 활성화
            //     if(i<=showleftCardamount-1){
            //         leftCardIndi[i].SetActive(true); 
            //     }                    
            //     else{
            //         leftCardIndi[i].SetActive(false); 
            //     }
            // }

            float[] cardLerps = new float[showleftCardamount];

            switch(showleftCardamount){ // 카드수에 맞게 위치 조정
                case 1: cardLerps = new float[] {0f}; break;
                case 2: cardLerps = new float[] {-25f,25f}; break;
                case 3: cardLerps = new float[] {-50f,0f,50f}; break;
                default:
                    float interval = cartSort_scale / (showleftCardamount-1);
                    for(int i = 0; i < showleftCardamount;i++){
                        cardLerps[i] = interval * i-cartSort_scale/2;
                    } break;
            }
            for(int i = 0; i < leftCardIndi.Count; i++){ 
                if(i<=showleftCardamount-1){
                    leftCard_pos[i].anchoredPosition = Vector2.right*cardLerps[i]; 
                } 
            }

            showleftCard = false;
        }
        if(showrightCard){
            // for(int i = 0; i < rightCardIndi.Count; i++){
            //     if(i<=showrightCardamount-1){
            //         rightCardIndi[i].SetActive(true); 
            //     }                    
            //     else{
            //         rightCardIndi[i].SetActive(false); 
            //     }
            // }

            float[] cardLerps = new float[showrightCardamount];

            switch(showrightCardamount){
                case 1: cardLerps = new float[] {0f}; break;
                case 2: cardLerps = new float[] {-25f,25f}; break;
                case 3: cardLerps = new float[] {-50f,0f,50f}; break;
                default:
                    float interval = cartSort_scale / (showrightCardamount-1);
                    for(int i = 0; i < showrightCardamount;i++){
                        cardLerps[i] = interval * i-cartSort_scale/2;
                    } break;
                    

            }

            for(int i = 0; i < rightCardIndi.Count; i++){
                if(i<=showrightCardamount-1){
                    rightCard_pos[i].anchoredPosition = Vector2.right*cardLerps[i]; 
                } 
            }


            showrightCard = false;
        }
    }

    public void Leftcard_Update(){
        
        for(int i =0; i<leftCard_card.Count;i++){
            leftCardIndi[i].SetActive(true); 
            leftCardIndi_compo[i].card = leftCard_card[i];            
            leftCardIndi_compo[i].CardUpdate();
            // leftCard_card[i]// = cards[i];
            // // leftCard_img[i].sprite = cards[i].illust;
            // // leftCard_title[i].text = cards[i].name;
            // // leftCard_message[i].text = cards[i].message;
        }
    }

    public void Rightcard_Update(){
        for(int i =0; i<rightCard_card.Count;i++){
            rightCardIndi[i].SetActive(true);                 
            rightCardIndi_compo[i].card = rightCard_card[i];
            rightCardIndi_compo[i].CardUpdate();
        }
    }

    public void CardMesage_Update(string ability,string story){
        cardAbility.text = ability;
        cardStroy.text = story;
    }

    // Start is called before the first frame update

}
