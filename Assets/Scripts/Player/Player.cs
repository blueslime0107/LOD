using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class Player : MonoBehaviour
{
    public Character character;
    public Team team;
    [SerializeField]public BattleManager battleManager;
    public Dice_Indi dice_Indi;
    [SerializeField]public Hp_Indi hp_Indi;
    [SerializeField]TextMeshPro textMeshPro; 
    Material material;
    float fade = 0.7f;
    
    public Transform movePoint;
    public Transform attPoint;
    public Vector3 originPoint;
    public GameObject cardGet;
    [HideInInspector]public int condition = 0;
    public int max_health;
    public int health;
    public Player lastHit;
    public List<int> breakCount = new List<int>();
    public int dice;
    public GameObject dice_obj;
    public Dice dice_com;
    public bool died;
    public GameObject player_floor;
    public List<CardAbility> pre_cards = new List<CardAbility>();
    public List<CardPack> cards = new List<CardPack>();
    public Sprite[] poses;
    public bool farAtt;
    SpriteRenderer render;
    
    public Material player_floor_render;

    bool card_actived;

    //List<card_text> player_deck;

    [HideInInspector] public bool isMoving;
    Vector3 moveTarget;
    float moveSpeed;

    [HideInInspector] public List<GameObject> card_effect = new List<GameObject>();

    Vector3 origin_pos;

    public CardEffect atkEffect;
    public bool special_active;
    public SpecialAtk specialAtk;

    public Animator deathMotion;

    public bool THEEGO;

    void Awake()
    {   
        dice_obj = Instantiate(dice_obj);
        dice_com = dice_obj.GetComponent<Dice>();
        dice_obj.tag = gameObject.tag.Substring(6);
        dice_com.battleManager = battleManager;
        dice_com.player = this;
        battleManager.players.Add(this);
        battleManager.dice_indis.Add(gameObject.transform.GetComponentInChildren<Dice_Indi>());
        battleManager.dices.Add(dice_com);
        player_floor_render = player_floor.GetComponent<SpriteRenderer>().material;
        material = GetComponent<SpriteRenderer>().material;
        origin_pos = transform.position;
        render = GetComponent<SpriteRenderer>();
        max_health = health;
        originPoint = transform.position;
        
    }

    public int speed;

    private void OnDisable() {
        battleManager.players.Remove(this);
        team.players.Remove(this);   
    }

    void Start(){
        // 자세를 기본자세로 하고 초기카드가 있으면 카드들을 주고 시작
        if(gameObject.tag.Equals("PlayerTeam2")){
            hp_Indi.HPObj.transform.Translate(Vector3.right*1.2f);
            hp_Indi.BreakObj.transform.Translate(Vector3.right*1.2f);
        }
        hp_Indi.BreakObj.SetActive(!battleManager.gameManager.sm.play_stage.noBreakCards);
        render.sprite = poses[0];
        if(pre_cards.Count > 0){
            foreach(CardAbility card in pre_cards.FindAll(x => x != null)){
                battleManager.GiveCard(card,this,true);
            }
        }
        textMeshPro.text = character.char_sprites.name;
        UpdateHp();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(died){
    //         fade -= Time.deltaTime/2;
    //         if(fade <= 0f){
    //             fade = 0f;
    //             gameObject.SetActive(false);
    //         }
    //         material.SetFloat("_Fade",fade);
    //         material.SetInt("_Noise",1);
    //     }
    
    // }


    bool goingOrigin = false;

    public IEnumerator GotoOrigin(){
        goingOrigin = true;
        StopCoroutine("KnockBackEnumerator");
        StopCoroutine("GotoPoint");
        isMoving = false;
        while(true)
        {if(transform.position == origin_pos){
            break;
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position,origin_pos,14f * Time.deltaTime);
            if(Vector3.Distance(transform.position,origin_pos) < 0.001f){
                transform.position = origin_pos;
                break;
            }
        }
        yield return null;
        }
        goingOrigin = false;
    }
    public IEnumerator GotoPoint(){
        goingOrigin = false;
        StopCoroutine("GotoOrigin");
        isMoving = true;
        while(true){
            if(goingOrigin){yield return null; break;}
            Debug.Log("moving");
            transform.position = Vector3.MoveTowards(transform.position,moveTarget,moveSpeed * Time.deltaTime);
            //transform.Translate(transform.position*Time.deltaTime);   // //////////002 목표로 이동하는것에는 속도에 델타타임을 곱해야 한다.//
            if(Vector3.Distance(transform.position,moveTarget) < 0.001f){
                isMoving = false;
                break;
            }
        yield return null;
        }
        Debug.Log("end");
    }
    public void SetPointMove(Vector3 point, float spd){
        if(!gameObject.activeSelf){return;}
        moveTarget = point;
        moveSpeed = spd;
        isMoving = true;
        StartCoroutine("GotoPoint");
    }


    public void SetDice(int value){
        if(dice <= 0){return;}
        dice_Indi.setDice(value);
    }

    public void AddDice(int value){
        if(dice <= 0){return;}
        int diceValue = 0;
        diceValue = dice + value;
        if(diceValue < 0){
            diceValue = 0;
        }
        dice_Indi.setDice(diceValue);
    }

    public void DamagedBy(Damage damage, Player player,string atk_sound=""){

        lastHit = player;

        if(atk_sound == ""){
            battleManager.sdm.Play((player.farAtt) ? "Gun1":"Slash1");
        }
        else{
            battleManager.sdm.Play(atk_sound);
        }
        
        for(int i = 0;i<cards.Count;i++){
                cards[i].ability.OnDamage(cards[i],lastHit,damage,battleManager);
            }
        for(int i = 0;i<lastHit.cards.Count;i++){
                lastHit.cards[i].ability.OnDamaging(lastHit.cards[i],this,damage,battleManager);
            }
        foreach(Player play in battleManager.players){
            for(int i = 0;i<play.cards.Count;i++){
                play.cards[i].ability.WhoEverDamage(play.cards[i],damage,battleManager,lastHit,this);
            }
        }

        ChangeCondition(4);
        transform.position = battleManager.gameManager.SetVector3z(transform.position,-1); 
        hp_Indi.gameObject.SetActive(true);
        battleManager.numberManager.IndicateDam(transform,damage.value);
        health -= damage.value;
        
        KnockBack(damage.value*((player.transform.position.x > transform.position.x) ? -1:1));

        if(health > max_health){
            health = max_health;
        }
        else if(health<=0){
            YouAreDead();
        }
        UpdateHp();
    }

    public void NewDamagedByInt(int damage, Player player,string atk_sound=""){
        Damage newdamage = new Damage();
        newdamage.value = damage;
        DamagedBy(newdamage,player,atk_sound);

    }

    public void DamagedByInt(int damage, Player player,Damage origin_dmg, CardPack cardAbility,string atk_sound=""){
        Damage newdamage = new Damage();
        newdamage.value = damage;
        DamagedBy(newdamage,player,atk_sound);

    }

    public void AddHealth(int value,bool changemaxHp=false){
        if(changemaxHp){
            Debug.Log("max_changed");
            max_health += value;
        }
        health += value;
        if(value < 0)
        battleManager.numberManager.IndicateDam(transform, -value);
        else
        battleManager.numberManager.IndicateHeal(transform, value);
        if(health > max_health){
            health = max_health;
        }
        else if(health<=0){
            YouAreDead();
        }
        UpdateHp();
    }

    public void SetHealth(int value,bool changemaxHp=false){
        if(changemaxHp){
            max_health = value;
        }
        if(health - value > 0){
            battleManager.numberManager.IndicateDam(transform, health-value);
        }
        if(health - value < 0){
            battleManager.numberManager.IndicateHeal(transform, Mathf.Abs(health-value));
        }
        health = value;
        if(health > max_health){
            health = max_health;
        }
        else if(health<=0){
            YouAreDead();
        }
        UpdateHp();
    }

    public void AddBreak(int value){
        breakCount[0] += value;
        if(health <= breakCount[0]){
                battleManager.AddCardPoint(team);
                battleManager.sdm.Play("BreakCardGet");
                hp_Indi.ActiveEff();
                breakCount.RemoveAt(0);
            }
        else{
            for(int i = 1;i<breakCount.Count;i++){
            if(breakCount[0] <= breakCount[1]){
                breakCount.RemoveAt(1);
            }
            }
        }
    }

    public void UpdateHp(){
        if(health > max_health){health = max_health;}
        hp_Indi.HpUpdate(this);
        if(breakCount.Count <= 0 || battleManager.gameManager.sm.play_stage.noBreakCards){return;}
        for(int i = 0;i<breakCount.Count;i++){
            if(health <= breakCount[0]){
                battleManager.AddCardPoint(team);
                battleManager.sdm.Play("BreakCardGet");
                hp_Indi.ActiveEff();
                breakCount.RemoveAt(0);
            }
        }
        if(health > max_health){
            health = max_health;
        }
        hp_Indi.HpUpdate(this);
    }

    public void YouAreDead(){
        if(died){return;}
        UpdateHp();
        battleManager.sdm.Play("CharDie");
        for(int i =0;i<cards.Count;i++){
                    cards[i].ability.OnDeath(cards[i],this,battleManager);
                }
        foreach(Player player in battleManager.players){
            if(player.Equals(this)){continue;}
            for(int i =0;i<player.cards.Count;i++){
                    player.cards[i].ability.OnDeath(player.cards[i],this,battleManager);
                }
        }
        if(health > 0){
            UpdateHp();
            return;
        }
        dice_com.cannot_roll = true;
        died = true;
        
        if(THEEGO){StartCoroutine(EGODeath());}
        else{StartCoroutine(NormalDeath());}
        SetDice(0);
    }

    IEnumerator NormalDeath(){
        deathMotion.SetBool("NormalDeath",true);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    IEnumerator EGODeath(){
        deathMotion.SetBool("EGODeath",true);
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }

    public void ChangeCondition(int num){
        render.sprite = poses[num];
    }

    public void KnockBack(float force){
        Vector3 targetVec = transform.position + Vector3.right*force;
        StartCoroutine(KnockBackEnumerator(targetVec));
    }

    public IEnumerator KnockBackEnumerator(Vector3 targetVec){
        while (Vector3.Distance(transform.position,targetVec) > 0.01f)
        {
            if(goingOrigin){yield return null; break;}
            if(transform.position.x < -battleManager.borderX || transform.position.x > battleManager.borderX){
                yield return null; break;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetVec, 50*Time.deltaTime);
            
            yield return null;
        }
        // int dir = direction;
        // while(force > 0){
        //     yield return null;
        //     if(dir.Equals(1)){
        //         if(transform.position.x > battleManager.borderX){
        //             dir = - dir;
        //             transform.position.Set(transform.position.x,battleManager.borderX,transform.position.z);
        //             continue;
        //         }
        //         transform.Translate(Vector3.right*force);

        //     }
        //     if(dir.Equals(-1)){
        //         if(transform.position.x < -battleManager.borderX){
        //             dir = - dir;
        //             transform.position.Set(transform.position.x,-battleManager.borderX,transform.position.z);
        //             continue;
        //         }
        //         transform.Translate(Vector3.left*force);

        //     }

        //     force -= 50*Time.deltaTime;
        // }
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
                return;
                
            if (update ){ battleManager.cardViewChar_left = this;}
            battleManager.ui.CardReload("Left");
            battleManager.render_cardViewChar_left = this;
            battleManager.ui.leftCard_card = cards;
            battleManager.ui.CardFold("Left");
            battleManager.ui.showleftCard = true;
            //player_floor_render.sp

        }
        
        if(gameObject.tag == "PlayerTeam2"){
            
            if(battleManager.right_cardLook_lock && !nah)
                return;
            
            if (update ){ battleManager.cardViewChar_right = this;}
            battleManager.ui.CardReload("Right");
            battleManager.render_cardViewChar_right = this;
            battleManager.ui.rightCard_card = cards;
            battleManager.ui.CardFold("Right");
            battleManager.ui.showrightCard = true;
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
    }
    
    public string GetCharName(){
        return character.char_sprites.name_;
    }

}
