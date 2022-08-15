using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public Button battleStartButton;
    public Text[] hpText;

    public bool showleftCard;
    public int showleftCardamount;

    public List<GameObject> leftCardIndi = new List<GameObject>();
    List<RectTransform> leftCard_pos = new List<RectTransform>();
    List<Image> leftCard_img = new List<Image>();
    List<TextMeshProUGUI> leftCard_title = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> leftCard_message = new List<TextMeshProUGUI>();
    public float cartSort_scale;

    void Awake(){
        
        for(int i = 0; i < leftCardIndi.Count; i++){
            leftCard_pos.Add(leftCardIndi[i].GetComponent<RectTransform>());        
            }
        for(int i = 0; i < leftCardIndi.Count; i++){
            leftCard_img.Add(leftCardIndi[i].GetComponent<Image>());        
            }
        for(int i = 0; i < leftCardIndi.Count; i++){
            card_text txt = leftCardIndi[i].GetComponent<card_text>();    
            leftCard_title.Add(txt.name);                    
            }
        for(int i = 0; i < leftCardIndi.Count; i++){
            card_text txt = leftCardIndi[i].GetComponent<card_text>();    
            leftCard_message.Add(txt.message);                    
            }
        
    }

    void Update(){
        if(showleftCard){
            for(int i = 0; i < leftCardIndi.Count; i++){
                if(i<=showleftCardamount-1){
                    leftCardIndi[i].SetActive(true); 
                }                    
                else{
                    leftCardIndi[i].SetActive(false); 
                }
            }

            float[] cardLerps = new float[showleftCardamount];

            switch(showleftCardamount){
                case 1: cardLerps = new float[] {0f}; break;
                case 2: cardLerps = new float[] {-25f,25f}; break;
                case 3: cardLerps = new float[] {-50f,0f,50f}; break;
                default:
                    float interval = cartSort_scale / (showleftCardamount-1);
                    Debug.Log(interval);
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
    }

    public void Leftcard_Update(List<CardAbility> cards){
        for(int i =0; i<cards.Count;i++){
            leftCard_img[i].sprite = cards[i].illust;
            leftCard_title[i].text = cards[i].name;
            leftCard_message[i].text = cards[i].message;
        }
    }


    // Start is called before the first frame update

}
