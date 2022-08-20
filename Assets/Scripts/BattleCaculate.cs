using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public GameManager gameManager;
    public BattleManager battleManager;
    public Player[] players;

    public List<CardAbility> my_ability = new List<CardAbility>();
    public List<CardAbility> ene_ability = new List<CardAbility>();



    public int damage;

    Player myChar;
    Player eneChar;

    Vector3 myOriginPos;

    int myNum;
    int eneNum;


    public void BattleMatch(int selfnum, int enenum){

        for(int i=0;i<6;i++){
            if(i != selfnum-1 && i != enenum-1){
                players[i].dice_Indi.render.color = new Color(0,0,0,1);
            }
        }


        myNum = selfnum-1;
        eneNum = enenum-1;
        damage = 0;
        myChar = players[selfnum-1];
        eneChar = players[enenum-1];
        myOriginPos = players[myNum].transform.position;

        my_ability = myChar.cards;
        ene_ability = eneChar.cards;

        myChar.transform.position += Vector3.back;
        eneChar.transform.position += Vector3.back;
        StartCoroutine(BattleMatchcor());
        

    }

    IEnumerator BattleMatchcor(){
        myChar.ChangeCondition(2);      
        myChar.SetPointMove(players[eneNum].movePoint.position, 15f);
        gameManager.main_camera_ctrl.SetTargetMove(myNum,eneNum,17f);
        BasicDice();
        yield return new WaitForSeconds(1f);
        BasicAttack();
        yield return new WaitForSeconds(1f);
        











        myChar.SetPointMove(myOriginPos, 15f);
        gameManager.main_camera_ctrl.SetZeroMove(17f);
        MatchFin();

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

    void BasicAttack(){
        damage = myChar.dice - eneChar.dice;
        if(damage>0){

            for(int i = 0; i<my_ability.Count;i++){
                my_ability[i].OnBattleWin(this);
            }



            myChar.ChangeCondition(3);
            eneChar.ChangeCondition(4);
            Damage(myChar,eneChar);
        }
        if(damage<0){
            damage = -damage;
            for(int i = 0; i<ene_ability.Count;i++){
                ene_ability[i].OnBattleWin(this);
            }
            myChar.ChangeCondition(4);
            eneChar.ChangeCondition(3);            
            Damage(eneChar,myChar);
        }
        if(damage == 0){
            myChar.ChangeCondition(3);
            eneChar.ChangeCondition(3);
        }
    }
    
    void MatchFin(){    
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
                }
                else{
                    battleManager.card_right_draw += 1;
                    battleManager.players[i].died_card_geted = false;
                    battleManager.players[i].YouAreDead();
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
        for(int i=0;i<battleManager.players.Count;i++){
            if(i != myNum || i != eneNum){
                players[i].dice_Indi.render.color = new Color(255,255,255,255);
            }
        }
            
        battleManager.blackScreen.SetActive(false);
        myChar.SetDice(0);
        myChar.ChangeCondition(0);
        eneChar.SetDice(0);
        eneChar.ChangeCondition(0);
        battleManager.battleing = false;
        battleManager.target1 = 0;
        battleManager.target2 = 0;
        myChar.transform.position += Vector3.forward;
        eneChar.transform.position += Vector3.forward;

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
    }

    void Damage(Player attack, Player defender){
        for(int i = 0; i<attack.cards.Count; i++){
                attack.cards[i].OnDamageing(this,attack);
            }
        for(int i = 0; i<defender.cards.Count; i++){
                defender.cards[i].OnDamaged(this,defender);
            }
        defender.Damage(damage,attack);
    }
}
