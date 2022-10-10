using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{
    public int dice_num;

    public BattleManager battleManager;
    public BattleCaculate battleCaculate;
    Material material;
    public Sprite[] dice_img;
    public bool isDiced = false;
    public int dice_value;
    Vector3 saved_pos;

    public bool onMouseDown;
    public bool onMouseEnter;

    public SpriteRenderer render;
    public Player player;
    public LineRenderer lineRender;
    void Awake() {
        render = GetComponent<SpriteRenderer>();
        material = GetComponent<SpriteRenderer>().material;
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

    public void putDice(int value){
        player.dice = value;
        player.ChangeCondition(1);
        dice_value = value;
        isDiced = true;       
        render.sprite = dice_img[value];
        for(int i = 0; i<player.cards.Count;i++){
            player.cards[i].ability.DiceApplyed(player.cards[i], player);
        }
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
        Debug.Log(("why"));
        lineRender.SetPosition(1, Vector3.zero+Vector3.forward);
        lineRender.SetPosition(0, Vector3.zero+Vector3.forward); 
        
             
    }
    

    void OnMouseEnter() {
        if(!battleManager.battleing)
            battleManager.mouseTouchingTarget = player;

        if(player.gameObject.tag.Equals("PlayerTeam1") && !battleManager.left_cardLook_lock){
            player.ShowCardDeck(false);
        }
        if(player.gameObject.tag.Equals("PlayerTeam2") && !battleManager.right_cardLook_lock){
            player.ShowCardDeck(false);
        }
        onMouseEnter = true;
    }

    void OnMouseExit() {
        onMouseEnter = false;
        if(player.gameObject.tag.Equals("PlayerTeam1") && !battleManager.left_cardLook_lock){
            battleManager.ui.Leftcard_Update(true);
        
        }
        if(player.gameObject.tag.Equals("PlayerTeam2") && !battleManager.right_cardLook_lock){
            battleManager.ui.Rightcard_Update(true);
        
        }
            
    }

    void OnMouseOver() {
        if(battleManager.target1 != null && !battleManager.battleing){
            lineRender.SetPosition(0, battleManager.mouseTouchingTarget.dice_Indi.transform.position+Vector3.forward); 
        }

    }



}
