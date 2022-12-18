using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{
    public int dice_num;

    [SerializeField] ParticleSystem particle;
    public BattleManager battleManager;
    public BattleCaculate battleCaculate;
    public Sprite[] dice_img;
    public bool isDiced = false;
    public int dice_value;
    Vector3 saved_pos;

    public bool onMouseDown;
    public bool onMouseEnter;

    public SpriteRenderer render;
    public SpriteRenderer sub_render;
    public Player player;
    public LineRenderer lineRender;
    public GameObject sub_dice;
    public List<Dice> dice_list= new List<Dice>();
    void Awake() {
        render = GetComponent<SpriteRenderer>();
        sub_render = sub_dice.GetComponent<SpriteRenderer>();
    }

    void OnEnable(){
        particle.Stop();
    }

    // void Update(){
    //     if(Input.GetMouseButtonUp(0)){
    //         if(targetSelected && battleManager.target1 > 0 && player.player_id != battleManager.target1){
    //         battleManager.target2 = player.player_id;
    //         battleManager.BattleTargetReady();
    //         //battleCaculate.BattleMatch(dice_num,target); 
    //         }
    //     }
        
    // }

    public void putDice(Dice dice){
        particle.Play();
        dice_list.Add(dice);
        player.dice = dice.dice_value;
        player.ChangeCondition(1);
        dice_value = dice.dice_value;
        isDiced = true;       
        render.sprite = dice_img[dice.dice_value];
        for(int i = 0; i<player.cards.Count;i++){
            player.cards[i].ability.DiceApplyed(player.cards[i], player);
        }
        
        
    }

    public void NextDice(){
        if(dice_value != 0){
            return;
        }
        if(dice_list.Count <= 0){
            player.ChangeCondition(0);
            return;
        }
        dice_list.RemoveAt(0);
        if(dice_list.Count <= 0){
            player.ChangeCondition(0);
            return;
        }

        player.dice = dice_list[0].dice_value;
        player.ChangeCondition(1);
        dice_value = dice_list[0].dice_value;
        isDiced = true;       
        render.sprite = dice_img[dice_list[0].dice_value];
        for(int i = 0; i<player.cards.Count;i++){
            player.cards[i].ability.DiceApplyed(player.cards[i], player);
        }

        

        if(dice_list.Count < 2){
            sub_dice.SetActive(false);
        }
    }

    public void put_subDice(Dice dice){
        Debug.Log("put_dice");
        if(!sub_dice.activeSelf){
            sub_dice.SetActive(true);
        }
        dice_list.Add(dice);    
        sub_render.sprite = dice_img[dice_list[0].dice_value];
    }

    public void setDice(int value){
        if(player.died) {
            player.dice = 0;
            return;
        }
        

        player.dice = value;
        dice_value = value;
        render.sprite = dice_img[value];
    }

    void OnMouseDown() {
        onMouseDown = true;
        try
        {if(gameObject.tag.Equals("Team1")){    
            battleManager.cardViewChar_left.player_floor_render.SetInt("_Active",0);   
            if(battleManager.cardViewChar_left.Equals(player)){
                player.ShowCardDeck(false);
                battleManager.cardViewChar_left = null;
                battleManager.left_cardLook_lock = false;
            }   
            else{
                battleManager.cardViewChar_left = player;
                battleManager.cardViewChar_left.player_floor_render.SetInt("_Active",1);            
                player.ShowCardDeck(true,true);
                battleManager.left_cardLook_lock = true;
            }
        }
        if(gameObject.tag.Equals("Team2")){    
            battleManager.cardViewChar_right.player_floor_render.SetInt("_Active",0);   
            if(battleManager.cardViewChar_right.Equals(player)){
                player.ShowCardDeck(false);
                battleManager.cardViewChar_right = null;
                battleManager.right_cardLook_lock = false;
            }   
            else{
                battleManager.cardViewChar_right = player;
                battleManager.cardViewChar_right.player_floor_render.SetInt("_Active",1);                        
                player.ShowCardDeck(true,true);
                battleManager.right_cardLook_lock = true;
            }
        }}
        catch{
            if(gameObject.tag.Equals("Team1")){
                battleManager.cardViewChar_left = player;
                battleManager.cardViewChar_left.player_floor_render.SetInt("_Active",1); 
                battleManager.left_cardLook_lock = true;
                }
            if(gameObject.tag.Equals("Team2")){battleManager.cardViewChar_right = player;
            battleManager.cardViewChar_right.player_floor_render.SetInt("_Active",1); 
            battleManager.right_cardLook_lock = true;}
            player.ShowCardDeck(true);
            
        }
        saved_pos = transform.position;
        //battleManager.target1 = player.player_id;
    }

    void OnMouseDrag() { // 마우스 
        if(battleManager.battle_start){       
            if(battleManager.left_turn && gameObject.tag == "Team2"){
                return;
            }
            if(battleManager.right_turn && gameObject.tag == "Team1"){
                return;
            }     
            lineRender.SetPosition(1, saved_pos+Vector3.forward);
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f);        
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);  
            objPosition += Vector3.forward;  
            lineRender.SetPosition(0, objPosition+Vector3.forward);  
        }
          
    }
//render.color = new Color(0,0,0,206);
    void OnMouseUp() {
        onMouseDown = false;
        lineRender.SetPosition(1, Vector3.zero+Vector3.forward);
        lineRender.SetPosition(0, Vector3.zero+Vector3.forward); 
             
    }
    

    void OnMouseEnter() {
        if(!battleManager.battleing)
            battleManager.mouseTouchingPlayer = player;

        if(player.gameObject.tag.Equals("PlayerTeam1") && !battleManager.left_cardLook_lock){
            player.player_floor_render.SetInt("_Active",1);
            player.ShowCardDeck(false);
        }
        if(player.gameObject.tag.Equals("PlayerTeam2") && !battleManager.right_cardLook_lock){
            player.player_floor_render.SetInt("_Active",1); 
            player.ShowCardDeck(false);
        }
        onMouseEnter = true;
    }

    void OnMouseExit() {
        onMouseEnter = false;
        battleManager.mouseTouchingPlayer = null;
        if(player.gameObject.tag.Equals("PlayerTeam1") && !battleManager.left_cardLook_lock){
            player.player_floor_render.SetInt("_Active",0);
            battleManager.ui.CardFold("Left",true);
        
        }
        if(player.gameObject.tag.Equals("PlayerTeam2") && !battleManager.right_cardLook_lock){
            player.player_floor_render.SetInt("_Active",0);
            battleManager.ui.CardFold("Right",true);
        
        }
            
    }

    void OnMouseOver() {
        if(battleManager.target1 != null && !battleManager.battleing){
            lineRender.SetPosition(0, battleManager.mouseTouchingPlayer.dice_Indi.transform.position+Vector3.forward); 
        }

    }



}
