using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Test7 : CardAbility
{
    public override void OnDamageing(BattleCaculate battle, Player attacker)
    {
        if(battle.damage < 3){
            this.card_active = true;
            battle.damage *= 2;
        }
    }


}

