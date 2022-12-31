using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public BattleManager bm;
    public StartButton battleStartButton;
    public Button Dice6;
    public GameObject cardMessage;
    public TextMeshProUGUI cardAbility;
    public TextMeshProUGUI cardStroy;

    

    public GameObject panorama_up;
    public GameObject panorama_down;
    public RectTransform rect_panorama_up;
    public RectTransform rect_panorama_down;
    [SerializeField] float panora_spd;
    
    

    public Slider left_gague;
    public Slider right_gague;

    [SerializeField] GameObject cardObject;

    [HideInInspector]public bool showleftCard;
    [HideInInspector]public bool showrightCard;

    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;

    public List<Card_text> leftCardIndi = new List<Card_text>();
    public List<CardPack> leftCard_card = new List<CardPack>();
    List<CardPack> origin_leftCard_card = new List<CardPack>();
    public CardExplain leftCardEx;
    
    public List<Card_text> rightCardIndi = new List<Card_text>();
    public List<CardPack> rightCard_card = new List<CardPack>();
    List<CardPack> origin_rightCard_card = new List<CardPack>();
    public CardExplain rightCardEx;

    void Awake(){
        rect_panorama_up = panorama_up.GetComponent<RectTransform>();
        rect_panorama_down = panorama_down.GetComponent<RectTransform>();
        
    }

    void Start(){
        CardUIUpdate("Left");
            CardUIUpdate("Right");
        StartCoroutine("UpdateCards");
    }

    IEnumerator UpdateCards(){
        while(true){
            CardUIUpdate("Left");
            CardUIUpdate("Right");
            yield return null;
        }
    }

    public void CardUIUpdate(string team){

        if(team.Equals("Left")){if(origin_leftCard_card.Equals(leftCard_card)){return;}}
        else{if(origin_rightCard_card.Equals(rightCard_card)){return;}}

        List<Card_text> indi = (team.Equals("Left")) ? leftCardIndi : rightCardIndi;
        List<CardPack> card = (team.Equals("Left")) ? leftCard_card : rightCard_card;

        // for(int i =0; i<indi.Count;i++){
        //     indi[i].gameObject.SetActive(false); 
        // }
        if(card.Count <= 0)
            return;
        for(int i =0; i<card.Count;i++){
            if(i.Equals(indi.Count)){
                Card_text obj = Instantiate(cardObject).GetComponent<Card_text>();
                obj.transform.SetParent((team.Equals("Left")) ? leftPanel.transform : rightPanel.transform,false);   
                obj.battleManager = bm;  
                obj.card_num = i;   
                obj.isLeft = (team.Equals("Left")) ? true : false;
                indi.Add(obj);
            }

            //indi[i].gameObject.SetActive(true); 
            indi[i].card = card[i];   
            indi[i].cardPrefap.cardUpdate(indi[i].card.ability);
            indi[i].CardUpdate();
        }
        if(team.Equals("Left")){origin_leftCard_card = leftCard_card;}
        else{origin_rightCard_card = rightCard_card;}
        
    }

    public void CardFold(string team,bool fold = false){
        List<Card_text> indi = (team.Equals("Left")) ? leftCardIndi : rightCardIndi;
        List<CardPack> card = (team.Equals("Left")) ? leftCard_card : rightCard_card;

        for(int i =0; i<indi.Count;i++){
            indi[i].gameObject.SetActive(false); 
        }
        if(card.Count <= 0 || fold)
            return;
        if(indi.Count <= 0)
            return;
        for(int i =0; i<card.Count;i++){
            indi[i].gameObject.SetActive(true); 
        }
    }

    public void VisualCardPanel(bool active){
        leftPanel.SetActive(active);
        rightPanel.SetActive(active);
    }


    public void CardMesage_Update(string ability,string story){
        cardAbility.text = ability;
        cardStroy.text = story;
    }

    IEnumerator PanoraOn(){
        
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
