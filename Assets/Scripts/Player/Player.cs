using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField]
    BattleManager battleManager;
    public Dice_Indi dice_Indi;
    [SerializeField]
    public Hp_Indi hp_Indi;
    Material material;
    float fade = 0.7f;
    
    public Transform movePoint;
    public Transform attPoint;
    public Vector3 originPoint;
    [HideInInspector]public int condition = 0;
    public int max_health;
    public int health;
    [HideInInspector]public bool card_geted = true;
    [HideInInspector]public bool died_card_geted = true;
    public int dice;
    public GameObject dice_obj;
    Dice dice_com;
    public bool died;
    public GameObject player_floor;
    public List<CardAbility> pre_cards = new List<CardAbility>();
    public List<CardPack> cards = new List<CardPack>();
    public Sprite[] poses;
    public bool farAtt;
    SpriteRenderer render;
    
    public Material player_floor_render;

    bool card_actived;

    List<card_text> player_deck;

    [HideInInspector] public bool isMoving;
    Vector3 moveTarget;
    float moveSpeed;

    [HideInInspector] public List<GameObject> card_effect = new List<GameObject>();
    public List<AttEffect> attack_effect = new List<AttEffect>();

    Vector3 origin_pos;

    [HideInInspector] public bool goto_origin;

    void Awake()
    {   
        dice_obj = Instantiate(dice_obj);
        dice_com = dice_obj.GetComponent<Dice>();
        dice_obj.tag = gameObject.tag.Substring(6);
        dice_com.battleManager = battleManager;
        battleManager.players.Add(this);
        battleManager.dice_indis.Add(gameObject.transform.GetComponentInChildren<Dice_Indi>());
        battleManager.dices.Add(dice_com);
        player_floor = gameObject.transform.GetChild(4).gameObject;
        player_floor_render = player_floor.GetComponent<SpriteRenderer>().material;
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
        originPoint = transform.position;
    }

    public int speed;

    void Start(){
        if(pre_cards.Count > 0){
            foreach(CardAbility card in pre_cards){
                battleManager.GiveCard(card,this);
            }
        }
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


    public void Damage(int value, Player attacker){
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
        foreach(Player player in battleManager.players){
            if(player.gameObject.tag.Equals(gameObject.tag) && !player.Equals(this)){
                for(int i =0;i<player.cards.Count;i++){
                    player.cards[i].ability.OnDeathOurTeam(player.cards[i]);
                }
            }
            if(player.Equals(this)){
                for(int i =0;i<player.cards.Count;i++){
                    player.cards[i].ability.OnDeath(player.cards[i],battleManager);
                }
            }
            if(!player.gameObject.tag.Equals(gameObject.tag) && !player.Equals(this)){
                for(int i =0;i<player.cards.Count;i++){
                    player.cards[i].ability.OnDeathEneTeam(player.cards[i]);
                }
            }
        
        
        }

        dice_com.cannot_roll = true;
        died = true;
        SetDice(0);
    }

    public void ChangeCondition(int num){
        render.sprite = poses[num];
    }






        // if(gameObject.tag.Equals("PlayerTeam2")){
        //     battleManager.right_cardLook_lock = !battleManager.right_cardLook_lock;
        // }
        // ShowCardDeck(true);
        // if(gameObject.tag.Equals("PlayerTeam1")){
        //     if(!battleManager.left_cardLook_lock)
        //     player_floor_render.SetInt("_Active",0);
        // }
        // if(gameObject.tag.Equals("PlayerTeam2")){
        //     if(!battleManager.right_cardLook_lock)
        //     player_floor_render.SetInt("_Active",0);
        // }
    

    public void ShowCardDeck(bool update,bool nah=false){

        if(gameObject.tag == "PlayerTeam1"){
            if(battleManager.left_cardLook_lock && !nah)
                {Debug.Log("help");
                return;}
            if (update ){ battleManager.cardViewChar_left = this;}
            battleManager.render_cardViewChar_left = this;
            battleManager.ui.leftCard_card = cards;
            battleManager.ui.Leftcard_Update();
            battleManager.ui.showleftCard = true;
            //player_floor_render.sp

        }
        
        if(gameObject.tag == "PlayerTeam2"){
            
            if(battleManager.right_cardLook_lock && !nah)
                {Debug.Log("me");
                return;}
            if (update ){ battleManager.cardViewChar_right = this;}
            battleManager.render_cardViewChar_right = this;
            battleManager.ui.rightCard_card = cards;
            Debug.Log(battleManager.ui.rightCard_card );
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
        if(collider.gameObject.tag.Equals("LeftBorder")){
            SetPointMove(transform.position+Vector3.right,15f);
        }
        if(collider.gameObject.tag.Equals("RightBorder")){
            SetPointMove(transform.position+Vector3.left,15f);
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
        foreach(CardPack card in cards){
            card.ability.AttackEffect(card,defender);
        }
        foreach(AttEffect effect in attack_effect){
            if(effect.player_set){
                effect.gameObject.transform.position = transform.position;
            }
            else{
                effect.gameObject.transform.position = defender.gameObject.transform.position;
            }
            effect.gameObject.SetActive(true);
            
            

        }

    }

    public void Battle_End(){
        foreach(AttEffect effect in attack_effect){
            effect.gameObject.SetActive(false);
        }
    }

}
