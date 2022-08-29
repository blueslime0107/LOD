using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewer : MonoBehaviour
{
    [HideInInspector]public bool showleftCard;

    public List<GameObject> leftCardIndi = new List<GameObject>();
    [HideInInspector]public List<card_text> leftCardIndi_compo = new List<card_text>();
    [HideInInspector]public List<CardAbility> leftCard_card = new List<CardAbility>();

    [HideInInspector]
    public bool showrightCard;

    public List<GameObject> rightCardIndi = new List<GameObject>();
    [HideInInspector]public List<card_text> rightCardIndi_compo = new List<card_text>();
    [HideInInspector]public List<CardAbility> rightCard_card = new List<CardAbility>();
    public float cartSort_scale;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < leftCardIndi.Count; i++){
            leftCardIndi_compo.Add(leftCardIndi[i].GetComponent<card_text>());        
            }
        for(int i = 0; i < rightCardIndi.Count; i++){
            rightCardIndi_compo.Add(rightCardIndi[i].GetComponent<card_text>());        
            }        
    }

    // Update is called once per frame
    void Update()
    {
        if(showleftCard){
            float[] cardLerps = new float[leftCard_card.Count];

            switch(leftCard_card.Count){ // 카드수에 맞게 위치 조정
                case 1: cardLerps = new float[] {0f}; break;
                case 2: cardLerps = new float[] {-1f,1f}; break;
                case 3: cardLerps = new float[] {-2f,0f,2f}; break;
                default:
                    float interval = cartSort_scale / (leftCard_card.Count-1);
                    for(int i = 0; i < leftCard_card.Count;i++){
                        cardLerps[i] = interval * i-cartSort_scale/2;
                    } break;
            }
            for(int i = 0; i < leftCardIndi.Count; i++){ 
                if(i<=leftCard_card.Count-1){
                    leftCardIndi[i].transform.localPosition = Vector3.right*cardLerps[i]; 
                } 
            }

            showleftCard = false;
        }
        if(showrightCard){

            float[] cardLerps = new float[rightCard_card.Count];

            switch(rightCard_card.Count){
                case 1: cardLerps = new float[] {0f}; break;
                case 2: cardLerps = new float[] {-1f,1f}; break;
                case 3: cardLerps = new float[] {-2f,0f,2f}; break;
                default:
                    float interval = cartSort_scale / (rightCard_card.Count-1);
                    for(int i = 0; i < rightCard_card.Count;i++){
                        cardLerps[i] = interval * i-cartSort_scale/2;
                    } break;
                    

            }

            for(int i = 0; i < rightCardIndi.Count; i++){
                if(i<=rightCard_card.Count-1){
                    rightCardIndi[i].transform.localPosition = Vector2.right*cardLerps[i]; 
                } 
            }


            showrightCard = false;
        }        
    }

    public void Leftcard_Update(){
        for(int i =0; i<leftCard_card.Count+1;i++){
            leftCardIndi[i].SetActive(false); 
        }
        for(int i =0; i<leftCard_card.Count;i++){
            leftCardIndi[i].SetActive(true); 
            leftCardIndi_compo[i].card = leftCard_card[i];            
            leftCardIndi_compo[i].CardUpdate();

        }
    }

    public void Rightcard_Update(){
        for(int i =0; i<rightCard_card.Count+1;i++){
            rightCardIndi[i].SetActive(false); 
        }
        for(int i =0; i<rightCard_card.Count;i++){
            rightCardIndi[i].SetActive(true);                 
            rightCardIndi_compo[i].card = rightCard_card[i];
            rightCardIndi_compo[i].CardUpdate();
        }
    }
}
