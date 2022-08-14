using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Button battleStartButton;
    public Text[] hpText;

    public bool showleftCard;
    public int showleftCardamount;

    public List<GameObject> leftCardIndi = new List<GameObject>();
    public List<RectTransform> leftCard_pos = new List<RectTransform>();
    public float cartSort_scale;

    void Awake(){
        
        for(int i = 0; i < leftCardIndi.Count; i++){
            leftCard_pos.Add(leftCardIndi[i].GetComponent<RectTransform>());        
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

    // Start is called before the first frame update

}
