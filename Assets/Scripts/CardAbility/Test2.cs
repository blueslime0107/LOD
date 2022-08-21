using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test2 : CardAbility //엑스칼리버
{
    public override void OnBattleWin(BattleCaculate battle)
    {
        battle.card_activated = true;
        battle.damage += 2;
    }

}

