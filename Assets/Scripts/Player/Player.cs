using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    BattleManager battleManager;
    public Dice_Indi dice_Indi;
    [SerializeField]
    Hp_Indi hp_Indi;
    Material material;
    float fade = 0.7f;
    
    public Transform movePoint;
    public Transform attPoint;
    public int player_id = 0;
    public int condition = 0;
    public int max_health;
    public int health;
    public bool card_geted = true;
    public bool died_card_geted = true;
    public int dice;
    public bool died;
    public List<CardPack> cards = new List<CardPack>();
    public Sprite[] poses;
    SpriteRenderer render;

    bool card_actived;

    List<card_text> player_deck;

    public bool isMoving;
    Vector3 moveTarget;
    float moveSpeed;

    public List<GameObject> card_effect = new List<GameObject>();
    public List<GameObject> attack_effect = new List<GameObject>();

    Vector3 origin_pos;

    [HideInInspector] public bool goto_origin;

    void Start()
    { 
        material = GetComponent<SpriteRenderer>().material;
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
        if(died){
            fade -= Time.deltaTime/2;
            if(fade <= 0f){
                fade = 0f;
                gameObject.SetActive(false);
            }
            material.SetFloat("_Fade",fade);
            material.SetInt("_Noise",1);
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

    public int getDamage;


    public void Damage(int value, Player attacker){
        getDamage = value;
        battleManager.battleCaculate.damage = getDamage;
        for(int i = 0; i<attacker.cards.Count; i++){
                attacker.cards[i].ability.OnDamaging(attacker.cards[i],this,  battleManager,getDamage);
            }
        for(int i = 0; i<cards.Count; i++){
                cards[i].ability.OnDamage(cards[i],attacker,battleManager,getDamage);
            }
        battleManager.battleCaculate.damage = getDamage;
         
        if(getDamage>0){
            ChangeCondition(4);
            attacker.ChangeCondition(3);
            // foreach(CardAbility card in attacker.cards){
            //     if(card.card_triggerd){                   ///////// 카드 공격 효과
            //         card.AttackEffect(transform);
            //     }
            // }
            if(attacker.transform.position.x - transform.position.x <0){
                transform.Translate(Vector3.right*getDamage/2);
            }
            if(attacker.transform.position.x - transform.position.x > 0){
                transform.Translate(Vector3.left*getDamage/2);
            }
            
        }
        else if(getDamage.Equals(0)){
            ChangeCondition(3);
            attacker.ChangeCondition(3);
        }
        health -= getDamage;
        UpdateHp();

    }

    public void AddHealth(int value){
        health += value;
        if(health > max_health){
            health = max_health;
        }
        else if(health<=0){
            YouAreDead();
        }
        UpdateHp();
    }

    public void UpdateHp(){
        hp_Indi.HpUpdate(health,max_health);
    }

    public void YouAreDead(){
        died = true;
        
    }

    public void ChangeCondition(int num){
        render.sprite = poses[num];
    }

    public void OnMouseDown() {
        if(gameObject.tag == "PlayerTeam1"){
            battleManager.cardViewChar_left = battleManager.players.IndexOf(this);
            battleManager.ui.leftCard_card = cards;
            battleManager.ui.Leftcard_Update();
            battleManager.ui.showleftCard = true;

        }
        
        if(gameObject.tag == "PlayerTeam2"){
            battleManager.cardViewChar_right = battleManager.players.IndexOf(this);
            battleManager.ui.rightCard_card = cards;
            battleManager.ui.Rightcard_Update();
            battleManager.ui.showrightCard = true;
        }
    }

    public void UpdateActiveStat(){
        for(int i = 0;i<cards.Count;i++){
            if(cards[i].card_active){
                player_deck[i].StartCoroutine("CardActivated");
                break;
                //cards[i].ability.ImmediEffect(cards[i],transform);

                //battleManager.battleCaculate.card_activated = true;
                //cards[i].card_active = false;
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

    public int TeamVector(bool reverse = false){
        if(reverse){
        if(gameObject.tag == "PlayerTeam2"){
            return -1;
        }
        else{
            return 1;
        }            
        }
        else{
        if(gameObject.tag == "PlayerTeam1"){
            return -1;
        }
        else{
            return 1;
        }            
        }

    }

    public void AttackEffect(Player defender){
        foreach(GameObject effect in attack_effect){
            effect.SetActive(true);
            effect.transform.position = attPoint.position;
        }

    }

    public void Battle_End(){
        foreach(GameObject effect in attack_effect){
            effect.SetActive(false);
        }
    }

}
