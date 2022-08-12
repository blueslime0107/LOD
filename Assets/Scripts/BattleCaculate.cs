using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public Player[] players;
    int damage;

    public void BattleMatch(int selfnum, int enenum){
        damage = 0;
        Player myChar = players[selfnum];
        Player eneChar = players[enenum];

        BasicDice(myChar,eneChar);
        


        myChar.SetDice(0);
        eneChar.SetDice(0);














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
}
