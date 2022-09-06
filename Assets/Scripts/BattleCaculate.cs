using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public GameManager gameManager;
    public BattleManager battleManager;
    public Player[] players;
    public BattleDice battleDice;

    public List<CardPack> my_ability = new List<CardPack>();
    public List<CardPack> ene_ability = new List<CardPack>();



    public int damage;
    public int damage_dice;

    public Player myChar;
    public Player eneChar;

    Vector3 myOriginPos;

    // int myNum;
    // int eneNum;

    public bool card_activated;

    bool coroutine_lock1 = false;

    public void BattleMatch(Player selfnum, Player enenum){
        
        foreach(Player player in players){
            if(player != selfnum && player != enenum){
                player.dice_Indi.render.color = new Color(0,0,0,1);
            }
        }
        // for(int i=0;i<6;i++){
        //     if(i != selfnum.player_id-1 && i != enenum.player_id-1){
                
        //     }
        // }

        // myNum = selfnum-1;
        // eneNum = enenum-1;

        // players[myNum].OnMouseDown(); 
        // players[eneNum].OnMouseDown(); 
        selfnum.OnMouseDown(); 
        enenum.OnMouseDown(); 

        damage = 0;
        myChar = selfnum;
        eneChar =  enenum;
        myOriginPos = myChar.transform.position;

        my_ability = myChar.cards;
        ene_ability = eneChar.cards;

        myChar.transform.position += Vector3.back;
        eneChar.transform.position += Vector3.back;
        StartCoroutine(BattleMatchcor());
        

    }

    IEnumerator BattleMatchcor(){
        myChar.ChangeCondition(2);      
        myChar.SetPointMove(eneChar.movePoint.position, 17f);
        gameManager.main_camera_ctrl.SetTargetMove(myChar,eneChar,17f);
        
        while(myChar.isMoving){
            yield return null;
        }
        BasicDice();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("BasicAttack");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("MainAttack");

        while(coroutine_lock1){
            yield return null;
        }
        yield return new WaitForSeconds(1f);











        StartCoroutine(MatchFin());

        yield return null;
    }


    void BasicDice(){
        battleDice.gameObject.SetActive(true);
        if(myChar.dice == 1 && eneChar.dice >= 6){
            myChar.AddDice(6);
        }
        else if(eneChar.dice == 1 && myChar.dice >= 6){
            eneChar.AddDice(6);
        }
    }

    IEnumerator BasicAttack(){

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
        yield return null;
        }
    IEnumerator MainAttack(){
        coroutine_lock1= true;
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
                    Debug.Log("Active");
                }
                while(ene_ability[i].card_active){
                    Debug.Log("Waiting");
                    yield return null;
                }
            }
            StartCoroutine(Damage(eneChar,myChar));   
            StopCoroutine(MainAttack());       
            // Damage(eneChar,myChar);
        }

        
    }

    
    
    IEnumerator MatchFin(){    
        for(int i =0;i<battleManager.players.Count;i++){
            if(battleManager.players[i].health <= 5 && battleManager.players[i].card_geted){
                if(i<3){
                    battleManager.card_left_draw += 1;
                    battleManager.players[i].card_geted = false;
                }
                else{
                    battleManager.card_right_draw += 1;
                    battleManager.players[i].card_geted = false;
                }
            }
        }
        for(int i =0;i<battleManager.players.Count;i++){ 
            if(battleManager.players[i].health <= 0 && battleManager.players[i].died_card_geted){
                if(i<3){
                    battleManager.card_left_draw += 1;
                    battleManager.players[i].died_card_geted = false;
                    battleManager.players[i].YouAreDead();
                    yield return new WaitForSeconds(1.5f);
                }
                else{
                    battleManager.card_right_draw += 1;
                    battleManager.players[i].died_card_geted = false;
                    battleManager.players[i].YouAreDead();
                    yield return new WaitForSeconds(1.5f);
                }
            }
        }
        // if(myChar.health <= 5  && myChar.card_geted){
        //     battleManager.card_draw += 1;
        //     myChar.card_geted = false;
        // }
        // if(eneChar.health <= 5  && eneChar.card_geted){
        //     battleManager.card_draw += 1;
        //     eneChar.card_geted = false;
        // }
        foreach(Player player in battleManager.players){
            if(player != myChar || player != eneChar){
                player.dice_Indi.render.color = new Color(255,255,255,255);
            }
        }

        myChar.SetPointMove(myOriginPos, 15f);
        gameManager.main_camera_ctrl.SetZeroMove(17f);
            
        battleManager.blackScreen.SetActive(false);
        myChar.SetDice(0);
        myChar.ChangeCondition(0);
        eneChar.SetDice(0);
        eneChar.ChangeCondition(0);
        battleManager.battleing = false;
        battleManager.target1 = null;
        battleManager.target2 = null;
        myChar.transform.position += Vector3.forward;
        eneChar.transform.position += Vector3.forward;
        myChar.Battle_End();
        eneChar.Battle_End();

        battleDice.gameObject.SetActive(false);

        if(battleManager.left_turn){
            battleManager.TurnTeam("Right");
            if(battleManager.players[3].dice + battleManager.players[4].dice + battleManager.players[5].dice == 0){
                battleManager.TurnTeam("Left");
            }
        }
        else if(battleManager.right_turn){
            battleManager.TurnTeam("Left");
            if(battleManager.players[0].dice + battleManager.players[1].dice + battleManager.players[2].dice == 0){
                battleManager.TurnTeam("Right");
            }
        }
        
        for(int i = 0; i<battleManager.on_battle_card_effect.Count;i++){
            battleManager.on_battle_card_effect[i].gameObject.SetActive(false);
            battleManager.on_battle_card_effect.Remove(battleManager.on_battle_card_effect[i]);
        }

        for(int i =0;i<myChar.cards.Count;i++){
            myChar.cards[i].ability.BattleEnded(myChar.cards[i]);
        }
        for(int i =0;i<eneChar.cards.Count;i++){
            eneChar.cards[i].ability.BattleEnded(eneChar.cards[i]);
        }

        damage = 0;
        battleDice.DamageUpdate();

        yield return null;
    }

    IEnumerator Damage(Player attacker, Player defender){

        for(int i = 0; i<attacker.cards.Count; i++){
                attacker.cards[i].ability.OnDamaging(attacker.cards[i],defender, battleManager,damage);
                if(attacker.cards[i].card_active){
                        attacker.UpdateActiveStat();
                    }
                    while(attacker.cards[i].card_active){
                        yield return null;
                    }
            }
        for(int i = 0; i<defender.cards.Count; i++){
                defender.cards[i].ability.OnDamage(defender.cards[i],attacker,battleManager,damage);
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
        foreach(Player player in battleManager.players){
            for(int i = 0;i<player.cards.Count;i++){
                player.cards[i].ability.WhoEverDamage(player.cards[i],damage);
            }
        }

        coroutine_lock1 = false;
        Debug.Log(coroutine_lock1);
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
