using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{
    public int dice_num;
    public DiceOBJ diceOBJ;
    public List<DiceOBJ> sub_diceOBJ;

    [SerializeField] ParticleSystem particle;
    public BattleManager battleManager;
    public BattleCaculate battleCaculate;
    public bool isDiced = false;
    Vector3 saved_pos;

    public bool onMouseDown;
    public bool onMouseEnter;

    public Player player;
    public BezierCurve lineRender;
    public GameObject sub_dice;
    public List<DiceProperty> dice_list= new List<DiceProperty>();

    void OnEnable(){
        particle.Stop();
    }

    public void updateDice(){
        diceOBJ.updateDice(dice_list.Count > 0 ? dice_list[0].value : 0 );

        int i =1;
        foreach(DiceOBJ diceOBJ in sub_diceOBJ){
            if(dice_list.Count > i){diceOBJ.updateDice(dice_list[i].value);}
            diceOBJ.gameObject.SetActive(dice_list.Count > i);
            i++;
        }
        player.dice = dice_list.Count > 0 ? dice_list[0].value : 0;
    }

    public void putDice(Dice dice){
        particle.Play();
        dice_list.Add(battleManager.MakeNewDice(dice.dice_value,dice.farAtt));
        player.ChangeCondition(1);
        updateDice();
        isDiced = true;     
        battleManager.sdm.Play("Paper1");
        
        
    }

    public void NextDice(){
        if(dice_list.Count > 0){
            dice_list.RemoveAt(0);
            if(dice_list.Count <= 0){
            player.ChangeCondition(0);
            updateDice();
            return;
            }
        }

        updateDice();
        player.ChangeCondition(1);
        isDiced = true;       
        
    }

    public void put_subDice(DiceProperty dice,bool atfirst = false){
        Debug.Log("sub_putdice");
        Debug.Log(dice);
        if(atfirst){dice_list.Insert(0,dice);}
        else{dice_list.Add(dice);}   
        updateDice();
    }

    public void setDice(int value){
        
        if(player.died) {
            player.dice = 0;
            return;
        }
        dice_list[0].value = value;
        
        particle.Play();
        updateDice();
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
        if(battleManager.cardActiveAble){       
            if(!battleManager.cur_team.players.Contains(player)) return; 
            if(!lineRender.gameObject.activeSelf){lineRender.gameObject.SetActive(true);}
            lineRender.SetStart(saved_pos,1);
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f);        
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);  
            objPosition += Vector3.forward;  
            lineRender.SetEnd(objPosition,1);
        }
          
    }
//render.color = new Color(0,0,0,206);
    void OnMouseUp() {
        onMouseDown = false;
        lineRender.gameObject.SetActive(false);
             
    }
    

    void OnMouseEnter() {
        if(battleManager.target1 != null && !battleManager.battleing){battleManager.sdm.Play("Aim");}
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
            lineRender.SetEnd(battleManager.mouseTouchingPlayer.dice_Indi.transform.position,1);
        }
    }



}
