using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class Test6 : CardAbility
{
    public override void OnDamaged(CardPack card, BattleCaculate battle, Player defender)
    {
        if(battle.damage < 3){
            card.card_active = true;
            battle.damage = 0;
        }
    }

    public override void ImmediEffect(CardPack card, Transform transform)
    {
        card.effect[0].SetActive(true);
        card.effect[0].transform.position = transform.position + Vector3.up;
        // Instantiate(effect,transform.position+Vector3.up*2,transform.rotation);
        //DeActive();
    }

}

