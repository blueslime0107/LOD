using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    BattleManager battleManager;
    public Dice_Indi dice;
    public List<Dice_Indi> dice_s;
    [SerializeField]
    Hp_Indi hp_Indi;

    
    public Transform movePoint;
    public int player_id = 0;
    public int condition = 0;
    public int max_health;
    public int health;
    public bool card_geted = true;
    public bool died_card_geted = true;
    //public int dice;
    public bool died;
    public List<CardAbility> cards = new List<CardAbility>();
    public Sprite[] poses;
    SpriteRenderer render;

    List<card_text> player_deck;

    public bool isMoving;
    Vector3 moveTarget;
    float moveSpeed;

    Vector3 origin_pos;

    [HideInInspector] public bool goto_origin;

    void Start()
    { 
        origin_pos = transform.position;
        render = GetComponent<SpriteRenderer>();
        if(gameObject.tag == "PlayerTeam1"){
            player_deck = battleManager.ui.leftCardIndi_compo;
        }
        else if(gameObject.tag == "PlayerTeam2"){
            player_deck = battleManager.ui.rightCardIndi_compo;
        }
        card_geted = true;
        max_health = health;
        UpdateHp();
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
        foreach(Dice_Indi die in dice_s){
            die.setDice(value);
        }
        
    }

    public void AddDice(int value){
        foreach(Dice_Indi die in dice_s){
            die.setDice(die.value + value);
        }
    }

    public void Damage(int value, Player attacker){
        Debug.Log("Player_Attacking");
        if(value>0){
            ChangeCondition(4);
            attacker.ChangeCondition(3);
            if(attacker.transform.position.x - transform.position.x <0){
                transform.Translate(Vector3.right*value/2);
            }
            if(attacker.transform.position.x - transform.position.x > 0){
                transform.Translate(Vector3.left*value/2);
            }
            
        }
        else if(value.Equals(0)){
            ChangeCondition(3);
            attacker.ChangeCondition(3);
        }
        health -= value;
        UpdateHp();

    }

    void UpdateHp(){
        hp_Indi.HpUpdate(health,max_health);
    }

    public void YouAreDead(){
        died = true;
        foreach(Dice_Indi die in dice_s){
            die.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void ChangeCondition(int num){
        Debug.Log("ChangeCondition");
        render.sprite = poses[num];
    }

    public void OnMouseDown() {
        if(gameObject.tag == "PlayerTeam1"){
            
            battleManager.ui.leftCard_card = cards;
            battleManager.ui.Leftcard_Update();
            battleManager.ui.showleftCard = true;

        }
        
        if(gameObject.tag == "PlayerTeam2"){
            battleManager.ui.rightCard_card = cards;
            battleManager.ui.Rightcard_Update();
            battleManager.ui.showrightCard = true;
        }
    }

    public void UpdateActiveStat(){
        for(int i = 0;i<cards.Count;i++){
            if(cards[i].card_active){
                player_deck[i].StartCoroutine("CardActivated");
                battleManager.battleCaculate.card_activated = true;
                cards[i].card_active = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(!battleManager.battleing){
            if(collider.gameObject.name.Equals("WallLeft") || collider.gameObject.name.Equals("WallRight")){
                goto_origin = true;
            }
        }

    }

}
