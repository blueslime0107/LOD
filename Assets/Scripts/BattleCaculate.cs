using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public BattleManager battleManager;
    public Player[] players;
    int damage;

    public void BattleMatch(int selfnum, int enenum){
        damage = 0;
        Player myChar = players[selfnum];
        Player eneChar = players[enenum];

        BasicDice(myChar,eneChar);
        


        












        MatchFin(myChar,eneChar);

    }

    void BasicDice(Player myChar, Player eneChar){
        
        if(myChar.dice == 1 && eneChar.dice >= 6){
            myChar.AddDice(6);
        }
        else if(eneChar.dice == 1 && myChar.dice >= 6){
            eneChar.AddDice(6);
        }
        damage = myChar.dice - eneChar.dice;
        if(damage>0){
            eneChar.Damage(damage);
        }
        if(damage<0){
            damage = -damage;
            myChar.Damage(damage);
        }
    }
    
    void MatchFin(Player myChar, Player eneChar){
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
            
        myChar.SetDice(0);
        eneChar.SetDice(0);
    }

}
