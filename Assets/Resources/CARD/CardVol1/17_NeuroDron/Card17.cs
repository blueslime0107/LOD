using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/6정신지배드론", order = 17)]
public class Card17 : CardAbility
{

    public override void OnDamage(CardPack card, Player attackerbm, Damage damage, BattleManager match)
    {
        
        if(damage.value <= 2){
            Active(card);
            damage.value = 0;
        }
    }
}
