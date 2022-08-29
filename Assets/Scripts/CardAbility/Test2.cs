using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test2 : CardAbility //엑스칼리버
{
    public override void OnBattleWin(BattleCaculate battle)
    {
        Actived();
        battle.damage += 2;
    }

    public override void AttackEffect(Transform transform)
    {
        inst_effect.transform.position = transform.position;
        inst_effect.transform.rotation = transform.rotation;
        //Instantiate(effect,transform.position,transform.rotation);
        DeActive();
    }

}

