using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public GameManager gameManager;
    public BattleManager bm;
    public List<Player> players = new List<Player>();
    public BattleDice battleDice;

    public List<CardPack> my_ability = new List<CardPack>();
    public List<CardPack> ene_ability = new List<CardPack>();

    [Space(15f), Header ("WaitTime")]
    [SerializeField][Range(0f,1f)] float diceRollTime;
    //[SerializeField] float battleBeforeTime;
    [SerializeField][Range(0f,1f)] float afterBeforeTime;
    [SerializeField][Range(0f,2f)] float battleFinTime;
    [SerializeField][Range(0f,2f)] float playerDeathTime;
    [SerializeField][Range(0f,1f)] float cardTime;
    [Space (5f)]
    [SerializeField][Range(0f,100f)] float playerMoveSpd;
    [Space (10f)]


    public Damage damage = new Damage();
    public int damage_dice;

    public Player myChar;
    public Player eneChar;

    public int ones_power = 1;


    public bool card_activated;

    bool coroutine_lock = true;

    void Start(){
        players = bm.players;
        coroutine_lock = true;
    }

    public void BattleMatch(Player selfnum, Player enenum){
        bm.sdm.Play("Fire1");
        foreach(Player player in players){
            if(player != selfnum && player != enenum){
                player.dice_Indi.gameObject.SetActive(false);
                player.hp_Indi.gameObject.SetActive(false);
            }
        }
        bm.ui.StartCoroutine("PanoraOn");

        selfnum.ShowCardDeck(false,true); 
        enenum.ShowCardDeck(false,true); 
        
        bm.left_cardLook_lock = true;
        bm.right_cardLook_lock = true;

        myChar = selfnum;
        eneChar =  enenum;

        my_ability = myChar.cards;
        ene_ability = eneChar.cards;

        myChar.StopAllCoroutines();
        myChar.transform.position = bm.gameManager.SetVector3z(myChar.transform.position,-1); 
        eneChar.StopAllCoroutines();
        eneChar.transform.position = bm.gameManager.SetVector3z(eneChar.transform.position,-1); 

        StartCoroutine(BattleMatchcor());
        

    }

    IEnumerator BattleMatchcor(){
        coroutine_lock = true;
        myChar.ChangeCondition(2);    
        eneChar.ChangeCondition(2);   

        BasicDice();
        StartCoroutine("BasicAttack");
        while(coroutine_lock){yield return null;}
        coroutine_lock = true;
        StartCoroutine("MainAttack");
        while(coroutine_lock){ yield return null;}
        yield return new WaitForSeconds(afterBeforeTime); // 합이 끝나고 난 뒤의 딜레이

        StartCoroutine(MatchFin());

        yield return null;
    }


    void BasicDice(){        
        if(myChar.dice == ones_power && eneChar.dice >= 6){
            myChar.AddDice(6);
        }
        else if(eneChar.dice == ones_power && myChar.dice >= 6){
            eneChar.AddDice(6);
        }
    }

    IEnumerator BasicAttack(){
        bool farAtking = false;
        try{ // 캐릭터가 원거리? 아님 주사위 속성이 원거리
        farAtking = myChar.farAtt || myChar.dice_Indi.dice_list[0].farAtt || eneChar.farAtt || eneChar.dice_Indi.dice_list[0].farAtt;}
        catch{
            if(myChar.dice_Indi.dice_list.Count > 0)
                farAtking = myChar.farAtt || myChar.dice_Indi.dice_list[0].farAtt;
            else
                farAtking = myChar.farAtt;
        }
        

        if(!farAtking)
        {myChar.SetPointMove(eneChar.movePoint.position, playerMoveSpd);
        gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,playerMoveSpd);
        
        while(myChar.isMoving){
            yield return null;
        }
        
        }

        battleDice.gameObject.SetActive(true);
        battleDice.SetPlayerPosition(myChar,eneChar);

        bm.CardLogText("Start","[Clash Start "+myChar.character.name +"->"+eneChar.character.name+"]","#00ff08");
        for(int i = 0; i<my_ability.Count;i++){ // 합 시작시 카드 효과
                    my_ability[i].ability.OnClashStart(my_ability[i],this,eneChar);
                }
        for(int i = 0; i<ene_ability.Count;i++){
                    ene_ability[i].ability.OnClashStart(ene_ability[i],this,myChar);
                }
        yield return new WaitForSeconds(diceRollTime); // 캐릭터들이 제자리에 온후 약간의 딜레이

        
        # region 데미지 결정됨
        SetDamage(myChar.dice - eneChar.dice);
        if(damage.value>0){    // 데미지 결정
            ParticleSystem particle = (myChar.gameObject.tag.Equals("PlayerTeam1")) ? battleDice.right_break : battleDice.left_break;
            particle.Play();
        }
        else if(damage.value<0){
            ParticleSystem particle = (eneChar.gameObject.tag.Equals("PlayerTeam1")) ? battleDice.right_break : battleDice.left_break;
            particle.Play();

        }
        else if(damage.Equals(0)){ // 같으면 둘다 터짐
            battleDice.left_break.Play();
            battleDice.right_break.Play();
        }
        # endregion 
        
        if(farAtking){
        # region 자신이 합을이기고 원거리 공격이 아니면 다아가기
        if(damage.value>0 && (!myChar.farAtt && !myChar.dice_Indi.dice_list[0].farAtt)){
            myChar.SetPointMove(eneChar.movePoint.position, 22f);
            gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,22f);
            
            while(myChar.isMoving){
                yield return null;
            }
            yield return new WaitForSeconds(battleFinTime);
        }
        # endregion
        # region 상대가 합을 이기고 원거리 공격을 가진게 아니면 자신에게 다아가기
        if(damage.value<0 && eneChar.dice_Indi.dice_list.Count > 0){
            if(!eneChar.farAtt && !eneChar.dice_Indi.dice_list[0].farAtt){
                eneChar.SetPointMove(myChar.movePoint.position, 22f);
                gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,22f);
                
                while(eneChar.isMoving){
                    yield return null;
                }
                yield return new WaitForSeconds(battleFinTime);
                }
        }
        # endregion
        }
        else{yield return new WaitForSeconds(battleFinTime);}
        
        

        coroutine_lock = false;
        yield return null;
        
        }
    IEnumerator MainAttack(){
        
        //damage_dice = damage;

        if(damage.value == 0){ // 무승부
            // while(card_activated){
            //     yield return null;
            // }
            bm.CardLogText("Draw","[Draw]","#969696");
            for(int i = 0; i<my_ability.Count;i++){
                my_ability[i].ability.OnClashDraw(my_ability[i],this,eneChar);
            }
            for(int i = 0; i<ene_ability.Count;i++){
                ene_ability[i].ability.OnClashDraw(ene_ability[i],this,myChar);
            }
            StartCoroutine(Damage(myChar,eneChar));
            yield return null;
        }
        if(damage.value>0){ // 승리
            bm.CardLogText((myChar.team.Equals(bm.left_team)) ? "Win":"Lose","[Win "+myChar.character.name+"/Lose "+eneChar.character.name+"]","#ffffff");
            for(int i = 0; i<ene_ability.Count;i++){
                ene_ability[i].ability.OnClashLose(ene_ability[i],this);
            }
            for(int i = 0; i<my_ability.Count;i++){
                my_ability[i].ability.OnClashWin(my_ability[i],this);
                BasicDice();
                if(my_ability[i].card_battleActive){
                    myChar.UpdateActiveStat();
                }
                while(my_ability[i].card_battleActive){
                    yield return null;
                    
                }
                
            }
            StartCoroutine(Damage(myChar,eneChar));
            yield return null;
            // Damage(myChar,eneChar);
        }
        if(damage.value<0){ // 패배
        bm.CardLogText((myChar.team.Equals(bm.left_team)) ? "Win":"Lose","[Win "+eneChar.character.name+"/Lose "+myChar.character.name+"]","#ffffff");
            damage.value = -damage.value;
            for(int i = 0; i<my_ability.Count;i++){
                my_ability[i].ability.OnClashLose(my_ability[i],this);
            }
            for(int i = 0; i<ene_ability.Count;i++){
                ene_ability[i].ability.OnClashWin(ene_ability[i],this);
                BasicDice();
                if(ene_ability[i].card_battleActive){
                    eneChar.UpdateActiveStat();
                }
                while(ene_ability[i].card_battleActive){
                    yield return null;
                }
            }
            StartCoroutine(Damage(eneChar,myChar));   
            yield return null;       
            // Damage(eneChar,myChar);
        }



        
    }

    
    
    IEnumerator MatchFin(){    
        if(myChar.health <= 0){
            myChar.YouAreDead();
            yield return new WaitForSeconds(playerDeathTime);
        }
        if(eneChar.health <= 0){
            eneChar.YouAreDead();
            yield return new WaitForSeconds(playerDeathTime);
        }




        foreach(Player player in bm.players){
            if(player != myChar || player != eneChar){
                player.dice_Indi.gameObject.SetActive(true);
                player.hp_Indi.gameObject.SetActive(true);
                player.dice_Indi.onMouseDown = false;
                player.dice_Indi.onMouseEnter = false;
            }
        }
        bm.ui.StartCoroutine("PanoraOff");

        myChar.SetPointMove(myChar.originPoint, 15f);
        eneChar.SetPointMove(eneChar.originPoint, 12f);
        gameManager.main_camera_ctrl.SetZeroMove(17f);
            
        # region battleVarReset
        bm.left_cardLook_lock = false;
        bm.right_cardLook_lock = false;
        bm.ui.CardFold("Left",true);
        bm.ui.CardFold("Right",true);

        ones_power = 1;
        
        bm.target1 = null;
        bm.target2 = null;
        myChar.transform.position = bm.gameManager.SetVector3z(myChar.transform.position,0);
        eneChar.transform.position = bm.gameManager.SetVector3z(eneChar.transform.position,0);
        myChar.Battle_End();
        eneChar.Battle_End();

        battleDice.gameObject.SetActive(false);
        # endregion

        bm.CardLogText("End","[Clash End]","#009105");

        
        for(int i = 0; i<bm.on_battle_card_effect.Count;i++){
            bm.on_battle_card_effect[i].gameObject.SetActive(false);
            bm.on_battle_card_effect.Remove(bm.on_battle_card_effect[i]);
        }
        # region BattleEnded
        for(int i =0;i<myChar.cards.Count;i++){
            myChar.cards[i].card_battleActive = false;
            myChar.cards[i].ability.ClashEnded(myChar.cards[i],this);
            
        }
        for(int i =0;i<eneChar.cards.Count;i++){
            eneChar.cards[i].card_battleActive = false;
            eneChar.cards[i].ability.ClashEnded(eneChar.cards[i],this);
            
        }
        #endregion


        
        if(bm.cardViewChar_left != null){
            bm.cardViewChar_left.player_floor_render.SetInt("_Active",1); 
            bm.cardViewChar_left.ShowCardDeck(true,true);
            bm.left_cardLook_lock = true;
        }
        if(bm.cardViewChar_right != null){
            bm.cardViewChar_right.player_floor_render.SetInt("_Active",1); 
            bm.cardViewChar_right.ShowCardDeck(true,true);
            bm.left_cardLook_lock = true;
        }

        battleDice.DamageUpdate();

        bm.blackScreen.SetActive(false);
        myChar.SetDice(0);
        myChar.ChangeCondition(0);
        eneChar.SetDice(0);
        eneChar.ChangeCondition(0);

        bm.CheckNextTeam();

        myChar.dice_Indi.NextDice();
        eneChar.dice_Indi.NextDice();

        bm.battleing = false;
        yield return null;
    }

    IEnumerator Damage(Player attacker, Player defender){

        // for(int i = 0; i<attacker.cards.Count; i++){
        //         attacker.cards[i].ability.OnDamaging(attacker.cards[i],defender,damage, bm);
        //         if(attacker.cards[i].card_battleActive){
        //                 attacker.UpdateActiveStat();
        //             }
        //             while(attacker.cards[i].card_battleActive){
        //                 yield return null;
        //             }
        //     }
        // for(int i = 0; i<defender.cards.Count; i++){
        //         defender.cards[i].ability.OnDamage(defender.cards[i],attacker,damage,bm);
        //         if(defender.cards[i].card_battleActive){
        //                 defender.UpdateActiveStat();
        //             }
        //             while(defender.cards[i].card_battleActive){
        //                 yield return null;
        //             }
        //     }
        if(damage.value>0){
            defender.ChangeCondition(4);
            attacker.ChangeCondition(3);
            // foreach(CardAbility card in attacker.cards){
            //     if(card.card_triggerd){                   ///////// 카드 공격 효과
            //         card.AttackEffect(transform);
            //     }
            // }
            if(attacker.transform.position.x - defender.transform.position.x <0){defender.transform.Translate(Vector3.right*damage.value/2);}
            if(attacker.transform.position.x - defender.transform.position.x > 0){defender.transform.Translate(Vector3.left*damage.value/2);}

            defender.DamagedBy(damage,attacker,attacker.character.atk_sound);
            
        }
        if(damage.value.Equals(0)){
            defender.ChangeCondition(3);
            attacker.ChangeCondition(3);

            if(attacker.transform.position.x - defender.transform.position.x <0){defender.transform.Translate(Vector3.right*0.4f);}
            if(attacker.transform.position.x - defender.transform.position.x > 0){defender.transform.Translate(Vector3.left*0.4f);}
            if(defender.transform.position.x - attacker.transform.position.x <0){attacker.transform.Translate(Vector3.right*0.4f);}
            if(defender.transform.position.x - attacker.transform.position.x > 0){attacker.transform.Translate(Vector3.left*0.4f);}
            bm.sdm.Play("Pery");
        }

        
        attacker.AttackEffect(defender);
        coroutine_lock = false;
        yield return null;
    }

    public void SetDamage(int value){
        damage.value = value;
        battleDice.DamageUpdate();
    }

    public void AddDamage(int value){
        damage.value += value;
        battleDice.DamageUpdate();
    }

}
