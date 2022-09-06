using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{
    public int dice_num;

    public BattleManager battleManager;
    public BattleCaculate battleCaculate;
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
        // if(battleManager.battle_end)
        //     return;
        // if(battleManager.left_turn && gameObject.tag == "Team2"){
        //         return;
        //     }
        //     if(battleManager.right_turn && gameObject.tag == "Team1"){
        //         return;
        //     } 
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
            //transform.position = objPosition;
            objPosition += Vector3.forward;  
            lineRender.SetPosition(0, objPosition+Vector3.forward);  
        }
          
    }
//render.color = new Color(0,0,0,206);
    void OnMouseUp() {
        onMouseDown = false;
        //transform.position = saved_pos;
        lineRender.SetPosition(1, Vector3.zero+Vector3.forward);
        lineRender.SetPosition(0, Vector3.zero+Vector3.forward); 
        
             
    }
    

    void OnMouseEnter() {
        onMouseEnter = true;
    }

    void OnMouseExit() {
        onMouseEnter = false;
            
    }


}
