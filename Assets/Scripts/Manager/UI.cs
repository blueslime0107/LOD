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
    
    

    public Slider left_gague;
    public Slider right_gague;

    [SerializeField] GameObject cardObject;

    [HideInInspector]public bool showleftCard;
    [HideInInspector]public bool showrightCard;

    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;

    public List<card_text> leftCardIndi = new List<card_text>();
    public List<CardPack> leftCard_card = new List<CardPack>();
    
    public List<card_text> rightCardIndi = new List<card_text>();
    public List<CardPack> rightCard_card = new List<CardPack>();

    void Awake(){
        rect_panorama_up = panorama_up.GetComponent<RectTransform>();
        rect_panorama_down = panorama_down.GetComponent<RectTransform>();
        // for(int i = 0; i < leftCardIndi.Count; i++){
        //     leftCard_pos.Add(leftCardIndi[i].GetComponent<RectTransform>());        
        //     }
        // for(int i = 0; i < leftCardIndi.Count; i++){
        //     leftCardIndi_compo.Add(leftCardIndi[i].GetComponent<card_text>());        
        //     }
        // for(int i = 0; i < rightCardIndi.Count; i++){
        //     rightCard_pos.Add(rightCardIndi[i].GetComponent<RectTransform>());        
        //     }
        // for(int i = 0; i < rightCardIndi.Count; i++){
        //     rightCardIndi_compo.Add(rightCardIndi[i].GetComponent<card_text>());        
        //     }
        
    }

    public void CardUIUpdate(string team, bool fold = false){
        List<card_text> indi = (team.Equals("Left")) ? leftCardIndi : rightCardIndi;
        List<CardPack> card = (team.Equals("Left")) ? leftCard_card : rightCard_card;
        

        for(int i =0; i<indi.Count;i++){
            indi[i].gameObject.SetActive(false); 
        }
        if(fold || card.Count <= 0)
            return;
        for(int i =0; i<card.Count;i++){
            if(i.Equals(indi.Count)){
                card_text obj = Instantiate(cardObject).GetComponent<card_text>();
                obj.transform.SetParent((team.Equals("Left")) ? leftPanel.transform : rightPanel.transform,false);   
                obj.battleManager = bm;  
                obj.card_num = i;    
                indi.Add(obj);
            }

            indi[i].gameObject.SetActive(true); 
            indi[i].card = card[i];   
            indi[i].cardPrefap.cardUpdate(indi[i].card.ability);
            indi[i].CardUpdate();
        }
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
