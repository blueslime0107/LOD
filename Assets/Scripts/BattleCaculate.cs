using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public GameManager gameManager;
    public BattleManager battleManager;
    public Player[] players;
    int damage;

    bool charMove = false;
    bool cameraMove = false;

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

        myChar.transform.position += Vector3.back;
        eneChar.transform.position += Vector3.back;
        StartCoroutine(BattleMatchcor());
        

    }

    IEnumerator BattleMatchcor(){
        myChar.ChangeCondition(2);      
        BasicDice();
        yield return new WaitForSeconds(1f);
        BasicAttack();
        yield return new WaitForSeconds(1f);
        












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
            myChar.ChangeCondition(3);
            eneChar.ChangeCondition(4);
            eneChar.Damage(damage);
        }
        if(damage<0){
            myChar.ChangeCondition(4);
            eneChar.ChangeCondition(3);
            damage = -damage;
            myChar.Damage(damage);
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
            Debug.Log("add");
            battleManager.card_draw += 1;
            myChar.card_geted = false;
        }
        if(eneChar.health <= 5  && eneChar.card_geted){
            Debug.Log("add");
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

}
