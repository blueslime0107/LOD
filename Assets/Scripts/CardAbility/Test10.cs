using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test10 : CardAbility
{
    public override void OnBattleWin(BattleCaculate battle)
    {
        battle.damage += 2;
    }

}

