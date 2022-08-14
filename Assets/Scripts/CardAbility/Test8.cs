using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Test8 : CardAbility
{
    public override void OnBattleWin(BattleCaculate battle)
    {
        battle.damage += 2;
    }

}

