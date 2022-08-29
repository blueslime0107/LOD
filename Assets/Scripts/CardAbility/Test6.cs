using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Test6 : CardAbility
{
    public override void OnDamaged(BattleCaculate battle, Player defender)
    {
        if(battle.damage < 3){
            //Actived();
            //this.card_active = true;
            battle.damage = 0;
        }
    }

    public override void ImmediEffect(Transform transform)
    {
        Instantiate(effect,transform.position+Vector3.up*2,transform.rotation);
        //DeActive();
    }

}

