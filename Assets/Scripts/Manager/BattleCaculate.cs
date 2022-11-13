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



    public int damage;
    public int damage_dice;

    public Player myChar;
    public Player eneChar;


    public bool card_activated;

    bool coroutine_lock = false;

    void Start(){
        players = bm.players;
    }

    public void BattleMatch(Player selfnum, Player enenum){
        
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

        damage = 0;
        myChar = selfnum;
        eneChar =  enenum;

        my_ability = myChar.cards;
        ene_ability = eneChar.cards;

        myChar.transform.position += Vector3.back;
        eneChar.transform.position += Vector3.back;

        StartCoroutine(BattleMatchcor());
        

    }

    IEnumerator BattleMatchcor(){
        
        myChar.ChangeCondition(2);    
        eneChar.ChangeCondition(2);   

        BasicDice();
        StartCoroutine("BasicAttack");
        if(!coroutine_lock){
            yield return null;
        }
        yield return new WaitForSeconds(1f); // 빠른 진행 조절 초
        StartCoroutine("MainAttack");

        while(coroutine_lock1){
            yield return null;
        }
        yield return new WaitForSeconds(1f);











        StartCoroutine(MatchFin());

        yield return null;
    }


    void BasicDice(){        
        if(myChar.dice == 1 && eneChar.dice >= 6){
            myChar.AddDice(6);
        }
        else if(eneChar.dice == 1 && myChar.dice >= 6){
            eneChar.AddDice(6);
        }
    }

    IEnumerator BasicAttack(){
        bool farAtking = false;
        try{
        farAtking = myChar.farAtt || myChar.dice_Indi.dice_list[0].farAtt || eneChar.farAtt || eneChar.dice_Indi.dice_list[0].farAtt;}
        catch{
            if(myChar.dice_Indi.dice_list.Count > 0)
                farAtking = myChar.farAtt || myChar.dice_Indi.dice_list[0].farAtt;
            else
                farAtking = myChar.farAtt;
        }
        

        if(!farAtking)
        {myChar.SetPointMove(eneChar.movePoint.position, 22f);
        gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,22f);
        
        while(myChar.isMoving){
            yield return null;
        }
        
        }

        battleDice.gameObject.SetActive(true);
        battleDice.SetPlayerPosition(myChar,eneChar);
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i<my_ability.Count;i++){
                    my_ability[i].ability.OnBattleStart(my_ability[i],this);
                }
        for(int i = 0; i<ene_ability.Count;i++){
                    ene_ability[i].ability.OnBattleStart(ene_ability[i],this);
                }
        SetDamage(myChar.dice - eneChar.dice);
        if(damage>0){
            if(myChar.gameObject.tag.Equals("PlayerTeam1")){
                battleDice.right_break.Play();
            }
            else{
                battleDice.left_break.Play();
            }
        }
        else if(damage<0){
            if(eneChar.gameObject.tag.Equals("PlayerTeam1")){
                battleDice.right_break.Play();
            }
            else{
                battleDice.left_break.Play();
            }

        }
        else if(damage.Equals(0)){
            battleDice.left_break.Play();
            battleDice.right_break.Play();
        }

        if(damage>0 && (!myChar.farAtt && !myChar.dice_Indi.dice_list[0].farAtt)){
            myChar.SetPointMove(eneChar.movePoint.position, 22f);
            gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,22f);
            
            while(myChar.isMoving){
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }

        if(damage<0 && eneChar.dice_Indi.dice_list.Count > 0){
            if(!eneChar.farAtt && !eneChar.dice_Indi.dice_list[0].farAtt){
                eneChar.SetPointMove(myChar.movePoint.position, 22f);
                gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,22f);
                
                while(eneChar.isMoving){
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);}
        }

        Debug.Log(damage);

        yield return new WaitForSeconds(1f);
        
        
        
        
        yield return null;

        coroutine_lock = true;
        }
    IEnumerator MainAttack(){
        coroutine_lock1= true;
        

        Debug.Log(damage);
        //damage_dice = damage;

        if(damage == 0){
            while(card_activated){
                yield return null;
            }
            myChar.ChangeCondition(3);
            eneChar.ChangeCondition(3);
            coroutine_lock1= false;
        }
        if(damage>0){
            for(int i = 0; i<my_ability.Count;i++){
                my_ability[i].ability.OnBattleWin(my_ability[i],this);
                if(my_ability[i].card_active){
                    myChar.UpdateActiveStat();
                }
                while(my_ability[i].card_active){
                    yield return null;
                }
                
            }
            StartCoroutine(Damage(myChar,eneChar));
            StopCoroutine(MainAttack());
            // Damage(myChar,eneChar);
        }
        if(damage<0){
            damage = -damage;
            for(int i = 0; i<ene_ability.Count;i++){
                ene_ability[i].ability.OnBattleWin(ene_ability[i],this);
                if(ene_ability[i].card_active){
                    eneChar.UpdateActiveStat();
                }
                while(ene_ability[i].card_active){
                    yield return null;
                }
            }
            StartCoroutine(Damage(eneChar,myChar));   
            StopCoroutine(MainAttack());       
            // Damage(eneChar,myChar);
        }

        
    }

    
    
    IEnumerator MatchFin(){    
        for(int i =0;i<bm.players.Count;i++){
            if(bm.players[i].health <= 5 && bm.players[i].card_geted){
                if(bm.players[i].gameObject.tag.Equals("PlayerTeam1")){
                    bm.card_left_draw += 1;
                    bm.players[i].card_geted = false;
                }
                else{
                    bm.card_right_draw += 1;
                    bm.players[i].card_geted = false;
                }
            }
        }
        for(int i =0;i<bm.players.Count;i++){ 
            if(bm.players[i].health <= 0 && bm.players[i].died_card_geted){
                if(bm.players[i].gameObject.tag.Equals("PlayerTeam1")){
                    bm.card_left_draw += 1;
                    bm.players[i].died_card_geted = false;
                    bm.players[i].YouAreDead();
                    yield return new WaitForSeconds(1.5f);
                }
                else{
                    bm.card_right_draw += 1;
                    bm.players[i].died_card_geted = false;
                    bm.players[i].YouAreDead();
                    yield return new WaitForSeconds(1.5f);
                }
            }
        }
        foreach(Player player in bm.players){
            if(player != myChar || player != eneChar){
                player.dice_Indi.gameObject.SetActive(true);
                player.hp_Indi.gameObject.SetActive(true);
            }
        }
        bm.ui.StartCoroutine("PanoraOff");

        myChar.SetPointMove(myChar.originPoint, 15f);
        gameManager.main_camera_ctrl.SetZeroMove(17f);
            

        bm.left_cardLook_lock = false;
        bm.right_cardLook_lock = false;
        bm.ui.CardUIUpdate("Left",true);
        bm.ui.CardUIUpdate("Right",true);

        bm.blackScreen.SetActive(false);
        myChar.SetDice(0);
        myChar.ChangeCondition(0);
        eneChar.SetDice(0);
        eneChar.ChangeCondition(0);
        bm.battleing = false;
        bm.target1 = null;
        bm.target2 = null;
        myChar.transform.position += Vector3.forward;
        eneChar.transform.position += Vector3.forward;
        myChar.Battle_End();
        eneChar.Battle_End();

        battleDice.gameObject.SetActive(false);

        
        for(int i = 0; i<bm.on_battle_card_effect.Count;i++){
            bm.on_battle_card_effect[i].gameObject.SetActive(false);
            bm.on_battle_card_effect.Remove(bm.on_battle_card_effect[i]);
        }

        for(int i =0;i<myChar.cards.Count;i++){
            myChar.cards[i].ability.BattleEnded(myChar.cards[i]);
            myChar.cards[i].card_lateActive = false;
        }
        for(int i =0;i<eneChar.cards.Count;i++){
            eneChar.cards[i].ability.BattleEnded(eneChar.cards[i]);
            eneChar.cards[i].card_lateActive = false;
        }



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

        damage = 0;
        battleDice.DamageUpdate();


        if(bm.left_turn){
            bm.TurnTeam("Right");
            if(bm.right_players.FindAll(x => x.dice <= 0).Count >= bm.right_players.Count){
                bm.TurnTeam("Left");
            }
        }
        else if(bm.right_turn){
            bm.TurnTeam("Left");
            if(bm.left_players.FindAll(x => x.dice <= 0).Count >= bm.left_players.Count){
                bm.TurnTeam("Right");
            }
        }

        myChar.dice_Indi.NextDice();
        eneChar.dice_Indi.NextDice();

        yield return null;
    }

    IEnumerator Damage(Player attacker, Player defender){

        for(int i = 0; i<attacker.cards.Count; i++){
                attacker.cards[i].ability.OnDamaging(attacker.cards[i],defender, bm,damage);
                if(attacker.cards[i].card_active){
                        attacker.UpdateActiveStat();
                    }
                    while(attacker.cards[i].card_active){
                        yield return null;
                    }
            }
        for(int i = 0; i<defender.cards.Count; i++){
                defender.cards[i].ability.OnDamage(defender.cards[i],attacker,bm,damage);
                if(defender.cards[i].card_active){
                        defender.UpdateActiveStat();
                    }
                    while(defender.cards[i].card_active){
                        yield return null;
                    }
            }
         
        if(damage>0){
            defender.ChangeCondition(4);
            attacker.ChangeCondition(3);
            // foreach(CardAbility card in attacker.cards){
            //     if(card.card_triggerd){                   ///////// 카드 공격 효과
            //         card.AttackEffect(transform);
            //     }
            // }
            if(attacker.transform.position.x - defender.transform.position.x <0){
                defender.transform.Translate(Vector3.right*damage/2);
            }
            if(attacker.transform.position.x - defender.transform.position.x > 0){
                defender.transform.Translate(Vector3.left*damage/2);
            }
            
        }
        else if(damage.Equals(0)){
            defender.ChangeCondition(3);
            attacker.ChangeCondition(3);
        }

        defender.health -= damage;
        attacker.AttackEffect(defender);
        defender.UpdateHp();
        foreach(Player player in bm.players){
            for(int i = 0;i<player.cards.Count;i++){
                player.cards[i].ability.WhoEverDamage(player.cards[i],damage);
            }
        }

        coroutine_lock1 = false;
        yield return null;
    }

    public void SetDamage(int value){
        damage = value;
        battleDice.DamageUpdate();
    }

    public void AddDamage(int value){
        damage += value;
        battleDice.DamageUpdate();
    }




}
