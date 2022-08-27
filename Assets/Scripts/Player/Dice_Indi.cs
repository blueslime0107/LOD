using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{

    public BattleManager battleManager;
    public BattleCaculate battleCaculate;
    public Sprite[] dice_img;
    public bool isDiced = false;
    public int value;
    Vector3 saved_pos;

    public bool targetSelected;

    [HideInInspector] public SpriteRenderer render;
    public Player player;
    public LineRenderer lineRender;
    void Awake() {
        render = GetComponent<SpriteRenderer>();        
        player = transform.GetComponentInParent<Player>();
    }

    void Start(){
        if(gameObject.tag == "Team1")
            battleManager.left_dice.Add(this);
        if(gameObject.tag == "Team2")
            battleManager.right_dice.Add(this);
        player.dice = this;
        player.dice_s.Add(this);
    }

    void Update(){
        if(Input.GetMouseButtonUp(0)){
            if(targetSelected && battleManager.target1 && player.player_id != battleManager.target1.player.player_id){
                battleManager.target2 = this;

                if(battleManager.target2.value.Equals(0)){

                }
            //battleCaculate.BattleMatch(dice_num,target); 
            }
        }
        
    }

    public void putDice(int value){
        //player.dice = value;
        player.ChangeCondition(1);
        this.value = value;
        isDiced = true;       
        render.sprite = dice_img[value];
        
        for(int i = 0; i<player.cards.Count;i++){
            player.cards[i].DiceApplyed(player,this); //////////////////// DiceApplyed 이벤트
        }
    }

    public void setDice(int value){
        //player.dice = value;
        this.value = value;
        render.sprite = dice_img[value];
    }

    void OnMouseDown() {
        saved_pos = transform.position;
        battleManager.target1 = this;
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
        
        //transform.position = saved_pos;
        lineRender.SetPosition(1, Vector3.zero+Vector3.forward);
        lineRender.SetPosition(0, Vector3.zero+Vector3.forward); 
             
    }

    void OnMouseEnter() {
        targetSelected = true;
    }

    void OnMouseExit() {
        targetSelected = false;
            
    }


}
