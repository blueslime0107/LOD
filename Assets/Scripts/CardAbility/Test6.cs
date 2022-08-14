using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Test6 : CardAbility
{
    public override void OnDamaged(BattleCaculate battle, Player defender)
    {
        if(battle.damage < 3){
            battle.damage = 0;
        }
    }

}

