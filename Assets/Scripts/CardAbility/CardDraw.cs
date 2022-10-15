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
    public Vector3 origin_position;

    bool mouseOn = false;
    bool mouseDrag = false;
    [SerializeField]
    Player target;
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

        


    }

    private void OnEnable() {
        origin_size = transform.localScale;        
    }

    IEnumerator MoveDown(){
        StopCoroutine("MoveUp");
        Vector3 target = origin_position+Vector3.down*8.5f+Vector3.up;
        while(true)
        { 
            transform.position = Vector3.MoveTowards(transform.position,target,100*Time.deltaTime);
            if(transform.position == target){
                break;
                
            }
            yield return null;
            
            

            }
        yield return null;
    }

    IEnumerator MoveUp(){
        StopCoroutine("MoveDown");
        Vector3 target = origin_position;
        while(true)
        { 
            transform.position = Vector3.MoveTowards(transform.position,target,100*Time.deltaTime);
            if(transform.position == target){
                break;
                
            }
            yield return null;
            
            

            }
        yield return null;
    }

    IEnumerator MoveSelect(){
        Vector3 target = Vector3.right*5+Vector3.up;
        while(true)
        { 
            transform.position = Vector3.MoveTowards(transform.position,target,100*Time.deltaTime);
            yield return null;
        }
    }

    void Update(){

        if(Input.GetMouseButtonUp(0)){
            StopCoroutine("MoveSelect");
            StartCoroutine("MoveDown");
            transform.localScale = origin_size; 
            battleManager.ui.cardMessage.SetActive(true);
        }
        if(Input.GetMouseButtonDown(0)){
            if(!mouseOn){
                StartCoroutine("MoveUp");
            }            
        }
        if(battleManager.card_gived) {  // 이미 카드를 줬을때 사라지기
            if(battleManager.left_turn){
                    foreach(Player player in battleManager.left_players){
                        for(int i = 0;i<player.cards.Count;i++){
                            player.cards[i].ability.WhenCardDestroy(player.cards[i],having_card);
                        
                        }
                    }
                    
                }
            else{
                foreach(Player player in battleManager.right_players){
                    for(int i = 0;i<player.cards.Count;i++){
                        player.cards[i].ability.WhenCardDestroy(player.cards[i],having_card);
                    
                    }
                }
            }
            gameObject.SetActive(false);
            }
            
        
        
    }

    public void DestroyTheCard(){
        gameObject.SetActive(false);
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
        battleManager.lineRender.SetPosition(1, transform.position);
        battleManager.lineRender.SetPosition(0, objPosition);
        // transform.position = objPosition;
        // transform.localScale = new Vector3(0.02f,0.02f,0);
    }

    private void OnMouseExit() {
        mouseOn = false;
    }

    void OnMouseEnter() {
        ui.CardMesage_Update(having_card.ability_message,having_card.story_message);
        mouseOn = true;
    }

    void OnMouseDown(){
        StartCoroutine("MoveSelect");
        ui.cardMessage.SetActive(false);
    }

    private void OnMouseUp() { // 자신이 선택됬고 캐릭터를 정했을때 카드 줌
        battleManager.lineRender.SetPosition(1, Vector3.zero);
        battleManager.lineRender.SetPosition(0, Vector3.zero);
        if(battleManager.mouseTouchingTarget != null){
            if(battleManager.card_getting_team.Equals("Left") && battleManager.card_left_draw > 0 && battleManager.mouseTouchingTarget.gameObject.tag.Equals("PlayerTeam1")){
                battleManager.GiveCard(having_card,battleManager.mouseTouchingTarget);
                battleManager.card_gived = true;
                battleManager.card_left_draw -= 1;
                gameObject.SetActive(false);
            }
            if(battleManager.card_getting_team.Equals("Right") && battleManager.card_right_draw > 0 && battleManager.mouseTouchingTarget.gameObject.tag.Equals("PlayerTeam2")){
                battleManager.GiveCard(having_card,battleManager.mouseTouchingTarget);
                battleManager.card_gived = true;
                battleManager.card_right_draw -= 1;
                gameObject.SetActive(false);
            }
            
            
        }
        else{
            ui.cardMessage.SetActive(true);
        }
        
        }

    void OnTriggerEnter2D(Collider2D collision) {
        
        if(mouseDrag){
            if(collision.gameObject.tag.Contains("PlayerTeam")){
                target = collision.gameObject.GetComponent<Player>();
                target.ShowCardDeck(false,true);
            }
           
        }
        

    }

    void OnTriggerExit2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.tag.Contains("PlayerTeam")){
                if(collision.gameObject.tag.Equals("PlayerTeam1")){
                    battleManager.ui.Leftcard_Update(true);
                }
                else{
                    battleManager.ui.Rightcard_Update(true);
                }
                target = null;
            }

        }
        

    }

}
