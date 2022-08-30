using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDraw : MonoBehaviour
{
    Canvas canvas;
    [SerializeField]CardAbility having_card;
    public UI ui;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI message;
    public TextMeshProUGUI ability_message;

    [SerializeField]
    public BattleManager battleManager;
    Image render;
    // [SerializeField]
    // Sprite[] card_img; // 카드 이미지


    Vector3 origin_size;
    Vector3 origin_position;

    bool mouseOn = false;
    bool mouseDrag = false;
    [SerializeField]
    int target;
    int card_id;

    // Start is called before the first frame update
    void Awake() // 생성되자마자 실행
    {
        
        GameObject obj1 = gameObject.transform.GetChild(0).gameObject;
        GameObject obj2 = obj1.transform.GetChild(0).gameObject;

        canvas = obj1.GetComponent<Canvas>();
        render = obj2.GetComponent<Image>();
        GameObject name_obj = obj2.transform.GetChild(0).gameObject;
        name = name_obj.GetComponent<TextMeshProUGUI>();
        GameObject message_obj = obj2.transform.GetChild(1).gameObject;
        message = message_obj.GetComponent<TextMeshProUGUI>();



        origin_position = transform.position;
        origin_size = transform.localScale;
    }

    void Update(){

        if(Input.GetMouseButtonUp(0)){
            transform.position = origin_position;
            transform.localScale = origin_size; 
        }
        if(Input.GetMouseButtonDown(0)){
            if(!mouseOn){
                transform.position += Vector3.up*5;
            }            
        }
        if(battleManager.card_gived)   // 이미 카드를 줬을때 사라지기
            Destroy(gameObject);
        
    }

    // 외부에서 실행 카드 이미지 바꾸기
    public void SetImage(CardAbility card){
        having_card = card;
        //having_card.effect = card_effect
        card_id = having_card.card_id;
        render.sprite = having_card.illust;
        name.text = having_card.name;
        message.text = having_card.message;
        //ability_message.text = having_card.ability_message;
    }

    // 드래그 중 마우스 따라가기 크기 줄어들기
    void OnMouseDrag(){
        mouseDrag = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);  
        transform.position = objPosition;
        transform.localScale = new Vector3(0.02f,0.02f,0);
    }

    private void OnMouseExit() {
        mouseOn = false;
    }

    void OnMouseEnter() {
        ui.CardMesage_Update(having_card.ability_message,having_card.story_message);
        mouseOn = true;
    }

    void OnMouseDown(){
        ui.cardMessage.SetActive(false);
    }

    private void OnMouseUp() { // 자신이 선택됬고 캐릭터를 정했을때 카드 줌
        if(target>0 && mouseOn){
            if(battleManager.card_getting_team && battleManager.card_left_draw > 0 && target-1<3){
                CardPack card = gameObject.AddComponent<CardPack>();
                card.ability = having_card;
                card.battleManager = battleManager;
                card.PreSetting();
                battleManager.players[target-1].cards.Add(card);
                battleManager.card_gived = true;
                battleManager.card_left_draw -= 1;
                Destroy(gameObject);
            }
            if(!battleManager.card_getting_team && battleManager.card_right_draw > 0 && target-1>2){
                CardPack card = gameObject.AddComponent<CardPack>();
                card.ability = having_card;
                card.PreSetting();
                battleManager.players[target-1].cards.Add(card);
                battleManager.card_gived = true;
                battleManager.card_right_draw -= 1;
                Destroy(gameObject);
            }
            
            
        }
        else{
            ui.cardMessage.SetActive(true);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.name == "LPlayer1"){
                target = 1;
            }                
            else if(collision.gameObject.name == "LPlayer2"){
                target = 2;
            }                
            else if(collision.gameObject.name == "LPlayer3"){
                target = 3;
            }                
            else if(collision.gameObject.name == "RPlayer4"){
                target = 4;
            }                
            else if(collision.gameObject.name == "RPlayer5"){
                target = 5;
            }                
            else if(collision.gameObject.name == "RPlayer6"){
                target = 6;
            }
        }
        

    }

    void OnTriggerStay2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.name == "LPlayer1"){
                target = 1;
            }                
            else if(collision.gameObject.name == "LPlayer2"){
                target = 2;
            }                
            else if(collision.gameObject.name == "LPlayer3"){
                target = 3;
            }                
            else if(collision.gameObject.name == "RPlayer4"){
                target = 4;
            }                
            else if(collision.gameObject.name == "RPlayer5"){
                target = 5;
            }                
            else if(collision.gameObject.name == "RPlayer6"){
                target = 6;
            }
        }
        

    }

    void OnTriggerExit2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.name == "LPlayer1"){
                target = 0;
            }                
            else if(collision.gameObject.name == "LPlayer2"){
                target = 0;
            }                
            else if(collision.gameObject.name == "LPlayer3"){
                target = 0;
            }                
            else if(collision.gameObject.name == "RPlayer4"){
                target = 0;
            }                
            else if(collision.gameObject.name == "RPlayer5"){
                target = 0;
            }                
            else if(collision.gameObject.name == "RPlayer6"){
                target = 0;
            }
        }
        

    }

}
