using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    BattleManager battleManager;
    [SerializeField]
    Dice_Indi dice_Indi;

    
    public Transform movePoint;
    public int player_id = 0;
    public int condition = 0;
    public int health;
    public bool card_geted = true;
    public int dice;
    public bool died;
    public List<CardAbility> cards = new List<CardAbility>();
    public Sprite[] poses;
    SpriteRenderer render;


    bool isMoving;
    Vector3 moveTarget;
    float moveSpeed;

    Vector3 origin_pos;

    [HideInInspector] public bool goto_origin;

    void Start()
    { 
        origin_pos = transform.position;
        render = GetComponent<SpriteRenderer>();
        card_geted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            transform.position = Vector3.MoveTowards(transform.position,moveTarget,moveSpeed * Time.deltaTime);
            //transform.Translate(transform.position*Time.deltaTime);   // //////////002 목표로 이동하는것에는 속도에 델타타임을 곱해야 한다.//
            if(Vector3.Distance(transform.position,moveTarget) < 0.001f){
                isMoving = false;
            }
        }
        if(goto_origin){
            if(transform.position == origin_pos){
                goto_origin = false;
            }
            else{
                transform.position = Vector3.MoveTowards(transform.position,origin_pos,14f * Time.deltaTime);
                if(Vector3.Distance(transform.position,origin_pos) < 0.001f){
                    goto_origin = false;
                    transform.position = origin_pos;
                }
            }
            
        }
    }

    public void SetPointMove(Vector3 point, float spd){
        moveTarget = point;
        moveSpeed = spd;
        isMoving = true;
    }

    public void SetDice(int value){
        dice_Indi.setDice(value);
    }

    public void AddDice(int value){
        dice_Indi.setDice(dice + value);
    }

    public void Damage(int value, Player attacker){
        if(value>0){
            if(attacker.transform.position.x - transform.position.x <0){
                transform.Translate(Vector3.right*value/2);
            }
            if(attacker.transform.position.x - transform.position.x > 0){
                transform.Translate(Vector3.left*value/2);
            }
            
        }
        health -= value;

    }

    public void YouAreDead(){
        died = true;
        gameObject.SetActive(false);
    }

    public void ChangeCondition(int num){
        render.sprite = poses[num];
    }

    void OnMouseDown() {
        if(gameObject.tag == "PlayerTeam1"){
            battleManager.ui.showleftCardamount = cards.Count;
            battleManager.ui.Leftcard_Update(cards);
            battleManager.ui.showleftCard = true;
            battleManager.cardViewChar_left = player_id-1;
        }
        
        if(gameObject.tag == "PlayerTeam2"){
            battleManager.ui.showrightCardamount = cards.Count;
            battleManager.ui.Rightcard_Update(cards);
            battleManager.ui.showrightCard = true;
            battleManager.cardViewChar_right = player_id-1;
        }
    }

}
