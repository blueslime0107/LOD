using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDraw : MonoBehaviour
{
    [SerializeField]Canvas canvas;
    [SerializeField]CardPrefap cardPrefap;
    [SerializeField]CardAbility having_card;
    public UI ui;
    public TextMeshProUGUI ability_message;

    [SerializeField]
    public BattleManager battleManager;
    Image render;

    Vector3 origin_size;
    public Vector3 origin_position;

    bool mouseOn = false;
    bool mouseDrag = false;
    [SerializeField]
    Player target;
    int card_id;

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
        Vector3 target = (battleManager.right_turn) ? Vector3.left*5+Vector3.up : Vector3.right*5+Vector3.up;
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
        cardPrefap.loaded = false;
        cardPrefap.cardUpdate(card);
        having_card = card;
        card_id = having_card.card_id;
    }

    // 드래그 중 마우스 따라가기 크기 줄어들기
    void OnMouseDrag(){
        mouseDrag = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); 
        if(battleManager.mouseTouchingPlayer){
            battleManager.lineRender.SetPosition(0, battleManager.mouseTouchingPlayer.dice_Indi.transform.position+Vector3.forward);
        } 
        else{
        battleManager.lineRender.SetPosition(1, transform.position);
        battleManager.lineRender.SetPosition(0, objPosition);

        }
        // transform.position = objPosition;
        // transform.localScale = new Vector3(0.02f,0.02f,0);
    }

    private void OnMouseExit() {
        mouseOn = false;
    }

    void OnMouseEnter() {
        battleManager.sdm.Play("Paper2");
        ui.CardMesage_Update(having_card.ability_message,having_card.story_message);
        mouseOn = true;
    }

    void OnMouseDown(){
        battleManager.sdm.Play("Paper1");
        StartCoroutine("MoveSelect");
        ui.cardMessage.SetActive(false);
    }

    private void OnMouseUp() { // 자신이 선택됬고 캐릭터를 정했을때 카드 줌
        battleManager.lineRender.SetPosition(1, Vector3.zero);
        battleManager.lineRender.SetPosition(0, Vector3.zero);
        if(battleManager.mouseTouchingPlayer != null){
            if(battleManager.card_getting_team.Equals("Left") && battleManager.card_left_draw > 0 && battleManager.mouseTouchingPlayer.gameObject.tag.Equals("PlayerTeam1")){
                battleManager.GiveCard(having_card,battleManager.mouseTouchingPlayer);
                battleManager.card_gived = true;
                battleManager.card_left_draw -= 1;
                gameObject.SetActive(false);
            }
            if(battleManager.card_getting_team.Equals("Right") && battleManager.card_right_draw > 0 && battleManager.mouseTouchingPlayer.gameObject.tag.Equals("PlayerTeam2")){
                battleManager.GiveCard(having_card,battleManager.mouseTouchingPlayer);
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
            try{if(collision.gameObject.tag.Contains("PlayerTeam")){
                target = collision.gameObject.GetComponent<Player>();
                target.ShowCardDeck(false,true);
            }}
            catch{
                return;
            }
           
        }
        

    }

    void OnTriggerExit2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.tag.Contains("PlayerTeam")){
                if(collision.gameObject.tag.Equals("PlayerTeam1")){
                    battleManager.ui.CardFold("Left",true);
                }
                else{
                    battleManager.ui.CardFold("Right",true);
                }
                target = null;
            }

        }
        

    }

}
