using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public BattleManager bm;
    [HideInInspector]public Button battleStartButton;
    public Button Dice6;
    public GameObject cardMessage;
    public TextMeshProUGUI cardAbility;
    public TextMeshProUGUI cardStroy;

    public GameObject panorama_up;
    public GameObject panorama_down;
    public RectTransform rect_panorama_up;
    public RectTransform rect_panorama_down;
    [SerializeField] float panora_spd;
    

    [HideInInspector]public bool showleftCard;

    public List<GameObject> leftCardIndi = new List<GameObject>();
    public List<card_text> leftCardIndi_compo = new List<card_text>();
    List<RectTransform> leftCard_pos = new List<RectTransform>();
    public List<CardPack> leftCard_card = new List<CardPack>();

    public Slider left_gague;
    public Slider right_gague;

    [HideInInspector]
    public bool showrightCard;
    //[HideInInspector]public int showrightCardamount;

    public List<GameObject> rightCardIndi = new List<GameObject>();
    List<RectTransform> rightCard_pos = new List<RectTransform>();
    public List<card_text> rightCardIndi_compo = new List<card_text>();
    public List<CardPack> rightCard_card = new List<CardPack>();
    public float cartSort_scale;

    void Awake(){
        rect_panorama_up = panorama_up.GetComponent<RectTransform>();
        rect_panorama_down = panorama_down.GetComponent<RectTransform>();
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
        
    }

    void Update(){
        # region 왼쪽카드보기
        if(showleftCard){
            float[] cardLerps = new float[leftCard_card.Count];

            switch(leftCard_card.Count){ // 카드수에 맞게 위치 조정
                case 1: cardLerps = new float[] {0f}; break;
                case 2: cardLerps = new float[] {-25f,25f}; break;
                case 3: cardLerps = new float[] {-50f,0f,50f}; break;
                default:
                    float interval = cartSort_scale / (leftCard_card.Count-1);
                    for(int i = 0; i < leftCard_card.Count;i++){
                        cardLerps[i] = interval * i-cartSort_scale/2;
                    } break;
            }
            for(int i = 0; i < leftCardIndi.Count; i++){ 
                if(i<=leftCard_card.Count-1){
                    leftCard_pos[i].anchoredPosition = Vector2.right*cardLerps[i]; 
                } 
            }

            showleftCard = false;
        }
        # endregion
        # region 오른쪽 카드보기
        if(showrightCard){

            float[] cardLerps = new float[rightCard_card.Count];

            switch(rightCard_card.Count){
                case 1: cardLerps = new float[] {0f}; break;
                case 2: cardLerps = new float[] {-25f,25f}; break;
                case 3: cardLerps = new float[] {-50f,0f,50f}; break;
                default:
                    float interval = cartSort_scale / (rightCard_card.Count-1);
                    for(int i = 0; i < rightCard_card.Count;i++){
                        cardLerps[i] = interval * i-cartSort_scale/2;
                    } break;
                    

            }

            for(int i = 0; i < rightCardIndi.Count; i++){
                if(i<=rightCard_card.Count-1){
                    rightCard_pos[i].anchoredPosition = Vector2.right*cardLerps[i]; 
                } 
            }


            showrightCard = false;
        }
        # endregion
        
    }

    public void Leftcard_Update(bool fold = false){
        for(int i =0; i<leftCardIndi.Count;i++){
            leftCardIndi[i].SetActive(false); 
        }
        if(fold)
            return;
        for(int i =0; i<leftCard_card.Count;i++){
            leftCardIndi[i].SetActive(true); 
            leftCardIndi_compo[i].card = leftCard_card[i];   
            leftCardIndi_compo[i].CardUpdate();

        }
    }

    public void Rightcard_Update(bool fold = false){
        for(int i =0; i<rightCardIndi.Count;i++){
            rightCardIndi[i].SetActive(false); 
        }
        if(fold)
            return;
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

    IEnumerator PanoraOn(){
        Debug.Log("debug");
        
        StopCoroutine("PanoraOff");
        // rect_panorama_up.anchoredPosition = Vector2.zero;
        // rect_panorama_down.anchoredPosition = Vector2.zero;
        while (true){
        rect_panorama_up.anchoredPosition = Vector2.MoveTowards(rect_panorama_up.anchoredPosition,Vector2.zero,panora_spd*Time.deltaTime);
        rect_panorama_down.anchoredPosition = Vector2.MoveTowards(rect_panorama_down.anchoredPosition,Vector2.zero,panora_spd*Time.deltaTime);
        if(rect_panorama_up.anchoredPosition == Vector2.zero && rect_panorama_down.anchoredPosition == Vector2.zero){
            break;
        }
        yield return null;
        }
        yield return null;
    }

    IEnumerator PanoraOff(){
        StopCoroutine("PanoraOn");
        // panorama_up.transform.position = panora_vec_up;
        // panorama_down.transform.position = panora_vec_down;
        while(true){
        rect_panorama_up.anchoredPosition = Vector2.MoveTowards(rect_panorama_up.anchoredPosition,Vector2.up*120,panora_spd*Time.deltaTime);
        rect_panorama_down.anchoredPosition = Vector2.MoveTowards(rect_panorama_down.anchoredPosition,Vector2.down*120,panora_spd*Time.deltaTime);
        if(rect_panorama_up.anchoredPosition == Vector2.up*120 && rect_panorama_down.anchoredPosition == Vector2.down*120){
            break;
        }
        yield return null;
        }
        yield return null;
    }

    // Start is called before the first frame update

}
