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
        if(myChar.health <= 0){
            myChar.YouAreDead();
            battleManager.card_draw += 1;     // 오류의 흔적 001 : 카드가 안들어가는게 아닌 if문 1줄 생략의 오해로 인한
            
        }                                          //카드 추가 카운트 버그            
        if(eneChar.health <= 0){
            eneChar.YouAreDead();
            battleManager.card_draw += 1;
        }
            


        if(myChar.health <= 5  && myChar.card_geted){
            battleManager.card_draw += 1;
            myChar.card_geted = false;
        }
        if(eneChar.health <= 5  && eneChar.card_geted){
            battleManager.card_draw += 1;
            eneChar.card_geted = false;
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
    }

    void Damage(Player attack, Player defender){
        for(int i = 0; i<attack.cards.Count; i++){
                attack.cards[i].OnDamageing(this,attack);
            }
        for(int i = 0; i<defender.cards.Count; i++){
                defender.cards[i].OnDamaged(this,defender);
            }
        defender.Damage(damage);
    }
}
